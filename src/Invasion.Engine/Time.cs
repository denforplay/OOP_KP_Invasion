using System.Diagnostics;

namespace Invasion.Engine
{
    public class Time
    {
        public static float DeltaTime;
        public static float TimeScale = 1;

        private static Stopwatch _watch;
        private static long _previousTicks;
        public static float FixedDeltaTime;

        public static void Start(float fps)
        {
            FixedDeltaTime = 1 / fps;
            _watch = new Stopwatch();
            _watch.Start();
            _previousTicks = _watch.Elapsed.Ticks;
        }

        public static bool Update()
        {
            if (TimeScale != 0)
            {
                long ticks = _watch.Elapsed.Ticks;
                DeltaTime += (float)(ticks - _previousTicks) / TimeSpan.TicksPerSecond;
                _previousTicks = ticks;
                if (DeltaTime > FixedDeltaTime)
                {
                    DeltaTime = 0;
                    return true;
                }
            }

            return false;
        }
    }
}
