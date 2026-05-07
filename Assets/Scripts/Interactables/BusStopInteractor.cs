using UnityEngine;

public class BusStopInteractor : MonoBehaviour, iInteractable
{
    bool canInteract;

    public void OnInteract()
    {
        if (canInteract)
        {
            GameManager.instance.ChangeState(GameManager.GameState.DayOver);
            canInteract = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            UIManager.instance.ShowTooltip("Press E to End Day");
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
            canInteract= false;
        }
    }
}