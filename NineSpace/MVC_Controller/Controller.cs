using System.Windows.Forms;
using NineSpace.MVC_Model;

namespace NineSpace.MVC_Controller
{
    public class Controller
    {
        private readonly Model _model;
        public Controller(Model model)
        {
            _model = model;
        }

        public void HandleKey(Keys e, bool down)
        {
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (e)
            {
                case Keys.Up:
                    _model.Player.Up = down;
                    break;
                case Keys.Left:
                    _model.Player.Left = down;
                    break;
                case Keys.Down:
                    _model.Player.Down = down;
                    break;
                case Keys.Right:
                    _model.Player.Right = down;
                    break;
                case Keys.Z:
                    if (!_model.NoShootMode)
                        _model.Player.Shooting = down;
                    break;
                case Keys.C:
                    _model.GameOver = true;
                    break;
            }
        }
    }
}
