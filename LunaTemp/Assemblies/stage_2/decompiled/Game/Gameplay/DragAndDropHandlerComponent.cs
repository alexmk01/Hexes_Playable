using System;
using Game.World;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
	public sealed class DragAndDropHandlerComponent : MonoBehaviour
	{
		private new Camera camera;

		private HexGridComponent hexGrid;

		private int dragLayers;

		private Plane dragPlane;

		private GameObject dragObject;

		private Vector3 dragStartPosition;

		private Vector3 draggingObjectPivotOffset;

		private Vector3 targetPosition;

		public event Action<GameObject, Vector3> DragStarted;

		public event Action<GameObject, Vector3, Vector3> Dragging;

		public event Action<GameObject, Vector3, Vector3> DragEnded;

		[Inject]
		private void Construct(HexGridComponent hexGrid, int dragLayers, float dragHeight)
		{
			this.hexGrid = hexGrid;
			this.dragLayers = dragLayers;
			dragPlane = new Plane(Vector3.up, hexGrid.transform.position + new Vector3(0f, dragHeight, 0f));
			camera = Camera.main;
		}

		private void Update()
		{
			Vector2 pointerPosition = Input.mousePosition;
			if (pointerPosition.x < 0f || pointerPosition.y < 0f || pointerPosition.x > (float)Screen.width || pointerPosition.y > (float)Screen.height)
			{
				return;
			}
			Vector3 worldPointerPosition = camera.ScreenToWorldPoint(new Vector3(pointerPosition.x, pointerPosition.y, camera.nearClipPlane));
			Ray pointerRay = camera.ScreenPointToRay(pointerPosition);
			if (dragObject == null)
			{
				if (Input.GetMouseButtonDown(0) && Physics.Raycast(pointerRay, out var hit, float.MaxValue, dragLayers))
				{
					dragObject = hit.collider.gameObject;
					dragStartPosition = dragObject.transform.position;
					draggingObjectPivotOffset = dragStartPosition - hit.point;
					this.DragStarted?.Invoke(dragObject, dragStartPosition);
				}
			}
			else if (Input.GetMouseButton(0))
			{
				hexGrid.GridPlane.Raycast(pointerRay, out var gridPlaneDistance);
				dragPlane.Raycast(pointerRay, out var dragPlaneDistance);
				targetPosition = pointerRay.GetPoint(gridPlaneDistance);
				Vector3 draggingPosition = pointerRay.GetPoint(dragPlaneDistance) + draggingObjectPivotOffset;
				this.Dragging?.Invoke(dragObject, draggingPosition, targetPosition);
			}
			else
			{
				this.DragEnded?.Invoke(dragObject, dragStartPosition, targetPosition);
				dragObject = null;
			}
		}
	}
}
