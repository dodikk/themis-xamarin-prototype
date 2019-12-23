using System.IO;


namespace Themis
{
    public class SecureCellDataMock: ISecureCellData
    {
        public SecureCellDataMock(byte[] rawData)
        {
            _rawData = rawData;
        }

        public SecureCellDataMock(Stream rawDataStream)
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
