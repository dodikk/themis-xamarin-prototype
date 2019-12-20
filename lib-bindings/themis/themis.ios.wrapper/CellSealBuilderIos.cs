using System;
using System.IO;
using Foundation;
using Themis.iOS.Utils;


namespace Themis.iOS
{
    public class CellSealBuilderIos: ICellSealBuilder
    {
        public ICellSealDynamic BuildCellSeal()
        {
            return new CellSealDynamic(builder: this);
        }

        public ICellSeal BuildCellSealForMasterKey(byte[] masterKeyData)
        {
            if (masterKeyData == null) throw new ArgumentNullException(nameof(masterKeyData));

            try
            {
                NSData nsMasterKeyData = ConvertUtilsIos.ByteArrayToNSData(masterKeyData);
                var result = new CellSealIos(masterKeyData: nsMasterKeyData);

                return result;
            }
            catch (ThemisXamarinBridgeException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "[FAIL] [iOS] CellSealBuilderIos.BuildCellSealForMasterKey()",
                    inner: ex);
            }
        }

        public ICellSeal BuildCellSealForMasterKeyStream(Stream masterKeyStream)
        {
            if (masterKeyStream == null) throw new ArgumentNullException(nameof(masterKeyStream));

            byte[] masterKeyBytes = ConvertUtilsPortable.StreamToByteArray(masterKeyStream);
            var result = BuildCellSealForMasterKey(masterKeyData: masterKeyBytes);

            return result;
        }

        public ISecureCellData BuildCypherText(byte[] cypherTextData)
        {
            if (cypherTextData == null) throw new ArgumentNullException(nameof(cypherTextData));

            NSData cypherTextNsdata = ConvertUtilsIos.ByteArrayToNSData(cypherTextData);
            var result = new SecureCellDataIos(cypherText: cypherTextNsdata);

            return result;
        }

        public ISecureCellData BuildCypherTextFromStream(Stream cypherTextStream)
        {
            if (cypherTextStream == null) throw new ArgumentNullException(nameof(cypherTextStream));

            NSData cypherTextNsdata = ConvertUtilsIos.StreamToNSData(cypherTextStream);
            var result = new SecureCellDataIos(cypherText: cypherTextNsdata);

            return result;
        }

        public ICellContextImprint BuildImprintKdfForMasterKey(byte[] masterKeyData)
        {
            if (masterKeyData == null) throw new ArgumentNullException(nameof(masterKeyData));

            NSData nsMasterKeyData = ConvertUtilsIos.ByteArrayToNSData(masterKeyData);
            var result = new CellContextImprintIos(masterKeyData: nsMasterKeyData);

            return result;
        }

        public ICellContextImprint BuildImprintKdfForStream(Stream masterKeyStream)
        {
            if (masterKeyStream == null) throw new ArgumentNullException(nameof(masterKeyStream));

            NSData nsMasterKeyData = ConvertUtilsIos.StreamToNSData(stream: masterKeyStream);
            var result = new CellContextImprintIos(masterKeyData: nsMasterKeyData);

            return result;
        }

        public ICellContextImprintDynamic BuildImprintKdfInstance()
        {
            return new CellContextImprintDynamic(builder: this);
        }
    }
}
