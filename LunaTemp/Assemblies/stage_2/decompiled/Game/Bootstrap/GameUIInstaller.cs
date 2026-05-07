using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Bootstrap
{
	public sealed class GameUIInstaller : MonoInstaller
	{
		public Canvas MainCanvas;

		public TutorialScreenViewComponent TutorialScreenViewPrefab;

		public GameEndScreenViewComponent WinScreenViewPrefab;

		public GameEndScreenViewComponent LoseScreenViewPrefab;

		public override void InstallBindings()
		{
			Transform canvasTransform = MainCanvas.transform;
			base.Container.Bind<TutorialScreenViewComponent>().FromComponentInNewPrefab(TutorialScreenViewPrefab.gameObject).UnderTransform(canvasTransform)
				.AsCached();
			base.Container.Bind<GameEndScreenViewComponent>().WithId("WinScreen").FromComponentInNewPrefab(WinScreenViewPrefab.gameObject)
				.UnderTransform(canvasTransform)
				.AsCached();
			base.Container.Bind<GameEndScreenViewComponent>().WithId("LoseScreen").FromComponentInNewPrefab(LoseScreenViewPrefab.gameObject)
				.UnderTransform(canvasTransform)
				.AsCached();
			base.Container.BindInterfacesAndSelfTo<TutorialScreenPresenter>().AsSingle().NonLazy();
			base.Container.BindInterfacesAndSelfTo<GameEndScreenPresenter>().AsSingle().NonLazy();
		}
	}
}
