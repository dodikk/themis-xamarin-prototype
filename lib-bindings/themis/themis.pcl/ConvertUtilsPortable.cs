using System;
using System.Linq;
using System.IO;


namespace Themis
{
    public static class ConvertUtilsPortable
    {
        public static byte[] StreamToByteArray(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }

            using (var memStream = new MemoryStream())
            {
                stream.CopyTo(memStream);
                byte[] result = memStream.GetBuffer();

                return result;
            }
        }

        public static MemoryStream ByteArrayToMemoryStream(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            var result =
                new MemoryStream(
                    buffer: data,
                    index: 0,
                    count: data.Length,
                    writable: false,
                    publiclyVisible: true);

            return result;
        }

        public static MemoryStream StringToMemoryStream(string str)
        {
            byte[] data = StringToByteArray(str);
            MemoryStream result = ByteArrayToMemoryStream(data);

            return result;
        }

        public static byte[] StringToByteArray(string str)
        {
            if (str == null)
            {
                return null;
            }

            byte[] result =
                str.ToCharArray()
                   .Select(c => (byte)c)
                   .ToArray();

            return result;
        }

        public static string ByteArrayToString(byte[] stringAsTextData)
        {
            if (stringAsTextData == null)
            {
                return null;
            }

            char[] stringAsTextChars =
                stringAsTextData.Select(b => (char)b)
                                .ToArray();

            string result =
                new string(
                    value: stringAsTextChars,
                    startIndex: 0,
                    length: stringAsTextChars.Length);

            return result;
        }

        public static string ByteArrayToHexString(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            string result = BitConverter.ToString(data).Replace("-", string.Empty);
            return result;
        }
    }
}
