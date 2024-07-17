using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeardPhantom.UnityExtended.Editor
{
    public class EditorBoundSceneProcessor : IProcessSceneWithReport
    {
        /// <inheritdoc />
        public int callbackOrder { get; }

        /// <inheritdoc />
        public void OnProcessScene(Scene scene, BuildReport report)
        {
            var editorBoundGameObjects = Object.FindObjectsByType<EditorBoundGameObject>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None);
            foreach (var editorBoundGameObject in editorBoundGameObjects)
            {
                editorBoundGameObject.DestroyIfNecessary();
            }
        }
    }
}