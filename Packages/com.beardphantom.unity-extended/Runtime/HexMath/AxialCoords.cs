using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UnityExtended.HexMath
{
    public readonly struct AxialCoords : IEquatable<AxialCoords>
    {
        public static readonly AxialCoords Zero = new(0, 0);

        private static readonly AxialCoords[] s_directionVectors =
        {
            new(1, 0),
            new(1, -1),
            new(0, -1),
            new(-1, 0),
            new(-1, 1),
            new(0, 1),
        };

        [NonSerialized]
        public readonly int Q;

        [NonSerialized]
        public readonly int R;

        public int S => -Q - R;

        public AxialCoords(int q, int r)
        {
            Q = q;
            R = r;
        }

        public int this[int index]
        {
            get
            {
                return index switch
                {
                    0 => Q,
                    1 => R,
                    2 => S,
                    _ => throw new ArgumentOutOfRangeException(nameof(index)),
                };
            }
        }

        public static AxialCoords FromOffsetPointy(in Vector2Int offset)
        {
            int q = offset.x - (offset.y - (offset.y & 1)) / 2;
            int r = offset.y;
            return new AxialCoords(q, r);
        }

        public static AxialCoords FromOffsetPointy(in Vector3Int offset)
        {
            return FromOffsetPointy((Vector2Int)offset);
        }

        public static AxialCoords FromOffsetFlat(in Vector2Int offset)
        {
            // Need to swizzle x/y
            var offsetSwizzled = new Vector2Int(offset.y, offset.x);
            int q = offsetSwizzled.x;
            int r = offsetSwizzled.y - (offsetSwizzled.x - (offsetSwizzled.x & 1)) / 2;
            return new AxialCoords(q, r);
        }

        public static AxialCoords FromOffsetFlat(in Vector3Int offset)
        {
            return FromOffsetFlat((Vector2Int)offset);
        }

        public Vector2Int ToOffsetFlat()
        {
            int col = Q;
            int row = R + (Q - (Q & 1)) / 2;
            return new Vector2Int(row, col);
        }

        public Vector3Int ToOffsetFlat3D()
        {
            return (Vector3Int)ToOffsetFlat();
        }

        public Vector2Int ToOffsetPointy()
        {
            int col = Q + (R - (R & 1)) / 2;
            int row = R;
            return new Vector2Int(col, row);
        }

        public Vector3Int ToOffsetPointy3D()
        {
            return (Vector3Int)ToOffsetPointy();
        }

        public IEnumerable<AxialCoords> Radius(int radius)
        {
            for (var i = 0; i <= radius; i++)
            {
                foreach (AxialCoords coord in Ring(i))
                {
                    yield return coord;
                }
            }
        }

        public IEnumerable<AxialCoords> Ring(int ringRadius)
        {
            if (ringRadius == 0)
            {
                yield return this;
                yield break;
            }

            AxialCoords hex = this + s_directionVectors[4].Scale(ringRadius);
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < ringRadius; j++)
                {
                    yield return hex;
                    hex = hex.GetNeighbor(i);
                }
            }
        }

        public IEnumerable<AxialCoords> Rhombus(int size)
        {
            for (int q = -size; q <= size; q++)
            {
                for (int r = -size; r <= size; r++)
                {
                    var offset = new AxialCoords(q, r);
                    yield return this + offset;
                }
            }
        }

        public IEnumerable<AxialCoords> AdjacentRadius(int radius, bool includeCenter = false)
        {
            if (includeCenter)
            {
                yield return this;
            }

            AxialCoords current = this + new AxialCoords(2 * radius + 1, -radius);
            for (var i = 0; i < 6; i++)
            {
                yield return current;
                current = current.RotateCW(this);
            }
        }

        public AxialCoords GetNeighbor(HexPointyTop.Direction direction)
        {
            return GetNeighbor((int)direction);
        }

        public AxialCoords GetNeighbor(HexFlatTop.Direction direction)
        {
            return GetNeighbor((int)direction);
        }

        public AxialCoords Scale(int factor)
        {
            return new AxialCoords(Q * factor, R * factor);
        }

        public AxialCoords InvScale(int factor)
        {
            return new AxialCoords(Q / factor, R / factor);
        }

        public AxialCoords ScaleToCoarseGrid(int radius)
        {
            float scale = radius * 2f + 1f;
            float q = Q / scale;
            float r = R / scale;
            int qR = Mathf.RoundToInt(q);
            int rR = Mathf.RoundToInt(r);
            var newAxial = new AxialCoords(qR, rR);
            float qErr = Mathf.Abs(qR - q);
            float rErr = Mathf.Abs(rR - r);
            return qErr > rErr
                ? new AxialCoords(-newAxial.R - newAxial.S, newAxial.R)
                : new AxialCoords(newAxial.Q, -newAxial.Q - newAxial.S);
        }

        public AxialCoords ScaleFromCoarseGrid(int radius)
        {
            int scale = radius * 2 + 1;
            return Scale(scale);
        }

        public AxialCoords RotateCW(in AxialCoords center)
        {
            AxialCoords vec = this - center;
            vec = new AxialCoords(-vec.R, -vec.S);
            vec += center;
            return vec;
        }

        public AxialCoords RotateCCW(in AxialCoords center)
        {
            AxialCoords vec = this - center;
            vec = new AxialCoords(-vec.S, -vec.Q);
            vec += center;
            return vec;
        }

        public int Distance(AxialCoords other)
        {
            AxialCoords vec = this - other;
            int distance = Mathf.Max(Mathf.Max(Mathf.Abs(vec.Q), Mathf.Abs(vec.R)), Mathf.Abs(vec.S));
            return distance;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"({Q}, {R})";
        }

        public bool Equals(AxialCoords other)
        {
            return Q == other.Q
                   && R == other.R;
        }

        public override bool Equals(object obj)
        {
            return obj is AxialCoords other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Q, R);
        }

        private AxialCoords GetNeighbor(int direction)
        {
            AxialCoords offset = s_directionVectors[direction];
            return this + offset;
        }

        public static AxialCoords operator +(AxialCoords a, AxialCoords b)
        {
            return new AxialCoords(a.Q + b.Q, a.R + b.R);
        }

        public static AxialCoords operator -(AxialCoords a, AxialCoords b)
        {
            return new AxialCoords(a.Q - b.Q, a.R - b.R);
        }

        public static bool operator ==(AxialCoords left, AxialCoords right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AxialCoords left, AxialCoords right)
        {
            return !left.Equals(right);
        }
    }
}