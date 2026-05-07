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
            UIManager.instance.ShowTooltip("Press E to Shop");
            PlayerController.instance.SetCurrentTrigger(gameObject);
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            UIManager.instance.DismissTooltip();
            PlayerController.instance.SetCurrentTrigger(null);
            canInteract = false;
        }
    }
}
