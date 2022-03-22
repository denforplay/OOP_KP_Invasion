using System.Diagnostics;

namespace Invasion.Engine
{
    public class Time
    {
        public static float DeltaTime;
        public static float TimeScale = 1;


        private static Stopwatch _watch;
        private static long _previousTicks;

        public static void Start()
        {
            _watch = new Stopwatch();
            _watch.Start();
            _previousTicks = _watch.Elapsed.Ticks;
        }

        public static void Update()
        {
            long ticks = _watch.Elapsed.Ticks;
            DeltaTime = (float)(ticks - _previousTicks) / TimeSpan.TicksPerSecond;
            _previousTicks = ticks;
        }
    }
}
