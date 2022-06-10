using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NineSpace.MVC_Model.Entities;
using NineSpace.Other;

namespace NineSpace.MVC_Model
{
    public class Model
    {
        // const
        public readonly int Width;
        public readonly int Height;
        public readonly bool NoShootMode;

        // var
        private int CurrentTick { get; set; }
        private int ScriptIndex { get; set; }
        private readonly Script _script;

        public readonly IPlayer Player;
        public readonly List<IEnemy> Enemies;
        public readonly List<IWall> Walls;
        public readonly List<PlayerProjectile> PlayerProjectiles;
        public readonly List<EnemyProjectile> EnemyProjectiles;

        // flags
        public bool GameOver { get; set; }

        public Model(int width, int height, IPlayer player, bool isNoShootMode)
        {
            Width = width;
            Height = height;

            Player = player;
            Enemies = new List<IEnemy>();
            Walls = new List<IWall>();
            PlayerProjectiles = new List<PlayerProjectile>();
            EnemyProjectiles = new List<EnemyProjectile>();
            _script = new Script(this);
            NoShootMode = isNoShootMode;
        }

        public void TickUpdate(object sender, EventArgs e)
        {
            UpdateWalls();
            UpdateEnemies();
            UpdateProjectiles();
            UpdatePlayer();
            UpdateCollisions();
            UpdateBorders();
            UpdateScript();
            CurrentTick++;
        }

        private void UpdateWalls()
        {
            foreach (var wall in Walls)
                UpdateLocation(wall);
        }

        private void UpdateEnemies()
        {
            foreach (var enemy in Enemies)
            {
                UpdateLocation(enemy);
                if (enemy.GetType() == typeof(Sentry))
                {
                    var sentry = (Sentry)enemy;
                    sentry.Cooldown--;
                    if (sentry.Cooldown <= 0)
                    {
                        foreach (var (offset, delta) in sentry.ProjectilePattern)
                            EnemyProjectiles.Add(new EnemyProjectile(sentry.Pos.Add(offset), delta));
                        sentry.Cooldown = sentry.CooldownBorder;
                    }
                }

                if (enemy.GetType() == typeof(Shooter))
                {
                    var shooter = (Shooter)enemy;
                    shooter.Cooldown--;
                    if (shooter.Cooldown <= 0)
                    {
                        foreach (var (offset, delta) in shooter.ProjectilePattern)
                            EnemyProjectiles.Add(new EnemyProjectile(shooter.Pos.Add(offset), delta));
                        shooter.Cooldown = shooter.CooldownBorder;
                    }
                }

            }
        }

        private void UpdateProjectiles()
        {
            foreach (var projectile in PlayerProjectiles)
                UpdateLocation(projectile);

            foreach (var projectile in EnemyProjectiles)
                UpdateLocation(projectile);
        }

        private void UpdatePlayer()
        {
            var playerDelta = new Point();
            // in bounds check
            if (Player.Up && Player.Pos.Y > Height / 2)
                playerDelta.Y = -Player.Speed * 2;
            if (Player.Left && Player.Pos.X > 0)
                playerDelta.X = -Player.Speed * 3;
            if (Player.Down && Player.Pos.Y < Height - 32)
                playerDelta.Y = Player.Speed * 2;
            if (Player.Right && Player.Pos.X < Width - 32)
                playerDelta.X = Player.Speed * 3;
            Player.Delta = playerDelta;

            if (Player.Shooting && Player.Cooldown <= 0)
            {
                foreach (var (offset, delta) in Player.ProjectilePattern)
                    PlayerProjectiles.Add(new PlayerProjectile(Player.Pos.Add(offset), delta));

                Player.Cooldown = Player.CooldownBorder;
            }

            UpdateLocation(Player);
            Player.Cooldown--;
        }

        private static void UpdateLocation(IEntity entity)
        {
            entity.Delta = entity.Delta.Add(entity.Velocity);
            entity.Pos = entity.Pos.Add(entity.Delta);
            entity.Rect = new Rectangle(entity.Pos, entity.Sz);
        }

        private void UpdateCollisions()
        {
            // playerproj - enemy
            for (var i = 0; i < PlayerProjectiles.Count; i++)
            {
                var proj = PlayerProjectiles[i];
                for (var j = 0; j < Enemies.Count; j++)
                {
                    var enemy = Enemies[j];
                    if (enemy.Rect.IntersectsWith(proj.Rect))
                    {
                        enemy.Health--;
                        PlayerProjectiles.Remove(proj);
                        if (enemy.Health <= 0)
                        {
                            Enemies.Remove(enemy);
                            break;
                        }
                    }
                }
            }
            // enemyproj - player
            foreach (var proj in EnemyProjectiles)
            {
                if (proj.Rect.IntersectsWith(Player.Rect))
                    GameOver = true;
            }

            // enemy - player
            foreach (var enemy in Enemies)
            {
                if (enemy.Rect.IntersectsWith(Player.Rect))
                    GameOver = true;
            }

            // wall - player
            foreach (var wall in Walls)
            {
                if (wall.Rect.IntersectsWith(Player.Rect))
                    GameOver = true;
            }
        }

        private void UpdateBorders()
        {
            for (var i = 0; i < EnemyProjectiles.Count; i++)
            {
                var proj = EnemyProjectiles[i];

                if (proj.Pos.X > Width
                    || proj.Pos.X < 0
                    || proj.Pos.Y > Height
                    || Walls.Any(wall => wall.Rect.IntersectsWith(proj.Rect)))
                    EnemyProjectiles.Remove(proj);
            }

            for (var i = 0; i < Enemies.Count; i++)
            {
                var enemy = Enemies[i];
                if (enemy.Pos.Y > Height)
                    Enemies.Remove(enemy);
            }

            for (var i = 0; i < PlayerProjectiles.Count; i++)
            {
                var proj = PlayerProjectiles[i];
                if (proj.Pos.X > Width
                    || proj.Pos.X < 0
                    || proj.Pos.Y < 0
                    || Walls.Any(wall => wall.Rect.IntersectsWith(proj.Rect)))
                    PlayerProjectiles.Remove(proj);
            }
        }

        private void UpdateScript()
        {
            while (ScriptIndex < _script.TickToAction.Count && CurrentTick == _script.TickToAction[ScriptIndex].Item1)
            {
                _script.TickToAction[ScriptIndex].Item2.Invoke();
                ScriptIndex++;
            }
        }
    }
}
