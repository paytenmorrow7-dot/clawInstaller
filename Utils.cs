using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OpenClawInstaller
{
    public static class Utils
    {
        private static readonly HttpClient client = InitHttpClient();

        private static HttpClient InitHttpClient()
        {
            var c = new HttpClient { Timeout = TimeSpan.FromMinutes(10) };
            c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 OpenClawInstaller/1.0");
            c.DefaultRequestHeaders.Add("Accept", "*/*");
            return c;
        }

        public static async Task DownloadFileAsync(string url, string destPath, Action<int> progressCallback)
        {
            try
            {
                using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                long totalBytes = response.Content.Headers.ContentLength ?? 0L;
                using var contentStream = await response.Content.ReadAsStreamAsync();
                
                using var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

                var buffer = new byte[8192];
                long totalRead = 0;
                int bytesRead;
                int lastPercent = -1;

                while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await fileStream.WriteAsync(buffer, 0, bytesRead);
                    totalRead += bytesRead;

                    if (totalBytes > 0)
                    {
                        int percent = (int)((double)totalRead / totalBytes * 100);
                        if (percent != lastPercent)
                        {
                            progressCallback(percent);
                            lastPercent = percent;
                        }
                    }
                }

                if (totalBytes > 0 && totalRead != totalBytes)
                {
                    throw new IOException($"文件下载不完整！期望: {totalBytes} 字节，实际仅下载: {totalRead} 字节。");
                }

                if (totalRead < 100 * 1024) 
                {
                    throw new IOException($"下载的文件体积异常 (仅 {totalRead} 字节)！你可能被网络防火墙拦截，下载到了一个错误网页。");
                }
            }
            catch (Exception)
            {
                if (File.Exists(destPath))
                {
                    File.Delete(destPath);
                }
                throw;
            }
        }

        public static void ExtractAndRenameZip(string zipPath, string extractDir, string expectedPrefix, string targetName)
        {
            ZipFile.ExtractToDirectory(zipPath, extractDir, overwriteFiles: true);

            string sourceFolder = null;
            foreach (var dir in Directory.GetDirectories(extractDir))
            {
                if (new DirectoryInfo(dir).Name.StartsWith(expectedPrefix))
                {
                    sourceFolder = dir;
                    break;
                }
            }

            if (sourceFolder == null) throw new DirectoryNotFoundException($"未找到前缀为 {expectedPrefix} 的文件夹。");

            string targetPath = Path.Combine(extractDir, targetName);
            RobustDeleteDirectory(targetPath);
            Thread.Sleep(500); 

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Directory.Move(sourceFolder, targetPath);
                    return;
                }
                catch (IOException)
                {
                    if (i == 4) throw;
                    Thread.Sleep(1000);
                }
            }
        }

        public static void ExtractZipDirect(string zipPath, string extractDir)
        {
            RobustDeleteDirectory(extractDir);
            Thread.Sleep(500);
            ZipFile.ExtractToDirectory(zipPath, extractDir, overwriteFiles: true);
        }

        /// <summary>
        /// 健壮的目录删除方法，能处理只读属性和短暂的文件占用锁定
        /// </summary>
        public static void RobustDeleteDirectory(string targetDir)
        {
            if (!Directory.Exists(targetDir)) return;

            // 1. 递归取消所有文件和子文件夹的只读属性
            try
            {
                string[] files = Directory.GetFiles(targetDir, "*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                }

                string[] dirs = Directory.GetDirectories(targetDir, "*", SearchOption.AllDirectories);
                foreach (string dir in dirs)
                {
                    File.SetAttributes(dir, FileAttributes.Normal);
                }
            }
            catch 
            {
                // 忽略获取文件列表时的权限异常，交给下方的递归删除去硬刚
            }

            // 2. 加入重试机制的递归删除 (对抗 Windows Defender 扫描带来的短暂锁定)
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    // true 表示递归删除内部所有文件和文件夹
                    Directory.Delete(targetDir, true);
                    return; 
                }
                catch (DirectoryNotFoundException)
                {
                    // 目录已经不存在，说明删成功了
                    return; 
                }
                catch (Exception)
                {
                    // 如果已经是最后一次重试，直接抛出异常
                    if (i == 4) throw;
                    
                    // 线程休眠 500 毫秒后重试
                    Thread.Sleep(500); 
                }
            }
        }
    }
}