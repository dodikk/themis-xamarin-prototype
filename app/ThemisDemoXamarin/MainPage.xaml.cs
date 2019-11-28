using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                using (ISecureCellData cypherText =
                    cellSeal.WrapData(
                        plainTextData: plainTextMessageData,
                        context: null))
                {

                    Console.WriteLine("[themis demo forms] Done.");


                    // print encrypted data
                    // ====
                    byte[] encryptedData = cypherText.GetEncryptedData();
                    char[] cypherTextChars = encryptedData.Select(b => (char)b).ToArray();
                    string cypherTextBase64 = Convert.ToBase64String(encryptedData);

                    string txtCypherText =
                        new string(
                            value: cypherTextChars,
                            startIndex: 0,
                            length: cypherTextChars.Length);
                    Console.WriteLine($"[themis demo forms] Cypher Text: {txtCypherText}");
                    Console.WriteLine($"[themis demo forms] Cypher Text base64: {cypherTextBase64}");



                    // decrypt and print
                    // ===
                    byte[] decryptedData =
                        cellSeal.UnwrapData(
                            cypherTextData: cypherText,
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
