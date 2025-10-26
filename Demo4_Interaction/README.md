# Demo 4 — Interactive Puzzle Game

## Objective
Design an immersive VR puzzle that involves direct manipulation and object placement, using state tracking and feedback to signal progress and completion.

## Description
This demo presents a potion-mixing puzzle in which the player must grab and place potions into a cauldron in the correct order. An incorrect order resets the puzzle, while a correct sequence triggers a transformation sequence where the frog turns into a prince, complete with particle and sound effects. The scene demonstrates interaction design, event control, and immersive feedback in VR.

## Video
[![Watch Demo 4 – Interaction](https://img.youtube.com/vi/_D0tF47Tlmg/0.jpg)](https://youtube.com/shorts/_D0tF47Tlmg?feature=share)

## How to Run
- Unity version: 6000.0.57f1  
- XR runtime: OpenXR  
- Open scene: `Assets/Scenes/Demo4Scene.unity`  
- Tag potions sequentially as `Potion_1`, `Potion_2`, etc.  
- Assign the `CauldronTrigger` collider to `PuzzleManager.cauldronCollider`.  
- Press Play to begin and follow the riddle displayed on the riddle panel.

## Key Scripts
| Script | Description |
|--------|-------------|
| `PuzzleManager.cs` | Controls the potion sequence, handles resets, and runs the finale. |
| `CauldronTrigger.cs` | Detects potion triggers and communicates with the puzzle logic. |
| `SimpleGrab.cs` | Handles grabbing and releasing potions using the controller grip button. |
| `XRSmoothLocomotion.cs` | Enables smooth locomotion and rotation. |
| `InputBootstrap.cs` | Initializes XR input bindings. |

## Implementation Details
- The puzzle checks each potion placement against the defined correct order.  
- Incorrect entries trigger a soft reset that restores potion positions.  
- Correct completion plays smoke effects, disables the frog, and activates the prince.  
- Audio clips provide clear feedback for success, failure, and transformation.  
- The UI riddle panel is shown at scene start to guide the player.

## Reflection
Building this demo required careful event sequencing and object state tracking. I learned to manage interactive elements that must remain consistent after resets and to connect visual, audio, and gameplay feedback so the player clearly understands progress and success in a VR puzzle environment.

## Credits
| Asset / Model | Creator | Source URL | License | Notes |
|----------------|----------|-------------|----------|--------|
| Hand Painted Stone Wall Texture | The Nerd Sherpa | https://opengameart.org/content/hand-painted-stone-wall | CC BY 4.0 | Used as wall background. |
| Iron Metal Texture | Sketchup Texture Club | https://www.sketchuptextureclub.com/textures/materials/metals/basic-metals/iron-metal-texture-seamless-09751 | Free User License | Used for cauldron metal material. |
| Normal Map (Iron) | Filter Forge | https://www.filterforge.com/filters/13092-normal.html | Generated Asset | Used for cauldron surface detail. |
| Bookshelf, Table, Chair, Books | tsishir | https://skfb.ly/6S6M7 | CC BY 4.0 | Used as background furniture props. |
| Cauldron | Kaine | https://skfb.ly/opIpQ | CC BY 4.0 | Used as central interactive object. |
| Prince Naveen | 𝕋𝕙𝕖 𝔼𝕧𝕖𝕟𝕚𝕟𝕘 𝕊𝕥𝕒𝕣 | https://skfb.ly/puPEV | CC BY 4.0 | Used for transformation sequence. |
| Low Poly Potions | FreddyAbson | https://skfb.ly/6TnJ9 | CC BY 4.0 | Used as interactable potion objects. |
| Frog Model Source | TT3D Generator | https://tt3d.vn.ugavel.com/ | Personal Use License | Used for frog sculpting reference. |

**Attribution Summary:**  
All models and textures are used under Creative Commons Attribution or equivalent free-use licenses, with full credit provided above.
