using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Cossacklabs.Themis;
using System.Linq;

namespace ThemisDemoXamarin.Droid
{
    [Activity(Label = "ThemisDemoXamarin", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
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
            var secureCell = new SecureCell(masterKeyString);

            string plainTextMessage = "Droid binding plain text message";
            Console.WriteLine($"Initial Text: {plainTextMessage}");

            

            Console.WriteLine("Encrypting...");

            byte[] plainTextMessageData =
                plainTextMessage
                    .ToCharArray()
                    .Select((ch) => (byte)ch)
                    .ToArray();

            SecureCellData cypherTextHolder =
                secureCell.Protect(
                    context: "no context",
                    data: plainTextMessageData);

            Console.WriteLine("Done.");

            // convert cyphertext for printing
            // -
            byte[] cypherText = cypherTextHolder.GetProtectedData();
            char[] cypherTextChars = cypherText.Select(b => (char)b).ToArray();

            string txtCypherText =
                new string(
                    value: cypherTextChars,
                    startIndex: 0,
                    length: cypherTextChars.Length);
            Console.WriteLine($"Cypher Text: {txtCypherText}");


            byte[] decryptedData = secureCell.Unprotect(
                context: "no context",
                protectedData: cypherTextHolder);
            char[] decryptedDataChars = decryptedData.Select(b => (char)b).ToArray();
            string decryptedText =
                new string(
                    value: decryptedDataChars,
                    startIndex: 0,
                    length: decryptedDataChars.Length);
            Console.WriteLine($"Decrypted Text: {decryptedText}");
        }
    }
}