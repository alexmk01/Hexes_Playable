using System;
using System.Collections.Generic;
using Game.Data;
using Game.Entities;
using Game.Gameplay;
using Game.World;
using UnityEngine;
using Zenject;

namespace Game.Rendering
{
	public sealed class HexGridRenderer : IInitializable, IDisposable
	{
		public readonly Color HexHighlightColor;

		private readonly GameConfigAsset gameConfig;

		private readonly HexGridComponent hexGrid;

		private readonly DragAndDropHandlerComponent dragAndDropHandler;

		private readonly Dictionary<Vector2Int, HexComponent> hexRenderers = new Dictionary<Vector2Int, HexComponent>(64);

		private Vector2Int currentHighlightedHex;

		public Color DefaultHexColor { get; private set; }

		public event Action<HexGridRenderer, HexComponent, HexComponent> HighlightedHexChanged;

		private void HighlightHex(Vector2Int hex)
		{
			if (currentHighlightedHex != hex)
			{
				if (hexRenderers.TryGetValue(currentHighlightedHex, out var currentHexRenderer))
				{
					currentHexRenderer.Color = DefaultHexColor;
				}
				if (hexRenderers.TryGetValue(hex, out var newHexRenderer))
				{
					newHexRenderer.Color = HexHighlightColor;
				}
				currentHighlightedHex = hex;
				this.HighlightedHexChanged?.Invoke(this, currentHexRenderer, newHexRenderer);
			}
		}

		private void ResetHexHighlight()
		{
			HighlightHex(new Vector2Int(int.MinValue, int.MinValue));
		}

		private void OnDragEnded(GameObject obj, Vector3 startPosition, Vector3 targetPosition)
		{
			ResetHexHighlight();
		}

		private void OnDragging(GameObject obj, Vector3 dragPosition, Vector3 targetPosition)
		{
			Vector2Int targetHex = hexGrid.WorldToHex(targetPosition);
			if (hexGrid.CanBeAddedTo(targetHex))
			{
				HighlightHex(targetHex);
			}
			else
			{
				ResetHexHighlight();
			}
		}

		public HexGridRenderer(GameConfigAsset gameConfig, HexGridComponent hexGrid, DragAndDropHandlerComponent dragAndDropHandler)
		{
			this.gameConfig = gameConfig;
			this.hexGrid = hexGrid;
			this.dragAndDropHandler = dragAndDropHandler;
			HexHighlightColor = gameConfig.GameFieldHexHighlightColor;
			dragAndDropHandler.Dragging += OnDragging;
			dragAndDropHandler.DragEnded += OnDragEnded;
		}

		public void Initialize()
		{
			float groundHexExtents = 0f;
			foreach (Vector2Int hex in hexGrid.GetAllHexes())
			{
				HexComponent hexRenderer = UnityEngine.Object.Instantiate(gameConfig.GameFieldHexPrefab).GetComponent<HexComponent>();
				if (DefaultHexColor.a == 0f)
				{
					DefaultHexColor = hexRenderer.Color;
				}
				Vector3 worldPos = hexGrid.HexToWorld(hex);
				float hexExtents = hexRenderer.Bounds.extents.y;
				if (groundHexExtents < hexExtents)
				{
					groundHexExtents = hexExtents;
				}
				worldPos.y -= hexExtents;
				hexRenderer.transform.position = worldPos;
				hexRenderers.Add(hex, hexRenderer);
			}
			if (gameConfig.GroundPrefab != null)
			{
				GameObject ground = UnityEngine.Object.Instantiate(gameConfig.GroundPrefab);
				ground.transform.position = new Vector3(0f, (0f - groundHexExtents) * 2f, 0f);
			}
			currentHighlightedHex = new Vector2Int(int.MinValue, int.MinValue);
		}

		public void Dispose()
		{
			if (dragAndDropHandler != null)
			{
				dragAndDropHandler.Dragging -= OnDragging;
				dragAndDropHandler.DragEnded -= OnDragEnded;
			}
		}
	}
}
