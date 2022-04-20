using Invasion.Engine.InputSystem.InputComponents;
using Invasion.Engine.InputSystem.Interfaces;
using Invasion.Engine.Interfaces;
using System.Numerics;
using System.Windows.Input;

namespace Invasion.Controller.Inputs;

public class WeaponInput : IInputComponent<Vector2>
{
    private IInputProvider _inputProvider;
    private KeyButton _leftButton;
    private KeyButton _rightButton;
    private KeyButton _shootKey;

    public WeaponInput(IInputProvider inputProvider, Key leftKey, Key rightKey, Key shootKey)
    {
        _inputProvider = inputProvider;
        _leftButton = new KeyButton(inputProvider, leftKey);
        _rightButton = new KeyButton(inputProvider, rightKey);
        _shootKey = new KeyButton(inputProvider, shootKey);
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