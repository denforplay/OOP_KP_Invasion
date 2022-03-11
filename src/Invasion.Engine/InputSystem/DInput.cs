﻿using SharpDX;
using SharpDX.DirectInput;
using SharpDX.Windows;

namespace Invasion.Engine.InputSystem
{
    public class DInput
    {
        private RenderForm _renderForm;
        
        private DirectInput _directInput;
        private Keyboard _keyboard;
        private KeyboardState _keyboardState;
        public KeyboardState KeyboardState { get => _keyboardState; }
        private bool _keyboardUpdated;
        public bool KeyboardUpdated { get => _keyboardUpdated; }
        private bool _keyboardAcquired;

        private Mouse _mouse;
        private MouseState _mouseState;
        public MouseState MouseState { get => _mouseState; }
        public Mouse Mouse => _mouse;
        private bool _mouseUpdated;
        public bool MouseUpdated { get => _mouseUpdated; }
        private bool _mouseAcquired;
        public DInput(RenderForm renderForm)
        {
            _directInput = new DirectInput();

            _keyboard = new Keyboard(_directInput);
            _keyboard.Properties.BufferSize = 16;
            _keyboard.SetCooperativeLevel(renderForm.Handle, CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);
            AcquireKeyboard();
            _keyboardState = new KeyboardState();

            _mouse = new Mouse(_directInput);
            _mouse.Properties.AxisMode = DeviceAxisMode.Relative;
            _mouse.SetCooperativeLevel(renderForm.Handle, CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);
            AcquireMouse();
            _mouseState = new MouseState();
            _renderForm = renderForm;
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

        private void AcquireMouse()
        {
            try
            {
                _mouse.Acquire();
                _mouseAcquired = true;
            }
            catch (SharpDXException e)
            {
                if (e.ResultCode.Failure)
                    _mouseAcquired = false;
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

        public void UpdateMouseState()
        {
            if (!_mouseAcquired) AcquireMouse();

            ResultDescriptor resultCode = ResultCode.Ok;
            try
            {
                _mouse.GetCurrentState(ref _mouseState);
                _mouseUpdated = true;
            }
            catch (SharpDXException e)
            {
                resultCode = e.Descriptor;
                _mouseUpdated = false;
            }

            if (resultCode == ResultCode.InputLost || resultCode == ResultCode.NotAcquired)
                _mouseAcquired = false;
        }

        public void Dispose()
        {
            Utilities.Dispose(ref _mouse);
            Utilities.Dispose(ref _keyboard);
            Utilities.Dispose(ref _directInput);
        }
    }
}
