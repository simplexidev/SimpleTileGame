using System;
using System.Drawing;

namespace SimpleTileGame.Model
{
    /// <summary>
    /// Represents a grid of <see cref="Tile"/> structures referencing a specific <see cref="Model.TileSet"/>.
    /// </summary>
    public readonly struct TileMap : IEquatable<TileMap>
    {
        private readonly Tile[,] tiles;

        /// <summary>
        /// Represents an uninitialized <see cref="TileMap"/> structure.
        /// </summary>
        public static readonly TileMap Empty = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="TileMap"/> structure with the specified tileset amd tiles.
        /// </summary>
        /// <param name="tileSet">The <see cref="Model.TileSet"/> used by this <see cref="TileMap"/>.</param>
        /// <param name="tiles">The <see cref="Tile"/> structures that make up this <see cref="TileMap"/>.</param>
        public TileMap(TileSet tileSet, Tile[,] tiles)
        {
            if (tileSet.IsEmpty)
                throw new ArgumentNullException(nameof(tileSet), Strings.TileSetCannotBeEmpty);
            if (tiles is null || tiles.Length == 0)
                throw new ArgumentNullException(nameof(tiles), Strings.TilesCannotBeNullOrEmpty);
            
            TileSet = tileSet;
            this.tiles = tiles;
        }

        /// <summary>
        /// Gets the <see cref="Model.TileSet"/> used by this <see cref="TileMap"/>.
        /// </summary>
        public TileSet TileSet { get; }

        /// <summary>
        /// Gets the size of this <see cref="TileMap"/>.
        /// </summary>
        /// <remarks>
        /// This property is in terms of tiles.
        /// </remarks>
        public Size Size => new(tiles.GetLength(0), tiles.GetLength(1));

        /// <summary>
        /// Gets the total count of <see cref="Tile"/> structures in this <see cref="TileMap"/>.
        /// </summary>
        public int Count => tiles.GetLength(0) * tiles.GetLength(1);

        /// <summary>
        /// Gets a value indicating whether this <see cref="TileMap"/> is empty.
        /// </summary>
        public bool IsEmpty => this == Empty;

        /// <summary>
        /// Gets a <see cref="Tile"/> from this <see cref="TileMap"/> at the specified row and column.
        /// </summary>
        /// <param name="row">The row the <see cref="Tile"/> is located in.</param>
        /// <param name="column">The column the <see cref="Tile"/> is located in.</param>
        /// <returns>A <see cref="Tile"/> object with the specified index.</returns>
        public Tile GetTile(int row, int column)
        {
            return tiles[row, column];
        }

        /// <summary>
        /// Spacifies whether this <see cref="TileMap"/> contains the same index, location, and bounds as another <see cref="TileMap"/>.
        /// </summary>
        /// <param name="other">The <see cref="TileMap"/> to test for equality.</param>
        /// <returns><see langword="true"/> if <paramref name="other"/> has the same values as this <see cref="TileMap"/>.</returns>
        public bool Equals(TileMap other)
        {
            return TileSet == other.TileSet && tiles == other.tiles;
        }

        /// <summary>
        /// Spacifies whether this <see cref="TileMap"/> contains the same values as the specified <see cref="object"/>.
        /// </summary>
        /// <param name="other">The <see cref="object"/> to test for equality.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="TileMap"/> and has the same values as this <see cref="TileMap"/>.</returns>
        public override bool Equals(object? obj)
        {
            return obj is TileMap set && Equals(set);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="TileMap"/>.
        /// </summary>
        /// <returns>An integer value representing a hash value for this <see cref="TileMap"/>,</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(TileSet, tiles);
        }

        /// <summary>
        /// Converts this <see cref="TileMap"/> into a human-readable string.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this <see cref="TileMap"/>.</returns>
        public override string ToString()
        {
            return $"[{TileSet}, {Count}]";
        }

        /// <summary>
        /// Compares two <see cref="TileMap"/> structures. The result indicates whether the values of the two <see cref="TileMap"> structures are equal.
        /// </summary>
        /// <param name="left">A <see cref="TileMap"/> to compare.</param>
        /// <param name="right">A <see cref="TileMap"/> to compare.</param>
        /// <returns><see langword="true"/> if the values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(TileMap left, TileMap right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="TileMap"/> structures. The result indicates whether the values of the two <see cref="TileMap"> structures are unequal.
        /// </summary>
        /// <param name="left">A <see cref="TileMap"/> to compare.</param>
        /// <param name="right">A <see cref="TileMap"/> to compare.</param>
        /// <returns><see langword="true"/> if the values of <paramref name="left"/> and <paramref name="right"/> differ; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(TileMap left, TileMap right)
        {
            return !left.Equals(right);
        }
    }
}