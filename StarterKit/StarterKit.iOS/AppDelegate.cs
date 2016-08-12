using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using HockeyApp.iOS;

namespace StarterKit.iOS
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
        public override bool FinishedLaunching(UIApplication uiApp, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Create app.
            var app = new App();

            // Start HockeyApp manager.
            InitializeHockeyApp();

            // Load our app.
            LoadApplication(app);

            return base.FinishedLaunching(uiApp, options);
        }

        private void InitializeHockeyApp()
        {
            if (String.IsNullOrWhiteSpace(App.Secrets.HockeyAppId))
                return;

            var hockeyManager = BITHockeyManager.SharedHockeyManager;

            hockeyManager.Configure(App.Secrets.HockeyAppId);
            hockeyManager.DebugLogEnabled = true;
            hockeyManager.StartManager();
        }
    }
}
