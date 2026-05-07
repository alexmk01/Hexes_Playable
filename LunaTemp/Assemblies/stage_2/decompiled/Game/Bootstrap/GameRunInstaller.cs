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
			base.Container.BindInstance(GameConfig);
			base.Container.Bind<CoroutineServiceComponent>().FromNewComponentOnNewGameObject().AsCached();
			base.Container.BindInstance(new HexDatabase(GameConfig.HexesData)).AsSingle();
			base.Container.Bind<HexGridComponent>().FromComponentInNewPrefab(HexGridPrefab.gameObject).UnderTransform(SceneRoot)
				.AsCached()
				.NonLazy();
			base.Container.Bind<HexFactory>().AsSingle().WithArguments(GameConfig.HexPrefab);
			base.Container.Bind<HexStackFactory>().AsSingle().WithArguments(playerHexStackLayer);
			base.Container.Bind<DragAndDropHandlerComponent>().FromNewComponentOnNewGameObject().AsCached()
				.WithArguments(playerHexStackLayermask, GameConfig.PlayerHexStackDragHeight);
			base.Container.BindInterfacesAndSelfTo<GameplayManager>().AsSingle().WithArguments(PlayerHexStacksSpawnPointsRoot)
				.NonLazy();
			base.Container.BindInterfacesAndSelfTo<HexGridRenderer>().AsSingle().NonLazy();
			base.Container.BindInterfacesAndSelfTo<HexAnimationService>().AsSingle().NonLazy();
			base.Container.BindInterfacesAndSelfTo<GameVFXManager>().AsSingle().NonLazy();
			base.Container.BindInterfacesAndSelfTo<PlayworksService>().AsSingle().NonLazy();
		}
	}
}
