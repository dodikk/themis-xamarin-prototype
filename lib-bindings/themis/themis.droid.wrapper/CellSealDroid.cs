using System;
using System.IO;
using Com.Cossacklabs.Themis;

namespace Themis.Droid
{
    public class CellSealDroid: ICellSeal
    {
        public CellSealDroid(byte[] masterKeyData)
        {
            if (masterKeyData == null) throw new ArgumentNullException(nameof(masterKeyData));

            try
            {
                _secureCell = new SecureCell(key: masterKeyData);
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "[FAIL] [droid] SecureCell java constructor failed",
                    inner: ex);
            }
        }

        public void Dispose()
        {
            try
            {
                if (_secureCell != null)
                {
                    _secureCell.Dispose();
                    _secureCell = null;
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

        public byte[] UnwrapData(
            ISecureCellData cypherTextData,
            byte[] context = null)
        {
            if (cypherTextData == null) throw new ArgumentNullException(nameof(cypherTextData));

            var castedCypherTextData = cypherTextData as SecureCellDataDroid;
            if (castedCypherTextData == null)
            {
                throw new ArgumentException(
                    message: $"Type mismatch: {cypherTextData.GetType()} received. Expected: {typeof(SecureCellDataDroid)}",
                    paramName: nameof(cypherTextData));
            }

            try
            {
                byte[] result =
                    _secureCell.Unprotect(
                        context: context,
                        protectedData: castedCypherTextData.SecureCellDataJava);

                return result;
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "[FAIL] [droid] SecureCell.Unprotect() java method failed",
                    inner: ex);
            }
        }

        public ISecureCellData WrapData(
            byte[] plainTextData,
            byte[] context = null)
        {
            if (plainTextData == null) throw new ArgumentNullException(nameof(plainTextData));

            try
            {
                SecureCellData cypherTextHandle =
                    _secureCell.Protect(
                        context: context,
                        data: plainTextData);

                var result = new SecureCellDataDroid(cypherTextHandle);

                return result;
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "[FAIL] [droid] SecureCell.Protect() java method failed",
                    inner: ex);
            }
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

        private SecureCell _secureCell;
    }
}
