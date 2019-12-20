using System;
using System.IO;

namespace Themis
{
    public class CellSealMock: ICellSeal
    {
        public void Dispose()
        {
            // IDLE
        }

        public byte[] UnwrapData(ISecureCellData cypherTextData, byte[] context = null)
        {
            // no decryption since it's a mock
            // -
            return cypherTextData.GetEncryptedData();
        }

        public Stream UnwrapDataAsStream(ISecureCellData cypherTextData, Stream contextStream = null)
        {
            // no decryption since it's a mock
            // -
            return cypherTextData.GetEncryptedDataAsStream();
        }

        public ISecureCellData WrapData(byte[] plainTextData, byte[] context = null)
        {
            // no encryption since it's a mock
            // -
            return new SecureCellDataMock(plainTextData);
        }

        public ISecureCellData WrapDataStream(Stream plainTextStream, Stream contextStream = null)
        {
            // no encryption since it's a mock
            // -
            return new SecureCellDataMock(plainTextStream);
        }
    }
}
