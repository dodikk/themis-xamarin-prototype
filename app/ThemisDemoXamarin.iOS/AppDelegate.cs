using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Themis;

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
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            this.ExecuteThemisNativeSample();

            return base.FinishedLaunching(app, options);
        }

        private void ExecuteThemisNativeSample()
        {
            var masterKeyString =
                new NSString("UkVDMgAAAC13PCVZAKOczZXUpvkhsC+xvwWnv3CLmlG0Wzy8ZBMnT+2yx/dg");

            var masterKeyData =
                new NSData(
                    base64String: masterKeyString,
                    options: NSDataBase64DecodingOptions.IgnoreUnknownCharacters);

            var cellSeal = new TSCellSeal(masterKeyData);
        }
    }
}
