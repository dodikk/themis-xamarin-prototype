using System;
using System.IO;


namespace Themis
{
    public interface ISecureCellData: IDisposable
    {
        byte[] GetEncryptedData();
        Stream GetEncryptedDataAsStream();
    }
}
