using System;
using System.IO;

namespace Themis
{
    /// <summary>
    /// Mirrors TSCellContextImprint / SecureCell API of themis as in Bear.app article
    ///
    /// /// The MasterKey is not cached and TSCellSealImprint is created under the hood for each operation
    /// 
    /// Note: For this reason there is no need for IDisposable
    ///       since native stuff is disposed under the hood right away
    ///       
    /// </summary>
    public interface ICellContextImprintDynamic
    {
        byte[] DeriveKey(byte[] fromData, byte[] context, byte[] key);
        Stream DeriveKeyAsStream(Stream fromStream, Stream contextStream, Stream keyStream);
    }
}
