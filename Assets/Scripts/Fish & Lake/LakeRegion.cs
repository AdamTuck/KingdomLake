using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LakeRegionObject", menuName = "Scriptable Objects/LakeRegionObject")]
public class LakeRegion : ScriptableObject
{
    public string lakeRegionName;
    public GameObject lakeRegionSwimZone;

    private FishNation controllingFaction;

    private List<Fish> regionFish = new List<Fish>();

    public FishNation GetControllingFaction () { return controllingFaction; }
    public List<Fish> GetRegionFishList () { return regionFish; }
}
