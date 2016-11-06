using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.Services
{
    // TODO: Add more options?
    public enum DeviceOrientation { Auto, Portait, Landscape }

    /// <summary>
    /// A service to enforce orientation restrictions
    /// </summary>
    public interface IOrientationService
    {
        DeviceOrientation Current { get; }
        void Push(DeviceOrientation orientation);
        void Pop();
    }
}
