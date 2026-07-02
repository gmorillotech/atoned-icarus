# Atoned Icarus — Week 1 Architecture Skeleton

**Purpose:** Get us from zero to a stable, shared foundation in 5 working days, without locking in decisions our still-evolving GDD hasn't settled yet. Everything here is designed to be *cheap to extend, expensive to rewrite later*.

---

## 1. Priority Risk: The Perspective-Switch Mechanic

This is the mechanic that makes our game distinct, and it's also the thing most likely to blow up our schedule if it's discovered to be broken in week 4 instead of week 1.

**Why it's risky:**
- Two fundamentally different player controllers (top-down 2D-on-3D vs. side-scroll) need to share state (health, inventory, stealth status) seamlessly.
- Camera transitions between orthographic-feeling top-down and a side-scroll rig have to be smooth or they'll feel like bugs.
- Physics/collision behaves differently in each mode. If our level geometry assumes one mode's collider setup, it may not "just work" in the other.
- Every other system (AI vision cones, hiding spots, robot patrol paths) potentially needs to know which mode it's operating in.

**Decision: build an isolated spike, not a feature.**

Before anyone writes robot AI, puzzle systems, or level art pipelines, I want one person (prob Lili) building a **throwaway test scene** that does nothing but:
1. A capsule/cube player that moves in top-down mode.
2. A trigger volume ("enter stairwell") that swaps to a side-scroll camera + controller.
3. A trigger at the top that swaps back.
4. No art, no AI, no puzzles, just proof that the transition is clean and the state (position, facing, "player exists") survives the swap.

**Timebox: 2 days.** If it's not working cleanly in 2 days, we escalate — either simplify the transition (e.g., a hard cut instead of a live camera blend) or descope to fewer transition types. If we're underestimating this, I need to know by Wednesday, not week 4.

**Architecture pattern for the switch:** a `PerspectiveMode` enum (`TopDown`, `SideScroll`) owned by a single `PerspectiveController` that:
- Disables/enables the appropriate `PlayerController` component (composition — two separate controller scripts, not one bloated one with mode-branching everywhere).
- Fires a `PerspectiveChanged` event that camera, AI vision systems, and UI can subscribe to.
- This keeps mode-switching logic in one place instead of scattered `if (mode == TopDown)` checks across the codebase.

---

## 2. Team Structure

**1. AI & Gameplay Programmer (Matthew)**

*Primary Responsibilities*
- Robot AI
- Finite State Machines / Behavior Trees
- Detection systems (vision, hearing, heat)
- Navigation & pathfinding
- Difficulty scaling (if time allows)
- Alert system
- ICARUS boss logic

*Deliverables*
- Every robot behavior
- AI debugging tools
- Detection cones
- Enemy balancing

**2. Gameplay & Systems Programmer (Isabella)**

*Primary Responsibilities*
- Player controller
- Stealth mechanics
- Taser system
- Health/checkpoints
- Inventory/keycards
- Interactions
- Save/progression logic

*Deliverables*
- Everything the player can do
- Elevator interactions
- Hiding system
- Win/Lose conditions

**3. Level Designer / Environment Artist (Gavin)**

*Primary Responsibilities*
- Build every floor
- Lighting
- Level pacing
- Environmental storytelling
- Parkour sections
- Robot placement
- Puzzle layouts

*Deliverables*
- Research lab
- Elevator shafts
- Windows section
- Final showroom
- Visual polish

**4. Technical Artist / UI / Audio (Lili)**

*Primary Responsibilities*
- UI
- Menus
- HUD
- Sound effects
- Music
- Robot animations
- Particles
- Camera transitions (top-down ↔ side-scroller)

*Deliverables*
- Main menu
- Pause menu
- Detection indicators
- Audio cues
- Atmosphere
- Polish

**5. Technical Lead / Producer / Integration (Me — George)**

*Primary Responsibilities*
- GitHub management
- Branching strategy
- Code reviews
- Integrate everyone's work
- Architecture
- Build management
- Sprint planning
- Help wherever blockers arise
- Also one technical feature, such as: Game Manager, Scene loading, Save system, Checkpoint manager, Event system, Project architecture

*Deliverables*
- Stable builds
- Merge conflict resolution
- Weekly playable versions
- Task tracking
- Help in any team as needed

*I'll expect everyone to contribute to final documentation for the sections they owned, and to the final presentation.*

---

## 3. Folder Structure

```
Assets/
├── _Project/                  # Everything we own (underscore keeps it pinned to top)
│   ├── Art/
│   │   ├── Characters/
│   │   ├── Environment/
│   │   ├── Robots/
│   │   └── UI/
│   ├── Audio/
│   │   ├── SFX/
│   │   ├── Music/
│   │   └── Ambience/
│   ├── Animations/
│   ├── Prefabs/
│   │   ├── Player/
│   │   ├── Robots/
│   │   ├── Interactables/       # doors, keycards, recharge stations
│   │   └── Environment/
│   ├── Scenes/
│   │   ├── Floors/               # Floor01, Floor02, ... (one per level)
│   │   ├── Transitions/          # perspective-switch test scenes
│   │   ├── Menus/
│   │   └── _Sandbox/             # personal scratch scenes, gitignored per-user subfolders
│   ├── Scripts/
│   │   ├── Core/                  # GameManager, EventBus, SceneLoader
│   │   ├── Player/
│   │   ├── AI/
│   │   │   ├── Robots/            # per-robot-type behavior
│   │   │   ├── Perception/        # vision, hearing, shared sensor logic
│   │   │   └── SharedStates/      # reusable FSM/BT nodes
│   │   ├── Interactables/
│   │   ├── Puzzles/
│   │   ├── UI/
│   │   └── Utils/
│   ├── ScriptableObjects/
│   │   ├── RobotConfigs/          # tunable data per robot type
│   │   ├── DifficultySettings/
│   │   └── GameEvents/            # SO-based event channels if we go that route
│   └── Settings/                  # input actions, URP settings, etc.
└── Plugins/ (third-party only — never mix with _Project)
```

**Naming conventions:**
- Scripts: `PascalCase.cs`, one public class per file, filename matches class name.
- Scenes: `Floor_01_Lobby`, `Floor_02_ServerRoom`, numbered so load order is obvious in the Project window.
- Prefabs: `PF_RobotPatrol`, `PF_Door_Keycard`, prefix makes them greppable and distinguishes from scripts of similar name.
- ScriptableObject assets: `SO_RobotConfig_Patrol`.

---

## 4. Git Branching Model

With 5 people and 6 weeks, I don't think we need GitFlow's full ceremony (no release branches, no hotfix branches, we're not maintaining a live product). We need something that minimizes merge conflicts on Unity's notoriously diff-unfriendly scene/prefab files.

**My call: trunk-based with short-lived feature branches.**

```
main            ← always buildable, always playable
  └── feature/robot-patrol-ai
  └── feature/perspective-switch-spike
  └── feature/hiding-spot-system
  └── feature/floor-01-greybox
```

Rules:
- `main` is protected, no direct commits, PR required, must build without errors.
- Branch naming: `feature/<short-description>` or `fix/<short-description>`.
- **Branches live 1–3 days max.** Long-lived branches are where Unity merge hell happens (scene files especially). If a feature will take longer, break it into smaller mergeable chunks.
- One person "owns" a given scene file at a time when possible.
- I'm setting up Unity's **Smart Merge (UnityYAMLMerge)** tool in Git. This alone prevents most scene-file disasters.
- `.gitignore`: I'll use Unity's official gitignore template (Library/, Temp/, Obj/, Build/, .vs/, *.csproj, none of these belong in source control).

**PR process (lightweight, not bureaucratic):**
1. Open PR against `main`.
2. One other teammate reviews (most likely Geo), it doesn't need to be exhaustive, just: does it build, does it follow the folder/naming conventions, does it duplicate existing systems.
3. Merge same day if possible. Momentum matters more than ceremony on a 6-week clock.

---

## 5. Core Architecture Pattern

**Event-driven core, avoiding a God singleton.**

A single `GameManager` singleton is fine for genuinely global state (current floor, game state: Playing/Paused/GameOver), but I want us to resist the urge to route everything through it. That's the #1 way student projects turn into unmaintainable spaghetti by week 3.

Core pieces for week 1:

- **`GameManager`** (singleton, minimal): tracks current floor index, overall game state. Nothing else.
- **`EventBus`** (plain C# events, since this is our first game project and SO-based event channels would add indirection without enough payoff on a 6-week clock): used for things like `OnPlayerDetected`, `OnPuzzleSolved`, `OnPerspectiveChanged`, `OnCheckpointReached`. This is what keeps robot AI, UI, and audio decoupled from each other. This is my technical feature ownership area, alongside GameManager and SceneLoader.
- **`SceneLoader`**: handles floor-to-floor transitions, checkpoint respawns. One place, not copy-pasted per scene.
- **Player systems as composition, not inheritance**: `PlayerController` (movement), `PlayerStealth` (visibility/noise state), `PlayerInventory` (keycards) as separate components on the same GameObject, communicating via events, not one 800-line `Player.cs`.
- **Robot AI as a shared base + per-type behavior**: a common `RobotPerceptionSystem` (vision/hearing sensors) that each robot type composes, rather than five parallel inheritance hierarchies. This is also where our "each robot showcases a different AI concept" goal gets architectural teeth, vision-based, heat-based, and sound-based robots all just plug different sensor components into the same shell.

This can wait until our AI design session, but I wanted to flag it now so nobody starts hardcoding robot-specific logic into a monolithic `Robot.cs` this week.

---

## 6. Week 1 Task Breakdown

| Person | Task | Notes |
|---|---|---|
| Lili | Perspective-switch spike (top-down ↔ side-scroll camera/controller swap) | Highest-risk item on the project |
| Me (George) | Git repo setup, branch protection, Smart Merge (UnityYAMLMerge) config + `GameManager`/`EventBus`/`SceneLoader` skeleton | My integration + core technical feature work |
| Matthew | Prototype one robot's perception (vision cone detection) in an isolated test scene | Doesn't need full game context yet, good parallel track, feeds directly into your AI ownership |
| Isabella | Player movement + basic interaction (top-down mode only for now) | Depends on nothing else yet; will hook into Lili's perspective controller once it lands |
| Gavin | Greybox Floor 1 layout (no art polish) + folder/scene naming pass | Low-risk, unblocks everyone once a real scene exists to build in |

**Definition of Done for week 1:** `main` builds, has one greyboxed floor scene, a player that moves in top-down mode, and a *proven* (even if ugly) perspective-switch prototype. No art polish, no full robot AI yet, just proof that the risky parts work.

---
