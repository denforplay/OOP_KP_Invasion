namespace Invasion.Engine.Interfaces
{
    /// <summary>
    /// Describes view functionality
    /// </summary>
    public interface IView : IDisposable
    {
        /// <summary>
        /// Update view
        /// </summary>
        void Update();
    }
}