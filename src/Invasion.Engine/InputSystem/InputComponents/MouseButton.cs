using Invasion.Engine.InputSystem.Interfaces;

namespace Invasion.Engine.InputSystem.InputComponents;

public enum MouseKey
{
    Left=0,
    Wheel=1,
    Right=2
}

public class MouseButton: IInputComponent<bool>
{
    private DInput _input;
    private MouseKey _key;

    public MouseButton(DInput input, MouseKey key)
    {
        _input = input;
        _key = key;
    }
    
    public bool ReadValue()
    {
        return _input.MouseState.Buttons[(int) _key];
    }
}