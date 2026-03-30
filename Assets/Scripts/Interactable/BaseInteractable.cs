using UnityEngine;

public class BaseInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] Transform mouthAttachPoint;
    [SerializeField] Transform environmentParent;

    Outline interactableOutline;
    Rigidbody rb;

    bool interactedWith;

    void Awake()
    {
        interactableOutline = GetComponent<Outline>();
        rb = GetComponent<Rigidbody>();
    }
    public void Interact()
    {
        interactedWith = !interactedWith;

        if (interactedWith)
        {
            rb.isKinematic = true;

            transform.SetParent(mouthAttachPoint, false);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            gameObject.layer = LayerMask.NameToLayer("Held Object");
        }
        else
        {
            transform.SetParent(environmentParent, true);
            rb.isKinematic = false;

            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    public void ShowOutline()
    {
        interactableOutline.enabled = true;
    }

    public void HideOutline()
    {
        interactableOutline.enabled = false;
    }
}
