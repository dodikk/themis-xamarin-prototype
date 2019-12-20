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

        public byte[] UnwrapData(ISecureCellData cypherTextData, byte[] context, byte[] key)
        {
            if (cypherTextData == null) throw new ArgumentNullException(nameof(cypherTextData));
            if (key == null) throw new ArgumentNullException(nameof(key));
            // Note: context can be null

            using (ICellSeal themisSeal = _builder.BuildCellSealForMasterKey(key))
            {
                return themisSeal.UnwrapData(cypherTextData, context);
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

        public Stream UnwrapDataAsStream(ISecureCellData cypherTextData, Stream contextStream, Stream keyStream)
        {
            if (cypherTextData == null) throw new ArgumentNullException(nameof(cypherTextData));
            if (keyStream == null) throw new ArgumentNullException(nameof(keyStream));
            // Note: context can be null

            using (ICellSeal themisSeal = _builder.BuildCellSealForMasterKeyStream(keyStream))
            {
                return themisSeal.UnwrapDataAsStream(cypherTextData, contextStream);
            }
        }

        private ICellSealBuilder _builder;
    }
}
