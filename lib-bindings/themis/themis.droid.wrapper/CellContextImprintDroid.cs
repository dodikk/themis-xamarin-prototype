using System;
using System.IO;
using Com.Cossacklabs.Themis;


namespace Themis.Droid
{
    public class CellContextImprintDroid: ICellContextImprint
    {
        public CellContextImprintDroid(byte[] masterKeyData)
        {
            if (masterKeyData == null) throw new ArgumentNullException(nameof(masterKeyData));

            _themisImprint = SecureCell.ContextImprintWithKey(masterKeyData);
        }

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

        public byte[] DeriveKey(byte[] fromData, byte[] context = null)
        {
            if (fromData == null) throw new ArgumentNullException(nameof(fromData));

            byte[] result = _themisImprint.Encrypt(fromData, context);

            return result;
        }

        public Stream DeriveKeyAsStream(Stream fromStream, Stream contextStream = null)
        {
            if (fromStream == null) throw new ArgumentNullException(nameof(fromStream));

            byte[] fromData = ConvertUtilsPortable.StreamToByteArray(fromStream);
            byte[] contextData = ConvertUtilsPortable.StreamToByteArray(contextStream);

            byte[] resultBytes = DeriveKey(
                fromData: fromData,
                context: contextData);

            Stream result = ConvertUtilsPortable.ByteArrayToMemoryStream(resultBytes);
            return result;
        }

        private SecureCell.IContextImprint _themisImprint;
    }
}
