using System;
using System.Linq;
using System.Security.Cryptography;


namespace Themis.iOS
{
    public class AesGcmIos: IAesGcmBlockCipher
    {
        private const int BitsInSingleByte = 8;

        public byte[] Decrypt(
            byte[] key,
            byte[] cipherTextDataWithTagSuffix,
            AesGcmBlockCipherConfig options)
        {
            if (options.TagLengthInBits < BitsInSingleByte)
            {
                throw new ArgumentException(nameof(options.TagLengthInBits));
            }
            int tagSizeInBytes = options.TagLengthInBits / BitsInSingleByte;


            int ciphertextLength = cipherTextDataWithTagSuffix.Length - tagSizeInBytes;
            if (ciphertextLength <= 0)
            {
                throw new ArgumentException(nameof(options.TagLengthInBits));
            }


            byte[] cipherTextData = cipherTextDataWithTagSuffix.AsSpan(
                start: 0,
                length: ciphertextLength).ToArray();

            byte[] tagData = cipherTextDataWithTagSuffix.AsSpan(
                start: ciphertextLength,
                length: tagSizeInBytes).ToArray();

            // https://crypto.stackexchange.com/questions/26783/ciphertext-and-tag-size-and-iv-transmission-with-aes-in-gcm-mode
            // -
            // Output size = input size That's correct, GCM uses CTR internally.
            // It encrypts a counter value for each block,
            // but it only uses as many bits as required from the last block.
            // CTR turns the block cipher into a stream cipher.
            // Note that this doesn't include the optional additional authenticated data
            // (AAD), the optional IV nor the required authentication tag.
            // -
            byte[] outPlainText =
                (byte[])Array.CreateInstance(
                    elementType: typeof(byte),
                    length: cipherTextData.Length);

            using (var cipher = new AesGcm(key: key))
            {
                cipher.Decrypt(
                    nonce: options.InitializationVector,
                    ciphertext: cipherTextData,
                    tag: tagData,
                    plaintext: outPlainText,
                    associatedData: null);

                return outPlainText;
            }
        }

        public byte[] Encrypt(
            byte[] key,
            byte[] plainTextData,
            AesGcmBlockCipherConfig options)
        {
            if (options.TagLengthInBits < BitsInSingleByte)
            {
                throw new ArgumentException(nameof(options.TagLengthInBits));
            }
            int tagSizeInBytes = options.TagLengthInBits / BitsInSingleByte;


            using (var cipher = new AesGcm(key: key))
            {
                byte[] outTagBuffer =
                    (byte[])Array.CreateInstance(
                        elementType: typeof(byte),
                        length: tagSizeInBytes);

                // https://crypto.stackexchange.com/questions/26783/ciphertext-and-tag-size-and-iv-transmission-with-aes-in-gcm-mode
                // -
                // Output size = input size That's correct, GCM uses CTR internally.
                // It encrypts a counter value for each block,
                // but it only uses as many bits as required from the last block.
                // CTR turns the block cipher into a stream cipher.
                // Note that this doesn't include the optional additional authenticated data
                // (AAD), the optional IV nor the required authentication tag.
                // -
                byte[] outCiphertext =
                    (byte[])Array.CreateInstance(
                        elementType: typeof(byte),
                        length: plainTextData.Length);

                cipher.Encrypt(
                    nonce: options.InitializationVector,
                    plaintext: plainTextData,
                    ciphertext: outCiphertext,
                    tag: outTagBuffer,
                    associatedData: null);

                // Note: [alex-d] Seems like tag is appended to encrypted text by JS
                // so we'll do that as well
                // https://github.com/bitwiseshiftleft/sjcl/blob/master/core/gcm.js#L34
                // https://github.com/bitwiseshiftleft/sjcl/blob/master/core/gcm.js#L51
                // https://github.com/rynomad/subtle
                // https://github.com/bitwiseshiftleft/sjcl
                // -
                // P.S. java does that too on android
                // -

                byte[] result = outCiphertext.Concat(outTagBuffer).ToArray();
                return result;
            }
        }
    }
}
