using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Lake : MonoBehaviour
{
    public string lakeName;
    public LakeRegion[] regions;
    public LakeRegion[] startingRegions;
    public FishNation[] fishNationsInLake;


    private void SetUpLake()
    {
        List<LakeRegion> startingControlledRegions = new List<LakeRegion>();
        startingControlledRegions.AddRange(startingRegions);

        foreach (FishNation nation in fishNationsInLake)
        {
            int homeRegionIndex = Random.Range(0, startingControlledRegions.Count);
            nation.homeRegion = startingControlledRegions[homeRegionIndex];
            startingControlledRegions.RemoveAt(homeRegionIndex);

            for (int i = 1; i < nation.targetStartingRegions - 1; i++)
            {
                
            }
        }
    }

    private void SpawnFish(int numFishToCreate, LakeRegion region, FishNation nation)
    {
        for (int i = 0; i < numFishToCreate; i++)
        {
            Fish newFish = LakeManager.instance.GetPooledObject();

            newFish.SetCurrentRegion(region);
            newFish.SetNationality(nation);

            newFish.transform.position = region.transform.position;
        }
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

    public LakeRegion GetClosestRegion (Vector3 targetPos)
    {
        LakeRegion bestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (LakeRegion region in regions)
        {
            Vector3 differenceToTarget = region.gameObject.transform.position - targetPos;
            float currentDistance = differenceToTarget.sqrMagnitude;

            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                bestTarget = region;
            }
        }

        return bestTarget;
    }

    public int GetNumControlledRegions(FishNation nation)
    {
        int controlledRegions = 0;

        foreach (LakeRegion region in regions)
        {
            if (region.GetControllingFaction() == nation)
                controlledRegions++;
        }

        return controlledRegions;
    }
}
