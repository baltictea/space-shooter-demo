using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using NineSpace.MVC_Controller;
using NineSpace.MVC_Model;
using NineSpace.Properties;
using Timer = System.Windows.Forms.Timer;

namespace NineSpace.VIEW
{
    public sealed class MainForm : Form
    {
        private Model _model;
        private Controller _controller;
        private readonly Graphics _graphics;

        private readonly SoundPlayer _soundPlayer;
        private readonly GraphicPack _graphicPack;
        private readonly Timer _ticker;

        public MainForm(Model model, Controller controller, int tickInterval)
        {
            KeyPreview = true;
            SetStyle(ControlStyles.DoubleBuffer 
                     | ControlStyles.AllPaintingInWmPaint 
                     | ControlStyles.UserPaint 
                     | ControlStyles.Opaque, true);

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;

            Width = model.Width + 256;
            Height = model.Height + 39;

            StartPosition = FormStartPosition.CenterScreen;

            _model = model;
            _controller = controller;
            _graphics = CreateGraphics();
            _graphicPack = new GraphicPack();
            _ticker = new Timer { Interval = tickInterval, Enabled = true };

            _soundPlayer = new SoundPlayer(Resources.shoot);

            KeyDown += (_, e) => _controller.HandleKey(e.KeyCode, true);
            KeyUp += (_, e) => _controller.HandleKey(e.KeyCode, false);

            _ticker.Tick += _model.TickUpdate;
            _ticker.Tick += GraphicsUpdate;
            _ticker.Tick += GameOverCheck;
        }

        private void GraphicsUpdate(object sender, EventArgs e)
        {
            _graphics.Clear(Color.Black);
            DrawEntitiesAndPlayer();
            DrawProjectiles();
            DrawUI();
            PlaySounds();
        }

        private void PlaySounds()
        {
            if (_model.Player.Cooldown == _model.Player.CooldownBorder - 1)
                _soundPlayer.Play();
        }

        private void GameOverCheck(object sender, EventArgs e)
        {
            if (_model.GameOver)
            {
                _ticker.Stop();
                _soundPlayer.Stop();
                _model = null;
                _controller = null;
                Close();
                new StartForm().Show();
            }
        }

        private void DrawEntitiesAndPlayer()
        {
            _graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(720, 0, 8, Height));
            
            foreach (var entity in _model.Enemies)
                _graphics.DrawImageUnscaled(_graphicPack[entity], entity.Pos);

            foreach (var entity in _model.Walls)
                _graphics.DrawImageUnscaled(_graphicPack[entity], entity.Pos);

            _graphics.DrawImageUnscaled(_graphicPack[_model.Player], _model.Player.Pos);
        }

        private void DrawProjectiles()
        {
            foreach (var projectile in _model.PlayerProjectiles)
                _graphics.DrawImageUnscaled(_graphicPack[projectile], projectile.Pos);
            foreach (var projectile in _model.EnemyProjectiles)
                _graphics.DrawImageUnscaled(_graphicPack[projectile], projectile.Pos);
        }

        // debug
        private readonly Font _font = new Font(FontFamily.GenericSansSerif, 20f, FontStyle.Bold);
        private void DrawUI()
        {
            ////debug
            //_graphics.DrawString($"Score: {_model.Score}", _font,
            //    new SolidBrush(Color.Red), new PointF(Width - 250, 50));
            //_graphics.DrawString($"_scriptIndex: {_model._scriptIndex}", _font,
            //    new SolidBrush(Color.Red), new PointF(Width - 250, 80));
            //_graphics.DrawString($"CurrentTick: {_model.CurrentTick}", _font,
            //    new SolidBrush(Color.Red), new PointF(Width - 250, 110));
            ////
            _graphics.DrawString($"Health: {_model.Player.Health}", _font,
                new SolidBrush(Color.White), new PointF(Width - 250, 140));
            _graphics.DrawString("Press C to quit", _font,
                new SolidBrush(Color.White), new PointF(Width - 250, 170));
        }
    }
}