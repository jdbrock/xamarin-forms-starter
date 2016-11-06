using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using StarterKit.Services;

namespace StarterKit.iOS.Services
{
    //TODO: Test on physical device
    /// <summary>
    /// Based on ideas explained at https://forums.xamarin.com/discussion/42849/fix-orientation-for-one-page-in-the-app-is-it-possible-with-xamarin-forms
    /// </summary>
    public class AppleOrientationService : IOrientationService
    {
        private const UIInterfaceOrientationMask DEFAULT_ORIENTATION_MASK = UIInterfaceOrientationMask.Portrait;

        private readonly Stack<UIDeviceOrientation> _evictedOrientations = new Stack<UIDeviceOrientation>();
        private readonly Stack<UIInterfaceOrientationMask> _evictedOrientationMasks = new Stack<UIInterfaceOrientationMask>();

        // ===========================================================================
        // = AppleOrientationService API
        // ===========================================================================

        public UIDeviceOrientation ActiveOrientation
        {
            get
            {
                return (UIDeviceOrientation)((int)((NSNumber)UIDevice.CurrentDevice.ValueForKey((NSString)"orientation")));
            }
            private set
            {
                UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromInt32((int)value), (NSString)"orientation");
            }
        }

        private UIInterfaceOrientationMask? _activeOrientationMask;
        public UIInterfaceOrientationMask ActiveOrientationMask
        {
            get
            {
                return _activeOrientationMask ?? DEFAULT_ORIENTATION_MASK;
            }
        }

        // ===========================================================================
        // = IOrientationService
        // ===========================================================================

        public DeviceOrientation Current
        {
            get
            {
                if (ActiveOrientationMask.HasFlag(UIInterfaceOrientationMask.Landscape) &&
                    !ActiveOrientationMask.HasFlag(UIInterfaceOrientationMask.Portrait))
                    return DeviceOrientation.Landscape;
                else if (!ActiveOrientationMask.HasFlag(UIInterfaceOrientationMask.Landscape) &&
                         ActiveOrientationMask.HasFlag(UIInterfaceOrientationMask.Portrait))
                    return DeviceOrientation.Portait;

                return DeviceOrientation.Auto;
            }
        }

        public void Pop()
        {
            // Set the desired mask anyway
            _activeOrientationMask = _evictedOrientationMasks.Any() ? _evictedOrientationMasks.Pop() : DEFAULT_ORIENTATION_MASK;

            // only try to actively set the orientation, if we've messed with it
            if(_evictedOrientations.Any())
                ActiveOrientation = _evictedOrientations.Pop();
        }

        public void Push(DeviceOrientation orientation)
        {
            var newAppleOrientationMask = _activeOrientationMask;
            var newAppleOrientation = ActiveOrientation;

            if (orientation == DeviceOrientation.Portait)
            {
                newAppleOrientationMask = UIInterfaceOrientationMask.Portrait;
                newAppleOrientation = UIDeviceOrientation.Portrait;
            }
            else if (orientation == DeviceOrientation.Landscape)
            {
                newAppleOrientationMask = UIInterfaceOrientationMask.Landscape;
                newAppleOrientation = UIDeviceOrientation.LandscapeLeft;
            }

            _evictedOrientationMasks.Push(ActiveOrientationMask);
            _activeOrientationMask = newAppleOrientationMask;

            _evictedOrientations.Push(ActiveOrientation);
            ActiveOrientation = newAppleOrientation;
        }
    }
}
