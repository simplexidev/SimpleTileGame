using System; // ArgumentNullException, IEquatable<T>, HashCode
using System.Drawing; // Point, Rectangle

namespace SimpleTileGame.Model
{
    /// <summary>
    /// Represents a sprite image.
    /// </summary>
    public readonly struct Sprite : IEquatable<Sprite>
    {
        /// <summary>
        /// Represents an uninitialized <see cref="Sprite"/> structure.
        /// </summary>
        public static readonly Sprite Empty = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> structure with the specified image.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="bounds"></param>
        public Sprite(string imagePath, Rectangle bounds)
        {
            ImagePath = imagePath;
            Image = new Bitmap(imagePath);
            Bounds = bounds;
        }

        /// <summary>
        /// Gets the absolute path of this <see cref="Sprite"/> structure's image.
        /// </summary>
        public string ImagePath { get; }

        /// <summary>
        /// Gets the image that this <see cref="Sprite"/> represents.
        /// </summary>
        public Bitmap Image { get; }

        /// <summary>
        /// Gets the bounds of this <see cref="Sprite"/>.
        /// </summary>
        /// <remarks>
        /// This property is in terms of pixels.
        /// </remarks>
        public Rectangle Bounds { get; }

        /// <summary>
        /// Performs drawing operations using the provided <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="graphics">The <see cref="Graphics"/> object used to perform the drawing operations.</param>
        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(Image, Bounds);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Sprite"/> is empty.
        /// </summary>
        public bool IsEmpty => this == Empty;

        /// <summary>
        /// Specifies whether this <see cref="Sprite"/> contains the same image and bounds as another
        /// <see cref="Sprite"/>.
        /// </summary>
        /// <param name="other">The <see cref="Sprite"/> to test for equality.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other"/> has the same values as this <see cref="Sprite"/>.
        /// </returns>
        public bool Equals(Sprite other)
        {
            return Image == other.Image && Bounds == other.Bounds;
        }

        /// <summary>
        /// Specifies whether this <see cref="Sprite"/> contains the same values as the specified <see cref="object"/>.
        /// </summary>
        /// <param name="other">The <see cref="object"/> to test for equality.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="obj"/> is a <see cref="Sprite"/> and has the same values as this
        /// <see cref="Sprite"/>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return obj is Sprite sprite && Equals(sprite);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="Sprite"/>.
        /// </summary>
        /// <returns>An integer value representing a hash value for this <see cref="Sprite"/>,</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(ImagePath, Bounds);
        }

        /// <summary>
        /// Converts this <see cref="Sprite"/> into a human-readable string.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this <see cref="Sprite"/>.</returns>
        public override string ToString()
        {
            return $"[{ImagePath}, {Bounds}";
        }

        /// <summary>
        /// Compares two <see cref="Sprite"/> structures. The result indicates whether the values of the two
        /// <see cref="Sprite"> structures are equal.
        /// </summary>
        /// <param name="left">A <see cref="Sprite"/> to compare.</param>
        /// <param name="right">A <see cref="Sprite"/> to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the values of <paramref name="left"/> and <paramref name="right"/> are equal;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(Sprite left, Sprite right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="Sprite"/> structures. The result indicates whether the values of the two
        /// <see cref="Sprite"> structures are unequal.
        /// </summary>
        /// <param name="left">A <see cref="Sprite"/> to compare.</param>
        /// <param name="right">A <see cref="Sprite"/> to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the values of <paramref name="left"/> and <paramref name="right"/> differ;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(Sprite left, Sprite right)
        {
            return !left.Equals(right);
        }
    }
}