using System;
using System.IO;
using Foundation;
using Themis.iOS.Utils;


namespace Themis.iOS
{
    public class SecureCellDataIos: ISecureCellData
    {
        public NSData CypherText => _cypherText;

        public SecureCellDataIos(
            NSData cypherText,
            bool shouldConsumeCypherTextObject = true)
        {
            if (cypherText == null) throw new ArgumentNullException(nameof(cypherText));

            _cypherText = cypherText;
            _shouldConsumeCypherTextObject = shouldConsumeCypherTextObject;
        }

        public void Dispose()
        {
            try
            {
                if (_cypherText != null)
                {
                    if (_shouldConsumeCypherTextObject)
                    {
                        _cypherText.Dispose();
                    }

                    _cypherText = null;
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
            byte[] result = ConvertUtilsIos.NSDataToByteArray(_cypherText);
            return result;
        }

        public Stream GetEncryptedDataAsStream()
        {
            return _cypherText.AsStream();
        }

        private bool _shouldConsumeCypherTextObject;
        private NSData _cypherText;
    }
}
