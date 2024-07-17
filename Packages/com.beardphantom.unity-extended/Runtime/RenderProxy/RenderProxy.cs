using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public class RenderProxy : IDisposable
    {
        private static readonly int _defaultLayer = LayerMask.NameToLayer("Default");

        public readonly ReadOnlyCollection<RenderProxySubObject> SubObjects;

        private readonly List<RenderProxySubObject> _subObjects;

        public RenderProxy(GameObject root, RenderProxyOptions options = (RenderProxyOptions)(-1))
        {
            var logUnsupportedTypes = options.HasFlagFast(RenderProxyOptions.LogUnsupportedRendererTypes);
            ListPool<RenderProxySubObject>.Get(out _subObjects);
            SubObjects = new ReadOnlyCollection<RenderProxySubObject>(_subObjects);
            using (ListPool<Renderer>.Get(out var renderers))
            {
                root.GetComponentsInChildren(options.HasFlagFast(RenderProxyOptions.FindInactiveRenderers), renderers);
                Assert.IsTrue(renderers.Count > 0, "_renderers.Count > 0");
                foreach (var renderer in renderers)
                {
                    switch (renderer)
                    {
                        case MeshRenderer meshRenderer:
                        {
                            var meshRendererSubObject = MeshRendererSubObject.CreateInstance(meshRenderer, options);
                            if (meshRendererSubObject.IsNotNull())
                            {
                                _subObjects.Add(meshRendererSubObject);
                            }

                            break;
                        }
                        case SkinnedMeshRenderer skinnedMeshRenderer:
                        {
                            _subObjects.Add(new SkinnedMeshRendererSubObject(skinnedMeshRenderer, options));
                            break;
                        }
                        default:
                        {
                            if (logUnsupportedTypes)
                            {
                                Debug.LogWarning(
                                    $"Unsupported renderer type: {renderer.GetType()}",
                                    root);
                            }

                            break;
                        }
                    }
                }
            }
        }

        public void Render(
            Vector3? position = default,
            Quaternion? rotation = default,
            Vector3? localScale = default,
            int layer = -1,
            Camera camera = null)
        {
            position ??= Vector3.zero;
            rotation ??= Quaternion.identity;
            localScale ??= Vector3.one;
            var trs = Matrix4x4.TRS(position.Value, rotation.Value, localScale.Value);
            Render(trs, layer, camera);
        }

        public void Render(
            Matrix4x4 transformation,
            int layer = -1,
            Camera camera = default,
            Material overrideMaterial = default)
        {
            layer = layer < 0 ? _defaultLayer : layer;
            foreach (var subObject in _subObjects)
            {
                subObject.Render(transformation, layer, camera, overrideMaterial);
            }
        }

        /// <inheritdoc />
        void IDisposable.Dispose()
        {
            foreach (var subObject in _subObjects)
            {
                subObject.Dispose();
            }

            ListPool<RenderProxySubObject>.Release(_subObjects);
        }
    }
}