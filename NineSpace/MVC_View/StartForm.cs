using System.Drawing;
using System.Windows.Forms;

namespace NineSpace.VIEW
{
    public sealed class StartForm : Form
    {
        public StartForm()
        {
            KeyPreview = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            Width = 240;
            Height = 180;

            StartPosition = FormStartPosition.CenterScreen;

            var noShoot = new Button
            {
                Text = "Friendly mode",
                Location = new Point(64, 16),
                Size = new Size(96, 48),
            };
            noShoot.Click += (_, e) => StartGame(31, true);
            Controls.Add(noShoot);

            var normal = new Button
            {
                Text = "Normal mode",
                Location = new Point(64, 76),
                Size = new Size(96, 48),
            };
            normal.Click += (_, e) => StartGame(31, false);
            Controls.Add(normal);
            Closing += (_, e) => Application.Exit();
        }

        private void StartGame(int interval, bool isFriendly)
        {
            Hide();
            Program.RunGame(interval, isFriendly);
        }
    }
}
