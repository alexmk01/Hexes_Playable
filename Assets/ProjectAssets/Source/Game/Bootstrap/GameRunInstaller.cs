using Game.Animation;
using Game.Data;
using Game.Entities;
using Game.Gameplay;
using Game.Infrastructure;
using Game.Playworks;
using Game.Rendering;
using Game.VFX;
using Game.World;
using UnityEngine;
using Zenject;

namespace Game.Bootstrap
{
    public sealed class GameRunInstaller : MonoInstaller
    {
        public Transform SceneRoot;
        public GameConfigAsset GameConfig;
        public HexGridComponent HexGridPrefab;
        public Transform PlayerHexStacksSpawnPointsRoot;
        
        public override void InstallBindings()
        {
            int playerHexStackLayer = LayerMask.NameToLayer(GameConfig.PlayerHexStackLayerName);
            int playerHexStackLayermask = 1 << playerHexStackLayer;
            Container.BindInstance(GameConfig);
            Container.Bind<CoroutineServiceComponent>().FromNewComponentOnNewGameObject().AsCached();
            Container.BindInstance(new HexDatabase(GameConfig.HexesData)).AsSingle();
            Container.Bind<HexGridComponent>().FromComponentInNewPrefab(HexGridPrefab.gameObject).UnderTransform(SceneRoot).AsCached().NonLazy();
            Container.Bind<HexFactory>().AsSingle().WithArguments(GameConfig.HexPrefab);
            Container.Bind<HexStackFactory>().AsSingle().WithArguments(playerHexStackLayer);
            Container.Bind<DragAndDropHandlerComponent>().FromNewComponentOnNewGameObject().AsCached().WithArguments(playerHexStackLayermask, GameConfig.PlayerHexStackDragHeight);
            Container.BindInterfacesAndSelfTo<GameplayManager>().AsSingle().WithArguments(PlayerHexStacksSpawnPointsRoot).NonLazy();
            Container.BindInterfacesAndSelfTo<HexGridRenderer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HexAnimationService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameVFXManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayworksService>().AsSingle().NonLazy();
        }
    }
}