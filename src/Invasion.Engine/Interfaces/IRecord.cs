namespace Invasion.Engine.Interfaces
{
    public interface IRecord
    {
        bool IsTarget((object, object) pair);
    }
}
