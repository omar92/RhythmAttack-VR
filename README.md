# RhythmAttack-VR

> **ITI T38 GD/GA Graduation Project** — A Virtual Reality music rhythm game where you slash and shoot enemies in sync with MIDI music.

🎮 **[Unity Connect Project Page](https://connect.unity.com/p/rhythmattack-vr)**

---

## Table of Contents

- [About the Game](#about-the-game)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [How to Play](#how-to-play)
- [Configuration](#configuration)
- [Project Structure](#project-structure)
- [Architecture Overview](#architecture-overview)
- [VR Platform Support](#vr-platform-support)
- [Credits](#credits)

---

## About the Game

**RhythmAttack-VR** is a VR rhythm combat game built with Unity. Notes are spawned in sync with MIDI music tracks — slash them with your sword, dodge the ones you can't cut, and blast the boss with your gun. The game uses a ScriptableObject-based architecture for flexible, data-driven level and gameplay configuration.

---

## Features

- 🎵 **MIDI-synced gameplay** — notes are generated from real MIDI files in real time
- ⚔️ **Dual combat modes** — Melee (sword) and Ranged (gun) phases
- 🛡️ **Defense & Attack** — dodge incoming notes and counter-attack the boss
- 🏆 **Scoring & Leaderboards** — points awarded for accurate hits; leaderboard tracking
- 🎚️ **Multiple difficulty levels** — swap MIDI tracks for different challenge levels
- 🔌 **Multi-platform VR** — supports SteamVR, Oculus, Windows Mixed Reality, and more via VRTK
- 🧱 **ScriptableObject architecture** — fully data-driven; designers can tweak balance without code changes

---

## Prerequisites

| Requirement | Version / Details |
|-------------|-------------------|
| **Unity** | **2018.1.5f1** (exact version required) |
| **VR Headset** | HTC Vive, Oculus Rift, Windows Mixed Reality, or any VRTK-compatible headset |
| **SteamVR** | Required for PC VR playback |
| **Operating System** | Windows 10 (64-bit recommended) |

> ⚠️ The project was built and tested with **Unity 2018.1.5f1**. Opening it in a different Unity version may cause compatibility issues with VRTK, SteamVR, and MidiPlayerTK plugins.

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/omar92/RhythmAttack-VR.git
```

### 2. Install Unity 2018.1.5f1

Download and install [Unity 2018.1.5f1](https://unity3d.com/get-unity/download/archive) using **Unity Hub** or the standalone installer.

### 3. Open the Project

1. Open **Unity Hub**.
2. Click **Add** and navigate to the cloned `RhythmAttack-VR` folder.
3. Select **Unity 2018.1.5f1** as the editor version.
4. Click **Open** — Unity will import all assets on the first launch (this may take a few minutes).

### 4. Open the Main Scene

Navigate to:

```
Assets/0_ProjectFiles/Scenes/GamePlayScene.unity
```

Double-click `GamePlayScene.unity` to open it.

### 5. Connect Your VR Headset (Optional)

- Launch **SteamVR** before pressing Play if you want to play in VR.
- The game also supports keyboard input for testing without a headset.

### 6. Press Play

Click the **▶ Play** button in the Unity Editor toolbar to start the game.

---

## How to Play

### Controls

| Action | VR Controller | Keyboard (Testing) |
|--------|--------------|-------------------|
| Slash notes | Swing the right controller (sword) | — |
| Shoot enemy | Pull right trigger (gun) | Space |
| Dodge notes | Move your head/body | Arrow keys |
| Grip weapon | Hold grip button | — |

### Gameplay Loop

1. **Music starts** — the MIDI track begins playing.
2. **Notes spawn** — coloured notes fly toward you in sync with the beat.
3. **Slash or dodge** — cut attack notes with your sword; dodge evade notes.
4. **Attack the Boss** — when the boss is exposed, switch to the gun and fire.
5. **Survive** — your health decreases when you miss notes; regeneration kicks in between phases.
6. **Score** — combo multipliers reward consistent, accurate hits.
7. **Win** — deplete the boss's health to finish the level.

### Scoring Tips

- Hit notes exactly on the beat for maximum points.
- Chain consecutive hits to build your combo multiplier.
- Avoid taking damage to preserve your combo.

---

## Configuration

All game parameters are stored as **ScriptableObject assets** located in:

```
Assets/0_ProjectFiles/ScriptableObjects/GameData/
```

| Asset | What it Controls |
|-------|-----------------|
| `LevelSettings.asset` | Player/boss health, note speed, damage values, healing rates |
| `LevelSounds.asset` | MIDI track paths for each difficulty level |
| `GeneralGameSounds.asset` | Sound effects (sword swing, gunshot, pain, boss dialogue) |
| `GameLevel.asset` | Currently active difficulty level |

### Adding a Custom MIDI Track

1. Place your `.mid` file in `Assets/0_ProjectFiles/Resources/Tracks/`.
2. Open `LevelSounds.asset` in the Inspector.
3. Assign the new track to the desired difficulty slot.

### Adjusting Game Balance

Open `LevelSettings.asset` and adjust values such as:
- `playerMaxHealth` / `bossMaxHealth`
- `noteMoveSpeed`
- `hitDamage` / `missDamage`
- `healthRegenRate`

---

## Project Structure

```
RhythmAttack-VR/
├── Assets/
│   ├── 0_ProjectFiles/             # All custom game content
│   │   ├── Audios/                 # Sound effects (MP3)
│   │   ├── Effects/                # Visual & audio effects
│   │   ├── Materials/              # Game materials
│   │   ├── Prefabs/                # Reusable game objects
│   │   │   ├── GamePLayElements/   # VR Player, Boss, health bars
│   │   │   ├── Engine/             # Scene manager prefabs
│   │   │   ├── Characters/         # Character models
│   │   │   └── UI/                 # UI prefabs
│   │   ├── Resources/
│   │   │   ├── Models/             # 3D models (Boss, Minions)
│   │   │   ├── Tracks/             # MIDI track files
│   │   │   └── UI/                 # UI resources
│   │   ├── Scenes/
│   │   │   ├── GamePlayScene.unity # Main gameplay scene
│   │   │   └── TestScene.unity     # Development / testing scene
│   │   ├── Scripts/
│   │   │   ├── MonoScripts/        # Runtime gameplay logic
│   │   │   │   ├── Midi/           # MIDI parsing & playback
│   │   │   │   ├── Emitter/        # Note spawning system
│   │   │   │   ├── Engine/         # Game state & managers
│   │   │   │   ├── VR/             # VR controller input
│   │   │   │   ├── MeleeWeapon/    # Sword mechanics
│   │   │   │   ├── RangedWeapon/   # Gun mechanics
│   │   │   │   ├── Player/         # Player health & states
│   │   │   │   └── Boss/           # Boss behaviour
│   │   │   ├── ScriptableObjectsScripts/
│   │   │   │   ├── DataTypes/      # FloatVariable, BoolVariable, etc.
│   │   │   │   ├── Engine/         # StateMachine, GameState
│   │   │   │   └── Events/         # GameEvent system
│   │   │   └── Editor/             # Custom Unity Editor tools
│   │   └── ScriptableObjects/      # Runtime data assets
│   ├── VRTK/                       # VR Toolkit SDK
│   ├── SteamVR/                    # SteamVR plugin
│   ├── Oculus/                     # Oculus SDK
│   └── MidiPlayer/                 # MidiPlayerTK plugin
├── Packages/                       # Unity Package Manager manifests
├── ProjectSettings/                # Unity project settings
└── README.md
```

---

## Architecture Overview

The project follows a **ScriptableObject-driven, event-based** architecture:

### State Machine
- `StateMachine.cs` — ScriptableObject state machine supporting smooth and forced transitions.
- `GameState.cs` — Base class for all game states (`OnEnter`, `OnExit`, `OnPause`, `OnUnPause`).
- `GameStateHandler.cs` — MonoBehaviour that drives state transitions at runtime.

### Event System
- `GameEvent.cs` — ScriptableObject events for loose coupling between systems.
- `GameEventListener.cs` — MonoBehaviour that subscribes to a `GameEvent` and invokes a UnityEvent response.

### MIDI & Rhythm Pipeline
1. `MidiHandler.cs` loads the MIDI file and maps notes to lanes.
2. `BroadcastMidiWave.cs` broadcasts note events on each beat.
3. `Emitter.cs` spawns note prefabs at the correct time and position.
4. `NoteScript.cs` handles the note's movement and collision with the player's weapon.

### ScriptableObject Data Containers
- `FloatVariable`, `BoolVariable`, `StringVariable`, `IntVariable` — runtime data shared across components without direct references.

---

## VR Platform Support

VRTK abstracts input across all major PC VR platforms:

| Platform | SDK |
|----------|-----|
| HTC Vive | SteamVR |
| Oculus Rift / Rift S | Oculus SDK |
| Windows Mixed Reality | SteamVR / WMR |
| Daydream | Google VR |
| Ximmerse | Ximmerse SDK |
| HyperealVR | HyperealVR SDK |

---

## Credits

- **Developer / Designer:** Omar (ITI T38 GD/GA Graduation Project)
- **Engine:** [Unity 2018.1.5f1](https://unity.com/)
- **VR Framework:** [VRTK — Virtual Reality Toolkit](https://vrtoolkit.readme.io/)
- **SteamVR Plugin:** [Valve SteamVR Unity Plugin](https://github.com/ValveSoftware/steamvr_unity_plugin)
- **MIDI Playback:** [MidiPlayerTK](https://www.midifile.net/)
- **Oculus SDK:** [Oculus Integration](https://developer.oculus.com/)
