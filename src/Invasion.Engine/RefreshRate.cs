namespace Invasion.Engine
{
    public class RefreshRate
    {
        private static int _refreshRate = 0;
        private int _counter;
        public static int Value => _refreshRate;

        public void Update()
        {
            _counter++;
        }
    }
}
