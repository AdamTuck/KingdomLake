using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private bool gamePaused;

    [Header("UIMessages")]
    [SerializeField] private GameObject pauseScreenObj;

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
}
