using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class TutorialScreenViewComponent : MonoBehaviour
    {
        public RectTransform Transform { get; private set; }
        public Canvas Canvas { get; private set; }

        [SerializeField]
        private Image hexPointerImage;

        public float PointerAnimationDuration = 1f;

        private Tweener pointerAnimation;
        private bool isVisible;

        private void Awake()
        {
            Transform = (RectTransform)transform;
            Canvas = GetComponentInParent<Canvas>();
        }

        public void Show(Vector2 hexStackPosition, Vector2 hexCellPosition)
        {
            if (isVisible) return;
            gameObject.SetActive(true);
            var pointerTransform = (RectTransform)hexPointerImage.transform;
            pointerTransform.anchoredPosition = hexStackPosition;
            
            if (pointerAnimation == null)
            {
                pointerAnimation = DOVirtual.Vector3(pointerTransform.anchoredPosition, hexCellPosition, PointerAnimationDuration, pos => pointerTransform.anchoredPosition = pos)
                    .SetEase(Ease.InOutQuad)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true)
                    .SetAutoKill(false);
            }
            else
            {
                pointerAnimation.Restart();
            }

            isVisible = true;
        }
        
        public void Hide()
        {
            if (!isVisible) return;
            pointerAnimation?.Pause();
            gameObject.SetActive(false);
            isVisible = false;
        }
        
        private void OnDestroy()
        {
            pointerAnimation?.Kill();
        }
    }
}