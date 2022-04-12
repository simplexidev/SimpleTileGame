using SimpleTileGame.Model;
using SimpleTileGame.Resources;

using System;
using System.Drawing;
using System.Windows.Forms;

//TODO: Add in-line documentation.
namespace SimpleTileGame
{
    /// <summary>
    /// Represents a window that is associated with a <see cref="GameContext"/>.
    /// </summary>
    public class GameForm : Form
    {
        /// <summary>
        /// A delegate that represents the operation to run when a <see cref="GameContext"/> redraws this <see cref="GameForm"/>.
        /// </summary>
        private readonly GameDrawOperation drawOp;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameForm"/> class with the specified client size, draw operation, and title.
        /// </summary>
        /// <param name="clientSize">The size of the client area of the <see cref="Form"/>.</param>
        /// <param name="drawOp">Th draw operation to run when a <see cref="GameContext"/> redraws the <see cref="GameForm"/>.</param>
        /// <param name="title">The title text shown on the <see cref="Form"/>.</param>
        internal GameForm(Size clientSize, GameDrawOperation drawOp, string title) : base()
        {
            this.drawOp = drawOp ?? throw new ArgumentNullException(nameof(drawOp), Strings.GameFormDrawOperationMustNotBeNull);
            SuspendLayout();
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = clientSize;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = nameof(GameForm);
            Text = title;
            ResumeLayout(false);
        }

        /// <summary>
        /// Raises the <see cref="Control.Paint"/> event.
        /// </summary>
        /// <param name="e">Provides data for the <see cref="Control.Paint"/> event.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            drawOp?.Invoke(e.Graphics);
        }
    }
}