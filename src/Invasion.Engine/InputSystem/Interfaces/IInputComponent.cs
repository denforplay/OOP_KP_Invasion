namespace Invasion.Engine.InputSystem.Interfaces
{
    /// <summary>
    /// Input component to read input
    /// </summary>
    /// <typeparam name="T">Returned type of answer on input</typeparam>
    public interface IInputComponent<out T>
    {
        /// <summary>
        /// Read value of input
        /// </summary>
        /// <returns>Input value</returns>
        T ReadValue();
    }
}