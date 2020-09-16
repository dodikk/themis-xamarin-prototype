using System;
using Foundation;
using UIKit;
using Themis;
using Themis.iOS; // a namespace from the binding project
using Xamarin.Forms;

namespace ThemisDemoXamarin.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // for Xamarin.Forms managed code demo in MainPage.xaml.cs
            // -
            DependencyService.Register<ICellSealBuilder, CellSealBuilderIos>();

            // Xamarin.iOS demo
            // -
            this.ExecuteThemisNativeSample();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void ExecuteThemisNativeSample()
        {
            // https://docs.cossacklabs.com/pages/objective-c-howto/#secure-cell

            var masterKeyString =
                new NSString("UkVDMgAAAC13PCVZAKOczZXUpvkhsC+xvwWnv3CLmlG0Wzy8ZBMnT+2yx/dg");

            var masterKeyData =
                new NSData(
                    base64String: masterKeyString,
                    options: NSDataBase64DecodingOptions.IgnoreUnknownCharacters);

            var cellSeal = new TSCellSeal(masterKeyData);

            string message = "iOS binding plain text message";
            string messageBase64 =
                NSData.FromString(message)
                      .GetBase64EncodedString(options: NSDataBase64EncodingOptions.None);

            Console.WriteLine($"Initial Text: {message}");
            Console.WriteLine($"Initial Text base64: {messageBase64}");
            Console.WriteLine("Encrypting...");

            NSData encryptedMessage = DoEncrypt(withCellSeal: cellSeal, plainTextMessage: message);
            if (null == encryptedMessage)
            {
                return;
            }

            string txtEncryptedMessage =
                encryptedMessage.GetBase64EncodedString(options: NSDataBase64EncodingOptions.None);

            // NSString.FromData returns null
            // NSString rawciphertext = NSString.FromData(encryptedMessage, encoding: NSStringEncoding.UTF8);
            Console.WriteLine($"ciphertext: <cannot be printed from NSData in Xamarin.iOS> | probably a security measure by themis team");
            Console.WriteLine($"ciphertext base64: {txtEncryptedMessage}");


            Console.WriteLine("Decrypting...");
            string decryptedMessage = DoDecrypt(withCellSeal: cellSeal, encryptedMessage);
            string decryptedBase64 =
                NSData.FromString(decryptedMessage)
                      .GetBase64EncodedString(options: NSDataBase64EncodingOptions.None);
            Console.WriteLine($"Decrypted text: {decryptedMessage}");
            Console.WriteLine($"Decrypted text base64: {decryptedBase64}");
        }

        private NSData DoEncrypt(TSCellSeal withCellSeal, string plainTextMessage)
        {
            if (null == plainTextMessage)
            {
                return null;
            }

            TSCellSeal cellSeal = withCellSeal;
            string message = plainTextMessage;

            // https://docs.cossacklabs.com/pages/objective-c-howto/#secure-cell

            NSData messageData = NSData.FromString(message);

            NSError themisError = null;
            NSData encryptedMessage =
                cellSeal.WrapData(
                    message: messageData,
                    error: out themisError);

            if (null != themisError)
            {
                Console.WriteLine($"Encyption error: {themisError.LocalizedDescription}");
                return null;
            }

            return encryptedMessage;
        }

        private string DoDecrypt(TSCellSeal withCellSeal, NSData cipherText)
        {
            if (null == cipherText)
            {
                return null;
            }

            TSCellSeal cellSeal = withCellSeal;

            // https://docs.cossacklabs.com/pages/objective-c-howto/#secure-cell

            NSError themisError = null;
            NSData plainTextData =
                cellSeal.UnwrapData(
                    message: cipherText,
                    error: out themisError);

            if (null != themisError)
            {
                Console.WriteLine($"Encyption error: {themisError.LocalizedDescription}");
                return null;
            }

            NSString rawPlainText =
                NSString.FromData(
                    data: plainTextData,
                    encoding: NSStringEncoding.UTF8);

            string plainText = rawPlainText.ToString();

            return plainText;
        }
    }
}
