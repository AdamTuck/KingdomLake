using UnityEngine;

public class Fish : MonoBehaviour, iCatchable
{
    [SerializeField] private int fishLevel;
    [SerializeField] private float fishStamina;
    [SerializeField] private float recovery;
    [SerializeField] private float weight;
    [SerializeField] private float value;
    [SerializeField] private int injury;

    private FishTypeScriptableObject fishType;

    public void OnCaught()
    {
        
    }

    public void OnHooked()
    {
        
    }
}