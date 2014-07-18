using System.Drawing;
using System.Windows.Forms;

namespace TribalWars.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Convert user entered movement keys (arrows, ...) to an XY modifier
    /// indicating how much to move the control in what direction
    /// </summary>
    public class KeyboardInputToMovementConverter
    {
        private const int Step = 5;
        private const int LargeStep = 20;

        private readonly int _justKeyStep;
        private readonly int _keyStepWithControl;
        private readonly Keys _keys;

        public KeyboardInputToMovementConverter(Keys keys, int justKeyStep = Step, int keyStepWithControl = LargeStep)
        {
            _justKeyStep = justKeyStep;
            _keyStepWithControl = keyStepWithControl;
            _keys = keys;
        }

        /// <summary>
        /// Return the direction and amount to move
        /// or null if there was no keyboard input for movement
        /// </summary>
        public Point? GetKeyMove()
        {
            switch (_keys)
            {
                case Keys.Left:
                case Keys.NumPad4:
                    return new Point(-_justKeyStep, 0);

                case Keys.Right:
                case Keys.NumPad6:
                    return new Point(_justKeyStep, 0);

                case Keys.Up:
                case Keys.NumPad8:
                    return new Point(0, -_justKeyStep);

                case Keys.Down:
                case Keys.NumPad2:
                    return new Point(0, _justKeyStep);

                case Keys.Left | Keys.Control:
                case Keys.NumPad4 | Keys.Control:
                    return new Point(-_keyStepWithControl, 0);

                case Keys.Right | Keys.Control:
                case Keys.NumPad6 | Keys.Control:
                    return new Point(_keyStepWithControl, 0);

                case Keys.Up | Keys.Control:
                case Keys.NumPad8 | Keys.Control:
                    return new Point(0, -_keyStepWithControl);

                case Keys.Down | Keys.Control:
                case Keys.NumPad2 | Keys.Control:
                    return new Point(0, _keyStepWithControl);
            }

            return null;
        }
    }
}
