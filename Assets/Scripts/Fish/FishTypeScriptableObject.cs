using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FishObject", menuName = "Scriptable Objects/FishObject")]
public class FishTypeScriptableObject : ScriptableObject
{
    [Header("Fish Properties")]
    public string fishTypeName;
    public GameObject fishTypePrefab;
    public float minimumWeight;
    public float maximumWeight;
    public float stamina;
    public float recovery;

    [Header("Location")]
    public float minDepth;
    public float maxDepth;

    [Header("Capitalism")]
    public float sellPriceLow;
    public float sellPriceHigh;

    [Header("Quest Fish Properties")]
    public bool isQuestFish;
    public DialogueSceneScriptableObject[] questFishScenes;
}