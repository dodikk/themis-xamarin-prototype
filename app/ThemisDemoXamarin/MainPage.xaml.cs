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

            ICellSeal cellSeal = cellSealBuilder.BuildCellSealForMasterKey(masterKeyData: masterKeyData);


            byte[] plainTextMessageData =
                plainTextMessage
                    .ToCharArray()
                    .Select((ch) => (byte)ch)
                    .ToArray();
            string plainTextBase64 = Convert.ToBase64String(plainTextMessageData);
            Console.WriteLine($"[themis demo forms] Initial Text base64: {plainTextBase64}");


            Console.WriteLine("[themis demo forms] Encrypting...");
            ISecureCellData cypherText =
                cellSeal.WrapData(
                    plainTextData: masterKeyData,
                    context: null);

            Console.WriteLine("[themis demo forms] Done.");


            // TODO: print encrypted data
            // ====

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

            Console.WriteLine($"[themis demo] Decrypted Text: {decryptedText}");
            Console.WriteLine($"[themis demo] Decrypted Text base64: {decryptedDataBase64}");
        }
    }
}
