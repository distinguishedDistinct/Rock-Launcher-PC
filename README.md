Rock-Thrower 2D (PC / Desktop Version)

Project Overview

This is a 2D physics-based projectile game built in Unity, currently configured for desktop testing. The game utilizes direct PC input (Mouse and Scroll Wheel) to aim and launch a projectile with variable force, utilizing trajectory prediction to hit a target.

Key Features

PC-Optimized Controls: Uses mouse input for aiming and the scroll wheel (requires minor script update) for adjusting launch force.

Trajectory Prediction: A visual LineRenderer accurately shows the projectile's path before launch.

Robust Input Handling: The underlying code includes essential fixes to prevent conflicts, ensuring UI elements like the force slider can be used without interfering with the aiming input.

Controls and How to Play (PC)

Action

Control

Description

Aim Launcher

Move Mouse

Rotates the launcher to point toward the mouse cursor's position on the screen.

Adjust Force

Scroll Wheel (Up/Down)

(NOTE: Requires script update) This is the intended control method to increase or decrease launch force.

Fine Tune Force

On-Screen Slider

Use the mouse to drag the UI slider for precise force setting.

Fire Projectile

Left Mouse Button (Click Down)

Fires the projectile with the current launch force.

Technical Details

Core Script: ProjectileLauncher.cs

This script, located in Assets/Scripts/Projectiles/, manages the core game loop:

Aiming Logic: Uses Camera.main.ScreenToWorldPoint(Input.mousePosition) to calculate the angle based on mouse location.

Physics Calculation: The trajectory line is based on the kinematic equation: 

$$P(t) = P_0 + V_0 t + \frac{1}{2} g t^2$$

.

Input Protection: Crucially, it uses EventSystem.current.IsPointerOverGameObject() to ignore mouse clicks/touches over UI elements.

File Locations

Asset Type

Primary File(s)

Typical Location

C# Script

ProjectileLauncher.cs

Assets/Scripts/Projectiles/

Scene

MainScene.unity

Assets/Scenes/

Getting Started

Clone the Repository:

git clone [https://github.com/distinguishedDistinct/Rock-Launcher-PC.git](https://github.com/distinguishedDistinct/Rock-Launcher-PC.git)


Open in Unity: Open the root project folder in your Unity Editor.

Test: Press Play in the Editor, or use File > Build Settings... to generate a desktop executable.
