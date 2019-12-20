using System;
using System.IO;
using Foundation;


namespace Themis.iOS.Utils
{
    public static class ConvertUtilsIos
    {
        public static NSData StreamToNSData(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }

            byte[] streamBytes = ConvertUtilsPortable.StreamToByteArray(stream);
            NSData result = ByteArrayToNSData(streamBytes);

            return result;
        }

        public static NSData ByteArrayToNSData(byte[] memoryBuffer)
        {
            if (memoryBuffer == null)
            {
                return null;
            }

            string base64ForBuffer = Convert.ToBase64String(memoryBuffer);

            NSData result =
                new NSData(
                    base64String: base64ForBuffer,
                    options: NSDataBase64DecodingOptions.None);

            return result;
        }

        public static byte[] NSDataToByteArray(NSData iosData)
        {
            if (iosData == null)
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream())
            using (Stream nsDataStream = iosData.AsStream())
            {
                nsDataStream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
