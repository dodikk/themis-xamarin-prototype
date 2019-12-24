using System;
using Android.OS;
using Themis.Clock;


namespace Themis.Droid.Clock
{
    /// <summary>
    /// Wraps android SystemClock
    /// https://developer.android.com/reference/android/os/SystemClock
    /// 
    /// </summary>
    ///
    /// 
    public class MonotonicClockDroid: IMonotonicClock
	{
        /// <summary>
        /// Not casting to DateTime of .net since it is too much coupled
        /// with Gregorian calendar and might suffer from irregularities
        /// of human readable date interpretation
        ///
        ///
        /// Note: milliseconds or nanoseconds precision is available via platform API
        /// still, the app usually uses second precision
        ///
        /// </summary>
        /// 
        /// <returns>Number of seconds since 1 jan 1970 UTC</returns>
        ///
        public Int64 GetNumberOfSecondsSinceDeviceLastBooted()
        {
            try
            {
                long millisecondsSinceBoot = SystemClock.ElapsedRealtime();
                long result = millisecondsSinceBoot / MillisecondsInOneSecond;

                return result;
            }
            catch (Exception ex)
            {
                throw new ClockReadUptimeException(
                    message: "SystemClock.ElapsedRealtime() has failed",
                    innerException: ex);
            }
        }

        private const Int64 MillisecondsInOneSecond = 1000;
    }
}
