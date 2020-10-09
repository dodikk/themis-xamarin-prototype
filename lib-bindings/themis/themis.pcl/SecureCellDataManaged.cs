using System.IO;


namespace Themis
{
    public class SecureCellDataManaged: ISecureCellData
    {
        public SecureCellDataManaged(byte[] rawData)
        {
            _rawData = rawData;
        }

        public SecureCellDataManaged(Stream rawDataStream)
        {
            _rawData = ConvertUtilsPortable.StreamToByteArray(rawDataStream);
        }

        public void Dispose()
        {
            // IDLE
        }

        public byte[] GetEncryptedData()
        {
            return _rawData;
        }

        public Stream GetEncryptedDataAsStream()
        {
            return ConvertUtilsPortable.ByteArrayToMemoryStream(_rawData);
        }

        private byte[] _rawData;
    }
}
