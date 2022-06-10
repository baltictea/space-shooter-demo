using System;
using System.Drawing;
using System.Windows.Forms;
using NineSpace.MVC_Controller;
using NineSpace.MVC_Model;
using NineSpace.MVC_Model.Entities;
using NineSpace.VIEW;

namespace NineSpace
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new StartForm());
        }

        public static void RunGame(int interval, bool isNoShootMode)
        {
            var model = new Model(720, 720, new Player(new Point(344, 600)), isNoShootMode);
            var form = new MainForm(model, new Controller(model), interval);
            form.Show();
        }
    }
}
