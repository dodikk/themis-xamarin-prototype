using System;
using System.IO;
using Foundation;
using Themis.iOS.Utils;


namespace Themis.iOS
{
    public class CellContextImprintIos : ICellContextImprint
    {
        public void Dispose()
        {
            try
            {
                if (_themisImprint != null)
                {
                    _themisImprint.Dispose();
                    _themisImprint = null;
                }
            }
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            catch
            {
                // Suppressing.
                // A destructor must never throw
            }
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
        }

        public CellContextImprintIos(NSData masterKeyData)
        {
            if (masterKeyData == null) throw new ArgumentNullException(nameof(masterKeyData));

            _themisImprint = new TSCellContextImprint(key: masterKeyData);
        }

        public byte[] DeriveKey(byte[] fromData, byte[] context = null)
        {
            if (fromData == null) throw new ArgumentNullException(nameof(fromData));

            byte[] data = fromData;

            NSData nsDataKey = ConvertUtilsIos.ByteArrayToNSData(fromData);
            var themisImprint = new TSCellContextImprint(key: nsDataKey);

            NSData nsDataContext = ConvertUtilsIos.ByteArrayToNSData(context);
            NSData nsDataToDerive = ConvertUtilsIos.ByteArrayToNSData(data);


            NSError themisError = null;
            NSData nsDataResult = null;

            try
            {
                nsDataResult = themisImprint.WrapData(
                    message: nsDataToDerive,
                    context: nsDataContext,
                    error: out themisError);
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "[FAILED] TSCellContextImprint.WrapData()",
                    inner: ex);
            }


            if (themisError != null)
            {
                throw new ThemisXamarinBridgeException($"NSError encrypting data with themis: {themisError.LocalizedDescription}");
            }

            byte[] result = ConvertUtilsIos.NSDataToByteArray(nsDataResult);
            return result;
        }

        public Stream DeriveKeyAsStream(Stream fromStream, Stream contextStream = null)
        {
            if (fromStream == null) throw new ArgumentNullException(nameof(fromStream));

            Stream keyMaterialDataStream = fromStream;

            byte[] keyMaterialBytes = ConvertUtilsPortable.StreamToByteArray(keyMaterialDataStream);
            byte[] contextBytes = ConvertUtilsPortable.StreamToByteArray(contextStream);

            byte[] resultBytes = DeriveKey(keyMaterialBytes, contextBytes);
            Stream result = ConvertUtilsPortable.ByteArrayToMemoryStream(resultBytes);

            return result;
        }


        private TSCellContextImprint _themisImprint;
    }
}
