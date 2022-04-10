using SimpleTileGame.Resources; // Strings

using System; // TimeSpan
using System.Drawing; // Point, Size, Graphics, Rectangle, Font, Brush
using System.Threading.Tasks; // Task
using System.Windows.Forms; // Form
using System.Windows.Input; // Keyboard, Key, KeyState

namespace SimpleTileGame.Model // TileMap, Tile
{
    /// <summary>
    /// Contains static methods allowing for the drawing of <see cref="TileMap"/> and <see cref="Sprite"/> objects.
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

    /// <summary>
    /// Represents a simple game context.
    /// </summary>
    public sealed class GameContext
    {
        public GameContext(TileMap map, Sprite playerSprite, Rectangle view)
        {
            if (map == TileMap.Empty)
                throw new ArgumentNullException(nameof(map), Strings.GameContextTileMapMustNotBeNull);
            if (playerSprite == Sprite.Empty)
                throw new ArgumentNullException(nameof(map), Strings.GameContextPlayerSpriteMustNotBeNull);
            if (view == Rectangle.Empty)
                throw new ArgumentNullException(nameof(map), Strings.GameContextViewMustNotBeNull);

            Map = map;
            PlayerSprite = playerSprite;
            View = view;
            Form = CreateGameForm();
        }

        public TileMap Map { get; private set; }
        public Sprite PlayerSprite { get; private set; }
        public Rectangle View { get; private set; }
        public GameForm Form { get; }
        public DateTime StartTime { get; private set; } = default;
        public TimeSpan GameTime { get; private set; } = default;
        //TODO: Move to Sprite.
        public ushort Speed { get; private set; } = 0;
        public bool IsRunning { get; private set; } = false;
        public uint Frames { get; private set; } = 0;

        public async void Start()
        {
            IsRunning = true;
            GameTime = new(0);
            StartTime = DateTime.Now;
            DateTime prevTime = StartTime;

            while (IsRunning)
            {
                TimeSpan elapsedTime = DateTime.Now - prevTime;
                GameTime += elapsedTime;
                Update(elapsedTime);
                await Task.Delay(8);
            }
        }

        public void Stop()
        {
            IsRunning = false;
            Map = TileMap.Empty;
            PlayerSprite = Sprite.Empty;
            View = Rectangle.Empty;
            Form?.Close();
        }

        public void Update(TimeSpan gameTime)
        {
            // Gametime elapsed
            double gameTimeElapsed = gameTime.TotalMilliseconds / 1000;
            // Calculate sprite movement based on Sprite Velocity and GameTimeElapsed
            int moveDistance = (int)(Speed * gameTimeElapsed);

            Rectangle newView = View;
            // Move player sprite, when Arrow Keys are pressed on Keyboard
            if ((Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0)
            {
                newView.X += moveDistance;
            }
            else if ((Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0)
            {
                newView.X -= moveDistance;
            }
            else if ((Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0)
            {
                newView.Y -= moveDistance;
            }
            else if ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
            {
                newView.Y += moveDistance;
            }
            View = newView;
            Form.Refresh();
            Frames++;
        }

        public void Draw(Graphics graphics)
        {
            // Draw the tile map.
            GameGraphics.DrawTileMap(graphics, View, Map);

            // Draw the player sprite.
            GameGraphics.DrawSprite(graphics, PlayerSprite);

            // Define the text we are going to display; in this case, we show certain performance values.
            string text = $"Game Time: {GameTime}\r\nMap Size: {Map.Size.Width},{Map.Size.Height}\r\nView Area: {View.X}, {View.Y}, {View.Width}, {View.Height}\r\nPlayer Location: {PlayerSprite.Bounds.X},{PlayerSprite.Bounds.Y}";

            // Draw the text to the view.
            //TODO: Verify the location of the text.
            GameGraphics.DrawText(graphics, text, FontFamily.GenericMonospace, Color.WhiteSmoke, View.Location);
        }

        private GameForm CreateGameForm()
        {
            return new GameForm(View.Size, (graphics) => Draw(graphics));
        }
    }

    public delegate void GameOnDrawOperation(Graphics graphics);
}
