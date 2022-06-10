using System.Drawing;

namespace NineSpace.Other
{
    public interface IEntity
    {
        Size Sz { get; }
        Rectangle Rect { get; set; }
        Point Pos { get; set; }
        Point Delta { get; set; }
        int Health { get; set; }
        Point Velocity { get; }
    }
}
