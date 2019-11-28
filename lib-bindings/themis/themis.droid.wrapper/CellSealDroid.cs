using System;
using Com.Cossacklabs.Themis;

namespace Themis.Droid
{
    public class CellSealDroid: ICellSeal
    {
        public CellSealDroid(byte[] masterKeyData)
        {
            _secureCell = new SecureCell(key: masterKeyData);
        }

        public void Dispose()
        {
            if (null != _secureCell)
            {
                _secureCell.Dispose();
                _secureCell = null;
            }
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

            byte[] result = 
                _secureCell.Unprotect(
                    context: context,
                    protectedData: castedCypherTextData.SecureCellDataJava);

            return result;
        }

        public ISecureCellData WrapData(byte[] plainTextData, byte[] context = null)
        {
            SecureCellData cypherTextHandle =
                _secureCell.Protect(
                    context: context,
                    data: plainTextData);

            var result = new SecureCellDataDroid(cypherTextHandle);

            return result;
        }

        private SecureCell _secureCell;
    }
}
