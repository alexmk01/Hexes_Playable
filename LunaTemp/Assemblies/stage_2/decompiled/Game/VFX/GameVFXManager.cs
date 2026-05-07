using System;
using System.Linq;
using Game.Data;
using Game.Entities;
using Game.Gameplay;
using UnityEngine;
using Zenject;

namespace Game.VFX
{
	public sealed class GameVFXManager : IInitializable, IDisposable
	{
		private readonly HexDatabase hexDatabase;

		private readonly GameplayManager gameplayManager;

		private readonly ParticleSystem hexDestructionEffect;

		private void OnStackHexesDestroyed(GameplayManager manager, HexStackComponent hexStack, int hexCount)
		{
			if (!hexStack.IsEmpty)
			{
				int hexType = hexStack.TopHex.HexType;
				Bounds lastHexBounds = hexStack.GetTopHexes(hexCount).Last().Bounds;
				ParticleSystem effectInstance = UnityEngine.Object.Instantiate(hexDestructionEffect.gameObject).GetComponent<ParticleSystem>();
				Vector3 position = lastHexBounds.center - new Vector3(0f, lastHexBounds.extents.y - 0.01f, 0f);
				effectInstance.transform.position = position;
				ParticleSystem.MainModule mainModule = effectInstance.main;
				mainModule.loop = false;
				if (hexDatabase.TryGetHexData(hexType, out var hexData))
				{
					mainModule.startColor = hexData.Color;
				}
				effectInstance.Play();
			}
		}

		public GameVFXManager(GameConfigAsset gameConfig, HexDatabase hexDatabase, GameplayManager gameplayManager)
		{
			this.hexDatabase = hexDatabase;
			this.gameplayManager = gameplayManager;
			if (gameConfig.HexDestructionEffectPrefab != null && gameConfig.HexDestructionEffectPrefab.TryGetComponent<ParticleSystem>(out hexDestructionEffect))
			{
				gameplayManager.StackHexesDestroyed += OnStackHexesDestroyed;
			}
		}

		void IInitializable.Initialize()
		{
		}

		public void Dispose()
		{
			if (gameplayManager != null)
			{
				gameplayManager.StackHexesDestroyed -= OnStackHexesDestroyed;
			}
		}
	}
}
