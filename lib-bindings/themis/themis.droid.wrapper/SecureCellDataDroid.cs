using System;
using System.IO;
using Com.Cossacklabs.Themis;

namespace Themis.Droid
{
    public class SecureCellDataDroid: ISecureCellData
    {
        public SecureCellData SecureCellDataJava => _cipherTextHandle;

        public SecureCellDataDroid(SecureCellData cipherTextHandle)
        {
            if (cipherTextHandle == null) throw new ArgumentNullException(nameof(cipherTextHandle));

            _cipherTextHandle = cipherTextHandle;
        }

        public void Dispose()
        {
            try
            {
                if (_cipherTextHandle != null)
                {
                    _cipherTextHandle.Dispose();
                    _cipherTextHandle = null;
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
                byte[] result = _cipherTextHandle.GetProtectedData();
                return result;
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "[FAIL] [droid] SecureCellData.GetProtectedData() java method failed",
                    dataAsHex: null,
                    contextAsHex: null,
                    dataAsBase64: null,
                    contextAsBase64: null,
                    inner: ex);
            }
        }

        public Stream GetEncryptedDataAsStream()
        {
            var resultBytes = GetEncryptedData();
            var result = ConvertUtilsPortable.ByteArrayToMemoryStream(resultBytes);

            return result;
        }

        private SecureCellData _cipherTextHandle;
    }
}
