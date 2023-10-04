namespace Invasion.Engine.Interfaces
{
    /// <summary>
    /// Describes collision record
    /// </summary>
    public interface IRecord
    {
        bool IsTarget((object, object) pair);
    }
}
