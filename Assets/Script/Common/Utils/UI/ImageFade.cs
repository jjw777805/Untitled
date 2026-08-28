using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyUtils
{
    [System.Serializable]
    public class ImageFade:MonoBehaviour
    {
        public Image targetImage;

        public void FadeTo(float targetAlpha, float duration, Action onComplete = null)
        {
            StartCoroutine(FadeRoutine(targetAlpha, duration, onComplete));
        }

        private IEnumerator FadeRoutine(float targetAlpha, float duration, Action onComplete)
        {
            Color color = targetImage.color;
            float startAlpha = color.a;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime; // 使用不受 Time.timeScale 影响的增量
                float t = Mathf.Clamp01(elapsed / duration);
                color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
                targetImage.color = color;
                yield return null; 
            }

            color.a = targetAlpha;
            targetImage.color = color;

            // 触发回调
            onComplete?.Invoke();
        }
    }
}