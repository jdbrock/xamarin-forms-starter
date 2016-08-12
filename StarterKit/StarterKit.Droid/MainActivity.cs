﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;

namespace StarterKit.Droid
{
    [Activity(Label = "StarterKit", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            var app = new App();

            if (!String.IsNullOrWhiteSpace(App.Secrets.HockeyAppId))
                CrashManager.Register(this, App.Secrets.HockeyAppId);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(app);
        }
    }
}

