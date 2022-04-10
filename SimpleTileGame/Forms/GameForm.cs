using SimpleTileGame.Model; // Camera

using System; // ArgumentNullException, EventArgs
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms; // Form, PaintEventArgs, KeyEventArgs, Keys

namespace SimpleTileGame
{
    public partial class GameForm : Form
    {
        private readonly GameOnDrawOperation drawOp;

        internal GameForm(Size clientSize, GameOnDrawOperation drawOp)
        {
            this.drawOp = drawOp ?? throw new ArgumentNullException(nameof(drawOp), Strings.GameFormDrawOperationCannotBeNull);
            ClientSize = clientSize;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            drawOp?.Invoke(e.Graphics);
        }

        #region WindowsForms Designer Support
        private IContainer components;

        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "GameForm";
            Text = "SimpleTileGame | Use Arrow Keys to Move";
            ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}