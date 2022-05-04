using Invasion.Engine.InputSystem.Interfaces;
using Invasion.Engine.Interfaces;
using System.Windows.Input;

namespace Invasion.Engine.InputSystem.InputComponents
{
    /// <summary>
    /// Represents class to read key from keyboard
    /// </summary>
    public class KeyButton : IInputComponent<bool>
    {
        private IInputProvider _inputProvider;
        private Key _key;

        /// <summary>
        /// Key button constructor
        /// </summary>
        /// <param name="inputProvider">Input provider</param>
        /// <param name="key">Keyboard key</param>
        public KeyButton(IInputProvider inputProvider, Key key)
        {
            _inputProvider = inputProvider;
            _key = key;
        }

        public bool ReadValue()
        {
            return _inputProvider.CheckKey(_key);
        }
    }
}
