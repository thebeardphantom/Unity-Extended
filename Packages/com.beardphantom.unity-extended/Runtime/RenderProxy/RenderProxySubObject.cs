using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public abstract class RenderProxySubObject : IDisposable
    {
        protected Matrix4x4 LocalToWorld;

        public abstract IEnumerable<Material> Materials { get; }

        public abstract void Render(Matrix4x4 transformation, int layer, Camera camera, Material overrideMaterial);

        /// <inheritdoc />
        public abstract void Dispose();
    }
}