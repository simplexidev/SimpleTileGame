using System.Drawing;

namespace SimpleTileGame.Model
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
                //TODO: Dev Documentation.
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
                    //TODO: Dev Documentation.
                    Tile tileToDraw = Map.GetTile(TopLeft.X + x, TopLeft.Y + y);

                    //TODO: Dev Documentation.
                    int tileSize = (int)Map.TileSet.TileSize;
                    Rectangle destinationRectangle = new(x * tileSize, y * tileSize, tileSize, tileSize);
                    Rectangle sourceRectangle = Map.TileSet.GetTile(tileToDraw.Index).Bounds.Value;
                    g.DrawImage(Map.TileSet.Image, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
                }
            }
        }

        //TODO: Documentation.
        public void MoveLeft()
        {
            TopLeft = new Point(TopLeft.X - 1, TopLeft.Y);
        }

        //TODO: Documentation.
        public void MoveRight()
        {
            TopLeft = new Point(TopLeft.X + 1, TopLeft.Y);
        }

        //TODO: Documentation.
        public void MoveUp()
        {
            TopLeft = new Point(TopLeft.X, TopLeft.Y - 1);
        }

        //TODO: Documentation.
        public void MoveDown()
        {
            TopLeft = new Point(TopLeft.X, TopLeft.Y + 1);
        }

    }
}
