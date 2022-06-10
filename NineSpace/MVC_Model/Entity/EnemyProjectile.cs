using System.Drawing;
using NineSpace.Other;

namespace NineSpace.MVC_Model.Entities
{
    public class EnemyProjectile : IEntity
    {
        public Size Sz { get; }
        public Rectangle Rect { get; set; }

        public Point Pos { get; set; }
        public Point Delta { get; set; }
        public int Health { get; set; }
        public Point Velocity { get; set; }

        public EnemyProjectile(Point pos, Point delta)
        {
            Sz = new Size(8, 8);
            Pos = pos;
            Rect = new Rectangle(pos, Sz);
            Delta = delta;
        }
    }
}