
# Demo 3 — Basic VR Exergame

## Objective
Create a simple VR exercise game that translates physical hand movement into interactive gameplay using tracked VR controllers.

## Description
This demo is a short, interactive VR experience where players use their hands to hit incoming targets. The targets move toward the player and disappear when struck, providing immediate score feedback through UI and sound. The goal is to encourage active motion and hand-eye coordination in a VR setting.

## Video
[![Watch Demo 3 – Immersion](https://img.youtube.com/vi/ftHgEV30io0/0.jpg)](https://youtube.com/shorts/ftHgEV30io0?feature=share)

## How to Run
- Unity version: 6000.0.57f1  
- XR runtime: OpenXR  
- Open scene: `Assets/Scenes/Demo3Scene.unity`  
- Connect a compatible VR headset and controllers.  
- Press Play to begin and move your hands to hit the targets.

## Key Scripts
| Script | Description |
|--------|-------------|
| `GameManager.cs` | Tracks hits and updates the score display. |
| `ScoreManager.cs` | Provides static score handling for other scripts. |
| `TargetSpawner.cs` | Spawns new targets periodically in front of the player. |
| `TargetMover.cs` | Moves spawned targets toward the player. |
| `HitOnHand.cs` | Detects collisions between hands and targets and plays hit sounds. |
| `HandHitEmitter.cs` | Sends haptic feedback on impact if supported. |
| `TrackedHand.cs` | Updates the hand’s position and rotation using XR input. |
| `XRSmoothLocomotion.cs` | Enables optional smooth player movement and rotation. |
| `InputBootstrap.cs` | Initializes and enables XR input actions. |

## Implementation Details
- Targets move forward automatically toward the player’s position.  
- Colliders on the hands detect hits, which increment the score and play audio feedback.  
- All gameplay occurs within a stationary VR environment for comfortable play.  
- The design focuses on motion accuracy and responsiveness.  

## Reflection
Developing this demo improved my understanding of integrating physical player input with virtual events. I learned how to use XR device tracking, coordinate object motion toward the player, and provide satisfying visual and tactile feedback to make interactions feel intuitive.

## Credits
| Asset / Model | Creator | Source URL | License | Notes |
|----------------|----------|-------------|----------|--------|
| Checkerboard Tile Pattern | Wikimedia Commons (User:Stannered) | https://commons.wikimedia.org/wiki/File:Checkerboard_tile.svg | Public Domain | Used as floor texture. |

**Attribution Summary:**  
Contains only one public-domain texture used for visual grounding.
