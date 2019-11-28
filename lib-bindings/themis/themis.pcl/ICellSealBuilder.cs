using System;

namespace Themis
{
    public interface ICellSealBuilder
    {
        ICellSeal BuildCellSealForMasterKey(byte[] masterKeyData);
    }
}
