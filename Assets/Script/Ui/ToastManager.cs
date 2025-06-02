using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Script.Ui
{
    public class ToastManager : MonoBehaviour
    {
        public static ToastManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI toastText;
        [SerializeField] private CanvasGroup toastCanvasGroup;

        private Queue<string> toastQueue = new Queue<string>();
        private bool isShowing = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            toastCanvasGroup.alpha = 0;
        }

        public void Notify(string message)
        {
            toastQueue.Enqueue(message);
            if (!isShowing)
                StartCoroutine(ShowToasts());
        }

        private IEnumerator ShowToasts()
        {
            isShowing = true;

            while (toastQueue.Count > 0)
            {
                string message = toastQueue.Dequeue();
                toastText.text = message;

                // Fade in
                yield return StartCoroutine(FadeCanvasGroup(toastCanvasGroup, 0, 1, 0.3f));

                // Wait visible
                yield return new WaitForSeconds(2f);

                // Fade out
                yield return StartCoroutine(FadeCanvasGroup(toastCanvasGroup, 1, 0, 0.3f));
            }

            isShowing = false;
        }

        private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
                yield return null;
            }
            cg.alpha = end;
        }
    }
}