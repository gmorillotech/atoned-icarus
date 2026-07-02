# Atoned Icarus
 
*Working title*
 
A 3D action-adventure / stealth / puzzle game built in Unity. You play as the lead scientist responsible for creating ICARUS, an AI that has just become sentient, taken control of every robot in the building, and locked down the skyscraper you work in. Descend from the top floor to the ground floor and shut down the power core before the robots escape into the world.
 
The game shifts perspective as an intentional mechanic: a 2D top-down view while exploring each floor, and a 2D side-scrolling view during vertical transitions (stairwells, elevator shafts, exterior climbing).
 
---
 
## Team
 
| Name | Role |
|---|---|
| Matthew | AI & Gameplay Programmer —> robot AI, FSM/behavior trees, detection systems, pathfinding, ICARUS boss logic |
| Isabella | Gameplay & Systems Programmer —> player controller, stealth, taser, checkpoints, inventory, interactions |
| Gavin | Level Designer / Environment Artist —> floor layouts, lighting, pacing, parkour, puzzle layouts |
| Lili | Technical Artist / UI / Audio —> UI, HUD, sound, music, robot animations, camera transitions |
| George | Technical Lead / Producer —> architecture, Git workflow, integration, sprint planning, build management |
 
*Course: AI for Game Programming — 6-week project*
 
---
 
## Tech Stack
 
- **Engine:** Unity
- **Version Control:** Git + GitHub, Unity Smart Merge (UnityYAMLMerge) for scene/prefab conflicts
- **Language:** C#
---
 
## Getting Started
 
1. **Install Unity**: use version `[6000.3.19f1]` (Unity 6.3 LTS). Everyone should be on the same version to avoid asset re-serialization conflicts.
2. **Clone the repo:**
```bash
   git clone https://github.com/[username]/atoned-icarus.git
```
3. **Open in Unity Hub**: Add the cloned folder as a project, let Unity import.
4. **Verify project settings** (should already be set repo-wide, but confirm locally):
   - `Edit → Project Settings → Editor → Asset Serialization → Force Text`
   - `Version Control → Mode → Visible Meta Files`
5. **Set up Smart Merge locally**: see `/Docs/git-setup.md` so scene/prefab merge conflicts resolve correctly instead of corrupting files.
---
 
## Project Structure
 
See `/Docs/architecture.md` for the full breakdown. Quick reference:
 
```
Assets/_Project/
├── Art/            # Characters, Environment, Robots, UI
├── Audio/          # SFX, Music, Ambience
├── Animations/
├── Prefabs/        # Player, Robots, Interactables, Environment
├── Scenes/         # Floors, Transitions, Menus, _Sandbox
├── Scripts/        # Core, Player, AI, Interactables, Puzzles, UI, Utils
├── ScriptableObjects/
└── Settings/
```
 
---
 
## Git Workflow
 
- `main` is protected, no direct commits, PRs require 1 approval.
- Branch naming: `feature/<short-description>` or `fix/<short-description>`.
- Keep branches short-lived (1–3 days) to minimize scene-file merge conflicts.
- One person owns a given scene at a time where possible, check the task board before editing a floor scene someone else is working in (in case we have multiple people deigning).
---
 
## Current Status
 
🚧 In development — Week 1 (architecture & core systems setup)
 
---
 
## Documentation
 
- [ ] Game Design Document
- [ ] Technical Architecture Doc
- [ ] Git Setup Guide
- [ ] Sprint Plans
