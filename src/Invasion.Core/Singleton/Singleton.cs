namespace Invasion.Core.Singleton
{
    /// <summary>
    /// Represent singleton
    /// </summary>
    /// <typeparam name="T">Type of single object</typeparam>
    public class Singleton<T> where T : new()
    {
        private static T _instance;

        /// <summary>
        /// Return single instance
        /// </summary>
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
