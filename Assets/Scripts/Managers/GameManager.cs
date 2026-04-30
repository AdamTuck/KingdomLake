using UnityEngine;
using DistantLands.Cozy;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int dayStartHour;
    [SerializeField] private int dayStartMinutes;

    [Header("References")]
    [SerializeField] PlayerController player;
    CameraManager cameraManager;
    UIManager uiManager;

    // Singleton
    public static GameManager instance;

    private GameState currentState;

    public enum GameState { DayStartReport, StartFishing, QuestScene, TownScene, ShopScene, DayOver }

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
        ChangeState(GameState.DayStartReport);
    }

    public void ChangeState (GameState state)
    {
        currentState = state;

        switch (currentState)
        {
            case GameState.DayStartReport:
                DayStartReport();
                break;
            case GameState.StartFishing:
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

    private void DayStartReport ()
    {
        Debug.Log("Starting Day...");
        UIManager.instance.EnableFishingReport(true);
        CozyWeather.instance.timeModule.SetHour(dayStartHour);
        CozyWeather.instance.timeModule.SetMinute(dayStartMinutes);

        cameraManager.EnableBattleCam();
    }

    private void StartFishing()
    {
        Debug.Log("Fishing beginning...");
        uiManager.EnableFishingReport(false);
        player.ChangeState(new PlayerFreeroamState(player, player.freeRoamSpawnFish.transform.position));

        cameraManager.EnableOverheadCam();
    }
    private void QuestScene()
    {
        cameraManager.EnableBattleCam();
        player.ChangeState(new PlayerMenuState(player));
        uiManager.EnableQuest(true);
    }
    private void TownScene() 
    {
        cameraManager.EnableOverheadCam();
        player.ChangeState(new PlayerFreeroamState(player, player.freeRoamSpawnTown.transform.position));
    }
    private void ShopScene() 
    {
        uiManager.EnableShop(true);
        cameraManager.EnableBattleCam();
        player.ChangeState(new PlayerMenuState(player));
    }
    private void DayOver() 
    { 

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
}
