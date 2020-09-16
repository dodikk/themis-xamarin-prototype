using System;
using System.IO;
using Com.Cossacklabs.Themis;


namespace Themis.Droid
{
    public class CellSealBuilderDroid: ICellSealBuilder
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

        public ICellSeal BuildCellSealForMasterKeyStream(Stream masterKeyStream)
        {
            if (masterKeyStream == null) throw new ArgumentNullException(nameof(masterKeyStream));

            byte[] masterKeyBytes = ConvertUtilsPortable.StreamToByteArray(masterKeyStream);
            var result = BuildCellSealForMasterKey(masterKeyData: masterKeyBytes);

            return result;
        }

        public ISecureCellData BuildCipherTextFromStream(Stream cipherTextStream)
        {
            if (cipherTextStream == null) throw new ArgumentNullException(nameof(cipherTextStream));

            byte[] cipherTextStreamBytes = ConvertUtilsPortable.StreamToByteArray(cipherTextStream);

            var wrappedStreamData =
                new SecureCellData(
                    protectedData: cipherTextStreamBytes,
                    additionalData: null);

            var result = new SecureCellDataDroid(cipherTextHandle: null);
            return result;
        }

        public ISecureCellData BuildCipherText(byte[] cipherTextData)
        {
            if (cipherTextData == null) throw new ArgumentNullException(nameof(cipherTextData));

            var wrappedStreamData =
                new SecureCellData(
                    protectedData: cipherTextData,
                    additionalData: null);

            var result = new SecureCellDataDroid(cipherTextHandle: wrappedStreamData);
            return result;
        }

        public ICellContextImprint BuildImprintKdfForMasterKey(byte[] masterKeyData)
        {
            if (masterKeyData == null) throw new ArgumentNullException(nameof(masterKeyData));

            var result = new CellContextImprintDroid(masterKeyData);
            return result;
        }

        public ICellContextImprint BuildImprintKdfForStream(Stream masterKeyStream)
        {
            if (masterKeyStream == null) throw new ArgumentNullException(nameof(masterKeyStream));

            byte[] masterKeyData = ConvertUtilsPortable.StreamToByteArray(masterKeyStream);
            var result = new CellContextImprintDroid(masterKeyData);

            return result;
        }

        public ICellContextImprintDynamic BuildImprintKdfInstance()
        {
            return new CellContextImprintDynamic(builder: this);
        }
    }
}
