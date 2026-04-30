using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DistantLands.Cozy;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private bool gamePaused;
    [SerializeField] private bool clockVisible;

    [Header("UIScreens")]
    [SerializeField] private GameObject splashScreenIntroObj;
    [SerializeField] private GameObject pauseScreenObj;
    [SerializeField] private GameObject FishReportObj;
    [SerializeField] private GameObject ShopScreenObj;
    [SerializeField] private GameObject QuestScreenObj;
    [SerializeField] private GameObject fishingViewFrameObj;
    [SerializeField] private DialogueAnimator dialogueAnimator;

    [Header("Object Refs")]
    [SerializeField] private CanvasGroup splashScreenBGObj;
    [SerializeField] private CanvasGroup daySpashScreenTxt;
    [SerializeField] private GameObject uiClockObject;
    [SerializeField] private DialogueSceneScriptableObject demoDialogueScene;
    

    // Singleton
    public static UIManager instance;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    private void Update()
    {
        UpdateUIClock();
    }

    private void UpdateUIClock()
    {
        if (clockVisible)
        {
            uiClockObject.SetActive(true);

            int hours, minutes;
            string hoursText, minutesText, amPmText;

            hours = CozyWeather.instance.timeModule.currentTime.hours;
            minutes = CozyWeather.instance.timeModule.currentTime.minutes;

            // Converts to 12hr time and switches AM/PM
            if (hours > 12)
            {
                hours -= 12;
                hoursText = hours.ToString();
                amPmText = "PM";
            }
            else if (hours == 0)
            {
                hours = 12;
                hoursText = hours.ToString();
                amPmText = "AM";
            }
            else if (hours == 12)
            {
                hoursText = hours.ToString();
                amPmText = "PM";
            }
            else
            {
                hoursText = hours.ToString();
                amPmText = "AM";
            }

            // Adds leading minutes zero
            if (minutes < 10)
                minutesText = "0" + minutes.ToString();
            else
                minutesText = minutes.ToString();

            uiClockObject.GetComponentInChildren<TextMeshProUGUI>().text = hoursText + ":" + minutesText + " " + amPmText;
        }
        else
            uiClockObject.SetActive(false);
    }

    public void PauseGameToggle()
    {
        if (gamePaused)
        {
            SetPauseOff(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseScreenObj.SetActive(true);
            gamePaused = true;

            PlayerInput.GetInstance().InputsLocked();

            if (PlayerController.instance.CurrentStateString() == "PlayerFreeroamState")
            {
                UnlockCursor();
            }
        }
    }

    public void SetPauseOff(bool leavingScene)
    {
        if (gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
            pauseScreenObj.SetActive(false);

            PlayerInput.GetInstance().InputsUnlocked();

            if (PlayerController.instance.CurrentStateString() == "PlayerFreeroamState" && !leavingScene)
                LockCursor();
            else
                UnlockCursor();
        }
    }

    public void EnableFishingReport (bool toEnable)
    {
        FishReportObj.SetActive(toEnable);
        StopTime(false);
    }

    public void EnableShop (bool toEnable)
    {
        ShopScreenObj.SetActive(toEnable);
        StopTime (toEnable);
    }

    public void EnableQuest (bool toEnable)
    {
        QuestScreenObj.SetActive(toEnable);
        StopTime (toEnable);

        if (toEnable)
            dialogueAnimator.StartDialogue(demoDialogueScene);
        else
            dialogueAnimator.CancelDialogue();
    }

    public void EnableFishingUI (bool isFishing)
    {
        fishingViewFrameObj.SetActive(isFishing);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StopTime(bool timeStopped)
    {
        if (timeStopped)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void ShowClock (bool showClock)
    {
        clockVisible = showClock;
    }

    // UI BUTTONS //

    public void DismissFishReportUI ()
    {
        FishReportObj.SetActive(false);
        GameManager.instance.ChangeState(GameManager.GameState.DayStart);
        StopTime(false);
    }

    public void DismissQuestUI ()
    {
        QuestScreenObj.SetActive(false);
        dialogueAnimator.CancelDialogue();
        GameManager.instance.ChangeState(GameManager.GameState.TownScene);
        StopTime(false);
    }

    public void DismissShopUI() 
    {
        ShopScreenObj.SetActive(false);
        GameManager.instance.ChangeState(GameManager.GameState.DayOver);
    }

    // Day Intro //

    public void StartDaySplashScreen()
    {
        StartCoroutine(AnimateIntro());
    }

    IEnumerator AnimateIntro()
    {
        splashScreenIntroObj.SetActive(true);

        LeanTween.alphaCanvas(splashScreenBGObj, 1f, 0);
        LeanTween.alphaCanvas(daySpashScreenTxt, 1f, 2);
        yield return new WaitForSeconds(3);

        GameManager.instance.ChangeState(GameManager.GameState.StartFishingFreeroam);

        LeanTween.alphaCanvas(splashScreenBGObj, 0f, 3);
        LeanTween.alphaCanvas(daySpashScreenTxt, 0f, 3);
        yield return new WaitForSeconds(3);

        splashScreenIntroObj.SetActive(false);
    }
}
