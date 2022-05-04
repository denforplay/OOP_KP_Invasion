namespace Invasion.Engine.Interfaces
{
    /// <summary>
    /// Describes live object
    /// </summary>
    public interface ILiveObject
    {
        /// <summary>
        /// Method calls on invoke object
        /// </summary>
        void OnInvoke();

        /// <summary>
        /// Method calls on start object
        /// </summary>
        void OnStart();

        /// <summary>
        /// Method calls to update object
        /// </summary>
        void OnUpdate();
    }
}
