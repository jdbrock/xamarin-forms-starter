using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.HockeyApp;

namespace StarterKit.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            var app = new StarterKit.App();

            if (!String.IsNullOrWhiteSpace(StarterKit.App.Secrets.HockeyAppId))
                Microsoft.HockeyApp.HockeyClient.Current.Configure(StarterKit.App.Secrets.HockeyAppId);

            LoadApplication(app);
        }
    }
}
