using System;
using System.IO;


namespace Themis
{
    public class CellContextImprintDynamic: ICellContextImprintDynamic
    {
        public CellContextImprintDynamic(ICellSealBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public byte[] DeriveKey(byte[] fromData, byte[] context, byte[] key)
        {
            if (fromData == null) throw new ArgumentNullException(nameof(fromData));
            if (key == null) throw new ArgumentNullException(nameof(key));
            // Note: context can be null

            using (ICellContextImprint themisImprintKdf = _builder.BuildImprintKdfForMasterKey(key))
            {
                return themisImprintKdf.DeriveKey(fromData, context);
            }
        }

        public Stream DeriveKeyAsStream(Stream fromStream, Stream contextStream, Stream keyStream)
        {
            if (fromStream == null) throw new ArgumentNullException(nameof(fromStream));
            if (keyStream == null) throw new ArgumentNullException(nameof(keyStream));
            // Note: context can be null

            using (ICellContextImprint themisImprintKdf = _builder.BuildImprintKdfForStream(keyStream))
            {
                return themisImprintKdf.DeriveKeyAsStream(fromStream, contextStream);
            }
        }

        private ICellSealBuilder _builder;
    }
}
