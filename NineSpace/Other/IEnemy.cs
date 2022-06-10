using System.Drawing;

namespace NineSpace.Other
{
    public interface IEnemy : IEntity
    {
        int Cooldown { get; set; }
        int CooldownBorder { get; set; }
        (Point, Point)[] ProjectilePattern { get; set; }
    }
}
