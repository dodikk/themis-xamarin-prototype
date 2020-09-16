using System;
using System.IO;
using Foundation;
using Themis.iOS.Utils;


namespace Themis.iOS
{
    public class SecureCellDataIos: ISecureCellData
    {
        public NSData cipherText => _cipherText;

        public SecureCellDataIos(
            NSData cipherText,
            bool shouldConsumeCipherTextObject = true)
        {
            if (cipherText == null) throw new ArgumentNullException(nameof(cipherText));

            _cipherText = cipherText;
            _shouldConsumeCipherTextObject = shouldConsumeCipherTextObject;
        }

        public void Dispose()
        {
            try
            {
                if (_cipherText != null)
                {
                    if (_shouldConsumeCipherTextObject)
                    {
                        _cipherText.Dispose();
                    }

                    _cipherText = null;
                }
            }
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            catch
            {
                // Suppressing.
                // A destructor must never throw
            }
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
        }

        public byte[] GetEncryptedData()
        {
            byte[] result = ConvertUtilsIos.NSDataToByteArray(_cipherText);
            return result;
        }

        public Stream GetEncryptedDataAsStream()
        {
            return _cipherText.AsStream();
        }

        private bool _shouldConsumeCipherTextObject;
        private NSData _cipherText;
    }
}
