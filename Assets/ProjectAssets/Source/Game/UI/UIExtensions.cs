using UnityEngine;

namespace Game.UI
{
    public static class UIExtensions
    {
        public static bool TryTransformScreenToLocalPosition(this RectTransform rectTransform, Camera camera, Vector3 worldPosition, out Vector2 localPosition)
        {
            Vector3 screenPosition = camera.WorldToScreenPoint(worldPosition);
            
            if (screenPosition.z >= 0f)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPosition, null, out localPosition);
                return true;
            }
            
            localPosition = default;
            return false;
        }
    }
}