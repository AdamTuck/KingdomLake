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

    [Header("Swim Properties")]
    public float minDepth;
    public float maxDepth;
    public float swimmingActivityLevel = 50f;
    public float swimSpeed = 0.001f;
    public float rotationSpeed = 4.0f;

    [Header("Capitalism")]
    public float sellPriceLow;
    public float sellPriceHigh;

    [Header("Quest Fish Properties")]
    public bool isQuestFish;
    public DialogueSceneScriptableObject[] questFishScenes;
}