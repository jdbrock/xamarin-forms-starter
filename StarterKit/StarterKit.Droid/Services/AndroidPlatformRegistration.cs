using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using StarterKit.Services;
using StarterKit.Platform.Services;

namespace StarterKit.Droid.Services
{
    public class AndroidPlatformRegistration : IPlatformRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<AndroidOrientationService>()
                .As<IOrientationService>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<AndroidKeyboardService>()
                .As<IKeyboardService>()
                .AsSelf()
                .SingleInstance();
        }
    }
}