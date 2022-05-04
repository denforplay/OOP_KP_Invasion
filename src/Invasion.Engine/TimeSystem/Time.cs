using System.Diagnostics;

namespace Invasion.Engine
{
    /// <summary>
    /// Represents class to work with time
    /// </summary>
    public class Time
    {
        public static float DeltaTime;
        public static float TimeScale = 1;

        private static Stopwatch _watch;
        private static long _previousTicks;
        public static float FixedDeltaTime;

        /// <summary>
        /// Start time
        /// </summary>
        /// <param name="fps">Fps</param>
        public static void Start(float fps)
        {
            FixedDeltaTime = 1 / fps;
            _watch = new Stopwatch();
            _watch.Start();
            _previousTicks = _watch.Elapsed.Ticks;
        }

        /// <summary>
        /// Update time
        /// </summary>
        /// <returns>True if time updated other returns false</returns>
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
