using System.Drawing;
using NineSpace.Other;

namespace NineSpace.MVC_Model.Entities
{
    public class Player : IPlayer
    {
        // constants
        public Size Sz { get; }
        public Rectangle Rect { get; set; }
        public (Point, Point)[] ProjectilePattern { get; }
        public int CooldownBorder { get; }
        public Point Velocity { get; set; }

        // flags
        public bool Up { get; set; }
        public bool Left { get; set; }
        public bool Down { get; set; }
        public bool Right { get; set; }
        public bool Shooting { get; set; }

        //variables
        public Point Pos { get; set; }
        public Point Delta { get; set; }
        public int Health { get; set; }
        public int Cooldown { get; set; }
        public int Speed { get; }


        public Player(Point location)
        {
            Sz = new Size(32, 32);
            Pos = location;
            Rect = new Rectangle(Pos, Sz);
            Delta = new Point();
            Speed = 4;
            CooldownBorder = 6;
            ProjectilePattern = new[]
            {
                (new Point(-12, 12), new Point(0, -24)),
                (new Point(12, -12), new Point(0, -24)),
                (new Point(36, 12), new Point(0, -24)),
            };
        }
    }
}