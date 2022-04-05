using Invasion.Engine.InputSystem;
using Invasion.Engine.InputSystem.InputComponents;
using SharpDX;
using SharpDX.DirectInput;

namespace Invasion.Controller.Inputs;

public class WeaponInput
{
    private DInput _dInput;
    private KeyButton _leftButton;
    private KeyButton _rightButton;
    private KeyButton _shootKey;

    public DInput DInput => _dInput;
    public WeaponInput(DInput input, Key leftKey, Key rightKey, Key shootKey)
    {
        _dInput = input;
        _leftButton = new KeyButton(input, leftKey);
        _rightButton = new KeyButton(input, rightKey);
        _shootKey = new KeyButton(input, shootKey);
    }

    public bool ReadShoot() => _shootKey.ReadValue();

    public Vector2 ReadValue()
    {
        Vector2 value = Vector2.Zero;
        if (_leftButton.ReadValue())
            value += new Vector2(-1, 0);

        if (_rightButton.ReadValue())
            value += new Vector2(1, 0);

        return value;
    }
}