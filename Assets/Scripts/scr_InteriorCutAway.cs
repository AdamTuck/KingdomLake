using UnityEngine;

public class scr_InteriorCutAway : MonoBehaviour
{
    public GameObject buildingRef;
    public GameObject textFieldRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            buildingRef.SetActive(false);
            textFieldRef.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            buildingRef.SetActive(true);
            textFieldRef.SetActive(true);
        }
    }
}
