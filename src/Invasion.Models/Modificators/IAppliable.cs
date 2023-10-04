using Invasion.Engine;

namespace Invasion.Models.Modificators;

/// <summary>
/// Interface provides 
/// </summary>
public interface IAppliable
{
    /// <summary>
    /// Returns true if that thing is was applied
    /// </summary>
    bool IsApplied { get; set; }

    /// <summary>
    /// Method to apply this thing on gameobject
    /// </summary>
    /// <param name="gameObject">Gameobject on what thing is applied</param>
    void Apply(GameObject gameObject);
}