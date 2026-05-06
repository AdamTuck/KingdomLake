using UnityEngine;
using DistantLands.Cozy;
using DistantLands.Cozy.Data;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int dayStartHour;
    [SerializeField] private int dayStartMinutes;
    [SerializeField] private int clockMinuteLenthInSeconds;

    [Header("References")]
    [SerializeField] PlayerController player;
    [SerializeField] PerennialProfile timeProfile;
    CameraManager cameraManager;
    UIManager uiManager;

    private bool firstDay = true;

    // Singleton
    public static GameManager instance;

    private GameState currentState;

    public enum GameState { DayStart, StartFishingFreeroam, QuestScene, TownScene, ShopScene, DayOver }

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        cameraManager = CameraManager.instance;
        uiManager = UIManager.instance;

        ChangeState(GameState.DayStart);
    }

    public void ChangeState (GameState state)
    {
        currentState = state;

        switch (currentState)
        {
            case GameState.DayStart:
                StartDay();
                break;
            case GameState.StartFishingFreeroam:
                StartFishing(); 
                break;
            case GameState.QuestScene:
                QuestScene(); 
                break;
            case GameState.TownScene:
                TownScene();
                break;
            case GameState.ShopScene:
                ShopScene();
                break;
            case GameState.DayOver:
                DayOver();
                break;
        }
    }

    // GAME STATES //

    private void StartDay()
    {
        if (firstDay)
        {
            //SaveManager.LoadGame();
            LakeManager.instance.InitializePool();
        }
        else
        {
            //SaveManager.SaveGame();
        }

        uiManager.ShowClock(false);
        cameraManager.EnableOverheadCam();
        CozyWeather.instance.timeModule.SetHour(dayStartHour);
        CozyWeather.instance.timeModule.SetMinute(dayStartMinutes);
        timeProfile.timeMovementSpeed = 0;

        uiManager.EnableFishingReport(false);

        uiManager.StartDaySplashScreen();
    }

    private void StartFishing()
    {
        Debug.Log("Starting Day...");

        player.ChangeState(new PlayerFreeroamState(player, player.freeRoamSpawnFish.transform.position));
        uiManager.ShowClock(true);
        player.ShowRod(true);
        timeProfile.timeMovementSpeed = clockMinuteLenthInSeconds;
    }
    private void QuestScene()
    {
        uiManager.ShowClock(false);

        cameraManager.EnableBattleCam();
        player.ChangeState(new PlayerMenuState(player));
        uiManager.EnableQuest(true);
    }
    private void TownScene() 
    {
        uiManager.ShowClock(true);
        timeProfile.timeMovementSpeed = 0;

        cameraManager.EnableOverheadCam();
        player.ChangeState(new PlayerFreeroamState(player, player.freeRoamSpawnTown.transform.position));
        player.ShowRod(false);
    }
    private void ShopScene() 
    {
        uiManager.EnableShop(true);
        uiManager.ShowClock(false);

        cameraManager.EnableBattleCam();
        player.ChangeState(new PlayerMenuState(player));
    }
    private void DayOver() 
    {
        if (firstDay)
            firstDay = false;

        timeProfile.timeMovementSpeed = 75;

        UIManager.instance.EnableFishingReport(true);
        cameraManager.EnableBattleCam();
    }

    // STATE CHANGES //

    public void EndFishingDay()
    {
        if (currentState != GameState.QuestScene)
            ChangeState(GameState.QuestScene);
    }

    public void EndTownDay()
    {
        if (currentState != GameState.ShopScene)
            ChangeState(GameState.ShopScene);
    }

    // Weather Event Controls //

    public void MorningFreezeInGameClock ()
    {
        if (currentState == GameState.DayOver)
            timeProfile.timeMovementSpeed = 0;
    }
}