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
        UIManager.instance.EnableFishingReport(false);
        player.ChangeState(new PlayerFreeroamState(player));

        cameraManager.EnableOverheadCam();
    }
    private void QuestScene()
    { 
    
    }
    private void TownScene() 
    { 
    
    }
    private void ShopScene() 
    {
        player.ChangeState(new PlayerMenuState(player));
    }
    private void DayOver() 
    { 

    }

    // STATE CHANGES //

    public void EndFishingDay()
    {
        ChangeState(GameState.QuestScene);
    }
}
