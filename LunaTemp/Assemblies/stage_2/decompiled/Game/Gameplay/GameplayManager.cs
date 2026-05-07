using System;
using System.Collections;
using System.Collections.Generic;
using Game.Data;
using Game.Entities;
using Game.Infrastructure;
using Game.Playworks;
using Game.World;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
	public sealed class GameplayManager : IInitializable, IDisposable
	{
		private readonly GameConfigAsset gameConfig;

		private readonly CoroutineServiceComponent coroutineService;

		private readonly HexDatabase hexDatabase;

		private readonly HexStackFactory hexStackFactory;

		private readonly HexGridComponent hexGrid;

		private readonly DragAndDropHandlerComponent playerStackDragAndDropHandler;

		private readonly PlayworksService playworksService;

		private readonly Transform playerHexStacksSpawnPointsRoot;

		private readonly List<HexStackComponent> nonPlayerStacks = new List<HexStackComponent>(32);

		private readonly List<HexStackComponent> availablePlayerStacks = new List<HexStackComponent>(4);

		private HexCount[] topHexCounts;

		private int totalTopHexCount;

		private bool isSpawningPlayerHexStacks;

		private bool isGameStarted;

		private GameResult gameResult;

		public IReadOnlyList<HexStackComponent> AvailablePlayerStacks => availablePlayerStacks;

		public IReadOnlyList<HexStackComponent> NonPlayerStacks => nonPlayerStacks;

		public event Action<GameplayManager, HexStackComponent, float> AddingNewPlayerStack;

		public event Action<GameplayManager, HexStackComponent> PlayerStackPlaced;

		public event Action<GameplayManager, HexStackComponent, HexStackComponent, int, float> MergingHexStack;

		public event Action<GameplayManager, HexStackComponent, int, float> DestroyingStackHexes;

		public event Action<GameplayManager, HexStackComponent, int> StackHexesDestroyed;

		public event Action<GameplayManager, HexStackComponent, int> HexStackDestroyed;

		public event Action<GameplayManager, GameResult> GameCompleted;

		private static float GetCountDependentDuration(float baseDuration, float minDuration, int count, float decreasingStep)
		{
			return Mathf.Max(baseDuration * (1f - decreasingStep * (float)Mathf.Max(count, 0)), minDuration);
		}

		private HexStackComponent CreateHexStack(Vector3 position, int maxHexTypeCount, int maxHexCount, bool isPlayerStack, List<HexCount> weightedHexTypes = null, float hexTypesWeightSum = 0f)
		{
			HexStackComponent hexStack = hexStackFactory.CreateStack(position, isPlayerStack);
			int totalHexTypeCount = topHexCounts.Length;
			int stackHexTypeCount = UnityEngine.Random.Range(1, Mathf.Min(maxHexTypeCount, totalHexTypeCount) + 1);
			HexCount[] hexesToAdd = new HexCount[stackHexTypeCount];
			int remainingStackHexCount = UnityEngine.Random.Range(3, maxHexCount + 1);
			for (int i = 0; i < stackHexTypeCount; i++)
			{
				int hexTypeIndex;
				if (weightedHexTypes != null)
				{
					weightedHexTypes.TryGetRandomWeightedItem((HexCount x) => x.Count, hexTypesWeightSum, out hexTypeIndex);
				}
				else
				{
					hexTypeIndex = UnityEngine.Random.Range(0, totalHexTypeCount);
				}
				int hexCount = UnityEngine.Random.Range(1, remainingStackHexCount + 1);
				int hexType = topHexCounts[hexTypeIndex].HexType;
				hexesToAdd[i] = new HexCount(hexType, hexCount);
				remainingStackHexCount -= hexCount;
			}
			hexStack.AddHexes(hexesToAdd);
			return hexStack;
		}

		private void CreatePlayerHexStacks()
		{
			if (!isSpawningPlayerHexStacks)
			{
				isSpawningPlayerHexStacks = true;
				UpdateHexCounts();
				int playerStackCount2 = Mathf.Min(gameConfig.StartPlayerHexStackCount, playerHexStacksSpawnPointsRoot.childCount);
				int maxHexCount2 = gameConfig.MaxHexStackSize / 2;
				float gridHeight2 = hexGrid.HexToWorld(Vector2Int.zero).y;
				coroutineService.StartCoroutine(Coroutine(playerStackCount2, maxHexCount2, gridHeight2));
			}
			IEnumerator Coroutine(int playerStackCount, int maxHexCount, float gridHeight)
			{
				List<HexCount> weightedHexTypes = ListPool<HexCount>.Get();
				weightedHexTypes.AddRange(topHexCounts);
				float hexTypesWeightSum = totalTopHexCount;
				float actionDuration = (isGameStarted ? gameConfig.PlayerStackSpawnActionDuration : 0f);
				for (int i = 0; i < playerStackCount; i++)
				{
					Vector3 spawnPoint = playerHexStacksSpawnPointsRoot.GetChild(i).position;
					spawnPoint.y = gridHeight;
					HexStackComponent playerStack = CreateHexStack(spawnPoint, 2, maxHexCount, true, weightedHexTypes, hexTypesWeightSum);
					playerStack.TrySetInteractive(false);
					this.AddingNewPlayerStack?.Invoke(this, playerStack, actionDuration);
					if (actionDuration > 0f)
					{
						yield return new WaitForSeconds(actionDuration);
					}
					playerStack.TrySetInteractive(true);
					foreach (int hexType in playerStack.GetHexTypes())
					{
						int hexTypeIndex = weightedHexTypes.FindIndex((HexCount x) => x.HexType == hexType);
						if (hexTypeIndex >= 0)
						{
							int weight = weightedHexTypes[hexTypeIndex].Count;
							if (weight > 1)
							{
								float num = hexTypesWeightSum;
								int num2 = weight - 1;
								hexTypesWeightSum = num - (float)num2;
								weightedHexTypes[hexTypeIndex] = new HexCount(hexType, 1);
							}
						}
					}
					availablePlayerStacks.Add(playerStack);
				}
				ListPool<HexCount>.Release(weightedHexTypes);
				isSpawningPlayerHexStacks = false;
			}
		}

		private void UpdateHexCounts()
		{
			if (topHexCounts == null)
			{
				topHexCounts = new HexCount[hexDatabase.HexTypeCount];
				List<int> hexTypes;
				using (ListPool<int>.Get(out hexTypes))
				{
					hexDatabase.GetHexTypes(hexTypes);
					for (int k = 0; k < hexTypes.Count; k++)
					{
						topHexCounts[k] = new HexCount(hexTypes[k], 0);
					}
				}
			}
			else
			{
				for (int j = 0; j < topHexCounts.Length; j++)
				{
					topHexCounts[j].Count = 0;
				}
			}
			totalTopHexCount = 0;
			for (int i = 0; i < nonPlayerStacks.Count; i++)
			{
				HexStackComponent stack = nonPlayerStacks[i];
				if (stack.IsEmpty || stack.IsBlocked)
				{
					continue;
				}
				int topHexType = stack.TopHex.HexType;
				int topHexTypeIndex = Array.FindIndex(topHexCounts, (HexCount x) => x.HexType == topHexType);
				foreach (HexComponent hex in stack.GetTopHexes())
				{
					int newCount = topHexCounts[topHexTypeIndex].Count + 1;
					topHexCounts[topHexTypeIndex] = new HexCount(topHexType, newCount);
					totalTopHexCount++;
				}
			}
		}

		private bool HexesCanBeDestroyed(HexStackComponent stack, out int count)
		{
			IReadOnlyList<HexComponent> hexes = stack.Hexes;
			count = 0;
			if (hexes.Count == 0)
			{
				return true;
			}
			int maxHexCount = gameConfig.MaxHexStackSize;
			if (hexes.Count >= maxHexCount)
			{
				if (hexes[0].HexType == hexes[hexes.Count - 1].HexType)
				{
					count = hexes.Count;
					return true;
				}
			}
			int topHexCount = stack.GetTopHexCount();
			if (topHexCount >= maxHexCount)
			{
				count = topHexCount;
				return true;
			}
			return false;
		}

		private void DestroyStack(HexStackComponent stack)
		{
			if (!(stack == null))
			{
				int stackIndex = nonPlayerStacks.IndexOf(stack);
				if (stackIndex >= 0)
				{
					List<HexStackComponent> list = nonPlayerStacks;
					int index = stackIndex;
					List<HexStackComponent> list2;
					int index2 = (list2 = nonPlayerStacks).Count - 1;
					List<HexStackComponent> list3 = nonPlayerStacks;
					HexStackComponent hexStackComponent = list3[list3.Count - 1];
					HexStackComponent hexStackComponent2 = nonPlayerStacks[stackIndex];
					HexStackComponent hexStackComponent4 = (list[index] = hexStackComponent);
					hexStackComponent4 = (list2[index2] = hexStackComponent2);
					nonPlayerStacks.RemoveAt(nonPlayerStacks.Count - 1);
				}
				hexGrid.RemoveItem(hexGrid.WorldToHex(stack.transform.position));
				UnityEngine.Object.Destroy(stack.gameObject);
				if (nonPlayerStacks.Count == 0)
				{
					CompleteGame(true);
				}
			}
		}

		private void CompleteGame(bool isWin)
		{
			if (!IsGameCompleted())
			{
				for (int i = 0; i < availablePlayerStacks.Count; i++)
				{
					availablePlayerStacks[i].TrySetInteractive(false);
				}
				gameResult = (isWin ? GameResult.Win : GameResult.Lose);
				this.GameCompleted?.Invoke(this, gameResult);
				playworksService.SetGameEnded();
			}
		}

		private bool IsGameCompleted()
		{
			return gameResult != GameResult.Undefined;
		}

		private IEnumerator DestroyStackHexesCoroutine(HexStackComponent stack, int hexCount, int destructionLevel, List<HexStackComponent> additionalStacksToCheck)
		{
			int hexType = stack.TopHex?.HexType ?? 0;
			if (hexCount > 0)
			{
				stack.IsBlocked = true;
				float duration2 = GetCountDependentDuration(gameConfig.HexDesctructionActionDuration, gameConfig.MinHexDesctructionActionDuration, destructionLevel, 0.3f);
				duration2 *= (float)hexCount;
				this.DestroyingStackHexes?.Invoke(this, stack, hexCount, duration2);
				yield return new WaitForSeconds(duration2);
				stack.IsBlocked = false;
				this.StackHexesDestroyed?.Invoke(this, stack, hexCount);
				stack.DestroyHexes(hexCount);
			}
			if (stack.IsEmpty)
			{
				this.HexStackDestroyed?.Invoke(this, stack, hexType);
				DestroyStack(stack);
			}
			else
			{
				additionalStacksToCheck.Add(stack);
			}
		}

		private IEnumerator MergeStackCoroutine(HexStackComponent startStack, List<HexStackComponent> nextStackBuffer, List<HexStackComponent> modifiedStackBuffer)
		{
			int hexType = startStack.TopHex.HexType;
			nextStackBuffer.Clear();
			int mergeLevel = 0;
			foreach (Vector2Int nextHex in hexGrid.GetNeighbours(hexGrid.WorldToHex(startStack.transform.position)))
			{
				if (!hexGrid.TryGetItem(nextHex, out var item))
				{
					continue;
				}
				HexStackComponent nextStack = item as HexStackComponent;
				if ((object)nextStack != null && !nextStack.IsEmpty && !nextStack.IsBlocked && nextStack.TopHex.HexType == hexType)
				{
					int hexCount = nextStack.GetTopHexCount();
					float actionDuration2 = GetCountDependentDuration(gameConfig.HexMoveActionDuration, gameConfig.MinHexMoveActionDuration, mergeLevel, 0.3f);
					actionDuration2 *= (float)hexCount;
					mergeLevel++;
					this.MergingHexStack?.Invoke(this, nextStack, startStack, hexCount, actionDuration2);
					startStack.IsBlocked = true;
					nextStack.IsBlocked = true;
					yield return new WaitForSeconds(actionDuration2);
					startStack.IsBlocked = false;
					nextStack.IsBlocked = false;
					nextStack.MoveHexes(hexType, startStack);
					if (nextStack.IsEmpty)
					{
						nextStackBuffer.Add(startStack);
						DestroyStack(nextStack);
					}
					else
					{
						nextStackBuffer.Add(nextStack);
					}
					if (!modifiedStackBuffer.Contains(startStack))
					{
						modifiedStackBuffer.Add(startStack);
					}
				}
				item = null;
			}
		}

		private IEnumerator StartStacksMergeCoroutine(HexStackComponent startStack)
		{
			List<HexStackComponent> nextStackBuffer = ListPool<HexStackComponent>.Get();
			List<HexStackComponent> modifiedStackBuffer = ListPool<HexStackComponent>.Get();
			while (startStack != null)
			{
				yield return coroutineService.StartCoroutine(MergeStackCoroutine(startStack, nextStackBuffer, modifiedStackBuffer));
				startStack = ((nextStackBuffer.Count != 0) ? nextStackBuffer[0] : null);
			}
			nextStackBuffer.Clear();
			int destructionLevel = 0;
			for (int j = 0; j < modifiedStackBuffer.Count; j++)
			{
				HexStackComponent stack = modifiedStackBuffer[j];
				if (HexesCanBeDestroyed(stack, out var hexCount))
				{
					coroutineService.StartCoroutine(DestroyStackHexesCoroutine(stack, hexCount, destructionLevel, nextStackBuffer));
					destructionLevel++;
				}
			}
			for (int i = 0; i < nextStackBuffer.Count; i++)
			{
				coroutineService.StartCoroutine(StartStacksMergeCoroutine(nextStackBuffer[i]));
			}
			ListPool<HexStackComponent>.Release(nextStackBuffer);
			ListPool<HexStackComponent>.Release(modifiedStackBuffer);
		}

		private void OnDragging(GameObject obj, Vector3 dragPosition, Vector3 targetPosition)
		{
			obj.transform.position = dragPosition;
		}

		private void OnDragEnded(GameObject obj, Vector3 startPosition, Vector3 targetPosition)
		{
			Vector2Int targetHex = hexGrid.WorldToHex(targetPosition);
			if (hexGrid.CanBeAddedTo(targetHex))
			{
				obj.transform.position = hexGrid.HexToWorld(targetHex);
				HexStackComponent hexStack = obj.GetComponent<HexStackComponent>();
				hexGrid.RemoveItem(hexGrid.WorldToHex(startPosition));
				hexGrid.TryAddItem(targetHex, hexStack);
				hexStack.TrySetInteractive(false);
				this.PlayerStackPlaced?.Invoke(this, hexStack);
				coroutineService.StartCoroutine(StartStacksMergeCoroutine(hexStack));
				availablePlayerStacks.Remove(hexStack);
				nonPlayerStacks.Add(hexStack);
				if (availablePlayerStacks.Count == 0)
				{
					CreatePlayerHexStacks();
				}
			}
			else
			{
				obj.transform.position = startPosition;
			}
		}

		public GameplayManager(GameConfigAsset gameConfig, CoroutineServiceComponent coroutineService, HexDatabase hexDatabase, HexStackFactory hexStackFactory, HexGridComponent hexGrid, DragAndDropHandlerComponent playerStackDragAndDropHandler, PlayworksService playworksService, Transform playerHexStacksSpawnPointsRoot)
		{
			this.gameConfig = gameConfig;
			this.coroutineService = coroutineService;
			this.hexDatabase = hexDatabase;
			this.hexStackFactory = hexStackFactory;
			this.hexGrid = hexGrid;
			this.playerStackDragAndDropHandler = playerStackDragAndDropHandler;
			this.playworksService = playworksService;
			this.playerHexStacksSpawnPointsRoot = playerHexStacksSpawnPointsRoot;
			playerStackDragAndDropHandler.Dragging += OnDragging;
			playerStackDragAndDropHandler.DragEnded += OnDragEnded;
		}

		public void Initialize()
		{
			int maxHexCount = gameConfig.MaxHexStackSize;
			UpdateHexCounts();
			foreach (Vector2Int hex in hexGrid.GetAllHexes())
			{
				if (hexGrid.CanBeAddedTo(hex) && UnityEngine.Random.value < gameConfig.HexStackSpawnDensity)
				{
					HexStackComponent stack = CreateHexStack(hexGrid.HexToWorld(hex), 2, maxHexCount, false);
					if (stack != null)
					{
						nonPlayerStacks.Add(stack);
						hexGrid.TryAddItem(hex, stack);
					}
				}
			}
			CreatePlayerHexStacks();
			isGameStarted = true;
		}

		public void Dispose()
		{
			if (playerStackDragAndDropHandler != null)
			{
				playerStackDragAndDropHandler.Dragging -= OnDragging;
				playerStackDragAndDropHandler.DragEnded -= OnDragEnded;
			}
		}
	}
}
