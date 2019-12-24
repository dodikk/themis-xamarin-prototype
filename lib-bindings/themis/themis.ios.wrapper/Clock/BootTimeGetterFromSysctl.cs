using System;
using System.Runtime.InteropServices;
using Themis.Clock;

namespace Themis.iOS.Clock
{
    /// <summary>
    /// Dealing with sysctlbyname() C calls via marshalling and DllImport
    /// to reduce the amount of native artefacts to link
    ///
    /// Similar approach to the swift implementation
    /// https://mobile.twitter.com/wilshipley/status/1130973433120952321
    /// 
    /// </summary>
    public static class BootTimeGetterFromSysctl
    {
        public struct TimeVal
        {
            public long sec;
            public long ms;
        }

        public static TimeVal GetBoottime()
        {
            // [alex-d] copy-pasted from: 
            // https://forums.xamarin.com/discussion/20006/access-to-sysctl-h
            //
            // seems legit according to
            // https://developer.apple.com/library/archive/documentation/System/Conceptual/ManPages_iPhoneOS/man3/sysctlbyname.3.html
            // ---

            IntPtr pLen = IntPtr.Zero;
            IntPtr pRawTimevalResult = IntPtr.Zero;

            try
            {
                // calculate size of buffer for TimeVal return data
                // -
                pLen = Marshal.AllocHGlobal(sizeof(int));
                int sizeComputationErrorCode =
                    sysctlbyname(
                        property: "kern.boottime",
                        outputDataPtr: IntPtr.Zero,
                        outputDataLengthPtr: pLen,
                        dataToWritePtr: IntPtr.Zero,
                        lengthOfDataToWrite: 0);
                if (sizeComputationErrorCode == -1)
                {
                    throw new ClockReadUptimeException("sysctlbyname() size allocation failed");
                }


                Int32 length = Marshal.ReadInt32(pLen);
                pRawTimevalResult = Marshal.AllocHGlobal(length);

                int bootTimeReadingErrorCode = 
                    sysctlbyname(
                        property: "kern.boottime",
                        outputDataPtr: pRawTimevalResult,
                        outputDataLengthPtr: pLen,
                        dataToWritePtr: IntPtr.Zero,
                        lengthOfDataToWrite: 0);
                if (bootTimeReadingErrorCode == -1)
                {
                    throw new ClockReadUptimeException("sysctlbyname() uptime data read failed");
                }

                // Seems safe to release the AllocHGlobal() related data afterwards
                //
                // -
                // Marshals data from an unmanaged block of memory to a newly allocated managed object of the type specified by a generic type parameter.
                // https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.ptrtostructure?view=netframework-4.8#System_Runtime_InteropServices_Marshal_PtrToStructure__1_System_IntPtr_
                // -
                // 
                TimeVal result = Marshal.PtrToStructure<TimeVal>(pRawTimevalResult);

                if (result.sec == 0)
                {
                    throw new ClockReadUptimeException(
                        message: "sysctlbyname() has succeeded but boot time is zero");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new ClockReadUptimeException(
                    message: "sysctlbyname() uptime data read failed",
                    innerException: ex);
            }
            finally
            {
                // assuming FreeHGlobal() does not throw according to the documentation
                // https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.freehglobal?redirectedfrom=MSDN&view=netframework-4.8#System_Runtime_InteropServices_Marshal_FreeHGlobal_System_IntPtr_
                //
                // TODO: maybe wrap these pointers into SafeHandle
                //       and cleanup via IDisposable ```using {}``` statement
                //       for better memory leak safety
                // https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.safehandle?redirectedfrom=MSDN&view=netframework-4.8
                // 

                try
                {
                    if (pLen != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(pLen);
                    }

                    if (pRawTimevalResult != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(pRawTimevalResult);
                    }
                }
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
                catch
                {
                    // IDLE: the destructor is supposed to not throw
                }
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
            }
        }

        public static Int64 GetCurrentUnixTimeInSeconds()
        {
            IntPtr pResult = IntPtr.Zero;

            try
            {
                pResult = Marshal.AllocHGlobal(sizeof(Int64));
                int errorCode = time(pResult);

                if (errorCode == -1)
                {
                    throw new ClockReadUptimeException(message: "time() call has failed");
                }

                Int64 result = Marshal.ReadInt64(pResult);
                return result;
            }
            finally
            {
                if (pResult != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pResult);
                }
            }
        }

#pragma warning disable IDE1006 // Naming Styles
        // int sysctlbyname(const char* name, void* oldp, size_t *oldlenp, void* newp, size_t newlen);
        // https://developer.apple.com/library/archive/documentation/System/Conceptual/ManPages_iPhoneOS/man3/sysctlbyname.3.html
        // -
        [DllImport(ObjCRuntime.Constants.SystemLibrary)]
        static private extern int sysctlbyname(
            [MarshalAs(UnmanagedType.LPStr)] string property,
            IntPtr outputDataPtr,
            IntPtr outputDataLengthPtr,
            IntPtr dataToWritePtr,
            uint lengthOfDataToWrite);
#pragma warning restore IDE1006 // Naming Styles


#pragma warning disable IDE1006 // Naming Styles
        // https://linux.die.net/man/2/time
        // -
        [DllImport(ObjCRuntime.Constants.SystemLibrary)]
        static private extern int time(IntPtr outResultTimePtr);
#pragma warning restore IDE1006 // Naming Styles
    }
}
