<img width="800" height="446" alt="20261008_120004-ezgif com-gif-maker" src="https://github.com/user-attachments/assets/0f01f5ac-da72-4da0-ac4a-df7eeea19777" />
# 🎮 Unity UI Management System

A Unity project demonstrating **dynamic UI creation**, **Addressables integration**, and a **custom UI binding system** through a simple login interface.

## 📝 About

> **⚠️ Please note:** This repository focuses on UI management and architecture. The login page serves as a demonstration of the underlying systems rather than a complete authentication solution.

This project demonstrates how to build a flexible and maintainable UI system in Unity.

Instead of placing UI elements directly in the scene, I use a custom **UIManager** with **Unity Addressables** to dynamically load and instantiate UI prefabs at runtime.

I also developed a custom **UI Collector** that automatically collects references to child UI elements with the `UIBind` component. This approach reduces the need for traditional methods such as `Transform.Find()` or manually assigning references through the Unity Inspector.

The project uses a **Single-Scene Structure**:

- **LoginScene:** The only scene in the project. It provides the environment for dynamically loading and displaying the login interface.

## ✨ Key Features

### 1. Dynamic UI Management

- Uses a custom `UIManager` to manage UI creation.
- Integrates **Unity Addressables** to load and instantiate UI prefabs at runtime.
- Keeps UI prefabs separate from the scene instead of placing them directly in the Hierarchy.
- Provides a foundation for managing UI elements in larger projects.

### 2. Custom UI Binding System

- Implements a custom **UI Collector** to gather references to child UI elements.
- Uses the `UIBind` component to identify UI elements that need to be collected.
- Reduces reliance on `Transform.Find()` and manual Inspector assignments.
- Improves code maintainability by simplifying how UI components are referenced.

### 3. Single-Scene Architecture

- Uses a single scene to demonstrate the UI management system.
- Dynamically creates the login interface through `UIManager`.
- Keeps the scene structure simple while demonstrating reusable UI management techniques.

## 🛠️ Built With

- **Engine:** Unity 6000.0.74f1
- **Language:** C#
- **Asset Management:** Unity Addressables
- **UI Management:** Custom UIManager
- **UI Binding:** Custom UI Collector & UIBind
- **Structure:** Single Scene

## 🎨 Assets
- AI-generated visual assets and free third-party icons.
