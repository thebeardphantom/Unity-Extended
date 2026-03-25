using UnityEngine;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for Unity UI Toolkit (UIToolkit) components, enabling functionality
    /// related to transforming positions and bounds between screen, world, and panel coordinates,
    /// as well as performing visibility checks using a camera.
    /// </summary>
    public static partial class UIToolkitExtensions
    {
        /// <summary>
        /// Sets the position of a VisualElement based on the specified screen point.
        /// </summary>
        /// <param name="visualElement">The VisualElement whose position will be updated.</param>
        /// <param name="screenPosition">The position in screen coordinates to map to the panel coordinates.</param>
        public static void SetPositionFromScreenPoint(this VisualElement visualElement, Vector2 screenPosition)
        {
            Vector2 panelPosition = visualElement.panel.GetPanelPositionFromScreenPoint(screenPosition);
            visualElement.style.translate = panelPosition;
        }

        /// <summary>
        /// Converts a screen point to a panel-relative position.
        /// </summary>
        /// <param name="panel">The panel on which the conversion should be performed.</param>
        /// <param name="screenPosition">The point on the screen, in screen coordinates, to be converted.</param>
        /// <returns>The position in panel coordinates corresponding to the provided screen point.</returns>
        public static Vector2 GetPanelPositionFromScreenPoint(this IPanel panel, Vector2 screenPosition)
        {
            screenPosition.y = Screen.height - screenPosition.y;
            var panelPosition = (Vector3)RuntimePanelUtils.ScreenToPanel(panel, screenPosition);
            return panelPosition;
        }

        /// <summary>
        /// Sets the position of a VisualElement based on a world point in 3D space.
        /// </summary>
        /// <param name="visualElement">
        /// The VisualElement whose position is being set.
        /// </param>
        /// <param name="worldPosition">
        /// The world position to set the VisualElement's position to.
        /// </param>
        /// <param name="camera">
        /// The Camera used to convert the world position to a panel position.
        /// </param>
        public static void SetPositionFromWorldPoint(this VisualElement visualElement, Vector2 worldPosition, Camera camera)
        {
            Vector2 panelPosition = visualElement.panel.GetPanelPositionFromWorldPoint(worldPosition, camera);
            visualElement.style.translate = panelPosition;
        }

        /// Transforms a 3D world space boundary into a panel space boundary for the specified panel and camera.
        /// <param name="panel">The panel to which the world boundaries are being transformed.</param>
        /// <param name="boundsWorld">The bounds in world space that need to be converted.</param>
        /// <param name="camera">The camera used to determine the panel transformation.</param>
        /// <return>The bounds in panel space.</return>
        public static Bounds WorldToPanelBounds(this IPanel panel, Bounds boundsWorld, Camera camera)
        {
            var topLeft = new Vector2(boundsWorld.min.x, boundsWorld.max.y);
            Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(panel, topLeft, boundsWorld.size, camera);
            var boundsPanel = new Bounds(rect.center, rect.size);
            return boundsPanel;
        }

        /// <summary>
        /// Converts a world position to a panel position in the context of the specified panel and camera.
        /// </summary>
        /// <param name="panel">The panel associated with the UI element where the position will be converted.</param>
        /// <param name="worldPosition">The world position to be converted into panel space.</param>
        /// <param name="camera">The camera used for the world-to-panel space transformation.</param>
        /// <returns>The position in panel space corresponding to the given world position.</returns>
        public static Vector2 GetPanelPositionFromWorldPoint(this IPanel panel, Vector2 worldPosition, Camera camera)
        {
            Vector2 panelPosition = RuntimePanelUtils.CameraTransformWorldToPanel(panel, worldPosition, camera);
            return panelPosition;
        }

        /// <summary>
        /// Determines whether a specified VisualElement is visible to a given camera.
        /// </summary>
        /// <param name="visualElement">
        /// The VisualElement whose visibility is to be checked.
        /// </param>
        /// <param name="camera">
        /// The camera to check the visibility of the VisualElement against.
        /// </param>
        /// <returns>
        /// True if the VisualElement is visible to the camera; otherwise, false.
        /// </returns>
        public static bool IsVisibleToCamera(this VisualElement visualElement, Camera camera)
        {
            Rect rect = visualElement.worldBound;
            return camera.pixelRect.Overlaps(rect);
        }
    }
}