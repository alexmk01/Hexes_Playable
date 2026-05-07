using System;
using Game.World;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public sealed class DragAndDropHandlerComponent : MonoBehaviour
    {
        public event Action<GameObject, Vector3> DragStarted;
        public event Action<GameObject, Vector3, Vector3> Dragging;
        public event Action<GameObject, Vector3, Vector3> DragEnded;

        new private Camera camera;
        private HexGridComponent hexGrid;
        private int dragLayers;
        private Plane dragPlane;
        private GameObject dragObject;
        private Vector3 dragStartPosition;
        private Vector3 draggingObjectPivotOffset;
        private Vector3 targetPosition;
        
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
            
            if (pointerPosition.x < 0 || pointerPosition.y < 0 || pointerPosition.x > Screen.width || pointerPosition.y > Screen.height)
            {
                return;
            }
            
            var worldPointerPosition = camera.ScreenToWorldPoint(new Vector3(pointerPosition.x, pointerPosition.y, camera.nearClipPlane));
            Ray pointerRay = camera.ScreenPointToRay(pointerPosition);
            
            if (dragObject == null)
            {
                if (Input.GetMouseButtonDown(0) && Physics.Raycast(pointerRay, out RaycastHit hit, float.MaxValue, dragLayers))
                {
                    dragObject = hit.collider.gameObject;
                    dragStartPosition = dragObject.transform.position;
                    draggingObjectPivotOffset = dragStartPosition - hit.point;
                    DragStarted?.Invoke(dragObject, dragStartPosition);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                hexGrid.GridPlane.Raycast(pointerRay, out float gridPlaneDistance);
                dragPlane.Raycast(pointerRay, out float dragPlaneDistance);
                targetPosition = pointerRay.GetPoint(gridPlaneDistance);
                Vector3 draggingPosition = pointerRay.GetPoint(dragPlaneDistance) + draggingObjectPivotOffset;
                Dragging?.Invoke(dragObject, draggingPosition, targetPosition);
            }
            else
            {
                DragEnded?.Invoke(dragObject, dragStartPosition, targetPosition);
                dragObject = null;
            }
        }
    }
}