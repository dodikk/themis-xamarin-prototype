using System.IO;

namespace Themis
{
    /// <summary>
    /// Mirrors TSCellSeal / SecureCell API of themis as is Bear.app article
    /// The MasterKey is not cached and TSCellContextImprint is created under the hood
    /// for each operation
    /// 
    /// Note: For this reason there is no need for IDisposable
    ///       since native stuff is disposed under the hood right away
    /// 
    /// </summary>
    public interface ICellSealDynamic
    {
        ISecureCellData WrapData(
            byte[] plainTextData,
            byte[] context,
            byte[] key);

        byte[] UnwrapData(
            ISecureCellData cypherTextData,
            byte[] context,
            byte[] key);

        ISecureCellData WrapDataStream(
            Stream plainTextStream,
            Stream contextStream,
            Stream keyStream);

        Stream UnwrapDataAsStream(
            ISecureCellData cypherTextData,
            Stream contextStream,
            Stream keyStream);
    }
}
