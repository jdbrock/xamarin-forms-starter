using System;


namespace StarterKit.Droid
{
    public class AndroidKeyboardService : IKeyboardService
    {
        private static global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity _activity;

        internal static void SetActivity(global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity activity)
        {
            _activity = activity;
        }

        public void SetKeyboardResizeModeWhereAvailable(KeyboardResizeMode mode)
        {
            switch (mode)
            {
                case KeyboardResizeMode.None:
                    _activity.Window.SetSoftInputMode(Android.Views.SoftInput.AdjustNothing);
                    break;

                case KeyboardResizeMode.Pan:
                    _activity.Window.SetSoftInputMode(Android.Views.SoftInput.AdjustPan);
                    break;

                case KeyboardResizeMode.Resize:
                    _activity.Window.SetSoftInputMode(Android.Views.SoftInput.AdjustResize);
                    break;

                default:
                    throw new Exception("Unknown keyboard resize mode: " + mode);
            }
        }
    }
}
