using System;
using System.IO;


namespace Themis
{
    public class CellSealDynamic: ICellSealDynamic
    {
        public CellSealDynamic(ICellSealBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public ISecureCellData WrapData(byte[] plainTextData, byte[] context, byte[] key)
        {
            if (plainTextData == null) throw new ArgumentNullException(nameof(plainTextData));
            if (key == null) throw new ArgumentNullException(nameof(key));
            // Note: context can be null

            using (ICellSeal themisSeal = _builder.BuildCellSealForMasterKey(key))
            {
                return themisSeal.WrapData(plainTextData, context);
            }
        }

        public byte[] UnwrapData(ISecureCellData cipherTextData, byte[] context, byte[] key)
        {
            if (cipherTextData == null) throw new ArgumentNullException(nameof(cipherTextData));
            if (key == null) throw new ArgumentNullException(nameof(key));
            // Note: context can be null

            using (ICellSeal themisSeal = _builder.BuildCellSealForMasterKey(key))
            {
                return themisSeal.UnwrapData(cipherTextData, context);
            }
        }

        public ISecureCellData WrapDataStream(Stream plainTextStream, Stream contextStream, Stream keyStream)
        {
            if (plainTextStream == null) throw new ArgumentNullException(nameof(plainTextStream));
            if (keyStream == null) throw new ArgumentNullException(nameof(keyStream));
            // Note: context can be null

            using (ICellSeal themisSeal = _builder.BuildCellSealForMasterKeyStream(keyStream))
            {
                return themisSeal.WrapDataStream(plainTextStream, contextStream);
            }
        }

        public Stream UnwrapDataAsStream(ISecureCellData cipherTextData, Stream contextStream, Stream keyStream)
        {
            if (cipherTextData == null) throw new ArgumentNullException(nameof(cipherTextData));
            if (keyStream == null) throw new ArgumentNullException(nameof(keyStream));
            // Note: context can be null

            using (ICellSeal themisSeal = _builder.BuildCellSealForMasterKeyStream(keyStream))
            {
                return themisSeal.UnwrapDataAsStream(cipherTextData, contextStream);
            }
        }

        private ICellSealBuilder _builder;
    }
}
