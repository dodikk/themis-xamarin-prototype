using System;


namespace Themis.Droid
{
    public class CellSealDroid: ICellSeal
    {
        public CellSealDroid()
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public byte[] UnwrapData(ISecureCellData cypherTextData, byte[] context = null)
        {
            throw new NotImplementedException();
        }

        public ISecureCellData WrapData(byte[] plainTextData, byte[] context = null)
        {
            throw new NotImplementedException();
        }
    }
}
