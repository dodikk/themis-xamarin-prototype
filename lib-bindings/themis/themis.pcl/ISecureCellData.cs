﻿using System;

namespace Themis
{
    public interface ISecureCellData: IDisposable
    {
        byte[] GetEncryptedData();
    }
}
