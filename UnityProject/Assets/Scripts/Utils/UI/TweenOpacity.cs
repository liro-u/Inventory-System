using UnityEngine;
using System.Collections;

public class TweenUIOpacity : MonoBehaviour
{
    public CanvasGroup targetCanvasGroup; // CanvasGroup to control UI opacity
    public float duration = 2f;           // Duration of the tween
    public float startOpacity = 1f;       // Starting opacity (0 is transparent, 1 is fully opaque)
    public float endOpacity = 0f;         // Ending opacity
    public bool loop = false;             // Enable looping
    public bool autoStart = true;         // Start tween automatically on Awake

    private void OnEnable()
    {
        if (autoStart)
        {
            StartTween();
        }
    }

    public void StartTween()
    {
        StopAllCoroutines();
        StartCoroutine(TweenOpacityCoroutine(startOpacity, endOpacity, duration));
    }

    private IEnumerator TweenOpacityCoroutine(float start, float end, float time)
    {
        if (targetCanvasGroup == null)
        {
            Debug.LogWarning("Target CanvasGroup is not assigned.");
            yield break;
        }

        while (true)
        {
            float elapsedTime = 0f;

            // Tween from start to end opacity
            while (elapsedTime < time)
            {
                float newOpacity = Mathf.Lerp(start, end, elapsedTime / time);
                targetCanvasGroup.alpha = newOpacity;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the final opacity is set
            targetCanvasGroup.alpha = end;

            // Exit loop if looping is not enabled
            if (!loop) yield break;

            // Swap start and end values for the next loop
            float temp = start;
            start = end;
            end = temp;
        }
    }
}
