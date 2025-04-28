using UnityEngine;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityExtended
{
    public static partial class UIToolkitExtensions
    {
        public static void SetPositionFromScreenPoint(this VisualElement visualElement, Vector2 screenPosition)
        {
            Vector2 panelPosition = visualElement.panel.GetPanelPositionFromScreenPoint(screenPosition);
            visualElement.transform.position = panelPosition;
        }

        public static Vector2 GetPanelPositionFromScreenPoint(this IPanel panel, Vector2 screenPosition)
        {
            screenPosition.y = Screen.height - screenPosition.y;
            var panelPosition = (Vector3)RuntimePanelUtils.ScreenToPanel(panel, screenPosition);
            return panelPosition;
        }

        public static void SetPositionFromWorldPoint(this VisualElement visualElement, Vector2 worldPosition, Camera camera)
        {
            Vector2 panelPosition = visualElement.panel.GetPanelPositionFromWorldPoint(worldPosition, camera);
            visualElement.transform.position = panelPosition;
        }

        public static Bounds WorldToPanelBounds(this IPanel panel, Bounds boundsWorld, Camera camera)
        {
            var topLeft = new Vector2(boundsWorld.min.x, boundsWorld.max.y);
            Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(panel, topLeft, boundsWorld.size, camera);
            var boundsPanel = new Bounds(rect.center, rect.size);
            return boundsPanel;
        }

        public static Vector2 GetPanelPositionFromWorldPoint(this IPanel panel, Vector2 worldPosition, Camera camera)
        {
            Vector2 panelPosition = RuntimePanelUtils.CameraTransformWorldToPanel(panel, worldPosition, camera);
            return panelPosition;
        }

        public static bool IsVisibleToCamera(this VisualElement visualElement, Camera camera)
        {
            Rect rect = visualElement.worldBound;
            return camera.pixelRect.Overlaps(rect);
        }
    }
}