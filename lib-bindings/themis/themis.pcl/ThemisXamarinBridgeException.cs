using System;
using System.Runtime.Serialization;

namespace Themis
{
    public class ThemisXamarinBridgeException: PlatformNotSupportedException
    {
        public ThemisXamarinBridgeException(): base()
        {
        }

        protected ThemisXamarinBridgeException(
            SerializationInfo info,
            StreamingContext context)
        : base(info, context)
        {
        }

        public ThemisXamarinBridgeException(string message): base(message)
        {
        }

        public ThemisXamarinBridgeException(
            string message,
            Exception inner)
        : base(message, inner)
        {
        }
    }

}
