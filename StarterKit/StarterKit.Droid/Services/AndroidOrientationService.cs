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
using Android.Content.PM;

using StarterKit.Services;

namespace StarterKit.Droid.Services
{
    /// <summary>
    /// Based on ideas explained at https://forums.xamarin.com/discussion/42849/fix-orientation-for-one-page-in-the-app-is-it-possible-with-xamarin-forms
    /// </summary>
    public class AndroidOrientationService : IOrientationService
    {
        private const ScreenOrientation DEFAULT_ORIENTATION = ScreenOrientation.Portrait;

        private readonly Stack<ScreenOrientation> _evictedOrientations = new Stack<ScreenOrientation>();
        private ScreenOrientation? _requestedAndroidOrientation = null;

        private MainActivity MainActivity => Xamarin.Forms.Forms.Context as MainActivity;

        // ===========================================================================
        // = Android API
        // ===========================================================================

        public ScreenOrientation GetAndroidOrientation()
        {
            return _requestedAndroidOrientation ?? DEFAULT_ORIENTATION;
        }

        // ===========================================================================
        // = IOrientationService
        // ===========================================================================

        public DeviceOrientation Current
        {
            get
            {
                var androidOrientation = MainActivity.RequestedOrientation;

                if (androidOrientation == ScreenOrientation.Portrait ||
                    androidOrientation == ScreenOrientation.UserPortrait)
                    return DeviceOrientation.Portait;
                else if (androidOrientation == ScreenOrientation.Landscape ||
                         androidOrientation == ScreenOrientation.UserLandscape)
                    return DeviceOrientation.Landscape;

                return DeviceOrientation.Auto;
            }
        }

        public void Pop()
        {
            _requestedAndroidOrientation = _evictedOrientations.Any() ? _evictedOrientations.Pop() : DEFAULT_ORIENTATION;

            MainActivity.OnConfigurationChanged(new Android.Content.Res.Configuration());
        }

        public void Push(DeviceOrientation orientation)
        {
            var androidOrientation = DEFAULT_ORIENTATION;
            if (orientation == DeviceOrientation.Portait)
                androidOrientation = ScreenOrientation.Portrait;
            else if (orientation == DeviceOrientation.Landscape)
                androidOrientation = ScreenOrientation.Landscape;

            _evictedOrientations.Push(MainActivity.RequestedOrientation);
            _requestedAndroidOrientation = androidOrientation;
            
            MainActivity.OnConfigurationChanged(new Android.Content.Res.Configuration());
        }
    }
}