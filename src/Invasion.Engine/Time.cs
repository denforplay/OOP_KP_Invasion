using System.Diagnostics;

namespace Invasion.Engine
{
    public class Time
    {
        public static float DeltaTime;
        public static float TimeScale = 1;

        private static Stopwatch _watch;
        private static long _previousTicks;
        public static float FixedDeltaTime = 1/60f;

        public static void Start()
        {
            _watch = new Stopwatch();
            _watch.Start();
            _previousTicks = _watch.Elapsed.Ticks;
        }

        public static bool Update()
        {
            long ticks = _watch.Elapsed.Ticks;
            DeltaTime += (float)(ticks - _previousTicks) / TimeSpan.TicksPerSecond;
            _previousTicks = ticks;
            if (DeltaTime > FixedDeltaTime)
            {
                DeltaTime -= FixedDeltaTime;
                return true;
            }

            return false;
        }
    }
}
