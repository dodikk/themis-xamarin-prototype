using System;
using Themis.Clock;


namespace Themis.iOS.Clock
{
    public class MonotonicClockIos : IMonotonicClock
    {
        /// <summary>
        /// 
        /// Wraps sysctlbyname() since it is easier than plain sysctl()
        ///
        /// https://forums.xamarin.com/discussion/20006/access-to-sysctl-h
        /// https://developer.apple.com/library/archive/documentation/System/Conceptual/ManPages_iPhoneOS/man3/sysctlbyname.3.html
        /// https://mobile.twitter.com/vixentael/status/1130944726386040834
        ///
        /// </summary>
        /// 
        /// <returns>Number of seconds since 1 jan 1970 UTC</returns>
        public Int64 GetNumberOfSecondsSinceDeviceLastBooted()
        {
            try
            {
                BootTimeGetterFromSysctl.TimeVal uptime = BootTimeGetterFromSysctl.GetBoottime();
                Int64 bootTime = uptime.sec;

                Int64 nowTime = BootTimeGetterFromSysctl.GetCurrentUnixTimeInSeconds();
                Int64 resultUptime = nowTime - bootTime;

                return resultUptime;
            }
            catch (ClockReadUptimeException)
            {
                // The Bear.app example does ```return -1;``` since that would cause the comparisons to fail
                // -
                // https://mobile.twitter.com/vixentael/status/1130944726386040834
                // https://www.cossacklabs.com/blog/end-to-end-encryption-in-bear-app.html
                // -
                // 

                // maybe rethrowing an exception is better for .NET users
                // since exceptions are common for C# and xamarin
                // -
                throw;
            }
        }


    }
}
