using System;
using System.Collections.Generic;
using System.Drawing;
using NineSpace.MVC_Model;
using NineSpace.MVC_Model.Entities;
using NineSpace.Other;
using NineSpace.Properties;

namespace NineSpace.VIEW
{
    public class GraphicPack
    {
        private static Dictionary<Type, Bitmap> _dictionary;
        public GraphicPack()
        {
            _dictionary = new Dictionary<Type, Bitmap>
            {
                {typeof(Fighter), Resources.fighter},
                {typeof(Sentry), Resources.sentry},
                {typeof(Shooter), Resources.shooter},
                {typeof(Player), Resources.player},
                {typeof(PlayerProjectile), Resources.player_projectile},
                {typeof(EnemyProjectile), Resources.enemy_projectile},
                {typeof(Wall4x1), Resources._4x1wall}
            };
        }

        public Bitmap this[IEntity entity] => _dictionary[entity.GetType()];
    }
}
