using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public class MeshRendererSubObject : RenderProxySubObject
    {
        #region Fields

        public readonly Mesh Mesh;

        public readonly List<Material> Materials;

        public readonly int SubMeshCount;

        private readonly RenderProxyOptions _options;

        #endregion

        #region Constructors

        public MeshRendererSubObject(MeshRenderer renderer, RenderProxyOptions options)
        {
            _options = options;

            var meshFilter = renderer.GetComponent<MeshFilter>();

            LocalToWorld = renderer.transform.localToWorldMatrix;

            Mesh = options.HasFlagFast(RenderProxyOptions.UseUniqueMeshInstance)
                ? Object.Instantiate(meshFilter.sharedMesh)
                : meshFilter.sharedMesh;

            // Copy shared materials
            using (ListPool<Material>.Get(out var sharedMaterials))
            {
                renderer.GetSharedMaterials(sharedMaterials);
                ListPool<Material>.Get(out Materials);

                var useUniqueMaterials = options.HasFlagFast(RenderProxyOptions.UseUniqueMaterialInstance);
                foreach (var sharedMaterial in sharedMaterials)
                {
                    Materials.Add(useUniqueMaterials ? Object.Instantiate(sharedMaterial) : sharedMaterial);
                }
            }

            SubMeshCount = Mesh.subMeshCount;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Render(Matrix4x4 transformation, int layer, Camera camera, Material overrideMaterial)
        {
            var hasOverrideMaterial = overrideMaterial.IsNotNull();
            var finalTransformation = transformation * LocalToWorld;
            for (var i = 0; i < SubMeshCount; i++)
            {
                var material = hasOverrideMaterial
                    ? overrideMaterial
                    : Materials[i];
                Graphics.DrawMesh(Mesh, finalTransformation, material, layer, camera, i);
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            ListPool<Material>.Release(Materials);
            if (_options.HasFlagFast(RenderProxyOptions.UseUniqueMaterialInstance))
            {
                foreach (var material in Materials)
                {
                    Object.Destroy(material);
                }
            }

            if (_options.HasFlagFast(RenderProxyOptions.UseUniqueMeshInstance))
            {
                Object.Destroy(Mesh);
            }
        }

        #endregion
    }
}