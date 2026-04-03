using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 offset;
    Vector3 newpos;
    public GameObject player;

    void Start()
    {
        offset = player.transform.position - transform.position;
    }

    void Update()
    {
        newpos = transform.position;

        newpos.x = player.transform.position.x - offset.x;
        newpos.z = player.transform.position.z - offset.z;

        transform.position = newpos;
    }
}
