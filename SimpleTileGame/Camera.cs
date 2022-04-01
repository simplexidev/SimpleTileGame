using SimpleTileGame.Model;

using System.Collections.Generic;
using System.Drawing;

namespace SimpleTileGame
{
    internal class Camera
    {
        private readonly TileMap world;
        private readonly Dictionary<TerrainType, Rectangle> tilesetTerrainPositions;
        private readonly Bitmap tileset;
        private readonly int tileSize;

        private Point topLeftTile;

        public int ColumnsToRender { get; set; }
        public int RowsToRender { get; set; }

        public Point TopLeftTile
        {
            get { return topLeftTile; }
            set { 
                if(value.X >= 0 && value.Y >= 0 && value.X < world.Columns - ColumnsToRender && value.Y < world.Rows - RowsToRender)
                {
                    topLeftTile = value;
                }
            }
        }

        public Camera(TileMap world, Bitmap tileset, Dictionary<TerrainType, Rectangle> tilesetTerrainPositions, int tileSize, int columnsToRender, int rowsToRender)
        {
            this.world = world;
            this.tileset = tileset; 
            this.tileSize = tileSize;
            ColumnsToRender = columnsToRender;
            RowsToRender = rowsToRender;
            this.tilesetTerrainPositions = tilesetTerrainPositions;
        } 

        public void Render(Graphics g)
        {
            for (int x = 0; x < ColumnsToRender; x++)
            {
                for (int y = 0; y < RowsToRender; y++)
                {
                    Tile tileToDraw = world.GetTile(TopLeftTile.X + x,TopLeftTile.Y + y);
                    
                    Rectangle destinationRectangle = new(x * tileSize, y * tileSize, tileSize, tileSize);
                    Rectangle sourceRectangle = tilesetTerrainPositions[tileToDraw.Terrain];
                    g.DrawImage(tileset, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
                }
            }
        }

        public void MoveLeft()
        {
            TopLeftTile = new Point(TopLeftTile.X - 1, TopLeftTile.Y);
        }
        public void MoveRight()
        {
            TopLeftTile = new Point(TopLeftTile.X + 1, TopLeftTile.Y);
        }
        public void MoveUp()
        {
            TopLeftTile = new Point(TopLeftTile.X, TopLeftTile.Y - 1);
        }
        public void MoveDown()
        {
            TopLeftTile = new Point(TopLeftTile.X, TopLeftTile.Y + 1);
        }
    }
}
