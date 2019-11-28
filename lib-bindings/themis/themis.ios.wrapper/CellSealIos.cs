using System;
using System.IO;
using Foundation;


namespace Themis.iOS
{
    public class CellSealIos: ICellSeal
    {
        public void Dispose()
        {
            try
            {
                if (_implCellSeal != null)
                {
                    _implCellSeal.Dispose();
                    _implCellSeal = null;

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

        public CellSealIos(NSData masterKeyData)
        {
            try
            {
                _implCellSeal = new TSCellSeal(masterKeyData);
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "TSCellSeal constructor has failed",
                    inner: ex);
            }
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


            NSError themisError = null;
            NSData plainTextData = null;
            try
            {
                plainTextData =
                    _implCellSeal.UnwrapData(
                        message: castedCypherTextData.CypherText,
                        context: nsContextData,
                        error: out themisError);
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "TSCellSeal.UnwrapData() has failed",
                    inner: ex);
            }


            if (null != themisError)
            {
                throw new ThemisXamarinBridgeException(
                    message: $"NSError decrypting data with themis: {themisError.LocalizedDescription}");
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

            NSData cypherText = null;
            try
            {
                cypherText =
                _implCellSeal.WrapData(
                    message: nsPlainTextData,
                    context: nsContextData,
                    error: out themisError);
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "TSCellSeal.WrapData() has failed",
                    inner: ex);
            }

            if (null != themisError)
            {
                throw new ThemisXamarinBridgeException(
                    message: $"NSError encrypting data with themis: {themisError.LocalizedDescription}");
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
