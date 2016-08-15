using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.Views
{
    public interface IView
    {
        bool WrapWithNavigationPage { get; }
        object ViewModel { get; set; }
    }

    public interface IView<T> : IView
    {
        T ViewModel { get; set; }
    }
}
