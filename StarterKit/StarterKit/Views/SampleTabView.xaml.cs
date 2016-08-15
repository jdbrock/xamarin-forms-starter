using Acr.UserDialogs;
using StarterKit.Services;
using StarterKit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace StarterKit.Views
{
    // NB. Workaround for specifying generic base classes in XAML.
    public partial class SampleTabView : TabViewBase<SampleTabViewModel>
    {
        public SampleTabView(IViewService viewService)
            : base(viewService)
        {
            InitializeComponent();
        }
    }
}
