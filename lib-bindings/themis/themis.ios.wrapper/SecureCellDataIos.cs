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
            if (null != _cypherText)
            {
                if (_shouldConsumeCypherTextObject)
                {
                    _cypherText.Dispose();
                }

                _cypherText = null;
            }
        }

        private bool _shouldConsumeCypherTextObject;
        private NSData _cypherText;
    }
}
