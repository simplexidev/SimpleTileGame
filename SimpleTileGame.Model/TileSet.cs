using System; // ArgumentNullException, ArguemntOutOfRangeException, InvalidOperationException
using System.Drawing; // Bitmap, Size

namespace SimpleTileGame.Model
{
    //TODO: Documentation.
    //TODO: IEquatable<TileSet> support.
    //TODO: Get rid of warning CA1416.
    public readonly struct TileSet
    {
        //TODO: Documentation.
        //TODO: Null/Empty checks.
        public TileSet(string imagePath, TileSize tileSize)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentNullException(nameof(imagePath), "An image path must be provided when initializing a `TileSet` structure.");
            if ((int)tileSize < 0 || (int)tileSize > 2)
                throw new ArgumentOutOfRangeException(nameof(imagePath), "The specified tile size is invalid.");

            Bitmap image = new(imagePath);
            ValidateImageSize(image, tileSize);
            Image = image;
            TileSize = tileSize;
            Tiles = GetTilesFromImage(image, tileSize);
        }
        
        //TODO: Documentation.
        public TileSize TileSize { get; }
        
        //TODO: Documentation.
        public Tile[,] Tiles { get; }

        //TODO: Documentation.
        public Bitmap Image { get; }

        //TODO: Documentation.
        public Size Size => new(Tiles.GetLength(0), Tiles.GetLength(1));

        //TODO: Documentation.
        public int Count => Tiles.GetLength(0) * Tiles.GetLength(1);

        //TODO: Documentation.
        private static void ValidateImageSize(Bitmap image, TileSize tileSize)
        {
            if (image.Width % (int)tileSize != 0 || image.Height % (int)tileSize != 0)
                throw new InvalidOperationException("Image size mismatch.");
        }

        //TODO: Documentation.
        private static Tile[,] GetTilesFromImage(Bitmap image, TileSize tileSize)
        {
            Tile[,] tiles = new Tile[image.Width / (int)tileSize, image.Height / (int)tileSize];
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j] = new(new(i, j), new(i * (int)tileSize, j * (int)tileSize, (int)tileSize, (int)tileSize));
                }
            }
            return tiles;
        }
    }
}