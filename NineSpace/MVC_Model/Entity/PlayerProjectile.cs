using System.Drawing;
using NineSpace.Other;

namespace NineSpace.MVC_Model.Entities
{
    public class PlayerProjectile : IEntity
    {
        public Size Sz { get; }
        public Rectangle Rect { get; set; }

        public Point Pos { get; set; }
        public Point Delta { get; set; }
        public int Health { get; set; }
        public Point Velocity { get; set; }

        public PlayerProjectile(Point location, Point delta)
        {
            Sz = new Size(8, 8);
            Pos = location;
            Rect = new Rectangle(location, Sz);
            Delta = delta;
        }
    }
}
