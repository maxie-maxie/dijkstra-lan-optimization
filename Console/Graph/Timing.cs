using System.Diagnostics;

namespace Timestamp
{
    public class Timing
    {
        private Stopwatch stopwatch;
        public Timing()
        {
            stopwatch = new Stopwatch();
        }
        public void StartTime()
        {
            stopwatch.Restart();
        }
        public void StopTime()
        {
            stopwatch.Stop();
        }
        public TimeSpan Result()
        {
            return stopwatch.Elapsed;
        }
    }
}