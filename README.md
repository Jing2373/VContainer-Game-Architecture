<div align="center">

# 🎮 VContainer Game Architecture

![Unity](https://img.shields.io/badge/Unity-6000.0-black.svg?style=flat-square&logo=unity)
![C#](https://img.shields.io/badge/C%23-9.0-green.svg?style=flat-square)
![Architecture](https://img.shields.io/badge/Architecture-MVVM-blue.svg?style=flat-square)

A core game architecture framework for Unity. It uses a clean 3-scene flow, MVVM architecture, and VContainer to build a strong foundation for games.

</div>

## 📝 About

> **⚠️ Please note:** This repository only has pure code and frameworks. It does not include actual game art or 3D models.

This repository shows my core game architecture. I use the **MVVM architecture** and **VContainer** for Dependency Injection (DI) to keep the code clean and easy to test. 

To make the game run smoothly, I used a **3-Scene Flow**:

* **InitialScene:** The first scene. It handles the download progress. It holds the *only* `DontDestroyOnLoad` GameObject in the entire game. This object keeps the Global VContainer settings, the main Camera, and the Canvas. Sharing one Camera and Canvas makes the UI easy to manage.
* **StartScene:** The main menu. It handles music and audio settings. By the way, I use the **Object Pool** pattern for the audio player to save memory and keep the game running fast.
* **GameScene:** The main game world. It includes tools for **Addressables** to download objects and manage game data.


## 🛠️ Built With

* **Engine:** Unity 6000.0.74f1
* **Framework:** VContainer, UniTask
* **Architecture:** MVVM
