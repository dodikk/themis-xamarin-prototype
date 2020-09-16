using Javax.Crypto;


namespace Themis.Droid
{
    public class AesGcmDroid: IAesGcmBlockCipher
    {
		public byte[] Decrypt(
	        byte[] key,
	        byte[] cipherTextDataWithTagSuffix,
	        AesGcmBlockCipherConfig options)
		{
            using (var cipher = Javax.Crypto.Cipher.GetInstance("AES/GCM/NoPadding"))
            {
                var adaptedOptions =
                    new Javax.Crypto.Spec.GCMParameterSpec(
                        tLen: options.TagLengthInBits,
                        options.InitializationVector);

                Java.Security.IKey adaptedKey =
                    new Javax.Crypto.Spec.SecretKeySpec(key, algorithm: "AES");

                cipher.Init(
                    opmode: CipherMode.DecryptMode,
                    key: adaptedKey,
                    @params: adaptedOptions);

                byte[] resultPlainText = cipher.DoFinal(input: cipherTextDataWithTagSuffix);
                return resultPlainText;
            }
        }

		public byte[] Encrypt(
			byte[] key,
			byte[] plainTextData,
			AesGcmBlockCipherConfig options)
		{
            using (var cipher = Javax.Crypto.Cipher.GetInstance("AES/GCM/NoPadding"))
            {
                var adaptedOptions =
                    new Javax.Crypto.Spec.GCMParameterSpec(
                        tLen: options.TagLengthInBits,
                        options.InitializationVector);

                Java.Security.IKey adaptedKey =
                    new Javax.Crypto.Spec.SecretKeySpec(key, algorithm: "AES");

                cipher.Init(
                    opmode: CipherMode.EncryptMode,
                    key: adaptedKey,
                    @params: adaptedOptions);


                
                byte[] cipherTextDataWithTagSuffix = cipher.DoFinal(input: plainTextData);
                return cipherTextDataWithTagSuffix;
            }
		}
    }
}
