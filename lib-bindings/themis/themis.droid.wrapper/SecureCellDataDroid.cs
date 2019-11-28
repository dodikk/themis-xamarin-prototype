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
            if (null != _cypherTextHandle)
            {
                _cypherTextHandle.Dispose();
                _cypherTextHandle = null;
            }
        }

        public byte[] GetEncryptedData()
        {
            byte[] result = _cypherTextHandle.GetProtectedData();
            return result;
        }

        private SecureCellData _cypherTextHandle;
    }
}
