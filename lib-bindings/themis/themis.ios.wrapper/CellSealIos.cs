using System;
using System.IO;
using Foundation;
using Themis.iOS.Utils;

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
            if (masterKeyData == null) throw new ArgumentNullException(nameof(masterKeyData));

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
            if (cypherTextData == null) throw new ArgumentNullException(nameof(cypherTextData));

            var castedCypherTextData = cypherTextData as SecureCellDataIos;
            if (null == castedCypherTextData)
            {
                throw new ArgumentException(
                    message: $"Type mismatch: {cypherTextData.GetType()} received. Expected: {typeof(SecureCellDataIos)}",
                    paramName: nameof(cypherTextData));
            }


            NSData nsContextData = ConvertUtilsIos.ByteArrayToNSData(context);


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


            if (themisError != null)
            {
                throw new ThemisXamarinBridgeException(
                    message: $"NSError decrypting data with themis: {themisError.LocalizedDescription}");
            }

            byte[] result = ConvertUtilsIos.NSDataToByteArray(plainTextData);
            return result;
        }

        public ISecureCellData WrapData(
            byte[] plainTextData,
            byte[] context = null)
        {
            if (plainTextData == null) throw new ArgumentNullException(nameof(plainTextData));

            NSError themisError = null;
            NSData cypherText = null;

            NSData nsPlainTextData = ConvertUtilsIos.ByteArrayToNSData(plainTextData);
            NSData nsContextData = ConvertUtilsIos.ByteArrayToNSData(context);

            
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

            if (themisError != null)
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

        public ISecureCellData WrapDataStream(
            Stream plainTextStream,
            Stream contextStream = null)
        {
            if (plainTextStream == null) throw new ArgumentNullException(nameof(plainTextStream));

            byte[] plainTextBytes = ConvertUtilsPortable.StreamToByteArray(plainTextStream);
            byte[] contextBytes = ConvertUtilsPortable.StreamToByteArray(contextStream);

            var result =
                WrapData(
                    plainTextData: plainTextBytes,
                    context: contextBytes);

            return result;
        }

        public Stream UnwrapDataAsStream(
            ISecureCellData cypherTextData,
            Stream contextStream = null)
        {
            if (cypherTextData == null) throw new ArgumentNullException(nameof(cypherTextData));

            byte[] contextBytes = ConvertUtilsPortable.StreamToByteArray(contextStream);
            byte[] resultBytes = UnwrapData(cypherTextData, context: contextBytes);

            var result = ConvertUtilsPortable.ByteArrayToMemoryStream(resultBytes);

            return result;
        }


        private TSCellSeal _implCellSeal;
    }
}
