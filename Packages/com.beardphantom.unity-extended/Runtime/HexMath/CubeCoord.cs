using System;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public struct CubeCoord
    {
        #region Fields

        private static readonly CubeCoord[] _directionVectors =
        {
            new(1, 0, -1),
            new(1, -1, 0),
            new(0, -1, 1),
            new(-1, 0, 1),
            new(-1, 1, 0),
            new(0, 1, -1)
        };

        [NonSerialized]
        public int Q;

        [NonSerialized]
        public int R;

        [NonSerialized]
        public int S;

        #endregion

        #region Constructors

        public CubeCoord(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }

        #endregion

        #region Methods

        public IEnumerable<CubeCoord> Ring(int radius)
        {
            var hex = this + _directionVectors[4].Scale(radius);
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < radius; j++)
                {
                    yield return hex;
                    hex = hex.GetNeighbor(i);
                }
            }
        }

        public CubeCoord GetNeighbor(HexPointTop.Direction direction)
        {
            return GetNeighbor((int)direction);
        }

        public CubeCoord GetNeighbor(HexFlatTop.Direction direction)
        {
            return GetNeighbor((int)direction);
        }

        public CubeCoord Scale(int factor)
        {
            return new CubeCoord(Q * factor, R * factor, S * factor);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"({Q}, {R}, {S})";
        }

        private CubeCoord GetNeighbor(int direction)
        {
            var offset = _directionVectors[direction];
            return this + offset;
        }

        public static CubeCoord operator +(CubeCoord a, CubeCoord b)
        {
            return new CubeCoord(a.Q + b.Q, a.R + b.R, a.S + b.S);
        }

        public static CubeCoord operator -(CubeCoord a, CubeCoord b)
        {
            return new CubeCoord(a.Q - b.Q, a.R - b.R, a.S - b.S);
        }

        #endregion
    }
}