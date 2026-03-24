# CLAUDE.md - Last Line Defense

## Project Overview
Mobile-first stage defense game focused on ad monetization.
Primary platform: Android. Engine: Unity 6 LTS. Language: C#.

## Core Rules
- Keep code modular and testable
- Prefer simple, explicit C# over abstract patterns
- Use ScriptableObjects for game data definitions
- Keep MonoBehaviours thin — move logic to plain C# classes
- All gameplay values must be configurable
- Core gameplay must work offline (except ads/analytics)

## Code Style
- PascalCase for public classes/methods
- camelCase for private fields
- Keep files under 400 lines, functions under 50 lines
- Immutable patterns preferred

## Architecture
- Gameplay independent from ad logic
- Ads wrapped behind IAdService interface
- Save system independent from UI
- UI subscribes to state changes (event-driven)

## Folder Rules
- Scripts/Core — GameManager, GameEvents, Bootstrap
- Scripts/Game — StageManager, CurrencyManager, HealthBase
- Scripts/Enemy — EnemyController, EnemyMover, EnemyHealth
- Scripts/Tower — TowerSlot, BasicTower, TowerTargeting, Projectile
- Scripts/Wave — WaveManager, EnemySpawner
- Scripts/Data — ScriptableObject definitions
- Scripts/UI — HUD, Result, Lobby controllers
- Scripts/Ads — IAdService, DummyAdService
- Scripts/Save — SaveData, SaveManager

## MVP Scope
- 10 stages, 4 tower types, 5 enemy types
- Rewarded ads (3 points) + interstitial
- Permanent upgrades (8), wave upgrades (pick 1 of 3)
- Basic save/load, Firebase Analytics

## Do Not
- Add multiplayer, PvP, guild systems
- Add complex 3D graphics or cutscenes
- Refactor unrelated systems
- Add placeholder complexity beyond MVP
