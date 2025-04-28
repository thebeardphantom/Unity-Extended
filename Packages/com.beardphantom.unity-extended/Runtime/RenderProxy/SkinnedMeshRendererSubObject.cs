using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public class SkinnedMeshRendererSubObject : RenderProxySubObject
    {
        private readonly Mesh _mesh;

        private readonly List<Material> _materials;

        private readonly int _subMeshCount;

        private readonly RenderProxyOptions _options;

        private readonly SkinnedMeshRenderer _rendererSrc;

        /// <inheritdoc />
        public override IEnumerable<Material> Materials => _materials;

        public SkinnedMeshRendererSubObject(SkinnedMeshRenderer renderer, RenderProxyOptions options)
        {
            _options = options;

            LocalToWorld = RenderProxyUtility.CreateMatrix(renderer.transform);

            _rendererSrc = renderer;
            _mesh = Object.Instantiate(renderer.sharedMesh);

            // Copy shared materials
            using (ListPool<Material>.Get(out List<Material> sharedMaterials))
            {
                renderer.GetSharedMaterials(sharedMaterials);
                ListPool<Material>.Get(out _materials);

                bool useUniqueMaterials = options.HasFlagFast(RenderProxyOptions.UseUniqueMaterialInstances);
                foreach (Material sharedMaterial in sharedMaterials)
                {
                    _materials.Add(useUniqueMaterials ? Object.Instantiate(sharedMaterial) : sharedMaterial);
                }
            }

            _subMeshCount = _mesh.subMeshCount;
        }

        /// <inheritdoc />
        public override void Render(Matrix4x4 transformation, int layer, Camera camera, Material overrideMaterial)
        {
            bool hasOverrideMaterial = overrideMaterial.IsNotNull();
            Matrix4x4 finalTransformation = LocalToWorld * transformation;
            for (var i = 0; i < _subMeshCount; i++)
            {
                Material material = hasOverrideMaterial ? overrideMaterial : _materials[i];
                _rendererSrc.BakeMesh(_mesh);
                RenderProxyUtility.DrawMesh(_mesh, material, finalTransformation, i, layer, camera);
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            ListPool<Material>.Release(_materials);
            if (_options.HasFlagFast(RenderProxyOptions.UseUniqueMaterialInstances))
            {
                foreach (Material material in _materials)
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