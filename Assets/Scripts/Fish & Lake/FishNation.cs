using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FishNation", menuName = "Scriptable Objects/FishNation")]
public class FishNation : ScriptableObject
{
    [Header("Nation Properties")]
    public string nationName;
    public Texture2D nationBanner;

    [Header("Nation Fish")]
    public int startingPopulation;
    public int targetStartingRegions;
    public LakeRegion homeRegion;

    private Fish[] nationFish;

    public Fish[] currentNationFishList ()
    {
        return nationFish;
    }
}