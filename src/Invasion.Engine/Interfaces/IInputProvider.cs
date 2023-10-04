using System.Windows.Input;

namespace Invasion.Engine.Interfaces
{
    /// <summary>
    /// Provides input functionality
    /// </summary>
    public interface IInputProvider : IDisposable
    {
        /// <summary>
        /// Check if key is pressed
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>True if key is pressed other returns false</returns>
        bool CheckKey(Key key);
        
        /// <summary>
        /// Update input state
        /// </summary>
        void Update();
    }
}
