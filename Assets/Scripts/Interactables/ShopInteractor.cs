using UnityEngine;

public class ShopInteractor : MonoBehaviour, iInteractable
{
    bool canInteract;

    public void OnInteract()
    {
        if (canInteract)
        {
            UIManager.instance.ShowShopUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            PlayerController.instance.SetCurrentTrigger(gameObject);
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            PlayerController.instance.SetCurrentTrigger(null);
            canInteract = false;
        }
    }
}
