using SharpDX;
using SharpDX.DirectInput;
using SharpDX.Windows;

namespace Invasion.Engine.InputSystem
{
    public class DInput
    {
        private DirectInput _directInput;
        private Keyboard _keyboard;
        private KeyboardState _keyboardState;
        public KeyboardState KeyboardState { get => _keyboardState; }
        private bool _keyboardUpdated;
        public bool KeyboardUpdated { get => _keyboardUpdated; }
        private bool _keyboardAcquired;

        public DInput(RenderForm renderForm)
        {
            _directInput = new DirectInput();

            _keyboard = new Keyboard(_directInput);
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
    }
}
