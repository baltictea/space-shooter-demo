using System.Drawing;
using NineSpace.Other;

namespace NineSpace.MVC_Model.Entities
{
    public class Shooter : IEnemy
    {
        public Size Sz { get; }
        public Rectangle Rect { get; set; }
        public Point Pos { get; set; }
        public int Cooldown { get; set; }
        public int CooldownBorder { get; set; }
        public (Point, Point)[] ProjectilePattern { get; set; }
        public Point Delta { get; set; }
        public int Health { get; set; }
        public Point Velocity { get; set; }

        public Shooter(Point location)
        {
            Sz = new Size(32, 32);
            Pos = location;
            Rect = new Rectangle(Pos, Sz);
            Delta = new Point(0, 6);
            CooldownBorder = 32;
            ProjectilePattern = new[]
            {
                (new Point(12, 12), new Point(-4, 8)),
                (new Point(12, 12), new Point(0, 8)),
                (new Point(12, 12), new Point(4, 8)),
            };
            Health = 3;
        }
    }
}
