namespace Spectere.SdlKit;

/// <summary>
/// Defines an amount of padding, in pixels.
/// </summary>
public struct Padding {
    /// <summary>
    /// The amount of padding on the bottom of an object, in pixels.
    /// </summary>
    public int Bottom;

    /// <summary>
    /// The amount of padding to the left of an object, in pixels.
    /// </summary>
    public int Left;
    
    /// <summary>
    /// The amount of padding to the right of an object, in pixels.
    /// </summary>
    public int Right;
    
    /// <summary>
    /// The amount of padding on the top of an object, in pixels.
    /// </summary>
    public int Top;

    public static bool operator ==(Padding left, Padding right) =>
        left.Left == right.Left
        && left.Right == right.Right
        && left.Top == right.Top
        && left.Bottom == right.Bottom;

    public static bool operator !=(Padding left, Padding right) => !(left == right);
    
    /// <summary>
    /// Initializes a new <see cref="Padding"/> structure.
    /// </summary>
    public Padding() {}

    /// <summary>
    /// Initializes a new <see cref="Padding"/> structure.
    /// </summary>
    /// <param name="horizontal">The amount of horizontal padding, in pixels.</param>
    /// <param name="vertical">The amount of vertical padding, in pixels.</param>
    public Padding(int horizontal, int vertical) {
        Left = Right = horizontal;
        Top = Bottom = vertical;
    }

    /// <summary>
    /// Initializes a new <see cref="Padding"/> structure.
    /// </summary>
    /// <param name="left">The amount of left padding, in pixels.</param>
    /// <param name="right">The amount of right padding, in pixels.</param>
    /// <param name="top">The amount of top padding, in pixels.</param>
    /// <param name="bottom">The amount of bottom padding, in pixels.</param>
    public Padding(int left, int right, int top, int bottom) {
        Left = left;
        Right = right;
        Top = top;
        Bottom = bottom;
    }

    /// <summary>
    /// Compares two <see cref="Padding"/> structures for equality.
    /// </summary>
    /// <param name="other">The <see cref="Padding"/> that should be compared to this instance.</param>
    /// <returns><c>true</c> if the structures are equal, otherwise <c>false</c>.</returns>
    public bool Equals(Padding other) => this == other;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Padding other && Equals(other);
    
    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Bottom, Left, Right, Top);
}
