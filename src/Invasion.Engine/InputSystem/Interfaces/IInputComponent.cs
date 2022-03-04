namespace Invasion.Engine.InputSystem.Interfaces
{
    public interface IInputComponent<out T>
    {
        T ReadValue();
    }
}