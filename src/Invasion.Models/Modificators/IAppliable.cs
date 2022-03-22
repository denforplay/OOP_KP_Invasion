using Invasion.Engine;

namespace Invasion.Models.Modificators;

public interface IAppliable
{
    bool IsApplied { get; set; }
    void Apply(GameObject gameObject);
}