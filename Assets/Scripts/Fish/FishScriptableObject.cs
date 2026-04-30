using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FishObject", menuName = "Scriptable Objects/FishObject")]
public class FishScriptableObject : ScriptableObject
{
    [Header("Fish Properties")]
    public string fishName;
    public GameObject fishPrefab;
    public FishType fishType;

    [Header("Location")]
    public Lakes lakes;
    public float minDepth;
    public float maxDepth;

    [Header("Capitalism")]
    public float sellPriceLow;
    public float sellPriceHigh;
}

[Flags]
public enum Lakes
{
    Lake1,
    Lake2,
    Lake3,
    Lake4,
    Lake5
}

public enum FishType
{
    Common,
    Special
}