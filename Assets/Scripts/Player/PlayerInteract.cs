using UnityEngine;

public interface IInteractable
{
    public void Interact();

    public void ShowOutline();
    public void HideOutline();
}

public class PlayerInteract : MonoBehaviour
{
    private IInteractable currentInteractable;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IInteractable>(out var interactable))
        {
            currentInteractable = interactable;
            currentInteractable.ShowOutline();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            currentInteractable.HideOutline();
            currentInteractable = null;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}
