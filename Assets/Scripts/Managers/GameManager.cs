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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
