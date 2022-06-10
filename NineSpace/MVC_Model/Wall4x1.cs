using System.Drawing;
using NineSpace.Other;

namespace NineSpace.MVC_Model
{
    internal class Wall4x1 : IWall
    {
        public Size Sz { get; }
        public Rectangle Rect { get; set; }
        public Point Pos { get; set; }
        public Point Delta { get; set; }
        public int Health { get; set; }
        public Point Velocity { get; set; }

        public Wall4x1(Point location)
        {
            Sz = new Size(256, 64);
            Pos = location;
            Rect = new Rectangle(location, Sz);
            Delta = new Point(0, 4);
        }
    }
}
