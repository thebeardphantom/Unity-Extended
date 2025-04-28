using UnityEngine;
#if UNITY_2023_2_OR_NEWER && !UNITY_6000_0_OR_NEWER
using UnityEngine.Rendering;
#endif

namespace BeardPhantom.UnityExtended
{
    public static class RenderProxyUtility
    {
        public static Matrix4x4 CreateMatrix(Transform tform)
        {
            Transform root = tform.root;
            Matrix4x4 matrix = Matrix4x4.TRS(
                root.InverseTransformPoint(tform.position),
                Quaternion.Inverse(root.rotation) * tform.rotation,
                tform.lossyScale);
            return matrix;
        }

        public static void DrawMesh(
            Mesh mesh,
            Material material,
            Matrix4x4 trs,
            int submesh,
            int layer = -1,
            Camera camera = null)
        {
#if UNITY_2023_2_OR_NEWER
            var renderParams = new RenderParams(material)
            {
                layer = layer,
                camera = camera,
                worldBounds = new Bounds(Vector3.zero, Vector3.one * 10000f),
                renderingLayerMask = GetDefaultRenderingLayerMask(),
            };
            Graphics.RenderMesh(renderParams, mesh, submesh, trs);
#else
            Graphics.DrawMesh(mesh, trs, material, layer, camera, submesh);
#endif
        }

#if UNITY_2023_2_OR_NEWER
        private static RenderingLayerMask GetDefaultRenderingLayerMask()
        {
#if UNITY_6000_0_OR_NEWER
            return RenderingLayerMask.defaultRenderingLayerMask;
#else
            return GraphicsSettings.defaultRenderingLayerMask;
#endif
        }
#endif
    }
}