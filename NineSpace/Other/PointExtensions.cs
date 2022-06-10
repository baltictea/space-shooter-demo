using System.Drawing;

namespace NineSpace.Other
{
    public static class PointExtensions
    {
        public static Point Add(this Point first, Point second) 
            => new Point(first.X + second.X, first.Y + second.Y);
    }
}
