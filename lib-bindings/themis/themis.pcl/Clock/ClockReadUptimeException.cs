using System;


namespace Themis.Clock
{
    public class ClockReadUptimeException: Exception
    {
        public ClockReadUptimeException(string message, Exception innerException)
            : base(message, innerException)
        {
            // IDLE
        }

        public ClockReadUptimeException(string message)
            : base(message)
        {
            // IDLE
        }
    }
}
