using UnityEngine;

public class Fish : MonoBehaviour, iCatchable
{
    [SerializeField] private int fishLevel;
    [SerializeField] private float fishStamina;
    [SerializeField] private float recovery;
    [SerializeField] private float weight;
    [SerializeField] private float value;
    [SerializeField] private int injury;

    [Header("General Swimming Vars")]
    [SerializeField] private float minDistanceToStopSwim;
    [SerializeField] private float rotationSpeed;
    private Vector3 swimDestination = Vector3.zero;
    private bool isSwimming = false;
    private bool isTurning = true;

    private FishTypeScriptableObject fishType;

    private LakeManager fishPool;
    private LakeRegion currentRegion;
    private FishNation fishNationality;

    public void SetObjectPool(LakeManager pool)
    {
        fishPool = pool;
    }

    public void Destroy()
    {
        if (fishPool != null)
            fishPool.RestoreObject(this);

        ResetFishStats(fishType);
    }

    public Fish (FishTypeScriptableObject fishType)
    {
        this.fishType = fishType;
        this.fishLevel = 1;

        this.weight = Random.Range(fishType.minimumWeight, fishType.maximumWeight);
        this.fishStamina = fishType.stamina; 
        this.recovery = fishType.recovery;

        this.value = Random.Range(fishType.sellPriceLow, fishType.sellPriceHigh) * this.weight;
        this.injury = 0;
    }

    private void Update()
    {
        if (isSwimming)
        {
            CheckForSwim();
        } 
        else { 
            if (Random.Range(0, 10000) < fishType.swimmingActivityLevel)
            {
                Bounds regionBounds = currentRegion.GetComponent<BoxCollider>().bounds;
                swimDestination = new Vector3(Random.Range(regionBounds.min.x, regionBounds.max.x), Random.Range(regionBounds.min.y, regionBounds.max.y), Random.Range(regionBounds.min.z, regionBounds.max.z));

                isSwimming = true;
            }
        }
    }

    private void CheckForSwim ()
    {
        if (Vector3.Distance(transform.position, swimDestination) <= minDistanceToStopSwim)
        {
            isSwimming = false;
        }
        else
        { 
            transform.Translate(0, 0, Time.deltaTime * fishType.swimSpeed);
            if (isTurning)
            {
                Vector3 turnDirection = swimDestination - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(turnDirection), rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void ResetFishStats (FishTypeScriptableObject _fishType)
    {
        fishType = _fishType;
        fishLevel = 1;

        weight = Random.Range(fishType.minimumWeight, fishType.maximumWeight);
        fishStamina = fishType.stamina;
        recovery = fishType.recovery;

        value = Random.Range(fishType.sellPriceLow, fishType.sellPriceHigh) * this.weight;
        injury = 0;
    }

    public void OnCaught()
    {
        
    }

    public void OnHooked()
    {
        
    }

    public LakeRegion GetCurrentRegion()
    {
        return currentRegion;
    }

    public void SetCurrentRegion (LakeRegion region)
    {
        currentRegion = region;
    }

    public void SetNationality(FishNation nationality)
    {
        fishNationality = nationality; 
    }

    public FishNation GetNationality()
    {
        return fishNationality;
    }
}