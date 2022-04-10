using SimpleTileGame.Model; // TileMap, TileSet, 

using System; // STAThread
using System.Drawing; // Size
using System.IO; // Directory
using System.Windows.Forms; // Application, ApplicationConfiguration

namespace SimpleTileGame
{
    internal static class Program
    {
        /// <summary>
        /// Contains static constants and values used throughout the application.
        /// </summary>
        internal static class Configuration
        {
            internal static readonly string ProgramRootPath = Directory.GetCurrentDirectory();
            //TODO: internal static readonly string TileSetSmallImagePath = Path.Combine(ProgramRootPath, "assets", "tileset.sm.png");
            //TODO: internal static readonly string TileSetMediumImagePath = Path.Combine(ProgramRootPath, "assets", "tileset.md.png");
            internal static readonly string TileSetLargeImagePath = Path.Combine(ProgramRootPath, "assets", "tileset.lg.png");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Initialize the TileSet to use based on an existing image with tiles that are 64x64.
            TileSet tileSet = new(Configuration.TileSetLargeImagePath, TileSize.Large);

            // Initialize a new TileMap with random tiles and a size of 30x30.
            TileMap tileMap = GenerateRandomTileMap(tileSet, new(100, 100));

            Sprite sprite = new(Configuration.SpriteLargeImagePath, new Rectangle(0, 0, 64, 64));

            // Initialize a new Camera with the specified TileMap and a size of 20x15, starting at location 0,0.
            GameContext context = new(tileMap, sprite, new Rectangle(0, 0, 15, 20));

            // Configure and run the Windows Forms Application with a new MainForm with the specified Camera.
            ApplicationConfiguration.Initialize();
            Application.Run(context.Form);
        }

        //TODO: Documentation.
        private static TileMap GenerateRandomTileMap(TileSet tileSet, Size mapSize)
        {
            Tile[,] tiles = new Tile[mapSize.Width, mapSize.Height];

            Random random = new();

            for (int x = 0; x < mapSize.Width; x++)
            {
                for (int y = 0; y < mapSize.Height; y++)
                {
                    tiles[x, y] = new Tile(new Point(random.Next(0, tileSet.Size.Width - 1), random.Next(0, tileSet.Size.Height - 1)), new Point(x, y));
                }
            }

            return new TileMap(tileSet, tiles);
        }
    }
}