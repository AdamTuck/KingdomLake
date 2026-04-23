using UnityEngine;

public class Windmill : MonoBehaviour
{
    [SerializeField] GameObject windmillBlades;
    [SerializeField] float windmillRotationSpeed;
    void Update()
    {
        windmillBlades.transform.Rotate (Vector3.forward, windmillRotationSpeed * Time.deltaTime);
    }
}
