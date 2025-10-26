
# Demo 1 — Solar System Simulation

## Objective
Demonstrate 3D transformations, object hierarchies, and lighting through a simulation of the Sun, Earth, and Moon system.

## Description
This demo visualizes orbital relationships in a miniature solar system. The Earth orbits the Sun, the Moon orbits the Earth, and the Earth rotates on a tilted axis. The Sun acts as the primary light source, dynamically illuminating the system to show realistic day/night cycles.

## Video
[![Watch Demo 1 – Transformation](https://img.youtube.com/vi/ST085IwEZbM/0.jpg)](https://youtu.be/ST085IwEZbM)

## How to Run
- Unity version: 6000.0.57f1
- Open scene: `Assets/Scenes/Demo1Scene.unity`
- Press Play to begin.
- Use the mouse to orbit the camera view with `MouseOrbitCamera`.

## Key Scripts
| Script | Description |
|--------|-------------|
| `MouseOrbitCamera.cs` | Allows smooth camera rotation around a target using mouse input. |
| `OrbitMotion.cs` | Controls orbital rotation of a planet around its parent body. |
| `SelfRotation.cs` | Spins the Earth on its local axis to represent day/night rotation. |

## Implementation Details
- The Sun, Earth, and Moon use hierarchical transforms to simulate real orbital motion.  
- The Earth is tilted at approximately 23.5°, demonstrating seasonal change.  
- The Sun provides the only light source to create directional shading and moon phases.  
- Materials give each body distinct visual properties for clarity.

## Reflection
This demo represents the foundation of spatial reasoning and motion in Unity. I learned how parent-child relationships affect transforms, how lighting direction impacts realism, and how to use rotation scripts to simulate continuous, smooth orbital motion.

## Credits
| Asset / Model | Creator | Source URL | License | Notes |
|----------------|----------|-------------|----------|--------|
| Blue Marble Earth Image | NASA Visible Earth | https://visibleearth.nasa.gov/images/73726/june-blue-marble-next-generation-w-topography-and-bathymetry/73746l | Public Domain | Used for Earth texture. |
| Lunar imagery | NASA Scientific Visualization Studio | https://svs.gsfc.nasa.gov/4720/ | Public Domain | Used for Moon Texture. |
| Solar imagery | NASA SVS | https://svs.gsfc.nasa.gov/30362/ | Public Domain | Used for Sun Texture. |

**Attribution Summary:**  
All assets are publicly available NASA datasets licensed under Public Domain (US Government Works).
