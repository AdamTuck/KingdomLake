using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera overheadCamera;
    [SerializeField] private Camera fishingCamera;
    [SerializeField] private Camera battleCamera;

    public static CameraManager instance;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    public void EnableBattleCam()
    {
        DisableAllCams();
        battleCamera.gameObject.SetActive(true);
    }

    public void EnableOverheadCam()
    {
        DisableAllCams();
        overheadCamera.gameObject.SetActive(true);
    }

    public void EnableFishingCam()
    {
        fishingCamera.gameObject.SetActive(true);
    }

    private void DisableAllCams()
    {
        overheadCamera.gameObject.SetActive(false);
        battleCamera.gameObject.SetActive(false);
        fishingCamera.gameObject.SetActive(false);
    }
}
