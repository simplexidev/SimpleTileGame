using System; // ArgumentNullException, IEquatable<T>, HashCode
using System.Drawing; // Point, Rectangle

namespace SimpleTileGame.Model
{
    /// <summary>
    /// Represents a tile image.
    /// </summary>
    public readonly struct Tile : IEquatable<Tile>
    {
        //TODO: Documentation.
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
        /// <param name="index">The index of this <see cref="Tile"/> in a <see cref="TileSet"/>.</param>
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

        //TODO: Documentation.
        public bool IsEmpty => this == Empty;

        /// <summary>
        /// Gets the index of this <see cref="Tile"/> in either a <see cref="TileSet"/> or <see cref="TileMap"/>.
        /// </summary>
        /// <remarks>
        /// This property is in terms of tiles.
        /// </remarks>
        public Point Index { get; }

        /// <summary>
        /// Gets the location of this <see cref="Tile"/> in a <see cref="TileMap"/>.
        /// </summary>
        /// <remarks>
        /// This property is in terms of tiles.
        /// This property should only be null if this <see cref="Tile"/> is part of a <see cref="TileSet"/>.
        /// </remarks>
        public Point? Location { get; }

        /// <summary>
        /// Gets the bounds of this <see cref="Tile"/>.
        /// </summary>
        /// <remarks>
        /// This property is in terms of pixels.
        /// </remarks>
        public Rectangle Bounds { get; }

        //TODO: Documentation.
        public bool Equals(Tile other)
        {
            return Index == other.Index && Location == other.Location && Bounds == other.Bounds;
        }

        //TODO: Documentation.
        public override bool Equals(object? obj)
        {
            return obj is Tile tile && Equals(tile);
        }

        //TODO: Documentation.
        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Location, Bounds);
        }

        //TODO: Documentation.
        public static bool operator ==(Tile left, Tile right)
        {
            return left.Equals(right);
        }

        //TODO: Documentation.
        public static bool operator !=(Tile left, Tile right)
        {
            return !left.Equals(right);
        }
    }
}