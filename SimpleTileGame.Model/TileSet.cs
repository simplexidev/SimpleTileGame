using System; // ArgumentNullException, ArguemntOutOfRangeException, IEquatable<T>, InvalidOperationException
using System.Drawing; // Bitmap, Size

namespace SimpleTileGame.Model
{
    /// <summary>
    /// Represents a set of <see cref="Tile"/> structures that can be used in a <see cref="TileMap"/>.
    /// </summary>
    public readonly struct TileSet : IEquatable<TileSet>
    {
        private readonly Tile[,] tiles;

        /// <summary>
        /// Represents an uninitialized <see cref="TileSet"/> structure.
        /// </summary>
        public static readonly TileSet Empty = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="TileSet"/> structure with the specified image and tile size.
        /// </summary>
        /// <param name="imagePath">A full path to the image this <see cref="TileSet"/> represents.</param>
        /// <param name="tileSize">The size of each tile in this <see cref="TileSet"/>.</param>
        public TileSet(string imagePath, TileSize tileSize)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentNullException(nameof(imagePath), Strings.ImagePathMustBeProvided);
            if ((int)tileSize != 16 && (int)tileSize != 32 && (int)tileSize != 64)
                throw new ArgumentOutOfRangeException(nameof(imagePath), Strings.TileSizeIsInvalid);

            Bitmap image = new(imagePath);
            ValidateImageSize(image, tileSize);
            Image = image;
            TileSize = tileSize;
            tiles = GetTilesFromImage(image, tileSize);
        }

        /// <summary>
        /// Gets the size of the <see cref="Tile"/> structures in this <see cref="TileSet"/>.
        /// </summary>
        public TileSize TileSize { get; }

        /// <summary>
        /// Gets the image that this <see cref="TileSet"/> represents.
        /// </summary>
        public Bitmap Image { get; }

        /// <summary>
        /// Gets the size of this <see cref="TileSet"/>.
        /// </summary>
        /// <remarks>
        /// This property is in terms of tiles.
        /// </remarks>
        public Size Size => new(tiles.GetLength(0), tiles.GetLength(1));

        /// <summary>
        /// Gets the total count of <see cref="Tile"/> structures in this <see cref="TileSet"/>.
        /// </summary>
        public int Count => tiles.GetLength(0) * tiles.GetLength(1);

        /// <summary>
        /// Gets a value indicating whether this <see cref="TileSet"/> is empty.
        /// </summary>
        public bool IsEmpty => this == Empty;

        /// <summary>
        /// Gets a <see cref="Tile"/> from this <see cref="TileSet"/> at the specified row and column.
        /// </summary>
        /// <param name="row">The row the <see cref="Tile"/> is located in.</param>
        /// <param name="column">The column the <see cref="Tile"/> is located in.</param>
        /// <returns>A <see cref="Tile"/> object with the specified index.</returns>
        public Tile GetTile(int row, int column)
        {
            return tiles[row, column];
        }

        /// <summary>
        /// Validates an image that it contains an even amount of tiles of the specified size.
        /// </summary>
        /// <param name="image">The image containing the tiles.</param>
        /// <param name="tileSize">The size of each tile.</param>
        private static void ValidateImageSize(Bitmap image, TileSize tileSize)
        {
            // TODO: Dev Documentation.
            if (image.Width % (int)tileSize != 0 || image.Height % (int)tileSize != 0)
                throw new InvalidOperationException(Strings.ImageSizeMismatch);
        }

        /// <summary>
        /// Gets a two-dinensional array of tiles from a <see cref="Bitmap"/>.
        /// </summary>
        /// <param name="image">The image containing the tiles.</param>
        /// <param name="tileSize">The size of each tile.</param>
        /// <returns>A two-dimensional array of tiles from a <see cref="Bitmap"/>.</returns>
        private static Tile[,] GetTilesFromImage(Bitmap image, TileSize tileSize)
        {
            // TODO: Dev Documentation.
            Tile[,] tiles = new Tile[image.Width / (int)tileSize, image.Height / (int)tileSize];
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    // TODO: Dev Documentation.
                    tiles[i, j] = new(new(i, j), new(i * (int)tileSize, j * (int)tileSize, (int)tileSize, (int)tileSize));
                }
            }
            return tiles;
        }

        /// <summary>
        /// Spacifies whether this <see cref="TileSet"/> contains the same index, location, and bounds as another <see cref="TileSet"/>.
        /// </summary>
        /// <param name="other">The <see cref="TileSet"/> to test for equality.</param>
        /// <returns><see langword="true"/> if <paramref name="other"/> has the same values as this <see cref="TileSet"/>.</returns>
        public bool Equals(TileSet other)
        {
            return TileSize == other.TileSize && Image == other.Image;
        }

        /// <summary>
        /// Spacifies whether this <see cref="TileSet"/> contains the same values as the specified <see cref="object"/>.
        /// </summary>
        /// <param name="other">The <see cref="object"/> to test for equality.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="TileSet"/> and has the same values as this <see cref="TileSet"/>.</returns>
        public override bool Equals(object? obj)
        {
            return obj is TileSet set && Equals(set);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="TileSet"/>.
        /// </summary>
        /// <returns>An integer value representing a hash value for this <see cref="TileSet"/>,</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(TileSize, Image);
        }

        /// <summary>
        /// Converts this <see cref="TileSet"/> into a human-readable string.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this <see cref="TileSet"/>.</returns>
        public override string ToString()
        {
            return $"[{TileSize}, {Count}]";
        }

        /// <summary>
        /// Compares two <see cref="TileSet"/> structures. The result indicates whether the values of the two <see cref="TileSet"> structures are equal.
        /// </summary>
        /// <param name="left">A <see cref="TileSet"/> to compare.</param>
        /// <param name="right">A <see cref="TileSet"/> to compare.</param>
        /// <returns><see langword="true"/> if the values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(TileSet left, TileSet right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="TileSet"/> structures. The result indicates whether the values of the two <see cref="TileSet"> structures are unequal.
        /// </summary>
        /// <param name="left">A <see cref="TileSet"/> to compare.</param>
        /// <param name="right">A <see cref="TileSet"/> to compare.</param>
        /// <returns><see langword="true"/> if the values of <paramref name="left"/> and <paramref name="right"/> differ; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(TileSet left, TileSet right)
        {
            return !left.Equals(right);
        }
    }
}