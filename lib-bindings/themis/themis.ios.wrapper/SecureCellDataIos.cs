using System.IO;
using Foundation;


namespace Themis.iOS
{
    public class SecureCellDataIos: ISecureCellData
    {
        public NSData CypherText => _cypherText;

        public SecureCellDataIos(
            NSData cypherText,
            bool shouldConsumeCypherTextObject = true)
        {
            _cypherText = cypherText;
            _shouldConsumeCypherTextObject = shouldConsumeCypherTextObject;
        }

        public void Dispose()
        {
            try
            {
                if (null != _cypherText)
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
            using (MemoryStream ms = new MemoryStream())
            using (Stream nsDataStream = _cypherText.AsStream())
            {
                nsDataStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private bool _shouldConsumeCypherTextObject;
        private NSData _cypherText;
    }
}
