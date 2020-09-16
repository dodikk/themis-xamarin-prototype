

namespace Themis
{
    public interface IAesGcmBlockCipher
    {
        byte[] Encrypt(byte[] key, byte[] plainTextData, AesGcmBlockCipherConfig options);
        byte[] Decrypt(byte[] key, byte[] cipherTextDataWithTagSuffix, AesGcmBlockCipherConfig options);
    }

    public struct AesGcmBlockCipherConfig
    {
        public byte[] InitializationVector { get; set; }
        public int TagLengthInBits { get; set; }
    }
}
