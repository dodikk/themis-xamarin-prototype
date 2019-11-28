using System;


namespace Themis.Droid
{
    public class CellSealBuilderDroid: ICellSealBuilder
    {
        public ICellSeal BuildCellSealForMasterKey(byte[] masterKeyData)
        {
            try
            {
                var result = new CellSealDroid(masterKeyData);
                return result;
            }
            catch (ThemisXamarinBridgeException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "[FAIL] [droid] CellSealBuilderDroid.BuildCellSealForMasterKey()",
                    inner: ex);
            }
        }
    }
}
