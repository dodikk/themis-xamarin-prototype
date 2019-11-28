using System;

namespace Themis
{
    public interface ICellSeal: IDisposable
    {
        // --
        // TODO: maybe add C# streams support later for real life usage
        // --

        ISecureCellData WrapData(byte[] plainTextData, byte[] context = null);
        byte[] UnwrapData(ISecureCellData cypherTextData, byte[] context = null);
    }
}
