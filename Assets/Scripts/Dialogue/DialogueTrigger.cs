using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] string line;
    [SerializeField] float duration = 4f;

    bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered && other.CompareTag("Sammy"))
        {
            DialogueManager.instance.ShowLine(line, duration);
            triggered = true;
        }
    }
}
