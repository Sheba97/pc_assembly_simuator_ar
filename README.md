# AR-Based PC Assembly Learning Application (Unity + AR Foundation)

This project is an **Augmented Reality (AR) educational application** developed using **Unity 6** and **AR Foundation**.  
It visualizes and guides users through a **step-by-step PC assembly process** using **image tracking** and **interactive world-space UI**.

The application is intended for **learning, demonstration, and training purposes**, especially for understanding PC hardware assembly in an immersive AR environment.

---

## Built With

- **Unity Editor:** 6.0.0 (6000.2.7f1)
- **AR Foundation**
- **ARCore (Android)**
- **XR Interaction Toolkit**
- **C#**
- **TextMeshPro**
- **World Space Canvas UI**

---

## Platform Support

- Android (ARCore-supported devices)
- Tested on physical Android devices
- Not intended for iOS at this stage

---

## Core Features

- **Image Tracking**
  - Detects predefined reference images using `ARTrackedImageManager`
  - Spawns a 3D PC assembly prefab aligned with the detected image

- **Step-by-Step PC Assembly Flow**
  - Install RAM
  - Install CPU Cooler
  - Place motherboard into case
  - Install GPU
  - Close the PC case

- **Interactive World-Space UI**
  - Context-sensitive buttons appear near the AR object
  - Buttons guide the user through each assembly step
  - UI follows the AR object and faces the camera

- **Prefab-Based Architecture**
  - Entire PC assembly logic is encapsulated in a prefab
  - Prefab is instantiated dynamically at runtime when an image is detected

---

## Project Structure Overview

Assets/
│
├── Scripts/
│ ├── PCAssemblyController.cs # Core assembly state machine
│ ├── AssemblyButtonRelay.cs # Relays UI button clicks to controller
│ ├── DragAndAutoHideController.cs # Image tracking & prefab spawning
│ ├── CanvasCameraBinder.cs # Binds World Space Canvas to AR Camera
│ └── BillboardToCamera.cs # Keeps UI facing the camera
│
├── Prefabs/
│ └── PC_Assembly_Root.prefab # Main AR assembly prefab
│
├── ImageTracking/
│ └── ReferenceImageLibrary # Images used for AR tracking
│
├── Models/
├── Materials/
└── Scenes/


---

##  Key Scripts Explained

### `PCAssemblyController`
Controls:
- Visibility of PC parts (motherboard, RAM, GPU, casing, etc.)
- Button enable/disable logic
- Step sequencing using coroutines and delays

Acts as the **single source of truth** for the assembly state.

---

### `AssemblyButtonRelay`
Used to safely connect **UI buttons inside a prefab** to the runtime-instantiated controller.

Why it exists:
- Prefabs cannot reliably reference scene objects
- Buttons call relay methods
- Relay forwards calls to the correct `PCAssemblyController` instance

---

### `DragAndAutoHideController`
- Listens to image tracking events
- Instantiates the PC assembly prefab **once per tracked image**
- Positions and updates the prefab relative to the detected image
- Injects the correct controller reference into the UI relay

---

### `CanvasCameraBinder`
- Automatically assigns `Camera.main` to the **World Space Canvas**
- Required so UI buttons can receive touch input in AR

---

## Known Limitations / Work in Progress

- World-space UI interaction may be sensitive to:
  - Z-depth positioning
  - Canvas scale
  - Raycast blocking by 3D meshes
- Button visuals respond to touch, but interaction reliability is still being refined
- UI and AR object alignment may require calibration per device

These issues are actively being debugged and improved.

---

##  How to Run the Project

1. Open the project using **Unity 6.0.0 (6000.2.7f1)**
2. Ensure **ARCore** is enabled in:
     Project Setings -> XR Plug-in Management

3. Add an **AR Reference Image Library**
4. Assign the reference images in `DragAndAutoHideController`
5. Build and deploy to an **ARCore-supported Android device**

---

## Educational Use Case

This project was developed as part of an **AR learning experiment** to:
- Visualize complex hardware assembly
- Improve spatial understanding using AR
- Explore interactive AR UI challenges in Unity

---

## Author

Developed by **Sheba** alias **Shehan Ranawaka** 
MSc Electrical & Computer Engineering  
Focus Areas:  
- Augmented Reality  
- Embedded & Computer Systems  
- Interactive Learning Applications  

---

## License

This project is provided for **educational and research purposes**.  
Commercial use requires prior permission.




