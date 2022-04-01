using SimpleTileGame.Model; // Camera

using System; // ArgumentNullException, EventArgs
using System.Windows.Forms; // Form, PaintEventArgs, KeyEventArgs, Keys

namespace SimpleTileGame
{
    public partial class MainForm : Form
    {
        private readonly Camera camera;

        private MainForm()
        {
            InitializeComponent();
        }

        public MainForm(Camera camera)
        {
            this.camera = camera ?? throw new ArgumentNullException(nameof(camera), Strings.CameraCannotBeNull);
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            camera.Render(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateInfoLabel();
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
            lblInfo.Text = $"TopLeft: {camera.TopLeft} | ViewSize: {camera.ViewSize} | MapSize: {camera.Map.Size}";
        }
    }
}