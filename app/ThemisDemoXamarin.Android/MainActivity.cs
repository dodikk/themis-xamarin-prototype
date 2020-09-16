using System;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;

using Com.Cossacklabs.Themis;
using Themis;
using Themis.Droid;


namespace ThemisDemoXamarin.Droid
{
    [Activity(Label = "ThemisDemoXamarin", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // for Xamarin.Forms managed code demo in MainPage.xaml.cs
            // -
            DependencyService.Register<ICellSealBuilder, CellSealBuilderDroid>();

            // Xamarin.Android demo
            // -
            ExecuteThemisNativeSample();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions,
            [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void ExecuteThemisNativeSample()
        {
            string masterKeyString = "UkVDMgAAAC13PCVZAKOczZXUpvkhsC+xvwWnv3CLmlG0Wzy8ZBMnT+2yx/dg";
            byte[] masterKeyBytes = Convert.FromBase64String(masterKeyString);
            var secureCell = SecureCell.SealWithKey(masterKeyBytes);
                // new SecureCell(masterKeyString);

            string plainTextMessage = "Droid binding plain text message";
            Console.WriteLine($"[themis demo] Initial Text: {plainTextMessage}");

            

            Console.WriteLine("[themis demo] Encrypting...");

            byte[] plainTextMessageData =
                plainTextMessage
                    .ToCharArray()
                    .Select((ch) => (byte)ch)
                    .ToArray();
            string plainTextBase64 = Convert.ToBase64String(plainTextMessageData);
            Console.WriteLine($"[themis demo] Initial Text base64: {plainTextBase64}");


            byte[] mockContextBytes =
                "no context".ToCharArray()
                            .Select(ch => (byte)ch)
                            .ToArray();

            byte[] cipherText = secureCell.Encrypt(
                plainTextMessageData,
                mockContextBytes);

            Console.WriteLine("[themis demo] Done.");

            // convert ciphertext for printing
            // -
            char[] cipherTextChars = cipherText.Select(b => (char)b).ToArray();
            string cipherTextBase64 = Convert.ToBase64String(cipherText);

            string txtCipherText =
                new string(
                    value: cipherTextChars,
                    startIndex: 0,
                    length: cipherTextChars.Length);
            Console.WriteLine($"[themis demo] cipher Text: {txtCipherText}");
            Console.WriteLine($"[themis demo] cipher Text base64: {cipherTextBase64}");


            byte[] decryptedData = secureCell.Decrypt(
                cipherText,
                mockContextBytes);

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