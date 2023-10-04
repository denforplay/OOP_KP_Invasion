using Invasion.Engine;

namespace Invasion.Models.Factories;

/// <summary>
/// Interface provides factory that creates object of T type
/// </summary>
/// <typeparam name="T">Type of created object</typeparam>
public interface IModelFactory<out T> where T : GameObject
{
    /// <summary>
    /// Method to create instance of T class
    /// </summary>
    /// <returns>Instance of T</returns>
    T Create();
}