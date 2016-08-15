using StarterKit.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StarterKit.Services
{
    public interface IViewService
    {
        void Register<TViewModel, TView>()
            where TViewModel : class, IViewModel
            where TView : IView;

        Page Resolve<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel;

        Page Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel;

        Page Resolve<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel;

        Page Resolve(object viewModel, Type type);
    }
}
