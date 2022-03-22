using Invasion.Engine;

namespace Invasion.Models.Factories;

public interface IModelFactory<out T> where T : GameObject
{
    T Create();
}