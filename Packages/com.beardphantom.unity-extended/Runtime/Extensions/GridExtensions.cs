using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for working with Unity's Grid and its relationship with 2D and 3D coordinates.
    /// </summary>
    public static class GridExtensions
    {
        /// <summary>
        /// Converts a world position in 2D space to the corresponding cell position in the grid.
        /// </summary>
        /// <param name="grid">The grid to perform the conversion on.</param>
        /// <param name="position">The world position in 2D space to convert.</param>
        /// <returns>The cell position in the grid as a 2D integer coordinate.</returns>
        public static Vector2Int WorldToCell2D(this Grid grid, Vector2 position)
        {
            return grid.WorldToCell2D((Vector3)position);
        }

        /// <summary>
        /// Converts a world position in 2D space into a cell position using the Grid's cell layout.
        /// </summary>
        /// <param name="grid">
        /// The Grid object that defines the cell layout and transformations.
        /// </param>
        /// <param name="position">
        /// The 2D world position to convert to cell coordinates.
        /// </param>
        /// <returns>
        /// The 2D cell position corresponding to the provided world position.
        /// </returns>
        public static Vector2Int WorldToCell2D(this Grid grid, Vector3 position)
        {
            return grid.WorldToCell(position).To2D();
        }

        /// <summary>
        /// Calculates the world position of the center of a cell in a 2D grid.
        /// </summary>
        /// <param name="grid">The grid to calculate the cell center for.</param>
        /// <param name="cell">The 2D cell coordinates whose center position will be calculated.</param>
        /// <returns>The world position of the cell center in 2D space.</returns>
        public static Vector2 GetCellCenterWorld2D(this Grid grid, Vector2Int cell)
        {
            return grid.GetCellCenterWorld2D(cell.To3D());
        }

        /// <summary>
        /// Converts the specified 3D grid cell position into a 2D world position corresponding
        /// to the center of the grid cell.
        /// </summary>
        /// <param name="grid">
        /// The grid to which the cell belongs.
        /// </param>
        /// <param name="cell3D">
        /// The 3D position of the grid cell.
        /// </param>
        /// <returns>
        /// A 2D vector representing the world position of the center of the cell.
        /// </returns>
        public static Vector2 GetCellCenterWorld2D(this Grid grid, Vector3Int cell3D)
        {
            return grid.GetCellCenterWorld(cell3D);
        }

        /// <summary>
        /// Converts a given world position in 2D space to the center of the corresponding cell in world coordinates.
        /// </summary>
        /// <param name="grid">The grid used to define the coordinate system.</param>
        /// <param name="position">The world position in 2D space to be converted.</param>
        /// <returns>The center of the corresponding cell in world coordinates as a 2D vector.</returns>
        public static Vector2 WorldToCellCenterWorld2D(this Grid grid, Vector2 position)
        {
            return grid.WorldToCellCenterWorld2D((Vector3)position);
        }

        /// <summary>
        /// Converts a world position to the center of the corresponding cell's world position in 2D space.
        /// </summary>
        /// <param name="grid">The grid to perform the conversion with.</param>
        /// <param name="position">The world position to convert.</param>
        /// <returns>The world position at the center of the cell in 2D space.</returns>
        public static Vector2 WorldToCellCenterWorld2D(this Grid grid, Vector3 position)
        {
            Vector3Int cell = grid.WorldToCell(position);
            return grid.GetCellCenterWorld2D(cell);
        }
    }
}