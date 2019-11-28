using System;
using Com.Cossacklabs.Themis;

namespace Themis.Droid
{
    public class CellSealDroid: ICellSeal
    {
        public CellSealDroid(byte[] masterKeyData)
        {
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
                if (null != _secureCell)
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

        public byte[] UnwrapData(ISecureCellData cypherTextData, byte[] context = null)
        {
            var castedCypherTextData = cypherTextData as SecureCellDataDroid;
            if (null == castedCypherTextData)
            {
                throw new ArgumentException(
                    message: $"Type mismatch: {cypherTextData.GetType()} received. Expecterd: {typeof(SecureCellDataDroid)}",
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

        public ISecureCellData WrapData(byte[] plainTextData, byte[] context = null)
        {
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

        private SecureCell _secureCell;
    }
}
