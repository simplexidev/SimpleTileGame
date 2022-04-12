using SimpleTileGame.Resources;

using System;
using System.Drawing;

//TODO: Review ToString() Output
namespace SimpleTileGame.Model
{
    /// <summary>
    /// Represents a tile image.
    /// </summary>
    public readonly struct Tile : IEquatable<Tile>
    {
        /// <summary>
        /// Represents a <see cref="Tile"/> structure with uninitialized values.
        /// </summary>
        public static readonly Tile Empty = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> structure with the specified index and bounds.
        /// </summary>
        /// <param name="index">The index of this tile, in tiles.</param>
        /// <param name="bounds">The bounds of this tile, in pixels.</param>
        /// <remarks>
        /// This constructor is to be used for <see cref="Tile"/> structures in a <see cref="TileSet"/>.
        /// </remarks>
        public Tile(Point index, Rectangle bounds) : this(index, null, bounds) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> structure with the specified index and location.
        /// </summary>
        /// <param name="index">The index of this <see cref="Tile"/> in a <see cref="TileSet"/>, in tiles.</param>
        /// <param name="location">The location of this <see cref="Tile"/> in a <see cref="TileMap"/>, in pixels.</param>
        /// <remarks>
        /// This constructor should be used only when initializing <see cref="Tile"/> structures in a
        /// <see cref="TileMap"/>.
        /// </remarks>
        public Tile(Point index, Point location) : this(index, location, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> structure with the specified index and bounds.
        /// </summary>
        /// <param name="index">The index of this <see cref="Tile"/> in a <see cref="TileSet"/>, in tiles.</param>
        /// <param name="location">The location of this <see cref="Tile"/> in a <see cref="TileMap"/>, in tiles.</param>
        /// <param name="bounds">The bounds of this <see cref="Tile"/> in a <see cref="TileSet"/>, in pixels.</param>
        private Tile(Point index, Point? location, Rectangle? bounds)
        {
            // Ensure that `index` has values that are greater than or equal to zero.
            if (index.X < 0 || index.Y < 0)
                throw new ArgumentOutOfRangeException(nameof(index), Strings.TileIndexMustNotBeNegative);
            
            // Ensure that `location` has values that are greater than or equal to zero, if it is not null.
            if (location != null && (location.Value.X < 0 || location.Value.Y < 0))
                throw new ArgumentOutOfRangeException(nameof(location), Strings.TileLocationMustNotBeNegative);

            // Ensure that `bounds` has values that are greater than or equal to zero, if it is not null.
            if (bounds != null && (bounds.Value.X < 0 || bounds.Value.Y < 0 || bounds.Value.Width < 0 || bounds.Value.Height < 0))
                throw new ArgumentOutOfRangeException(nameof(bounds), Strings.TileBoundsMustNotBeNegative);

            // Ensure that `bounds` has a width and height greater than zero, if it is not null.
            if (bounds != null && (bounds.Value.Width == 0 || bounds.Value.Height == 0))
                throw new ArgumentOutOfRangeException(nameof(bounds), Strings.TileBoundsSizeMustNotBeZero);

            // Directly set the properties with the provided values.
            Index = index;
            Location = location;
            Bounds = bounds;
        }

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
        public Rectangle? Bounds { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Tile"/> is empty.
        /// </summary>
        public bool IsEmpty => this == Empty;

        /// <summary>
        /// Specifies whether this <see cref="Tile"/> contains the same values as the specified <see cref="Tile"/>.
        /// </summary>
        /// <param name="other">The <see cref="Tile"/> to test for equality.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other"/> has the same values as this <see cref="Tile"/>.
        /// </returns>
        public bool Equals(Tile other) => Index == other.Index && Location == other.Location && Bounds == other.Bounds;

        /// <summary>
        /// Specifies whether this <see cref="Tile"/> contains the same values as the specified <see cref="object"/>.
        /// </summary>
        /// <param name="other">The <see cref="object"/> to test for equality.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="obj"/> is a <see cref="Tile"/> and has the same values as this
        /// <see cref="Tile"/>.
        /// </returns>
        public override bool Equals(object? obj) => obj is Tile tile && Equals(tile);

        /// <summary>
        /// Returns a hash code for this <see cref="Tile"/>.
        /// </summary>
        /// <returns>An integer value representing a hash value for this <see cref="Tile"/>,</returns>
        public override int GetHashCode() => HashCode.Combine(Index, Location, Bounds);

        /// <summary>
        /// Converts this <see cref="Tile"/> into a human-readable string.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this <see cref="Tile"/>.</returns>
        public override string ToString() => $"[{Index}, {(Location != null ? Location.ToString() : "null")}, {Bounds}]";

        /// <summary>
        /// Compares two <see cref="Tile"/> structures. The result indicates whether the values of the two
        /// <see cref="Tile"> structures are equal.
        /// </summary>
        /// <param name="left">A <see cref="Tile"/> to compare.</param>
        /// <param name="right">A <see cref="Tile"/> to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the values of <paramref name="left"/> and <paramref name="right"/> are equal;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(Tile left, Tile right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="Tile"/> structures. The result indicates whether the values of the two
        /// <see cref="Tile"> structures are unequal.
        /// </summary>
        /// <param name="left">A <see cref="Tile"/> to compare.</param>
        /// <param name="right">A <see cref="Tile"/> to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the values of <paramref name="left"/> and <paramref name="right"/> differ;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(Tile left, Tile right) => !left.Equals(right);
    }
}