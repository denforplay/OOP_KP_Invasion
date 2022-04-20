namespace Invasion.Engine.Interfaces
{
    /// <summary>
    /// Describes live object
    /// </summary>
    public interface ILiveObject
    {
        void OnInvoke();
        void OnStart();
        void OnUpdate();
        void OnEnable();
        void OnDisable();
    }
}
