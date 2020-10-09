using System;
using System.Runtime.Serialization;

namespace Themis
{
    public class ThemisXamarinBridgeException: PlatformNotSupportedException
    {
        public string DataAsHex { get; private set; }
        public string ContextAsHex { get; private set; }

        public string DataAsBase64 { get; private set; }
        public string ContextAsBase64 { get; private set; }

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
            string dataAsHex,
            string contextAsHex,
            string dataAsBase64,
            string contextAsBase64,
            Exception inner)
        : base(message, inner)
        {
            DataAsHex = dataAsHex;
            ContextAsHex = contextAsHex;

            DataAsBase64 = dataAsBase64;
            ContextAsBase64 = contextAsBase64;
        }
    }

}
