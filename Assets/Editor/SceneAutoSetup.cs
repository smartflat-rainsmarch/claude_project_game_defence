#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using LastLineDefense.Core;
using LastLineDefense.Game;
using LastLineDefense.Wave;
using LastLineDefense.Tower;
using LastLineDefense.Enemy;
using LastLineDefense.UI;
using LastLineDefense.Ads;
using LastLineDefense.Save;
using LastLineDefense.Analytics;
using LastLineDefense.Data;

namespace LastLineDefense.Editor
{
    public static class SceneAutoSetup
    {
        [MenuItem("Defense/Auto Setup ALL Scenes (One Click)", priority = 0)]
        public static void SetupAllScenes()
        {
            SetupBuildSettings();
            SetupBootScene();
            SetupLobbyScene();
            SetupStageScene();

            Debug.Log("=== ALL SCENES SETUP COMPLETE ===");
            EditorUtility.DisplayDialog("Setup Complete",
                "All 3 scenes (Boot, Lobby, Stage) have been set up.\n\n" +
                "1. Open Boot scene\n" +
                "2. Press Play to test\n\n" +
                "Boot → Lobby → Stage flow should work.",
                "OK");
        }

        [MenuItem("Defense/Setup Boot Scene Only")]
        public static void SetupBootScene()
        {
            var scene = EditorSceneManager.OpenScene("Assets/_Project/Scenes/Boot.unity");

            // Clear existing
            ClearScene();

            // Camera
            var cam = CreateGameObject("Main Camera");
            cam.AddComponent<Camera>().orthographic = true;
            cam.tag = "MainCamera";

            // EventSystem
            var es = CreateGameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();

            // Managers (DontDestroyOnLoad)
            var managers = CreateGameObject("Managers");
            managers.AddComponent<GameManager>();
            managers.AddComponent<SaveManager>();
            managers.AddComponent<DummyAdService>();
            managers.AddComponent<RemoteConfigManager>();
            managers.AddComponent<IAPManager>();
            managers.AddComponent<CrashlyticsHelper>();

            // Bootstrap
            var bootstrap = CreateGameObject("Bootstrap");
            var sb = bootstrap.AddComponent<SceneBootstrap>();
            SetPrivateField(sb, "isBootScene", true);
            SetPrivateField(sb, "nextSceneName", "Lobby");

            EditorSceneManager.SaveScene(scene);
            Debug.Log("[AutoSetup] Boot scene configured");
        }

        [MenuItem("Defense/Setup Lobby Scene Only")]
        public static void SetupLobbyScene()
        {
            var scene = EditorSceneManager.OpenScene("Assets/_Project/Scenes/Lobby.unity");

            ClearScene();

            // Camera
            var cam = CreateGameObject("Main Camera");
            cam.AddComponent<Camera>().orthographic = true;
            cam.tag = "MainCamera";

            // EventSystem
            var es = CreateGameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();

            // Canvas
            var canvas = CreateCanvas("Canvas");

            // TopBar
            var topBar = CreateUIChild(canvas, "TopBar");
            var rt = topBar.GetComponent<RectTransform>();
            SetAnchor(rt, 0, 1, 1, 1);
            rt.pivot = new Vector2(0.5f, 1);
            rt.anchoredPosition = new Vector2(0, 0);
            rt.sizeDelta = new Vector2(0, 80);

            var currencyText = CreateTMPText(topBar, "CurrencyText", "Coins: 0", 24);
            SetAnchor(currencyText.GetComponent<RectTransform>(), 0, 0, 0.5f, 1);

            var highestText = CreateTMPText(topBar, "HighestStageText", "Best: Stage 0", 24);
            SetAnchor(highestText.GetComponent<RectTransform>(), 0.5f, 0, 1, 1);

            // CenterPanel
            var center = CreateUIChild(canvas, "CenterPanel");
            var crt = center.GetComponent<RectTransform>();
            SetAnchor(crt, 0.1f, 0.3f, 0.9f, 0.7f);

            var stageText = CreateTMPText(center, "SelectedStageText", "Stage 1", 36);
            var strt = stageText.GetComponent<RectTransform>();
            SetAnchor(strt, 0, 0.6f, 1, 1);

            var prevBtn = CreateButton(center, "PrevButton", "< Prev");
            SetAnchor(prevBtn.GetComponent<RectTransform>(), 0, 0.2f, 0.3f, 0.55f);

            var startBtn = CreateButton(center, "StartButton", "START");
            SetAnchor(startBtn.GetComponent<RectTransform>(), 0.3f, 0.2f, 0.7f, 0.55f);
            startBtn.GetComponent<Image>().color = new Color(0.2f, 0.8f, 0.2f);

            var nextBtn = CreateButton(center, "NextButton", "Next >");
            SetAnchor(nextBtn.GetComponent<RectTransform>(), 0.7f, 0.2f, 1, 0.55f);

            // LobbyUI Controller
            var lobbyUI = CreateGameObject("LobbyUI");
            var controller = lobbyUI.AddComponent<LobbyUIController>();
            SetPrivateField(controller, "currencyText", currencyText.GetComponent<TMP_Text>());
            SetPrivateField(controller, "highestStageText", highestText.GetComponent<TMP_Text>());
            SetPrivateField(controller, "selectedStageText", stageText.GetComponent<TMP_Text>());
            SetPrivateField(controller, "startButton", startBtn.GetComponent<Button>());
            SetPrivateField(controller, "prevButton", prevBtn.GetComponent<Button>());
            SetPrivateField(controller, "nextButton", nextBtn.GetComponent<Button>());

            EditorSceneManager.SaveScene(scene);
            Debug.Log("[AutoSetup] Lobby scene configured");
        }

        [MenuItem("Defense/Setup Stage Scene Only")]
        public static void SetupStageScene()
        {
            var scene = EditorSceneManager.OpenScene("Assets/_Project/Scenes/Stage.unity");

            ClearScene();

            // Camera
            var cam = CreateGameObject("Main Camera");
            cam.transform.position = new Vector3(0, 0, -10);
            var camera = cam.AddComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = 6;
            camera.backgroundColor = new Color(0.15f, 0.15f, 0.2f);
            cam.tag = "MainCamera";

            // EventSystem
            var es = CreateGameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();

            // === Canvas ===
            var canvas = CreateCanvas("Canvas");

            // TopHUD
            var hud = CreateUIChild(canvas, "TopHUD");
            SetAnchor(hud.GetComponent<RectTransform>(), 0, 0.92f, 1, 1);

            var goldText = CreateTMPText(hud, "GoldText", "Gold: 120", 20);
            SetAnchor(goldText.GetComponent<RectTransform>(), 0, 0, 0.33f, 1);

            var waveText = CreateTMPText(hud, "WaveText", "Wave: 1/3", 20);
            SetAnchor(waveText.GetComponent<RectTransform>(), 0.33f, 0, 0.66f, 1);

            var hpText = CreateTMPText(hud, "BaseHpText", "HP: 20", 20);
            SetAnchor(hpText.GetComponent<RectTransform>(), 0.66f, 0, 1, 1);

            // ResultPanel (inactive)
            var resultPanel = CreateUIChild(canvas, "ResultPanel");
            SetAnchor(resultPanel.GetComponent<RectTransform>(), 0.1f, 0.2f, 0.9f, 0.8f);
            resultPanel.AddComponent<Image>().color = new Color(0, 0, 0, 0.8f);

            var resultTitle = CreateTMPText(resultPanel, "ResultTitleText", "STAGE CLEAR!", 32);
            SetAnchor(resultTitle.GetComponent<RectTransform>(), 0, 0.7f, 1, 1);

            var rewardText = CreateTMPText(resultPanel, "RewardText", "Reward: 60", 24);
            SetAnchor(rewardText.GetComponent<RectTransform>(), 0, 0.5f, 1, 0.7f);

            var adBtn = CreateButton(resultPanel, "RewardAdButton", "Watch Ad x2");
            SetAnchor(adBtn.GetComponent<RectTransform>(), 0.1f, 0.3f, 0.9f, 0.48f);
            adBtn.GetComponent<Image>().color = new Color(0.9f, 0.7f, 0.1f);

            var nextStageBtn = CreateButton(resultPanel, "NextButton", "Next Stage");
            SetAnchor(nextStageBtn.GetComponent<RectTransform>(), 0.1f, 0.15f, 0.45f, 0.28f);

            var retryBtn = CreateButton(resultPanel, "RetryButton", "Retry");
            SetAnchor(retryBtn.GetComponent<RectTransform>(), 0.55f, 0.15f, 0.9f, 0.28f);

            var lobbyBtn = CreateButton(resultPanel, "LobbyButton", "Lobby");
            SetAnchor(lobbyBtn.GetComponent<RectTransform>(), 0.3f, 0.02f, 0.7f, 0.13f);

            resultPanel.SetActive(false);

            // TowerSelectionPanel (inactive — shown on slot click)
            var towerSelectPanel = CreateUIChild(canvas, "TowerSelectionPanel");
            SetAnchor(towerSelectPanel.GetComponent<RectTransform>(), 0.05f, 0.02f, 0.95f, 0.15f);
            towerSelectPanel.AddComponent<Image>().color = new Color(0, 0, 0, 0.7f);

            var tBtn1 = CreateButton(towerSelectPanel, "TowerBtn_Basic", "Basic\n50G");
            SetAnchor(tBtn1.GetComponent<RectTransform>(), 0.01f, 0.1f, 0.24f, 0.9f);
            tBtn1.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.8f);

            var tBtn2 = CreateButton(towerSelectPanel, "TowerBtn_Splash", "Splash\n80G");
            SetAnchor(tBtn2.GetComponent<RectTransform>(), 0.26f, 0.1f, 0.49f, 0.9f);
            tBtn2.GetComponent<Image>().color = new Color(1f, 0.5f, 0f);

            var tBtn3 = CreateButton(towerSelectPanel, "TowerBtn_Slow", "Slow\n60G");
            SetAnchor(tBtn3.GetComponent<RectTransform>(), 0.51f, 0.1f, 0.74f, 0.9f);
            tBtn3.GetComponent<Image>().color = Color.cyan;

            var tBtn4 = CreateButton(towerSelectPanel, "TowerBtn_Laser", "Laser\n120G");
            SetAnchor(tBtn4.GetComponent<RectTransform>(), 0.76f, 0.1f, 0.99f, 0.9f);
            tBtn4.GetComponent<Image>().color = new Color(0.7f, 0.2f, 0.9f);

            var towerSelUI = towerSelectPanel.AddComponent<TowerSelectionUI>();

            // Wire tower prefabs to TowerSelectionUI
            var basicPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Towers/Tower_Basic.prefab");
            var splashPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Towers/Tower_Splash.prefab");
            var slowPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Towers/Tower_Slow.prefab");
            var laserPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Towers/Tower_Laser.prefab");

            var towerOptionsField = typeof(TowerSelectionUI).GetField("towerOptions",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (towerOptionsField != null)
            {
                var options = new TowerOption[]
                {
                    new TowerOption { displayName = "Basic", buildCost = 50, prefab = basicPrefab },
                    new TowerOption { displayName = "Splash", buildCost = 80, prefab = splashPrefab },
                    new TowerOption { displayName = "Slow", buildCost = 60, prefab = slowPrefab },
                    new TowerOption { displayName = "Laser", buildCost = 120, prefab = laserPrefab }
                };
                towerOptionsField.SetValue(towerSelUI, options);
            }

            var towerButtonsField = typeof(TowerSelectionUI).GetField("towerButtons",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (towerButtonsField != null)
            {
                towerButtonsField.SetValue(towerSelUI, new Button[]
                {
                    tBtn1.GetComponent<Button>(),
                    tBtn2.GetComponent<Button>(),
                    tBtn3.GetComponent<Button>(),
                    tBtn4.GetComponent<Button>()
                });
            }

            // DO NOT SetActive(false) — TowerSelectionUI.Awake() handles hiding


            // UpgradeSelectionPanel (Awake hides it)
            var upgradePanel = CreateUIChild(canvas, "UpgradeSelectionPanel");
            SetAnchor(upgradePanel.GetComponent<RectTransform>(), 0.05f, 0.25f, 0.95f, 0.75f);
            upgradePanel.AddComponent<Image>().color = new Color(0, 0, 0, 0.85f);

            var upgTitle = CreateTMPText(upgradePanel, "UpgradeTitle", "Choose Upgrade", 28);
            SetAnchor(upgTitle.GetComponent<RectTransform>(), 0, 0.8f, 1, 1);

            var upgBtn1 = CreateButton(upgradePanel, "UpgradeBtn_1", "Upgrade 1");
            SetAnchor(upgBtn1.GetComponent<RectTransform>(), 0.05f, 0.4f, 0.32f, 0.75f);
            upgBtn1.GetComponent<Image>().color = new Color(0.2f, 0.6f, 0.2f);

            var upgBtn2 = CreateButton(upgradePanel, "UpgradeBtn_2", "Upgrade 2");
            SetAnchor(upgBtn2.GetComponent<RectTransform>(), 0.37f, 0.4f, 0.63f, 0.75f);
            upgBtn2.GetComponent<Image>().color = new Color(0.2f, 0.6f, 0.2f);

            var upgBtn3 = CreateButton(upgradePanel, "UpgradeBtn_3", "Upgrade 3");
            SetAnchor(upgBtn3.GetComponent<RectTransform>(), 0.68f, 0.4f, 0.95f, 0.75f);
            upgBtn3.GetComponent<Image>().color = new Color(0.2f, 0.6f, 0.2f);

            var upgradeSelUI = upgradePanel.AddComponent<UpgradeSelectionUI>();
            // DO NOT SetActive(false) — UpgradeSelectionUI.Awake() handles hiding

            // TutorialPanel (inactive)
            var tutPanel = CreateUIChild(canvas, "TutorialPanel");
            SetAnchor(tutPanel.GetComponent<RectTransform>(), 0.05f, 0.3f, 0.95f, 0.7f);
            tutPanel.AddComponent<Image>().color = new Color(0, 0, 0, 0.85f);

            var tutText = CreateTMPText(tutPanel, "TutorialText", "Welcome!", 22);
            SetAnchor(tutText.GetComponent<RectTransform>(), 0.05f, 0.3f, 0.95f, 0.9f);

            var tutNextBtn = CreateButton(tutPanel, "NextStepButton", "Next");
            SetAnchor(tutNextBtn.GetComponent<RectTransform>(), 0.3f, 0.05f, 0.7f, 0.25f);

            tutPanel.SetActive(false);

            // PausePanel (inactive)
            var pausePanel = CreateUIChild(canvas, "PausePanel");
            SetAnchor(pausePanel.GetComponent<RectTransform>(), 0.2f, 0.3f, 0.8f, 0.7f);
            pausePanel.AddComponent<Image>().color = new Color(0, 0, 0, 0.9f);

            var resumeBtn = CreateButton(pausePanel, "ResumeButton", "Resume");
            SetAnchor(resumeBtn.GetComponent<RectTransform>(), 0.1f, 0.55f, 0.9f, 0.85f);

            var quitBtn = CreateButton(pausePanel, "QuitButton", "Quit");
            SetAnchor(quitBtn.GetComponent<RectTransform>(), 0.1f, 0.15f, 0.9f, 0.45f);

            pausePanel.SetActive(false);

            // Speed + Pause buttons
            var speedBtn = CreateButton(canvas, "SpeedButton", "x1");
            SetAnchor(speedBtn.GetComponent<RectTransform>(), 0.85f, 0.92f, 0.95f, 1);

            var pauseBtn = CreateButton(canvas, "PauseButton", "||");
            SetAnchor(pauseBtn.GetComponent<RectTransform>(), 0.95f, 0.92f, 1, 1);

            // === Stage Systems ===
            var systems = CreateGameObject("StageSystems");

            var stageManager = systems.AddComponent<StageManager>();
            var currencyMgr = systems.AddComponent<CurrencyManager>();
            var healthBase = systems.AddComponent<HealthBase>();
            var waveMgr = systems.AddComponent<WaveManager>();
            var spawner = systems.AddComponent<EnemySpawner>();
            var waveUpgrade = systems.AddComponent<WaveUpgradeManager>();
            var resultFlow = systems.AddComponent<ResultFlowController>();
            var stageUI = systems.AddComponent<StageUIController>();
            var resultUI = systems.AddComponent<ResultUIController>();
            var tutorial = systems.AddComponent<TutorialManager>();
            var gameSpeed = systems.AddComponent<GameSpeedController>();
            var pauseMenu = systems.AddComponent<PauseMenuUI>();
            var analytics = systems.AddComponent<AnalyticsIntegrator>();

            // StageDataLoader with all 10 stages
            var dataLoader = systems.AddComponent<StageDataLoader>();
            var allStages = new StageData[10];
            for (int i = 0; i < 10; i++)
            {
                allStages[i] = AssetDatabase.LoadAssetAtPath<StageData>($"Assets/_Project/ScriptableObjects/Balance/StageData_{i + 1:00}.asset");
            }
            SetPrivateField(dataLoader, "allStages", allStages);

            // Wire StageManager
            SetPrivateField(stageManager, "currencyManager", currencyMgr);
            SetPrivateField(stageManager, "healthBase", healthBase);

            // Wire StageUIController
            SetPrivateField(stageUI, "goldText", goldText.GetComponent<TMP_Text>());
            SetPrivateField(stageUI, "waveText", waveText.GetComponent<TMP_Text>());
            SetPrivateField(stageUI, "baseHpText", hpText.GetComponent<TMP_Text>());

            // Wire ResultUIController
            SetPrivateField(resultUI, "resultPanel", resultPanel);
            SetPrivateField(resultUI, "resultTitleText", resultTitle.GetComponent<TMP_Text>());
            SetPrivateField(resultUI, "rewardText", rewardText.GetComponent<TMP_Text>());
            SetPrivateField(resultUI, "rewardAdButton", adBtn.GetComponent<Button>());
            SetPrivateField(resultUI, "nextButton", nextStageBtn.GetComponent<Button>());
            SetPrivateField(resultUI, "retryButton", retryBtn.GetComponent<Button>());
            SetPrivateField(resultUI, "lobbyButton", lobbyBtn.GetComponent<Button>());

            // Wire ResultFlowController
            SetPrivateField(resultFlow, "resultUI", resultUI);

            // Wire TutorialManager
            SetPrivateField(tutorial, "tutorialPanel", tutPanel);
            SetPrivateField(tutorial, "tutorialText", tutText.GetComponent<TMP_Text>());
            SetPrivateField(tutorial, "nextButton", tutNextBtn.GetComponent<Button>());

            // Wire PauseMenuUI
            SetPrivateField(pauseMenu, "pausePanel", pausePanel);
            SetPrivateField(pauseMenu, "pauseButton", pauseBtn.GetComponent<Button>());
            SetPrivateField(pauseMenu, "resumeButton", resumeBtn.GetComponent<Button>());
            SetPrivateField(pauseMenu, "quitButton", quitBtn.GetComponent<Button>());

            // Wire GameSpeedController
            SetPrivateField(gameSpeed, "speedButton", speedBtn.GetComponent<Button>());
            SetPrivateField(gameSpeed, "speedText", speedBtn.GetComponentInChildren<TMP_Text>());

            // === Route ===
            var routeRoot = CreateGameObject("RouteRoot");
            var wp1 = CreateChild(routeRoot, "Waypoint_01");
            wp1.transform.position = new Vector3(-4, 3, 0);
            var wp2 = CreateChild(routeRoot, "Waypoint_02");
            wp2.transform.position = new Vector3(-4, 0, 0);
            var wp3 = CreateChild(routeRoot, "Waypoint_03");
            wp3.transform.position = new Vector3(4, 0, 0);
            var wp4 = CreateChild(routeRoot, "GoalPoint");
            wp4.transform.position = new Vector3(4, -4, 0);

            // SpawnPoint
            var spawnPoint = CreateGameObject("SpawnPoint");
            spawnPoint.transform.position = new Vector3(-4, 4, 0);

            // BasePoint
            var basePoint = CreateGameObject("BasePoint");
            basePoint.transform.position = new Vector3(4, -4, 0);

            // TowerSlots
            var slotsRoot = CreateGameObject("TowerSlotsRoot");
            for (int i = 0; i < 5; i++)
            {
                var slot = CreateChild(slotsRoot, $"TowerSlot_{i + 1:00}");
                slot.transform.position = new Vector3(-2 + i * 2, 1.5f, 0);
                slot.AddComponent<BoxCollider2D>().size = new Vector2(1, 1);
                var sr = slot.AddComponent<SpriteRenderer>();
                sr.color = new Color(0.3f, 0.8f, 0.3f, 0.5f);

                var towerSlot = slot.AddComponent<TowerSlot>();

                // Load tower prefab
                var towerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Towers/Tower_Basic.prefab");
                if (towerPrefab != null)
                {
                    SetPrivateField(towerSlot, "towerPrefab", towerPrefab);
                }
                SetPrivateField(towerSlot, "slotIndicator", sr);
            }

            // RuntimeRoot
            var runtimeRoot = CreateGameObject("RuntimeRoot");
            var enemiesRoot = CreateChild(runtimeRoot, "Enemies");
            CreateChild(runtimeRoot, "Projectiles");
            CreateChild(runtimeRoot, "Towers");

            // Wire EnemySpawner
            SetPrivateField(spawner, "spawnPoint", spawnPoint.transform);
            SetPrivateField(spawner, "enemyParent", enemiesRoot.transform);
            var enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Project/Prefabs/Enemies/Enemy_Basic.prefab");
            if (enemyPrefab != null)
                SetPrivateField(spawner, "enemyPrefab", enemyPrefab);

            // Wire button onClick events
            WireButton(adBtn, resultFlow, "OnClickRewardAd");
            WireButton(nextStageBtn, resultFlow, "OnClickNextStage");
            WireButton(retryBtn, resultFlow, "OnClickRetry");
            WireButton(lobbyBtn, resultFlow, "OnClickLobby");

            EditorSceneManager.SaveScene(scene);
            Debug.Log("[AutoSetup] Stage scene configured");
        }

        // === Helper Methods ===

        private static void SetupBuildSettings()
        {
            var scenes = new EditorBuildSettingsScene[]
            {
                new EditorBuildSettingsScene("Assets/_Project/Scenes/Boot.unity", true),
                new EditorBuildSettingsScene("Assets/_Project/Scenes/Lobby.unity", true),
                new EditorBuildSettingsScene("Assets/_Project/Scenes/Stage.unity", true)
            };
            EditorBuildSettings.scenes = scenes;
        }

        private static void ClearScene()
        {
            var allObjects = Object.FindObjectsByType<GameObject>();
            foreach (var obj in allObjects)
            {
                if (obj != null)
                    Object.DestroyImmediate(obj);
            }
        }

        private static GameObject CreateGameObject(string name)
        {
            var go = new GameObject(name);
            return go;
        }

        private static GameObject CreateChild(GameObject parent, string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent.transform);
            return go;
        }

        private static GameObject CreateCanvas(string name)
        {
            var go = new GameObject(name);
            var canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            go.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            go.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1080, 1920);
            go.AddComponent<GraphicRaycaster>();
            return go;
        }

        private static GameObject CreateUIChild(GameObject parent, string name)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent.transform, false);
            return go;
        }

        private static GameObject CreateTMPText(GameObject parent, string name, string text, int fontSize)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent.transform, false);
            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = fontSize;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.white;
            return go;
        }

        private static GameObject CreateButton(GameObject parent, string name, string label)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent.transform, false);

            var img = go.AddComponent<Image>();
            img.color = new Color(0.3f, 0.3f, 0.3f);
            go.AddComponent<Button>();

            var textGo = new GameObject("Text", typeof(RectTransform));
            textGo.transform.SetParent(go.transform, false);
            var tmp = textGo.AddComponent<TextMeshProUGUI>();
            tmp.text = label;
            tmp.fontSize = 20;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.white;
            var trt = textGo.GetComponent<RectTransform>();
            trt.anchorMin = Vector2.zero;
            trt.anchorMax = Vector2.one;
            trt.sizeDelta = Vector2.zero;

            return go;
        }

        private static void SetAnchor(RectTransform rt, float minX, float minY, float maxX, float maxY)
        {
            rt.anchorMin = new Vector2(minX, minY);
            rt.anchorMax = new Vector2(maxX, maxY);
            rt.sizeDelta = Vector2.zero;
            rt.anchoredPosition = Vector2.zero;
        }

        private static void SetPrivateField(object target, string fieldName, object value)
        {
            var type = target.GetType();
            while (type != null)
            {
                var field = type.GetField(fieldName,
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);
                if (field != null)
                {
                    field.SetValue(target, value);
                    return;
                }
                type = type.BaseType;
            }
            Debug.LogWarning($"[AutoSetup] Field '{fieldName}' not found on {target.GetType().Name}");
        }

        private static void WireButton(GameObject buttonGo, object target, string methodName)
        {
            var button = buttonGo.GetComponent<Button>();
            if (button == null) return;

            var method = target.GetType().GetMethod(methodName,
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (method == null) return;

            var action = System.Delegate.CreateDelegate(typeof(UnityEngine.Events.UnityAction), target, method);
            UnityEditor.Events.UnityEventTools.AddVoidPersistentListener(
                button.onClick,
                (UnityEngine.Events.UnityAction)action);
        }
    }
}
#endif
