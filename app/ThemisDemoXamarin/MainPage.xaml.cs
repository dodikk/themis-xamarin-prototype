using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Themis;


namespace ThemisDemoXamarin
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ExecuteThemisFormsSample();
        }

        private void ExecuteThemisFormsSample()
        {
            string plainTextMessage = "Xamarin.Forms plain text message";



            var cellSealBuilder = DependencyService.Get<ICellSealBuilder>();

            string masterKey = "UkVDMgAAAC13PCVZAKOczZXUpvkhsC+xvwWnv3CLmlG0Wzy8ZBMnT+2yx/dg";
            byte[] masterKeyData =
                masterKey
                    .ToCharArray()
                    .Select((ch) => (byte)ch)
                    .ToArray();

            using (ICellSeal cellSeal = cellSealBuilder.BuildCellSealForMasterKey(masterKeyData: masterKeyData))
            {

                // print plain text
                // ===
                byte[] plainTextMessageData =
                    plainTextMessage
                        .ToCharArray()
                        .Select((ch) => (byte)ch)
                        .ToArray();
                string plainTextBase64 = Convert.ToBase64String(plainTextMessageData);
                Console.WriteLine($"[themis demo forms] Initial Text: {plainTextMessage}");
                Console.WriteLine($"[themis demo forms] Initial Text base64: {plainTextBase64}");


                // encrypt
                // ===
                Console.WriteLine("[themis demo forms] Encrypting...");
                using (ISecureCellData cipherText =
                    cellSeal.WrapData(
                        plainTextData: plainTextMessageData,
                        context: null))
                {

                    Console.WriteLine("[themis demo forms] Done.");


                    // print encrypted data
                    // ====
                    byte[] encryptedData = cipherText.GetEncryptedData();
                    char[] cipherTextChars = encryptedData.Select(b => (char)b).ToArray();
                    string cipherTextBase64 = Convert.ToBase64String(encryptedData);

                    string txtCipherText =
                        new string(
                            value: cipherTextChars,
                            startIndex: 0,
                            length: cipherTextChars.Length);
                    Console.WriteLine($"[themis demo forms] cipher Text: {txtCipherText}");
                    Console.WriteLine($"[themis demo forms] cipher Text base64: {cipherTextBase64}");



                    // decrypt and print
                    // ===
                    byte[] decryptedData =
                        cellSeal.UnwrapData(
                            cipherTextData: cipherText,
                            context: null);
                    char[] decryptedDataChars = decryptedData.Select(b => (char)b).ToArray();
                    string decryptedDataBase64 = Convert.ToBase64String(decryptedData);

                    string decryptedText =
                        new string(
                            value: decryptedDataChars,
                            startIndex: 0,
                            length: decryptedDataChars.Length);

                    Console.WriteLine($"[themis demo forms] Decrypted Text: {decryptedText}");
                    Console.WriteLine($"[themis demo forms] Decrypted Text base64: {decryptedDataBase64}");
                }
            }
        }
    }
}
