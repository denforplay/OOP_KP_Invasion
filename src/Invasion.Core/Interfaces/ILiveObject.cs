namespace Invasion.Core.Interfaces
{
    public interface ILiveObject
    {
        void OnInvoke();
        void OnStart();
        void OnUpdate();
        void OnEnable();
        void OnDisable();
    }
}
