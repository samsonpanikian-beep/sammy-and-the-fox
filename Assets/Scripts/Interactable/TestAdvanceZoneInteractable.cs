using UnityEngine;

public class TestAdvanceZoneInteractable : MonoBehaviour, IInteractable
{
    GameManager gameManager;
    MeshRenderer meshRenderer;

    Outline interactableOutline;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        interactableOutline = GetComponent<Outline>();
    }
    public void Interact()
    {
        gameManager.AdvanceGameZone();
        meshRenderer.material.color = Color.black;
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
