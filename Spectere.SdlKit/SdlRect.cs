using System.Runtime.InteropServices;

namespace Spectere.SdlKit;

/// <summary>
/// A rectangle, with the origin at the upper-left.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct SdlRect {
    /// <summary>
    /// The upper-left corner of this rectangle. This is functionally equivalent to the <see cref="X"/> and
    /// <see cref="Y"/> fields and is provided for convenience.
    /// </summary>
    [FieldOffset(0)]
    public SdlPoint Position = default;

    /// <summary>
    /// The X coordinate of the upper-left point of the rectangle.
    /// </summary>
    [FieldOffset(0)]
    public int X = default;

    /// <summary>
    /// The Y coordinate of the upper-left point of the rectangle.
    /// </summary>
    [FieldOffset(4)]
    public int Y = default;

    /// <summary>
    /// The size of this rectangle, represented as an <see cref="SdlPoint"/>. This is functionally equivalent to the
    /// <see cref="Width"/> and <see cref="Height"/> fields and is provided for convenience.
    /// </summary>
    [FieldOffset(8)]
    public SdlPoint Size = default;
    
    /// <summary>
    /// The width of the rectangle.
    /// </summary>
    [FieldOffset(8)]
    public int Width = default;
    
    /// <summary>
    /// The height of the rectangle.
    /// </summary>
    [FieldOffset(12)]
    public int Height = default;

    /// <summary>
    /// Defines a new rectangle.
    /// </summary>
    /// <param name="x">The upper-left X coordinate of the new rectangle.</param>
    /// <param name="y">The upper-left Y coordinate of the new rectangle.</param>
    /// <param name="width">The width of the new rectangle.</param>
    /// <param name="height">The height of the new rectangle.</param>
    public SdlRect(int x, int y, int width, int height) {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Defines a new rectangle.
    /// </summary>
    /// <param name="origin">An <see cref="SdlPoint"/> representing the upper-left point of the rectangle.</param>
    /// <param name="width">The width of the new rectangle.</param>
    /// <param name="height">The height of the new rectangle.</param>
    public SdlRect(SdlPoint origin, int width, int height) {
        Position = origin;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Defines a new rectangle.
    /// </summary>
    /// <param name="origin">An <see cref="SdlPoint"/> representing the upper-left point of the rectangle, in
    /// screen-space coordinates.</param>
    /// <param name="size">An <see cref="SdlPoint"/> representing the size of the rectangle.</param>
    public SdlRect(SdlPoint origin, SdlPoint size) {
        Position = origin;
        Size = size;
    }

    /// <summary>
    /// Deconstructs this <see cref="SdlRect"/> into a tuple.
    /// </summary>
    /// <param name="rect">The rectangle to deconstruct.</param>
    /// <returns>A tuple representing this <see cref="SdlRect"/> in the order: X, Y, Width, Height</returns>
    public static (int x, int y, int width, int height) Deconstruct(SdlRect rect) => (
        x: rect.X,
        y: rect.Y,
        width: rect.Width,
        height: rect.Height
    );

    /// <summary>
    /// Deconstructs this <see cref="SdlRect"/> into a tuple.
    /// </summary>
    /// <returns>A tuple representing this <see cref="SdlRect"/> in the order: X, Y, Width, Height</returns>
    public (int x, int y, int width, int height) Deconstruct() => Deconstruct(this);

    /// <summary>
    /// Defines a new rectangle based on two points in space.
    /// </summary>
    /// <param name="x1">The X coordinate of the rectangle's upper-left point. This can be any point in 2D space.</param>
    /// <param name="y1">The y coordinate of the rectangle's upper-left point. This can be any point in 2D space.</param>
    /// <param name="x2">The X coordinate of the rectangle's bottom-right point. This can be any point in 2D space.</param>
    /// <param name="y2">The Y coordinate of the rectangle's bottom-right point. This can be any point in 2D space.</param>
    /// <returns>A new <see cref="SdlRect"/> instance.</returns>
    public static SdlRect FromScreenSpace(int x1, int y1, int x2, int y2) => new(
        x: x1 < x2 ? x1 : x2,
        y: y1 < y2 ? y1 : y2,
        width: Math.Abs(x2 - x1),
        height: Math.Abs(y2 - y1)
    );

    /// <summary>
    /// Defines a new rectangle based on two points in space.
    /// </summary>
    /// <param name="point1">An <see cref="SdlPoint"/> containing the new rectangle's upper-left point. This can be any
    /// point in 2D space.</param>
    /// <param name="point2">An <see cref="SdlPoint"/> containing the new rectangle's bottom-right point. This can be any
    /// point in 2D space.</param>
    /// <returns>A new <see cref="SdlRect"/> instance.</returns>
    public static SdlRect FromScreenSpace(SdlPoint point1, SdlPoint point2) => new(
        x: point1.X < point2.X ? point1.X : point2.X,
        y: point1.Y < point2.Y ? point1.X : point2.Y,
        width: Math.Abs(point2.X - point1.X),
        height: Math.Abs(point2.Y - point1.Y)
    );
}
