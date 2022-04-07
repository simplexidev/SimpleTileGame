using System; // ArgumentNullException, ArguemntOutOfRangeException, IEquatable<T>, InvalidOperationException
using System.Drawing; // Bitmap, Size

//TODO: Add a `ImagePath` property.
//TODO: Review Documentation
//TODO: Review ToString() Output
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
            // Ensure imagePath is not null or empty.
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentNullException(nameof(imagePath), Strings.ImagePathMustBeProvided);

            // Ensure a valid TileSize value is provided.
            if ((int)tileSize != 16 && (int)tileSize != 32 && (int)tileSize != 64)
                throw new ArgumentOutOfRangeException(nameof(tileSize), Strings.TileSizeIsInvalid);

            // Initialize a new image based on the image path provided.
            Bitmap image = new(imagePath);

            // Validate the image's size is divisible by the provided tile size.
            ValidateImageSize(image, tileSize);

            // Set the image.
            Image = image;

            // Set the tile size.
            TileSize = tileSize;

            // Get the tiles based on the image provided.
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
        /// <remarks>This property is in terms of tiles.</remarks>
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
        /// <param name="row">The location the <see cref="Tile"/>, in tiles.</param>
        /// <returns>A <see cref="Tile"/> object with the specified index.</returns>
        public Tile GetTile(Point index)
        {
            if (index.X > Size.Width || index.Y > Size.Height)
                throw new ArgumentOutOfRangeException(nameof(location), "The specified tile index is out of range.")
            return tiles[index.X, index.Y];
        }

        /// <summary>
        /// Validates an image that it contains an even amount of tiles of the specified size.
        /// </summary>
        /// <param name="image">The image containing the tiles.</param>
        /// <param name="tileSize">The size of each tile.</param>
        private static void ValidateImageSize(Bitmap image, TileSize tileSize)
        {
            // Ensure the dimensions of the image are divisible by the specified tile size.
            if (image.Width % (int)tileSize != 0 || image.Height % (int)tileSize != 0)
                throw new InvalidOperationException(Strings.ImageSizeMismatch);
        }

        /// <summary>
        /// Gets a two-dimensional array of tiles from a <see cref="Bitmap"/>.
        /// </summary>
        /// <param name="image">The image containing the tiles.</param>
        /// <param name="tileSize">The size of each tile.</param>
        /// <returns>A two-dimensional array of tiles from a <see cref="Bitmap"/>.</returns>
        private static Tile[,] GetTilesFromImage(Bitmap image, TileSize tileSize)
        {
            // Ensure a valid Image value is provided.
            if (image is null)
                throw new ArgumentNullException(nameof(image), "An image must be provided when using a tile set.");

            // Ensure a valid TileSize value is provided.
            if ((int)tileSize != 16 && (int)tileSize != 32 && (int)tileSize != 64)
                throw new ArgumentOutOfRangeException(nameof(tileSize), Strings.TileSizeIsInvalid);

            // Determine the TileMap's width and height.
            int tileMapWidth = image.Width / (int)tileSize;
            int tileMapHeight = image.Height / (int)tileSize;

            // Initialize a new Tile array with the TileMap's width and height.
            Tile[,] tiles = new Tile[tileMapWidth, tileMapHeight];

            for (int x = 0; x < tileMapWidth; x++)
            {
                for (int y = 0; y < tileMapHeight; y++)
                {
                    // Get the index for the tile.
                    Point index = new(x, y);

                    // Get the bounds location and size.
                    Point boundsLocation = new(x * (int)tileSize, y * (int)tileSize);
                    Size boundsSize = new((int)tileSize, (int)tileSize);

                    // Set the new tile at the specified index in the Tile array.
                    tiles[x, y] = new(index, new Rectangle(boundsLocation, boundsSize));
                }
            }

            // Return the populated Tile array.
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