using System;


namespace Themis.Droid
{
    public class CellSealBuilderDroid: ICellSealBuilder
    {
        public ICellSeal BuildCellSealForMasterKey(byte[] masterKeyData)
        {
            var result = new CellSealDroid(masterKeyData);
            return result;
        }
    }
}
