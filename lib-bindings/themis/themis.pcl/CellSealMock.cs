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

        public byte[] UnwrapData(ISecureCellData cipherTextData, byte[] context = null)
        {
            // no decryption since it's a mock
            // -
            return cipherTextData.GetEncryptedData();
        }

        public Stream UnwrapDataAsStream(ISecureCellData cipherTextData, Stream contextStream = null)
        {
            // no decryption since it's a mock
            // -
            return cipherTextData.GetEncryptedDataAsStream();
        }

        public ISecureCellData WrapData(byte[] plainTextData, byte[] context = null)
        {
            // no encryption since it's a mock
            // -
            return new SecureCellDataManaged(plainTextData);
        }

        public ISecureCellData WrapDataStream(Stream plainTextStream, Stream contextStream = null)
        {
            // no encryption since it's a mock
            // -
            return new SecureCellDataManaged(plainTextStream);
        }
    }
}
