using SimpleTileGame.Model;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SimpleTileGame
{
    public partial class Form1 : Form
    {
        private readonly TileMap world = new(30, 30);
        private Camera camera;
        private readonly int tilesize = 64;

        public Form1()
        {
            InitializeComponent();
            Dictionary<TerrainType, Rectangle> tilesetTerrainPositions = new();
            for (int tileTypeCounter = 0; tileTypeCounter < 5; tileTypeCounter++)
            {
                tilesetTerrainPositions[(TerrainType)tileTypeCounter] = new Rectangle(tilesize * tileTypeCounter,0, tilesize, tilesize);
            }

            camera = new Camera(world, GetTileset(), tilesetTerrainPositions, tilesize, 20, 15);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateInfoLabel();
        }

        private static Bitmap GetTileset()
        {
            var runnintProgramPath = Directory.GetCurrentDirectory();
            var bitmapPath = Path.Combine(runnintProgramPath, "graphics/tileset_64px.png");
            return new Bitmap(bitmapPath);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            camera.Render(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    camera.MoveLeft();
                    break;
                case Keys.Up:
                    camera.MoveUp();
                    break;
                case Keys.Right:
                    camera.MoveRight();
                    break;
                case Keys.Down:
                    camera.MoveDown();
                    break;
            }
            Refresh();
            UpdateInfoLabel();
        }

        private void UpdateInfoLabel()
        {
            lblInfo.Text = $"Top left camera tile: {camera.TopLeftTile} rendering {camera.ColumnsToRender} x {camera.RowsToRender} in a {world.Columns}x {world.Rows} world  ";
        }
    }
}