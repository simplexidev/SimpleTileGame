using System.Drawing; // Point, Size, Graphics, Rectangle

namespace SimpleTileGame.Model // TileMap, Tile
{
    //TODO: Documentation.
    public class Camera
    {
        private Point topLeft = Point.Empty;

        //TODO: Documentation.
        public Camera(TileMap map, Size viewSize)
        {
            Map = map;
            ViewSize = viewSize;
        }

        //TODO: Documentation.
        public TileMap Map { get; }

        //TODO: Documentation.
        public Point TopLeft
        {
            get => topLeft;
            private set
            {
                if (value.X >= 0 && value.Y >= 0 && value.X < Map.Size.Width - ViewSize.Width && value.Y < Map.Size.Height - ViewSize.Height)
                    topLeft = value;
            }
        }

        //TODO: Documentation.
        public Size ViewSize { get; }

        //TODO: Documentation.
        public void Render(Graphics g)
        {
            for (int x = 0; x < ViewSize.Width; x++)
            {
                for (int y = 0; y < ViewSize.Height; y++)
                {
                    // Get the tile to draw.
                    Tile tile = Map.GetTile(new(TopLeft.X + x, TopLeft.Y + y));

                    // Get the TileSize as an int for ease-of-use.
                    int tileSize = (int)Map.TileSet.TileSize;

                    // Determine what part of the camera's viewable are to render.
                    Rectangle destinationRectangle = new(x * tileSize, y * tileSize, tileSize, tileSize);

                    // Get the source bounds of the Tile from the TileSet.
                    Rectangle sourceRectangle = Map.TileSet.GetTile(tile.Index).Bounds.Value;

                    // Render the tile.
                    g.DrawImage(Map.TileSet.Image, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
                }
            }
        }

        /// <summary>
        /// Moves the camera left one tile.
        /// </summary>
        public void MoveLeft()
        {
            TopLeft = new Point(TopLeft.X - 1, TopLeft.Y);
        }

        /// <summary>
        /// Moves the camera right one tile.
        /// </summary>
        public void MoveRight()
        {
            TopLeft = new Point(TopLeft.X + 1, TopLeft.Y);
        }

        /// <summary>
        /// Moves the camera up one tile.
        /// </summary>
        public void MoveUp()
        {
            TopLeft = new Point(TopLeft.X, TopLeft.Y - 1);
        }

        /// <summary>
        /// Moves the camera down one tile.
        /// </summary>
        public void MoveDown()
        {
            TopLeft = new Point(TopLeft.X, TopLeft.Y + 1);
        }
    }
}
