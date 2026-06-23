using System.Collections.Generic;
using UnityEngine;

public class LakeRegion : MonoBehaviour
{
    [Header("Starting Properties")]

    [SerializeField] private string lakeRegionName;
    [SerializeField] private FishNation isFactionHomeRegion;
    [SerializeField] private int maxArmySize;
    [SerializeField] private LakeRegion[] neighbouringRegions;

    [Header("Current Properties")]
    [SerializeField] private Lake currentLake;
    [SerializeField] private FishNation controllingFaction;
    [SerializeField] private List<Fish> regionFish = new List<Fish>();
    [SerializeField] private List<LakeRegion> closestRegions = new List<LakeRegion>();

    public FishNation GetControllingFaction () { return controllingFaction; }
    public List<Fish> GetRegionFishList () { return regionFish; }

    public void AddRegionFish (List<Fish> fishList)
    {
        regionFish.AddRange (fishList);
    }

    public void RemoveRegionFish()
    {
        regionFish = null;
    }

    public void RemoveRegionFish(Fish fish)
    {
        regionFish.Remove(fish);
    }

    public LakeRegion[] GetNeighbouringRegions () 
    {     
        return neighbouringRegions; 
    }

    public LakeRegion GetRandomNeighbourRegion (bool isEmpty)
    {
        LakeRegion chosenRegion = null;

        List<LakeRegion> regionCandidates = new List<LakeRegion>();
        regionCandidates.AddRange(neighbouringRegions);

        int neighbourIndex = Random.Range(0, neighbouringRegions.Length);
        chosenRegion = closestRegions[neighbourIndex];

        return chosenRegion;
    }
}