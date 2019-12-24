using System;


namespace Themis.Clock
{
    /// <summary>
    /// https://mobile.twitter.com/vixentael/status/1130944726386040834
    /// http://danielemargutti.com/2018/02/22/the-secret-world-of-nstimer/
    /// https://developer.android.com/reference/android/os/SystemClock
    /// https://forums.xamarin.com/discussion/20006/access-to-sysctl-h
    /// https://developer.apple.com/library/archive/documentation/System/Conceptual/ManPages_iPhoneOS/man3/sysctlbyname.3.html
    /// 
    /// </summary>
    public interface IMonotonicClock
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
        Int64 GetNumberOfSecondsSinceDeviceLastBooted();
    }
}
