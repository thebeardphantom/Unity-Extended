using NUnit.Framework;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Tests.Editor
{
    public class HexMath
    {
        #region Methods

        private static BoundsInt CreateBounds(int sizeX, int sizeY)
        {
            var bounds = new BoundsInt();
            bounds.SetMinMax(new Vector2Int(-sizeX, -sizeY).To3D(), new Vector2Int(sizeX, sizeY).To3D(1));
            return bounds;
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 3)]
        [TestCase(16, 9)]
        [TestCase(32, 52)]
        public void CellToIndex_Passes(int sizeX, int sizeY)
        {
            var bounds = CreateBounds(sizeX, sizeY);
            var indexExpected = 0;
            foreach (var cell3D in bounds.allPositionsWithin)
            {
                var cellExpected = cell3D.To2D();
                var indexActual = cellExpected.CellToIndex(bounds);
                Assert.That(indexActual, Is.EqualTo(indexExpected));
                indexExpected++;
            }
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 3)]
        [TestCase(16, 9)]
        [TestCase(32, 52)]
        public void IndexToCell_Passes(int sizeX, int sizeY)
        {
            var bounds = CreateBounds(sizeX, sizeY);
            var index = 0;
            foreach (var cell3D in bounds.allPositionsWithin)
            {
                var cellExpected = cell3D.To2D();
                var cellActual = HexFlatTop.IndexToCell(index, bounds);
                Assert.That(cellActual, Is.EqualTo(cellExpected));
                index++;
            }
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 3)]
        [TestCase(16, 9)]
        [TestCase(32, 52)]
        public void CellToIndexToCell_Passes(int sizeX, int sizeY)
        {
            var bounds = CreateBounds(sizeX, sizeY);
            var indexExpected = 0;
            foreach (var cell3D in bounds.allPositionsWithin)
            {
                var cellExpected = cell3D.To2D();
                var indexActual = cellExpected.CellToIndex(bounds);
                Assert.That(indexActual, Is.EqualTo(indexExpected));
                var cellActual = HexFlatTop.IndexToCell(indexActual, bounds);
                Assert.That(cellActual, Is.EqualTo(cellExpected));
                indexExpected++;
            }
        }

        #endregion
    }
}