using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class GameEndScreenViewComponent : MonoBehaviour
    {
        public event Action ExitButtonClicked;

        [SerializeField]
        private Button exitButton;
        
        public float FadeInDuration = 0.5f;

        public void Show()
        {
            gameObject.SetActive(true);

            if (TryGetComponent(out CanvasGroup canvasGroup))
            {
                DOVirtual.Float(0f, 1f, FadeInDuration, alpha => canvasGroup.alpha = alpha).SetEase(Ease.InOutQuad);
            }
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
            if (exitButton != null)
            {
                exitButton.onClick.AddListener(() => ExitButtonClicked?.Invoke());
            }
        }
    }
}