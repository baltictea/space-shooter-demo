using System.Drawing;
using NineSpace.Other;

namespace NineSpace.MVC_Model.Entities
{
    public class Fighter : IEnemy
    {
        public Size Sz { get; }
        public Rectangle Rect { get; set; }
        public Point Pos { get; set; }
        public Point Delta { get; set; }
        public int Health { get; set; }
        public Point Velocity { get; set; }
        public int Cooldown { get; set; }
        public int CooldownBorder { get; set; }
        public (Point, Point)[] ProjectilePattern { get; set; }

        public Fighter(Point location, Point velocity)
        {
            Sz = new Size(32, 32);
            Pos = location;
            Rect = new Rectangle(Pos, Sz);
            Delta = new Point(0, 0);
            Health = 3;
            Velocity = velocity;
        }
    }
}
