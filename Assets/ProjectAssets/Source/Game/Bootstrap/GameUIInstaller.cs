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

            Container.Bind<TutorialScreenViewComponent>()
                .FromComponentInNewPrefab(TutorialScreenViewPrefab.gameObject)
                .UnderTransform(canvasTransform)
                .AsCached();
            
            Container.Bind<GameEndScreenViewComponent>()
                .WithId(GameEndScreenPresenter.WinScreenKey)
                .FromComponentInNewPrefab(WinScreenViewPrefab.gameObject)
                .UnderTransform(canvasTransform)
                .AsCached();
            
            Container.Bind<GameEndScreenViewComponent>()
                .WithId(GameEndScreenPresenter.LoseScreenKey)
                .FromComponentInNewPrefab(LoseScreenViewPrefab.gameObject)
                .UnderTransform(canvasTransform)
                .AsCached();
            
            Container.BindInterfacesAndSelfTo<TutorialScreenPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameEndScreenPresenter>().AsSingle().NonLazy();
        }
    }
}