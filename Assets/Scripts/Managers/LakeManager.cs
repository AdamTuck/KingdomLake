using UnityEngine;

public class LakeManager : MonoBehaviour
{
    [Header("Lake Properties")]
    [SerializeField] private int numberOfFishInLake;

    [Header("Lake Stocking")]
    [SerializeField] private FishScriptableObject[] lake1Fish;

    [Header("Object Refs")]
    [SerializeField] private GameObject fishMovementArea;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetUpLakeFish ()
    {
        for (int i=0; i<numberOfFishInLake; i++)
        {

            //Vector3 randomFishPos;
        }
    }
}