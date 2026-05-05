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

    public Fish (FishTypeScriptableObject fishType)
    {
        this.fishType = fishType;
        this.fishLevel = 1;

        this.weight = Random.Range(fishType.minimumWeight, fishType.maximumWeight);
        this.fishStamina = fishType.stamina; 
        this.recovery = fishType.recovery;

        this.value = Random.Range(fishType.sellPriceLow, fishType.sellPriceHigh) * this.weight;
        this.injury = 0;
    }

    public void OnCaught()
    {
        
    }

    public void OnHooked()
    {
        
    }
}