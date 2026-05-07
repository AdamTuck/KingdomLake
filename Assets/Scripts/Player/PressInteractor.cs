using System.Runtime.CompilerServices;
using UnityEngine;

public class PressInteractor : Interact
{
    [Header("Press/Click")]
    [SerializeField] private LayerMask pressLayer;
    [SerializeField] private float pressDistance;

    [Header("Camera")]
    [SerializeField] private Camera playerCamera;

    private RaycastHit raycastHit;
    private iInteractable interactableObject;

    public override void Interaction()
    {
        interactableObject = raycastHit.transform.GetComponent<iInteractable>();

        if (interactableObject == null)
            return;

        interactableObject.OnInteract();
        return;
    }
}
