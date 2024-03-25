using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit; 

/// <summary>
/// The structure that defines a point.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SdlPoint {
    /// <summary>
    /// The X coordinate of the point.
    /// </summary>
    public int X;
    
    /// <summary>
    /// The Y coordinate of the point.
    /// </summary>
    public int Y;

    /// <summary>
    /// An <see cref="SdlPoint"/> that always references (0, 0).
    /// </summary>
    public static readonly SdlPoint Zero = new(0, 0);

    /// <summary>
    /// Defines a new point.
    /// </summary>
    /// <param name="x">The X coordinate of the point.</param>
    /// <param name="y">The Y coordinate of the point.</param>
    public SdlPoint(int x, int y) {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Defines a new point.
    /// </summary>
    /// <param name="point">An <see cref="SdlPoint"/> whose properties should be copied into the new point.</param>
    public SdlPoint(SdlPoint point) {
        X = point.X;
        Y = point.Y;
    }

    /// <summary>
    /// Deconstructs this <see cref="SdlPoint"/> into a tuple.
    /// </summary>
    /// <param name="point">The point to deconstruct.</param>
    /// <returns>A tuple representing this <see cref="SdlPoint"/> in the order: X, Y</returns>
    public static (int x, int y) Deconstruct(SdlPoint point) => (
        x: point.X,
        y: point.Y
    );

    public static bool operator ==(SdlPoint left, SdlPoint right) => left.Equals(right);
    
    public static bool operator !=(SdlPoint left, SdlPoint right) => !(left == right);

    /// <inheritdoc/>
    public override bool Equals(object? obj) {
        if(obj is not SdlPoint other) {
            return false;
        }

        return X == other.X && Y == other.Y;
    }

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <summary>
    /// Deconstructs this <see cref="SdlPoint"/> into a tuple.
    /// </summary>
    /// <returns>A tuple representing this <see cref="SdlPoint"/> in the order: X, Y</returns>
    public (int x, int y) Deconstruct() => Deconstruct(this);
    
    /// <summary>
    /// Determines whether or not an <see cref="SdlPoint"/> is equal to (0, 0).
    /// </summary>
    /// <param name="point">The <see cref="SdlPoint"/> to check.</param>
    /// <returns><c>true</c> if the passed <see cref="SdlPoint"/> is equal to (0, 0), otherwise <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(SdlPoint point) => point.X == 0 && point.Y == 0;

    /// <summary>
    /// Determines whether or not this <see cref="SdlPoint"/> is equal to (0, 0).
    /// </summary>
    /// <returns><c>true</c> if this <see cref="SdlPoint"/> is equal to (0, 0), otherwise <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero() => IsZero(this);
}
