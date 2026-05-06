using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LakeObject", menuName = "Scriptable Objects/LakeObject")]
public class Lake : ScriptableObject
{
    public string lakeName;
    public LakeRegion[] regions;
    public FishNation[] fishNationsInLake;


    private void SetUpLake()
    {

    }

    private void DeployNationFish()
    {
        for (int i = 0; i<fishNationsInLake.Length; i++)
        {
            List<LakeRegion> currentNationRegions = new List<LakeRegion>();

            for (int j = 0; j < regions.Length; j++)
            {
                if (regions[j].GetControllingFaction() == fishNationsInLake[i])
                {
                    currentNationRegions.Add(regions[j]);
                }
            }

            for (int j = 0; j < fishNationsInLake[i].currentNationFishList().Length; j++)
            {
                //currentNationRegions[j].
            }
        }
    }
}
