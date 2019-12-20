using System;
using System.IO;

namespace Themis
{
    public interface ICellSealBuilder
    {
        ICellSeal BuildCellSealForMasterKey(byte[] masterKeyData);
        ICellSeal BuildCellSealForMasterKeyStream(Stream masterKeyStream);
        ICellSealDynamic BuildCellSeal();

        ICellContextImprintDynamic BuildImprintKdfInstance();
        ICellContextImprint BuildImprintKdfForMasterKey(byte[] masterKeyData);
        ICellContextImprint BuildImprintKdfForStream(Stream masterKeyStream);

        ISecureCellData BuildCypherText(byte[] cypherTextData);
        ISecureCellData BuildCypherTextFromStream(Stream cypherTextStream);
    }
}
