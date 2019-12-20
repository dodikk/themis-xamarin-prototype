using System.IO;


namespace Themis
{
    public class CellSealBuilderMock: ICellSealBuilder
    {
        public ICellSealDynamic BuildCellSeal()
        {
            return new CellSealDynamic(builder: this);
        }

        public ICellSeal BuildCellSealForMasterKey(byte[] masterKeyData)
        {
            return new CellSealMock();
        }

        public ICellSeal BuildCellSealForMasterKeyStream(Stream masterKeyStream)
        {
            return new CellSealMock();
        }

        public ISecureCellData BuildCypherText(byte[] cypherTextData)
        {
            return new SecureCellDataMock(rawData: cypherTextData);
        }

        public ISecureCellData BuildCypherTextFromStream(Stream cypherTextStream)
        {
            return new SecureCellDataMock(rawDataStream: cypherTextStream);
        }

        public ICellContextImprint BuildImprintKdfForMasterKey(byte[] masterKeyData)
        {
            return new CellContextImprintMock();
        }

        public ICellContextImprint BuildImprintKdfForStream(Stream masterKeyStream)
        {
            return new CellContextImprintMock();
        }

        public ICellContextImprintDynamic BuildImprintKdfInstance()
        {
            return new CellContextImprintDynamic(builder: this);
        }
    }
}
