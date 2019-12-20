using System;
using System.Collections.Generic;
using System.IO;


namespace Themis
{
    public class CellContextImprintMock: ICellContextImprint
    {
        public byte[] DeriveKey(byte[] fromData, byte[] context = null)
        {
            var result = new List<byte>();

            result.AddRange(fromData);
            if (context != null)
            {
                result.AddRange(context);
            }
            else
            {
                result.AddRange(ConvertUtilsPortable.StringToByteArray("&context=<null>"));
            }

            return result.ToArray();
        }

        public Stream DeriveKeyAsStream(Stream fromStream, Stream contextStream = null)
        {
            byte[] resultBytes =
                DeriveKey(
                    fromData: ConvertUtilsPortable.StreamToByteArray(fromStream),
                    context: ConvertUtilsPortable.StreamToByteArray(contextStream));

            MemoryStream result = ConvertUtilsPortable.ByteArrayToMemoryStream(resultBytes);

            return result;
        }

        public void Dispose()
        {
            // IDLE
        }
    }
}
