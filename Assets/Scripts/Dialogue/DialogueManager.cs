using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] float fadeDuration = 0.5f;

    CanvasGroup canvasGroup;

    private void Awake()
    {
        instance = this;
        canvasGroup = dialogueText.transform.parent.GetComponent<CanvasGroup>();
    }

    public void ShowLine(string text, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayLine(text, duration));
    }

    IEnumerator DisplayLine(string text, float duration)
    {
        dialogueText.text = text;
        yield return StartCoroutine(Fade(1f));
        yield return new WaitForSeconds(duration);
        yield return StartCoroutine(Fade(0f));
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
