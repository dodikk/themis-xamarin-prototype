using System;
using Com.Cossacklabs.Themis;

namespace Themis.Droid
{
    public class SecureCellDataDroid: ISecureCellData
    {
        public SecureCellData SecureCellDataJava => _cypherTextHandle;

        public SecureCellDataDroid(SecureCellData cypherTextHandle)
        {
            _cypherTextHandle = cypherTextHandle;
        }

        public void Dispose()
        {
            try
            {
                if (null != _cypherTextHandle)
                {
                    _cypherTextHandle.Dispose();
                    _cypherTextHandle = null;
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
            try
            {
                byte[] result = _cypherTextHandle.GetProtectedData();
                return result;
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "[FAIL] [droid] SecureCellData.GetProtectedData() java method failed",
                    inner: ex);
            }
        }

        private SecureCellData _cypherTextHandle;
    }
}
