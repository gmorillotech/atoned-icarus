#!/bin/bash
# Creates the _Project folder structure and drops a .gitkeep in every empty folder
# so Git tracks them even before any real files exist.

FOLDERS=(
  "_Project/Art/Characters"
  "_Project/Art/Environment"
  "_Project/Art/Robots"
  "_Project/Art/UI"
  "_Project/Audio/SFX"
  "_Project/Audio/Music"
  "_Project/Audio/Ambience"
  "_Project/Animations"
  "_Project/Prefabs/Player"
  "_Project/Prefabs/Robots"
  "_Project/Prefabs/Interactables"
  "_Project/Prefabs/Environment"
  "_Project/Scenes/Floors"
  "_Project/Scenes/Transitions"
  "_Project/Scenes/Menus"
  "_Project/Scenes/_Sandbox"
  "_Project/Scripts/Core"
  "_Project/Scripts/Player"
  "_Project/Scripts/AI/Robots"
  "_Project/Scripts/AI/Perception"
  "_Project/Scripts/AI/SharedStates"
  "_Project/Scripts/Interactables"
  "_Project/Scripts/Puzzles"
  "_Project/Scripts/UI"
  "_Project/Scripts/Utils"
  "_Project/ScriptableObjects/RobotConfigs"
  "_Project/ScriptableObjects/DifficultySettings"
  "_Project/ScriptableObjects/GameEvents"
  "_Project/Settings"
)

for folder in "${FOLDERS[@]}"; do
  mkdir -p "$folder"
  touch "$folder/.gitkeep"
done

echo "Created ${#FOLDERS[@]} folders with .gitkeep placeholders."
echo "Next: open Unity to let it generate .meta files, then commit."
