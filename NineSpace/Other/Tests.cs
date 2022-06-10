using System;
using System.Drawing;
using System.Linq;
using NineSpace.MVC_Model;
using NineSpace.MVC_Model.Entities;
using NUnit.Framework;

namespace NineSpace.Other
{
    public class Tests
    {
        [Test]
        public void IsCollisionWithEnemyKillsPlayer()
        {
            var model = new Model(640, 640, new Player(new Point(0,64)), false);
            model.Enemies.Add(new Fighter(new Point(0, 0), new Point(0, 8)));

            for (var i = 0; i < 10; i++) 
                model.TickUpdate(this, EventArgs.Empty);

            Assert.AreEqual(true, model.GameOver);
        }

        [Test]
        public void IsCollisionWithWallKillsPlayer()
        {
            var model = new Model(640, 640, new Player(new Point(0, 64)), false);
            model.Walls.Add(new Wall4x1(new Point(0,0)));

            for (var i = 0; i < 10; i++) 
                model.TickUpdate(this, EventArgs.Empty);

            Assert.AreEqual(true, model.GameOver);
        }

        [Test]
        public void IsCollisionWithProjectileKillsPlayer()
        {
            var model = new Model(640, 640, new Player(new Point(0, 64)), false);
            model.EnemyProjectiles.Add(new EnemyProjectile(new Point(0, 64), new Point(0, 8)));

            for (var i = 0; i < 10; i++) 
                model.TickUpdate(this, EventArgs.Empty);

            Assert.AreEqual(true, model.GameOver);
        }

        [Test]
        public void IsCollisionWithProjectileKillsEnemy()
        {
            var model = new Model(640, 640, new Player(new Point(64, 64)), false);
            model.Enemies.Add(new Fighter(new Point(0, 64), Point.Empty));
            model.PlayerProjectiles.Add(new PlayerProjectile(new Point(0, 0), new Point(0, 8)));
            model.PlayerProjectiles.Add(new PlayerProjectile(new Point(0, 1), new Point(0, 8)));
            model.PlayerProjectiles.Add(new PlayerProjectile(new Point(0, 2), new Point(0, 8)));

            for (var i = 0; i < 10; i++)
                model.TickUpdate(this, EventArgs.Empty);

            Assert.AreEqual(false, model.Enemies.Any());
        }

        [Test]
        public void AreBoundsStopPlayer()
        {
            var model = new Model(640, 640, new Player(new Point(640, 640)), false);
            model.Player.Right = true;
            for (var i = 0; i < 10; i++)
                model.TickUpdate(this, EventArgs.Empty);

            Assert.AreEqual(false, model.Player.Pos.X > 640);
        }

        [Test]
        public void ArePlayerProjectilesDisappearWhenCollideWithWall()
        {
            var model = new Model(640, 640, new Player(Point.Empty), false);
            model.Walls.Add(new Wall4x1(new Point(0,16)));
            model.PlayerProjectiles.Add(new PlayerProjectile(new Point(0, 0), new Point(0, 8)));
            for (var i = 0; i < 10; i++)
                model.TickUpdate(this, EventArgs.Empty);

            Assert.AreEqual(false, model.PlayerProjectiles.Any());
        }

        [Test]
        public void AreEnemyProjectilesDisappearWhenCollideWithWall()
        {
            var model = new Model(640, 640, new Player(Point.Empty), false);
            model.Walls.Add(new Wall4x1(new Point(0, 16)));
            model.EnemyProjectiles.Add(new EnemyProjectile(new Point(0, 0), new Point(0, 8)));
            for (var i = 0; i < 10; i++)
                model.TickUpdate(this, EventArgs.Empty);

            Assert.AreEqual(false, model.PlayerProjectiles.Any());
        }

    }
}
