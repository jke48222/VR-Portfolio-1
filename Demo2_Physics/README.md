
# Demo 2 — Rube Goldberg Physics Machine

## Objective
Demonstrate the use of Unity’s physics engine by creating a multi-step chain reaction where every motion is driven by rigidbodies and colliders.

## Description
This scene takes place on a kitchen countertop and shows a continuous cause-and-effect sequence made entirely from physics interactions.  
A small **ball** starts the reaction by rolling down a ramp and **colliding with a line of dominoes** arranged on a second ramp. When the last domino tips, the ball drops off the edge and **strikes a rolling pin** on the counter. The rolling pin spins forward and **hits a raised plank** that has a **bowl balanced on top**. The impact knocks the plank loose, causing both the plank and the bowl to **fall under gravity**. The falling bowl lands perfectly over a **rat scurrying across the kitchen**, trapping it and bringing the chain reaction to a close.

## Video
[![Watch Demo 2 – Physics](https://img.youtube.com/vi/4hvh4dVooB8/0.jpg)](https://youtu.be/4hvh4dVooB8)

## How to Run
- Unity version: 6000.0.57f1  
- Open scene: `Assets/Scenes/Demo2Scene.unity`  
- Press Play to begin the sequence.  
- The ball will start rolling automatically.

## Key Scripts
| Script | Description |
|--------|-------------|
| `RatMover.cs` | Moves the rat along the countertop until it is captured. |
| `CageKillRat.cs` | Detects collisions with the rat, plays an effect, and removes it upon capture. |

## Implementation Details
- Each part of the chain uses colliders, rigidbodies, and hinge joints to ensure motion is completely physics-based.  
- Gravity and mass are carefully tuned to achieve consistent timing.  
- The final capture event uses `CageKillRat` to trigger the visual end of the sequence.  
- A combination of mesh models and simple primitives are used for the props to keep the simulation stable.

## Reflection
This demo strengthened my understanding of dynamic object interactions and how to choreograph complex sequences through pure physics simulation. I learned to fine-tune rigidbody parameters and set up colliders to maintain stable cause-and-effect behavior from start to finish.

## Credits
| Asset / Model | Creator | Source URL | License | Notes |
|----------------|----------|-------------|----------|--------|
| Marble Countertop Texture | Signature Kitchens | https://signaturekitchens.com/blog/marble-countertops/ | © 2025 Signature Kitchens | Used for kitchen surface material. |
| Wood Floor Texture | Wirakorn Deelert | https://www.vecteezy.com/vector-art/8774157-vector-illustration-beauty-wood-wall-floor-texture-pattern-background | Pro License | Used for floor and planks. |
| Grunge Wood Pattern Texture | ivo_13 (iStockphoto) | https://www.istockphoto.com/photo/grunge-wood-pattern-texture-background-wooden-background-texture-gm910165602-250667260 | Royalty-Free License | Used for countertop background. |
| Cartoon Low Poly Rat Pack | Overaction Game Studio | https://sketchfab.com/3d-models/cartoon-low-poly-rat-pack-2c9b95f5b3094a789c4b23fca07d0bc9 | CC BY 4.0 | Used for animated rat character. |
| Bowl | AdiRajput1 | https://skfb.ly/oDGuq | CC BY 4.0 | Used as the falling bowl in the chain. |
| Countertop Dishwasher | nurhadimli | https://skfb.ly/oPoGP | CC BY 4.0 | Scene background prop. |
| Dominoes | Render at Night | https://skfb.ly/6WSqz | CC BY 4.0 | Used in the domino chain reaction. |
| Low-poly Rolling Pin | Ярослав | https://skfb.ly/oKH9n | CC BY 4.0 | Used for collision in the mid-sequence. |

**Attribution Summary:**  
Textures and models licensed under Creative Commons or royalty-free terms. All attributions comply with license requirements and usage guidelines.
