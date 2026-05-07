using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class GameEndScreenViewComponent : MonoBehaviour
	{
		[SerializeField]
		private Button exitButton;

		public float FadeInDuration = 0.5f;

		public event Action ExitButtonClicked;

		public void Show()
		{
			base.gameObject.SetActive(true);
			if (TryGetComponent<CanvasGroup>(out var canvasGroup))
			{
				DOVirtual.Float(0f, 1f, FadeInDuration, delegate(float alpha)
				{
					canvasGroup.alpha = alpha;
				}).SetEase(Ease.InOutQuad);
			}
		}

		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		private void Start()
		{
			if (exitButton != null)
			{
				exitButton.onClick.AddListener(delegate
				{
					this.ExitButtonClicked?.Invoke();
				});
			}
		}
	}
}
