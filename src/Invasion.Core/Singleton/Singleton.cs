namespace Invasion.Core.Singleton
{
    public class Singleton<T> where T : new()
    {
        private static T _instance;
        public static T GetInstance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }
}
