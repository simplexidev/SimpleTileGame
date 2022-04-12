using System.Drawing;

namespace SimpleTileGame.Model
{
    /// <summary>
    /// Contains static methods allowing for the drawing of <see cref="TileMap"/> and <see cref="Sprite"/> objects and text.
    /// </summary>
    public static class GameGraphics
    {
        /// <summary>
        /// Draws a <see cref="Sprite"/> object to the specified <see cref="Graphics"/> context.
        /// </summary>
        /// <param name="graphics">The <see cref="Graphics"/> context to draw on.</param>
        /// <param name="sprite">The <see cref="Sprite"/> to draw.</param>
        public static void DrawSprite(Graphics graphics, Sprite sprite)
        {
            // Draws the sprite.
            graphics.DrawImage(sprite.Image, sprite.Bounds);
        }

        /// <summary>
        /// Draws text to the specified <see cref="Graphics"/> context.
        /// </summary>
        /// <param name="graphics">The <see cref="Graphics"/> context to draw on.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="fontFamily">The <see cref="FontFamily"/> used for the text.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="location">The location of the text.</param>
        public static void DrawText(Graphics graphics, string text, FontFamily fontFamily, Color color, Point location)
        {
            // Draw the specified text.
            graphics.DrawString(text, new Font(fontFamily, 11.75f, FontStyle.Regular), new SolidBrush(color), location);
        }

        /// <summary>
        /// Draws a <see cref="TileMap"/> object to the specified <see cref="Graphics"/> context.
        /// </summary>
        /// <param name="graphics">The <see cref="Graphics"/> context to draw on.</param>
        /// <param name="region">The area of the map to draw.</param>
        /// <param name="tileMap">The <see cref="TileMap"/> to draw.</param>
        public static void DrawTileMap(Graphics graphics, Rectangle region, TileMap tileMap)
        {
            // Draw the new view of the tile map.
            for (int x = 0; x < region.Width; x++)
            {
                for (int y = 0; y < region.Height; y++)
                {
                    // Get the tile to draw.
                    Tile tile = tileMap.GetTile(new(region.X + x, region.Y + y));

                    // Get the TileSize as an int for ease-of-use.
                    int tileSize = (int)tileMap.TileSet.TileSize;

                    // Determine what part of the camera's viewable are to render.
                    Rectangle destinationRectangle = new(x * tileSize, y * tileSize, tileSize, tileSize);

                    // Get the source bounds of the Tile from the TileSet.
                    Rectangle sourceRectangle = tileMap.TileSet.GetTile(tile.Index).Bounds.Value;

                    // Render the tile.
                    graphics.DrawImage(tileMap.TileSet.Image, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
                }
            }
        }
    }
}
