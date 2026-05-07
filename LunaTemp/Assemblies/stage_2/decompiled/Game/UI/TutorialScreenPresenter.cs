using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entities;
using Game.Gameplay;
using Game.Infrastructure;
using Game.World;
using UnityEngine;
using Zenject;

namespace Game.UI
{
	public sealed class TutorialScreenPresenter : IInitializable, IDisposable
	{
		public float ViewUnhideDelay = 2f;

		private readonly HexGridComponent hexGrid;

		private readonly GameplayManager gameplayManager;

		private readonly CoroutineServiceComponent coroutineService;

		private readonly DragAndDropHandlerComponent dragAndDropHandler;

		private readonly TutorialScreenViewComponent view;

		private Camera camera;

		private Coroutine viewCoroutine;

		private float viewUnhideTime;

		private float time;

		private bool TryFindTutorialPointerPoints(out Vector2 hexStackPosition, out Vector2 hexCellPosition)
		{
			IReadOnlyList<HexStackComponent> playerStacks = gameplayManager.AvailablePlayerStacks;
			IReadOnlyList<HexStackComponent> nonPlayerStacks = gameplayManager.NonPlayerStacks;
			RectTransform viewTransform = view.Transform;
			for (int i = 0; i < playerStacks.Count; i++)
			{
				HexStackComponent playerStack = playerStacks[i];
				HexComponent playerTopHex = playerStack.TopHex;
				int topHexType = playerTopHex.HexType;
				for (int j = 0; j < nonPlayerStacks.Count; j++)
				{
					HexStackComponent nonPlayerStack = nonPlayerStacks[j];
					HexComponent nonPlayerTopHex = nonPlayerStack.TopHex;
					if (nonPlayerTopHex.HexType != topHexType)
					{
						continue;
					}
					Vector2Int stackHex = hexGrid.WorldToHex(nonPlayerStack.transform.position);
					foreach (Vector2Int neighbourHex in hexGrid.GetNeighbours(stackHex))
					{
						if (!hexGrid.IsValidHex(neighbourHex) || hexGrid.IsBusyHex(neighbourHex) || !viewTransform.TryTransformScreenToLocalPosition(camera, playerTopHex.transform.position, out hexStackPosition) || !viewTransform.TryTransformScreenToLocalPosition(camera, hexGrid.HexToWorld(neighbourHex), out hexCellPosition))
						{
							continue;
						}
						return true;
					}
				}
			}
			hexStackPosition = Vector2.negativeInfinity;
			hexCellPosition = Vector2.negativeInfinity;
			return false;
		}

		private void HideViewCompletely()
		{
			if (viewCoroutine != null)
			{
				coroutineService.StopCoroutine(viewCoroutine);
				view.Hide();
				viewCoroutine = null;
			}
		}

		private IEnumerator ViewUpdateCoroutine()
		{
			yield return null;
			time = 0f;
			while (true)
			{
				if (viewUnhideTime >= 0f)
				{
					if (time > viewUnhideTime)
					{
						if (TryFindTutorialPointerPoints(out var hexStackPosition, out var hexCellPosition))
						{
							view.Show(hexStackPosition, hexCellPosition);
						}
						viewUnhideTime = -1f;
						hexStackPosition = default(Vector2);
						hexCellPosition = default(Vector2);
					}
					else
					{
						view.Hide();
					}
				}
				yield return null;
				time += Time.deltaTime;
			}
		}

		private void OnDragging(GameObject obj, Vector3 startPosition, Vector3 targetPosition)
		{
			viewUnhideTime = time + ViewUnhideDelay;
		}

		private void OnPlayerStackPlaced(GameplayManager manager, HexStackComponent playerStack)
		{
			HideViewCompletely();
		}

		private void OnGameCompleted(GameplayManager manager, GameResult result)
		{
			HideViewCompletely();
		}

		public TutorialScreenPresenter(HexGridComponent hexGrid, GameplayManager gameplayManager, CoroutineServiceComponent coroutineService, DragAndDropHandlerComponent dragAndDropHandler, TutorialScreenViewComponent view)
		{
			this.hexGrid = hexGrid;
			this.gameplayManager = gameplayManager;
			this.coroutineService = coroutineService;
			this.dragAndDropHandler = dragAndDropHandler;
			this.view = view;
			gameplayManager.PlayerStackPlaced += OnPlayerStackPlaced;
			gameplayManager.GameCompleted += OnGameCompleted;
			dragAndDropHandler.Dragging += OnDragging;
		}

		public void Initialize()
		{
			camera = Camera.main;
			viewCoroutine = coroutineService.StartCoroutine(ViewUpdateCoroutine());
		}

		public void Dispose()
		{
			if (gameplayManager != null)
			{
				gameplayManager.PlayerStackPlaced -= OnPlayerStackPlaced;
				gameplayManager.GameCompleted -= OnGameCompleted;
			}
			if (dragAndDropHandler != null)
			{
				dragAndDropHandler.Dragging -= OnDragging;
			}
		}
	}
}
