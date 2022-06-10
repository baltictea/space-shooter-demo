using System;
using System.Collections.Generic;
using System.Drawing;
using NineSpace.MVC_Model.Entities;

namespace NineSpace.MVC_Model
{
    public class Script
    {
        private readonly Model _model;
        public readonly List<(int, Action)> TickToAction;
        public Script (Model model)
        {
            _model = model;
            TickToAction = new List<(int, Action)>
            {
                (50, AddWall(0)),
                (50, AddWall(464)),

                (100, AddSentry(112)),
                (100, AddSentry(576)),

                (200, AddFighter(144)),
                (210, AddFighter(244)),
                (220, AddFighter(344)),
                (230, AddFighter(444)),
                (250, AddFighter(544)),
                (260, AddFighter(444)),
                (270, AddFighter(344)),
                (280, AddFighter(244)),
                (290, AddFighter(144)),

                (400, AddFighter(144)),
                (400, AddFighter(244)),
                (400, AddFighter(344)),
                (400, AddFighter(444)),
                (400, AddFighter(544)),

                (450, AddWall(0)),
                (450, AddWall(464)),
                (450, AddSentry(344)),

                (550, AddWall(232)),
                (550, AddSentry(32)),
                (550, AddSentry(672)),

                (600, AddWall(0)),
                (600, AddWall(464)),
                (600, AddSentry(344)),

                (650, AddSentry(112)),
                (650, AddSentry(576)),

                (700, AddShooter(344)),

                (800, AddShooter(327)),
                (800, AddShooter(377)),

                (900, AddShooter(252)),
                (900, AddShooter(344)),
                (900, AddShooter(452)),

                (950, AddSentry(112)),
                (950, AddSentry(576)),

                (1000, AddWall(64)),
                (1000, AddWall(400)),
                (1000, AddSentry(32)),
                (1000, AddSentry(672)),
                
                (1010, AddFighter(344)),
                (1020, AddFighter(344)),
                (1030, AddFighter(344)),
                (1040, AddFighter(344)),
                (1050, AddFighter(344)),

                (1050, AddSentry(112)),
                (1050, AddSentry(576)),
            };
        }

        private Action AddFighter(int x)
        {
            return () => _model.Enemies.Add(
                new Fighter(new Point(x, 0), new Point(0, 1)));
        }

        private Action AddWall(int x)
        {
            return () => _model.Walls.Add(
                new Wall4x1(new Point(x, 0)));
        }

        private Action AddSentry(int x)
        {
            return () => _model.Enemies.Add(
                new Sentry(new Point(x, 0)));
        }

        private Action AddShooter(int x)
        {
            return () => _model.Enemies.Add(
                new Shooter(new Point(x, 0)));
        }
    }
}
