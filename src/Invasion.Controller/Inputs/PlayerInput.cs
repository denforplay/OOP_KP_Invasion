using Invasion.Engine.InputSystem.InputComponents;
using Invasion.Engine.InputSystem.Interfaces;
using Invasion.Engine.Interfaces;
using System.Numerics;
using System.Windows.Input;

namespace Invasion.Controller.Inputs
{
    /// <summary>
    /// Represents player input
    /// </summary>
    public class PlayerInput : IInputComponent<Vector2>
    {
        private KeyButton _upButton;
        private KeyButton _downButton;
        private KeyButton _leftButton;
        private KeyButton _rightButton;

        /// <summary>
        /// Player input constructor
        /// </summary>
        /// <param name="inputProvider">Input provider</param>
        /// <param name="upKey">Up movement key</param>
        /// <param name="downKey">Down movement key</param>
        /// <param name="rightKey">Right movement key</param>
        /// <param name="leftKey">Left movement key</param>
        public PlayerInput(IInputProvider inputProvider, Key upKey, Key downKey, Key rightKey, Key leftKey)
        {
            _upButton = new KeyButton(inputProvider, upKey);
            _downButton = new KeyButton(inputProvider, downKey);
            _leftButton = new KeyButton(inputProvider, leftKey);
            _rightButton = new KeyButton(inputProvider, rightKey);
        }

        public Vector2 ReadValue()
        {
            Vector2 value = Vector2.Zero;
            if (_upButton.ReadValue())
                value += new Vector2(0, 1);

            if (_downButton.ReadValue())
                value += new Vector2(0, -1);

            if (_leftButton.ReadValue())
                value += new Vector2(-1, 0);

            if (_rightButton.ReadValue())
                value += new Vector2(1, 0);

            return value;
        }
    }
}
