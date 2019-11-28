﻿using System;
using Foundation;
using Themis.iOS;


namespace Themis
{
    public class CellSealBuilderIos: ICellSealBuilder
    {
        public ICellSeal BuildCellSealForMasterKey(byte[] masterKeyData)
        {
            try
            {
                string masterKeyBase64 = Convert.ToBase64String(masterKeyData);
                NSData nsMasterKeyData =
                    new NSData(
                        base64String: masterKeyBase64,
                        options: NSDataBase64DecodingOptions.None);

                var result = new CellSealIos(masterKeyData: nsMasterKeyData);

                return result;
            }
            catch (ThemisXamarinBridgeException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ThemisXamarinBridgeException(
                    message: "[FAIL] [iOS] CellSealBuilderIos.BuildCellSealForMasterKey()",
                    inner: ex);
            }
        }
    }
}
