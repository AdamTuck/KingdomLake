using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }
}
