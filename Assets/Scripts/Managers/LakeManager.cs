using System.Collections.Generic;
using UnityEngine;

public class LakeManager : MonoBehaviour
{
    [Header("Lakes")]
    [SerializeField] private Lake[] lakes;

    [Header("Properties")]
    [SerializeField] private int poolSizes;

    [Header("Object Refs")]
    [SerializeField] private GameObject fishMovementArea;
    [SerializeField] private GameObject troutPoolReserves;
    [SerializeField] private FishTypeScriptableObject fishType;
    [SerializeField] private List<Fish> troutPool = new List<Fish>();
    [SerializeField] private List<Fish> usedTroutPool = new List<Fish>();

    private Fish tempPoolObject;

    public static LakeManager instance;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }


    // Fish Pooling (lmao get it)
    // (cause pools have water and fish also live in water so there you have it gamers)

    public void InitializePool ()
    {
        for (int i = 0; i < poolSizes; i++)
        {
            AddNewObject();
        }
    }

    private void AddNewObject()
    {
        tempPoolObject = Instantiate(fishType.fishTypePrefab, troutPoolReserves.transform).GetComponent<Fish>();
        tempPoolObject.gameObject.SetActive(false);
        tempPoolObject.SetObjectPool(this);
        troutPool.Add(tempPoolObject);
        tempPoolObject.ResetFishStats(fishType);
    }

    public Fish GetPooledObject()
    {
        Fish objToGet;

        if (troutPool.Count > 0)
        {
            objToGet = troutPool[0];
            usedTroutPool.Add(objToGet);
            troutPool.RemoveAt(0);
        }
        else
        {
            // Adds new object with default values to the pool
            AddNewObject();
            objToGet = GetPooledObject();
        }

        objToGet.gameObject.SetActive(true);
        return objToGet;
    }

    public void DestroyPooledObject(Fish objToDestroy)
    {
        LakeRegion regionContainingFish = objToDestroy.GetCurrentRegion();

        regionContainingFish.GetRegionFishList().Remove(objToDestroy);

        objToDestroy.Destroy();
    }

    public void RestoreObject(Fish objToRestore)
    {
        objToRestore.gameObject.SetActive(false);
        objToRestore.gameObject.transform.SetParent(troutPoolReserves.transform);
        usedTroutPool.Remove(objToRestore);
        troutPool.Add(objToRestore);
    }
}