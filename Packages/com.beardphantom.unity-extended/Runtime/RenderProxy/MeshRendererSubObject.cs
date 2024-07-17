using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public class MeshRendererSubObject : RenderProxySubObject
    {
        private readonly Mesh _mesh;

        private readonly List<Material> _materials;

        private readonly int _subMeshCount;

        private readonly RenderProxyOptions _options;

        /// <inheritdoc />
        public override IEnumerable<Material> Materials => _materials;

        private MeshRendererSubObject(MeshRenderer renderer, MeshFilter meshFilter, RenderProxyOptions options)
        {
            _options = options;

            LocalToWorld = RenderProxyUtility.CreateMatrix(renderer.transform);

            _mesh = options.HasFlagFast(RenderProxyOptions.UseUniqueMeshInstance)
                ? Object.Instantiate(meshFilter.sharedMesh)
                : meshFilter.sharedMesh;

            // Copy shared materials
            using (ListPool<Material>.Get(out var sharedMaterials))
            {
                renderer.GetSharedMaterials(sharedMaterials);
                ListPool<Material>.Get(out _materials);

                var useUniqueMaterials = options.HasFlagFast(RenderProxyOptions.UseUniqueMaterialInstances);
                foreach (var sharedMaterial in sharedMaterials)
                {
                    _materials.Add(useUniqueMaterials ? Object.Instantiate(sharedMaterial) : sharedMaterial);
                }
            }

            _subMeshCount = _mesh.subMeshCount;
        }

        public static MeshRendererSubObject CreateInstance(MeshRenderer renderer, RenderProxyOptions options)
        {
            var meshFilter = renderer.GetComponent<MeshFilter>();
            if (meshFilter.IsNull())
            {
                // Fixes 3D TextMeshPro objects
                return null;
            }

            return new MeshRendererSubObject(renderer, meshFilter, options);
        }

        /// <inheritdoc />
        public override void Render(Matrix4x4 transformation, int layer, Camera camera, Material overrideMaterial)
        {
            var hasOverrideMaterial = overrideMaterial.IsNotNull();
            var finalTransformation = LocalToWorld * transformation;
            for (var i = 0; i < _subMeshCount; i++)
            {
                var material = hasOverrideMaterial ? overrideMaterial : _materials[i];
                RenderProxyUtility.DrawMesh(_mesh, material, finalTransformation, i, layer, camera);
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            ListPool<Material>.Release(_materials);
            if (_options.HasFlagFast(RenderProxyOptions.UseUniqueMaterialInstances))
            {
                foreach (var material in _materials)
                {
                    Object.Destroy(material);
                }
            }

            if (_options.HasFlagFast(RenderProxyOptions.UseUniqueMeshInstance))
            {
                Object.Destroy(_mesh);
            }
        }
    }
}