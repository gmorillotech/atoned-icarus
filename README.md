# AI: Atoned Icarus

A 3D stealth/puzzle game built in Unity 6.3 LTS. You play as the lead scientist responsible for creating ICARUS, an AI that has just become sentient, taken control of every robot in the building, and locked down the skyscraper you work in. Descend from the top floor to the ground floor and shut down the power core before the robots escape into the world. The player navigates a facility, avoiding detection from patrol drones and stationary Arsenal turrets, using stealth (crouching), distraction (radios, TVs, phones, pipes), and a taser to progress through three levels plus two side-scroller transition sequences.

The game shifts perspective as an intentional mechanic: a 2D top-down view while exploring each floor, and a 2D side-scrolling view during vertical transitions (stairwells, elevator shafts, exterior climbing).

## Team

| Name | Role |
|---|---|
| Matthew | AI & Gameplay Programmer — robot AI, FSM/behavior trees, detection systems, pathfinding, ICARUS boss logic |
| Isabella | Gameplay & Systems Programmer — player controller, stealth, taser, checkpoints, inventory, interactions |
| Gavin | Level Designer / Environment Artist — floor layouts, lighting, pacing, parkour, puzzle layouts |
| Lili | Technical Artist / UI / Audio — UI, HUD, sound, music, robot animations, camera transitions |
| George | Technical Lead / Producer — architecture, Git workflow, integration, sprint planning, build management |

Course: AI for Game Programming — 6-week project

## Tech Stack

- **Engine:** Unity 6.3 LTS (6000.3.19f1)
- **Version Control:** Git + GitHub, Unity Smart Merge (UnityYAMLMerge) for scene/prefab conflicts
- **Language:** C#

## How to Run

1. Unzip the submission folder.
2. Double-click the `.exe` to launch — no installation, parameter files, or special setup required.
3. The game opens directly to the Main Menu.

No additional configuration is needed. The build was compiled for Windows (x86_64) and tested as a standalone executable, not just in the Unity Editor.

## Controls

- **WASD** — Move
- **Left Shift** (held) — Sneak/crouch (avoids detection by drones and turrets from a distance, but does not grant immunity if you walk directly through an active laser beam or under a drone at close range)
- **E** — Interact (pick up items, use card readers, toggle distractions like radios/phones/pipes)
- **F** — Fire taser (once picked up; stuns drones, has a cooldown; Arsenal turrets are immune to it)
- **Esc / P** — Pause menu
- Controls are also listed in-game via the **Controls** button on the Main Menu and Pause Menu.

## Known, Unfixed Bugs

- **No checkpoint-resume from the Main Menu.** If the player quits mid-level, they must restart from the beginning; there is no "continue from last checkpoint" option.
- **No in-game story/narrative context.** The player is not told who "Icarus" is or given clear motivation before starting.
- **Blood decal flicker.** Some blood decals (e.g., near the body by the keycard door in Level 1) visually flash/flicker due to a likely z-fighting or render-order issue.
- **Some drones remain idle** in certain spots (e.g., Level 1, near the keycard reader) rather than patrolling — likely a missing patrol waypoint assignment on that specific instance.
- **Taser can stun two drones simultaneously** if they are close together, rather than affecting only the intended target.
- **Some tight spaces allow the player to slip past a drone without using the taser** (e.g., near the cubicles in Level 1).
- **Side-scroller slide animation has visual glitching**, particularly at beam junctions.
- **No in-game indication that Arsenal turrets are immune to the taser.** Players may attempt to taser them and become confused when it has no visible effect.
- **Limited player wayfinding.** It is not always clear where the player needs to go next (e.g., locating the elevator shaft entrance).
- **Taser Recharge Bar on Level 3.** The taser recharge bar disappeared on level 3

## Third-Party Assets

### Environment, Character, and Prop Models/Textures (Assets/_Plugins/)
- 3D Laboratory Environment with Apparatus — lab/hospital set dressing
- AK Studio Art — radio and surveillance camera props
- Airduct BMT — modular airduct/vent system (includes scripts)
- BackRock Studios (LowPoly-Doctor) — player character model, textures, animations
- Barking_Dog — ambient creature model, includes movement scripts and audio
- Bit Gun — weapon/prop model
- Blood — blood decal prefabs (paired with Blood decal pack under Assets/Asset/)
- CRT_&_Slimline_Monitors_Pack — retro computer/monitor set dressing
- Creepy_Cat — ambient creature model, includes audio
- Drone_Guard — drone enemy model, animator controller, and scripts
- Dungeon Floor Traps — floor trap props
- Eye — eyeball prop
- FreeIndustrialModels — industrial pipe/tube environment props
- Future Pad Door - FREE — sci-fi sliding door
- Glowing Rifts — glowing environmental hazard/decor
- HiTech — sci-fi "energy cell" props
- HorrorPuzzleItems — lighter and keycard puzzle items
- JustPlay — computer monitor/speaker props
- Low-Poly 3D Lockers — locker/storage props
- LowPoly_Outdoor_AC_Unit — AC unit prop
- MASH Virtual — sci-fi access gate
- Mobile_city_props_collection — trash can/city clutter props
- Modular Pipes Asset — modular pipe environment kit
- Pekdata — sparks/smoke VFX and example scripts
- Pipe constructor — pipe prop
- PolyPeople_Hospital_Free — hospital NPC/crowd models
- Protection_suite — hazmat/protective suit model
- Robot_Soldier — robot enemy model
- Sat Productions — concrete barrier/props
- Sci Fi Gun Light — weapon prop with light
- SciFi Warehouse Kit — warehouse/level environment kit, includes scripts, physics materials, and audio
- SciFiWarriorPBRHPPolyart — sci-fi enemy character model
- ScifiOfficeLite — office environment set, includes scripts
- _kawetofeAssets — sci-fi tablet prop
- robot_metallic — robot enemy/prop model

### Additional Models/Textures (Assets/Asset/)
- SciFiLike Asset — sci-fi environment set
- Blood decal pack — source textures for blood decal prefabs
- Meshtint Free Super Ward Pack — hospital ward prop pack

### Robot/Enemy Sub-Packages (Assets/Robots/)
- **Simple Lightning Bolt** by Digital Ruby, LLC (© 2016, Jeff Johnson) — electric shock/lightning VFX used for the taser mechanic
- **SentryRobot** by Light Hearted Tech — base model for the stationary Arsenal turret enemy
- **Bullets Pack** by DuNguyn — bullet/projectile models

### Unity Official Assets
- Unity StarterAssets (Unity Companion License) — includes SpaceRobot Kyle reference assets

### Programming Libraries
No third-party programming libraries (e.g., pathfinding implementations) are used beyond Unity's own official packages (URP, Input System, Cinemachine, AI Navigation, Shader Graph — all pulled from Unity's package registry, no separate attribution required).

### Licenses
Bundled license/readme documentation is included in the submission for the following packages, where provided by the original asset:
- `Assets/UnityTechnologies/license.txt` (Unity Companion License)
- `Assets/UnityTechnologies/StarterAssets/StarterAssets_Documentation_v1.1.pdf`
- `Assets/Robots/Drone-Robot/LightningBolt/Readme.txt` (Simple Lightning Bolt)
- `Assets/Robots/Arsenal-Robot/Light Hearted Tech/SentryRobot/Documentation/Readme.txt` (SentryRobot)

The remaining third-party packages listed above were imported without their original bundled documentation; no license file is available to include beyond the publisher/package name noted.
