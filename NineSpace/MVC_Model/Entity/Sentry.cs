using System.Drawing;
using NineSpace.Other;

namespace NineSpace.MVC_Model.Entities
{
    public class Sentry : IEnemy
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

        public Sentry(Point location)
        {
            Sz = new Size(32, 32);
            Pos = location;
            Rect = new Rectangle(Pos, Sz);
            Delta = new Point(0, 4);
            CooldownBorder = 32;
            ProjectilePattern = new[]
            {
                (new Point(12, 12), new Point(0, -2)),
                (new Point(12, 12), new Point(0, 10)),
                (new Point(12, 12), new Point(-6, 4)),
                (new Point(12, 12), new Point(6, 4)),
            };
            Health = 3;
        }
    }
}
