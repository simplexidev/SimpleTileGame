using System; // ArgumentNullException, IEquatable<T>, HashCode
using System.Drawing; // Point, Rectangle

namespace SimpleTileGame.Model
{
    /// <summary>
    /// Represents a tile image.
    /// </summary>
    public readonly struct Tile : IEquatable<Tile>
    {
        /// <summary>
        /// Represents an uninitialized <see cref="Tile"/> structure.
        /// </summary>
        public static readonly Tile Empty = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> structure with the specified index and bounds.
        /// </summary>
        /// <param name="index">The index of this tile, in tiles.</param>
        /// <param name="bounds">The bounds of this tile, in pixels.</param>
        public Tile(Point index, Rectangle bounds) : this(index, null, bounds) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> structure with the specified index and bounds.
        /// </summary>
        /// <param name="index">The index of this <see cref="Tile"/>.</param>
        /// <param name="location">The location of this <see cref="Tile"/> in a <see cref="TileMap"/>, in tiles.</param>
        /// <param name="bounds">The bounds of this <see cref="Tile"/> in a <see cref="TileMap"/>, in pixels.</param>
        public Tile(Point index, Point? location, Rectangle bounds)
        {
            if (index.IsEmpty) throw new ArgumentNullException(nameof(index), "A tile's index cannot be empty.");
            if (bounds.IsEmpty) throw new ArgumentNullException(nameof(bounds), "A tile's bounds cannot be empty.");

            Index = index;
            Location = location;
            Bounds = bounds;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Tile"/> is empty.
        /// </summary>
        public bool IsEmpty => this == Empty;

        /// <summary>
        /// Gets the location of this <see cref="Tile"/> in a <see cref="TileMap"/>.
        /// </summary>
        /// <remarks>
        /// This property is in terms of tiles.
        /// This property should only be null if this <see cref="Tile"/> is part of a <see cref="TileSet"/>.
        /// </remarks>
        public Point? Location { get; }

        /// <summary>
        /// Gets the index of this <see cref="Tile"/> in a <see cref="TileSet"/>.
        /// </summary>
        /// <remarks>
        /// This property is in terms of tiles.
        /// </remarks>
        public Point Index { get; }

        /// <summary>
        /// Gets the bounds of this <see cref="Tile"/>.
        /// </summary>
        /// <remarks>
        /// This property is in terms of pixels.
        /// </remarks>
        public Rectangle Bounds { get; }

        /// <summary>
        /// Spacifies whether this <see cref="Tile"/> contains the same index, location, and bounds as another <see cref="Tile"/>.
        /// </summary>
        /// <param name="other">The <see cref="Tile"/> to test for equality.</param>
        /// <returns><see langword="true"/> if <paramref name="other"/> has the same values as this <see cref="Tile"/>.</returns>
        public bool Equals(Tile other)
        {
            return Index == other.Index && Location == other.Location && Bounds == other.Bounds;
        }

        /// <summary>
        /// Spacifies whether this <see cref="Tile"/> contains the same values as the specified <see cref="object"/>.
        /// </summary>
        /// <param name="other">The <see cref="object"/> to test for equality.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="Tile"/> and has the same values as this <see cref="Tile"/>.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Tile tile && Equals(tile);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="Tile"/>.
        /// </summary>
        /// <returns>An integer value representing a hash value for this <see cref="Tile"/>,</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Location, Bounds);
        }

        /// <summary>
        /// Converts this <see cref="Tile"/> into a human-readable string.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this <see cref="Tile"/>.</returns>
        public override string ToString()
        {
            return $"[{Index}, {(Location != null ? Location.ToString() : "null")}, {Bounds}";
        }

        /// <summary>
        /// Compares two <see cref="Tile"/> structures. The result indicates whether the values of the two <see cref="Tile"> structures are equal.
        /// </summary>
        /// <param name="left">A <see cref="Tile"/> to compare.</param>
        /// <param name="right">A <see cref="Tile"/> to compare.</param>
        /// <returns><see langword="true"/> if the values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Tile left, Tile right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="Tile"/> structures. The result indicates whether the values of the two <see cref="Tile"> structures are unequal.
        /// </summary>
        /// <param name="left">A <see cref="Tile"/> to compare.</param>
        /// <param name="right">A <see cref="Tile"/> to compare.</param>
        /// <returns><see langword="true"/> if the values of <paramref name="left"/> and <paramref name="right"/> differ; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Tile left, Tile right)
        {
            return !left.Equals(right);
        }
    }
}