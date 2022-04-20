using Invasion.Engine.Interfaces;
using SharpDX;
using SharpDX.DirectInput;
using SharpDX.Windows;
using System.Windows.Input;

namespace Invasion.Engine.InputSystem
{
    /// <summary>
    /// Represents directX input provider
    /// </summary>
    public class DirectXInputProvider : IInputProvider
    {
        private DirectInput _directInput;
        private SharpDX.DirectInput.Keyboard _keyboard;
        private KeyboardState _keyboardState;
        private bool _keyboardUpdated;
        private bool _keyboardAcquired;

        public DirectXInputProvider(RenderForm renderForm)
        {
            _directInput = new DirectInput();

            _keyboard = new SharpDX.DirectInput.Keyboard(_directInput);
            _keyboard.Properties.BufferSize = 16;
            AcquireKeyboard();
            _keyboardState = new KeyboardState();
        }

        private void AcquireKeyboard()
        {
            try
            {
                _keyboard.Acquire();
                _keyboardAcquired = true;
            }
            catch (SharpDXException e)
            {
                if (e.ResultCode.Failure)
                    _keyboardAcquired = false;
            }
        }

        public void UpdateKeyboardState()
        {
            if (!_keyboardAcquired) AcquireKeyboard();
            ResultDescriptor resultCode = ResultCode.Ok;
            try
            {
                _keyboard.GetCurrentState(ref _keyboardState);
                _keyboardUpdated = true;
            }
            catch (SharpDXException e)
            {
                resultCode = e.Descriptor;
                _keyboardUpdated = false;
            }

            if (resultCode == ResultCode.InputLost || resultCode == ResultCode.NotAcquired)
                _keyboardAcquired = false;
        }

        public void Dispose()
        {
            Utilities.Dispose(ref _keyboard);
            Utilities.Dispose(ref _directInput);
        }

        public bool CheckKey(System.Windows.Input.Key key)
        {
            if (_keyboardUpdated)
            {
                var sharpDxKey = Enum.Parse<SharpDX.DirectInput.Key>(key.ToString());
                if (_keyboard.GetCurrentState().IsPressed(sharpDxKey))
                    return true;
            }

            return false;
        }

        public void Update()
        {
            UpdateKeyboardState();
        }
    }
}
