# Unity 씬 연결 가이드 (Setup Guide)
# Last Line Defense - 프리팹/씬/Inspector 연결 순서

---

## 1. Boot 씬 설정

### Hierarchy 구성
```
Boot Scene
├─ Managers (빈 GameObject)
│  ├─ GameManager.cs 부착
│  ├─ SaveManager.cs 부착
│  ├─ DummyAdService.cs 부착
│  ├─ RemoteConfigManager.cs 부착
│  └─ IAPManager.cs 부착
├─ Bootstrap (빈 GameObject)
│  └─ SceneBootstrap.cs 부착
│     ├─ Is Boot Scene: ✅ 체크
│     └─ Next Scene Name: "Lobby"
├─ Main Camera
└─ EventSystem
```

### Build Settings에 씬 추가
File > Build Settings > Add Open Scenes
순서: Boot(0), Lobby(1), Stage(2)

---

## 2. Lobby 씬 설정

### Hierarchy 구성
```
Lobby Scene
├─ Canvas (Screen Space - Overlay)
│  ├─ TopBar (빈 GameObject)
│  │  ├─ CurrencyText (TextMeshPro)
│  │  └─ HighestStageText (TextMeshPro)
│  ├─ CenterPanel (빈 GameObject)
│  │  ├─ SelectedStageText (TextMeshPro)
│  │  ├─ PrevButton (Button)
│  │  ├─ NextButton (Button)
│  │  └─ StartButton (Button)
│  └─ BottomPanel (빈 GameObject)
│     ├─ UpgradeButton (Button) → UpgradeShopUI.Open() 연결
│     └─ DailyRewardButton (Button)
├─ LobbyUI (빈 GameObject)
│  └─ LobbyUIController.cs 부착
│     ├─ Currency Text → CurrencyText 드래그
│     ├─ Highest Stage Text → HighestStageText 드래그
│     ├─ Selected Stage Text → SelectedStageText 드래그
│     ├─ Start Button → StartButton 드래그
│     ├─ Prev Button → PrevButton 드래그
│     └─ Next Button → NextButton 드래그
├─ UpgradeShop (빈 GameObject, 비활성)
│  └─ UpgradeShopUI.cs 부착
├─ DailyReward (빈 GameObject)
│  ├─ DailyRewardManager.cs 부착
│  └─ DailyRewardUI.cs 부착
├─ Main Camera
└─ EventSystem
```

---

## 3. Stage 씬 설정

### Hierarchy 구성
```
Stage Scene
├─ Main Camera
├─ Canvas (Screen Space - Overlay)
│  ├─ TopHUD (빈 GameObject)
│  │  ├─ GoldText (TextMeshPro)
│  │  ├─ WaveText (TextMeshPro)
│  │  └─ BaseHpText (TextMeshPro)
│  ├─ TowerPanel (하단, 비활성)
│  │  └─ TowerSelectionUI.cs 부착
│  ├─ UpgradePanel (비활성)
│  │  └─ UpgradeSelectionUI.cs 부착
│  ├─ ResultPanel (비활성)
│  │  ├─ ResultTitleText (TextMeshPro)
│  │  ├─ RewardText (TextMeshPro)
│  │  ├─ RewardAdButton (Button)
│  │  ├─ NextButton (Button)
│  │  ├─ RetryButton (Button)
│  │  └─ LobbyButton (Button)
│  ├─ TutorialPanel (비활성)
│  │  ├─ TutorialText (TextMeshPro)
│  │  └─ NextStepButton (Button)
│  ├─ PausePanel (비활성)
│  │  ├─ ResumeButton (Button)
│  │  └─ QuitButton (Button)
│  ├─ SpeedButton (Button)
│  └─ PauseButton (Button)
├─ StageSystems (빈 GameObject)
│  ├─ StageManager.cs 부착
│  │  ├─ Starting Gold: 120
│  │  ├─ Starting Base Hp: 20
│  │  ├─ Total Waves: 3
│  │  ├─ Clear Reward: 60
│  │  ├─ Currency Manager → 아래 드래그
│  │  └─ Health Base → 아래 드래그
│  ├─ CurrencyManager.cs 부착
│  ├─ HealthBase.cs 부착
│  ├─ WaveManager.cs 부착
│  │  └─ Time Between Waves: 3
│  ├─ EnemySpawner.cs 부착
│  │  ├─ Spawn Point → SpawnPoint 드래그
│  │  ├─ Enemy Prefab → Enemy 프리팹 드래그
│  │  └─ Enemy Parent → RuntimeRoot/Enemies 드래그
│  ├─ WaveUpgradeManager.cs 부착
│  ├─ ResultFlowController.cs 부착
│  │  ├─ Result UI → ResultUIController 드래그
│  │  └─ Ad Service Behaviour → DummyAdService 드래그 (Boot의 Managers)
│  ├─ StageUIController.cs 부착
│  │  ├─ Gold Text → GoldText 드래그
│  │  ├─ Wave Text → WaveText 드래그
│  │  └─ Base Hp Text → BaseHpText 드래그
│  ├─ ResultUIController.cs 부착
│  ├─ TutorialManager.cs 부착
│  ├─ GameSpeedController.cs 부착
│  ├─ PauseMenuUI.cs 부착
│  └─ AnalyticsIntegrator.cs 부착
├─ RouteRoot (빈 GameObject)
│  ├─ Waypoint_01 (빈 GameObject, 위치 조정)
│  ├─ Waypoint_02
│  ├─ Waypoint_03
│  └─ GoalPoint
├─ SpawnPoint (빈 GameObject — 적 생성 위치)
├─ BasePoint (빈 GameObject — 기지 위치)
├─ TowerSlotsRoot (빈 GameObject)
│  ├─ TowerSlot_01 (빈 GO + BoxCollider2D + TowerSlot.cs)
│  ├─ TowerSlot_02
│  ├─ TowerSlot_03
│  ├─ TowerSlot_04
│  └─ TowerSlot_05
├─ RuntimeRoot (빈 GameObject)
│  ├─ Enemies (빈 GO — 적 부모)
│  ├─ Projectiles (빈 GO — 투사체 부모)
│  └─ Towers (빈 GO — 타워 부모)
└─ EventSystem
```

---

## 4. 프리팹 생성

### Enemy 프리팹 (Assets/_Project/Prefabs/Enemies/)

1. 빈 GameObject 생성 → 이름: "Enemy_Basic"
2. SpriteRenderer 추가 → 빨간 사각형 스프라이트
3. BoxCollider2D 추가
4. 스크립트 부착:
   - EnemyController.cs (Reward Gold: 10, Base Damage: 1)
   - EnemyMover.cs (Move Speed: 2)
   - EnemyHealth.cs (Max Hp: 30)
5. Prefabs/Enemies 폴더에 드래그하여 프리팹 생성

### Tower 프리팹 (Assets/_Project/Prefabs/Towers/)

1. 빈 GameObject 생성 → 이름: "Tower_Basic"
2. SpriteRenderer 추가 → 파란 사각형 스프라이트
3. 스크립트 부착:
   - BasicTower.cs (Damage: 10, Attack Interval: 1.0)
   - TowerTargeting.cs (Attack Range: 3)
4. 자식으로 "FirePoint" 빈 GO 추가 (발사 위치)
5. Prefabs/Towers 폴더에 드래그

### Projectile 프리팹 (Assets/_Project/Prefabs/Projectiles/)

1. 빈 GameObject 생성 → 이름: "Projectile_Basic"
2. SpriteRenderer 추가 → 작은 흰 원형
3. 스크립트 부착:
   - ProjectileController.cs (Move Speed: 8)
4. Prefabs/Projectiles 폴더에 드래그

---

## 5. 프리팹 연결

1. EnemySpawner의 Enemy Prefab에 → Enemy_Basic 프리팹 드래그
2. BasicTower의 Projectile Prefab에 → Projectile_Basic 프리팹 드래그
3. BasicTower의 Fire Point에 → FirePoint GO 드래그
4. TowerSlot의 Tower Prefab에 → Tower_Basic 프리팹 드래그

---

## 6. 테스트 실행

1. Boot 씬을 열고 Play
2. 자동으로 Lobby 씬으로 전환되는지 확인
3. Start 버튼 → Stage 씬 전환 확인
4. 적 스폰 → 경로 이동 확인
5. 타워 슬롯 클릭 → 타워 설치 확인
6. 타워 공격 → 적 처치 → 골드 증가 확인
7. 모든 웨이브 클리어 → 결과 화면 확인

---

## 7. ScriptableObject 에셋 생성

Unity 메뉴: Assets > Create > Defense > ...

### Enemy Data 에셋
- Create > Defense > Enemy Data
- 이름: "EnemyData_Normal"
- maxHp: 30, moveSpeed: 2, rewardGold: 10

### Tower Data 에셋
- Create > Defense > Tower Data
- 이름: "TowerData_Basic"
- buildCost: 50, damage: 10, attackRange: 3, attackInterval: 1

### Stage Data 에셋
- Create > Defense > Stage Data
- 이름: "StageData_01"
- stageIndex: 0, startingGold: 120, baseHp: 20, clearReward: 60
