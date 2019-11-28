using System;
using System.IO;
using Foundation;


namespace Themis.iOS
{
    public class CellSealIos: ICellSeal
    {
        public void Dispose()
        {
            if (_implCellSeal != null)
            {
                _implCellSeal.Dispose();
                _implCellSeal = null;

            }
        }

        public CellSealIos(NSData masterKeyData)
        {
            _implCellSeal = new TSCellSeal(masterKeyData);
        }

        public byte[] UnwrapData(
            ISecureCellData cypherTextData,
            byte[] context = null)
        {
            var castedCypherTextData = cypherTextData as SecureCellDataIos;
            if (null == castedCypherTextData)
            {
                throw new ArgumentException(
                    message: $"Type mismatch: {cypherTextData.GetType()} received. Expecterd: {typeof(SecureCellDataIos)}",
                    paramName: nameof(cypherTextData));
            }


            NSData nsContextData = null;
            if (null != context)
            {
                string base64ContextData = Convert.ToBase64String(context);
                nsContextData =
                    new NSData(
                        base64String: base64ContextData,
                        options: NSDataBase64DecodingOptions.None);
            }

            NSError themisError;
            NSData plainTextData =
                _implCellSeal.UnwrapData(
                    message: castedCypherTextData.CypherText,
                    context: nsContextData,
                    error: out themisError);

            if (null != themisError)
            {
                throw new ArgumentException(
                    message: $"Error decrypting data with themis: {themisError.LocalizedDescription}",
                    paramName: nameof(cypherTextData));
            }

            using (MemoryStream ms = new MemoryStream())
            using (Stream nsDataStream = plainTextData.AsStream())
            {
                nsDataStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public ISecureCellData WrapData(
            byte[] plainTextData,
            byte[] context = null)
        {
            NSError themisError;

            string base64PlainText = Convert.ToBase64String(plainTextData);
            NSData nsPlainTextData =
                new NSData(
                    base64String: base64PlainText,
                    options: NSDataBase64DecodingOptions.None);

            NSData nsContextData = null;
            if (null != context)
            {
                string base64ContextData = Convert.ToBase64String(context);
                nsContextData =
                    new NSData(
                        base64String: base64ContextData,
                        options: NSDataBase64DecodingOptions.None);
            }

            NSData cypherText =
            _implCellSeal.WrapData(
                message: nsPlainTextData,
                context: nsContextData,
                error: out themisError);

            if (null != themisError)
            {
                throw new ArgumentException(
                    message: $"Error encrypting data with themis: {themisError.LocalizedDescription}",
                    paramName: nameof(plainTextData));
            }

            var result =
                new SecureCellDataIos(
                    cypherText: cypherText,
                    shouldConsumeCypherTextObject: true);

            return result;
        }

        private TSCellSeal _implCellSeal;
    }
}
