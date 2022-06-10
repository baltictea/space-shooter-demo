using System.Drawing;

namespace NineSpace.Other
{
    public interface IPlayer : IEntity
    {
        //constants
        (Point, Point)[] ProjectilePattern { get; }
        int CooldownBorder { get; }

        //flags
        bool Up { get;set; }
        bool Left { get; set; }
        bool Down { get; set; }
        bool Right { get; set; }
        bool Shooting { get; set; }

        //variables
        int Cooldown { get; set; }
        int Speed { get; }
    }
}
