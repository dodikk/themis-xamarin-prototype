using System;
using System.IO;


namespace Themis
{
    /// <summary>
    /// Mirrors TSCellContextImprint / SecureCell API of themis as is
    /// </summary>
    public interface ICellContextImprint: IDisposable
    {
        byte[] DeriveKey(byte[] fromData, byte[] context = null);
        Stream DeriveKeyAsStream(Stream fromStream, Stream contextStream = null);
    }
}
