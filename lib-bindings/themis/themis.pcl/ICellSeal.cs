using System;
using System.IO;


namespace Themis
{
    /// <summary>
    /// Mirrors TSCellSeal / SecureCell API of themis as is
    /// </summary>
    public interface ICellSeal: IDisposable
    {
        ISecureCellData WrapData(byte[] plainTextData, byte[] context = null);
        byte[] UnwrapData(ISecureCellData cipherTextData, byte[] context = null);

        ISecureCellData WrapDataStream(Stream plainTextStream, Stream contextStream = null);
        Stream UnwrapDataAsStream(ISecureCellData cipherTextData, Stream contextStream = null);
    }
}
