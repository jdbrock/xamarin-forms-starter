using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Autofac;
using StarterKit.Views;
using MaterialDesignColors;
using StarterKit.ViewModels;

namespace StarterKit.Services
{
    public class ViewService : IViewService
    {
        private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();
        private readonly IComponentContext _componentContext;

        public ViewService(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public void Register<TViewModel, TView>()
            where TViewModel : class, IViewModel
            where TView : IView
        {
            _map[typeof(TViewModel)] = typeof(TView);
        }

        public Page Resolve<TViewModel>(Action<TViewModel> setStateAction = null) where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            return Resolve<TViewModel>(out viewModel, setStateAction);
        }

        public Page Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel
        {
            viewModel = _componentContext.Resolve<TViewModel>();

            var viewType = _map[typeof(TViewModel)];
            var view = _componentContext.Resolve(viewType) as IView<TViewModel>;

            if (setStateAction != null)
                viewModel.SetState(setStateAction);

            view.ViewModel = viewModel;

            if (view.WrapWithNavigationPage)
                return WrapWithNavigationPage((Page)view);

            return (Page)view;
        }

        public Page Resolve<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel
        {
            var viewType = _map[typeof(TViewModel)];
            var view = _componentContext.Resolve(viewType) as IView<TViewModel>;
            view.ViewModel = viewModel;

            if (view.WrapWithNavigationPage)
                return WrapWithNavigationPage((Page)view);

            return (Page)view;
        }

        public Page Resolve(object viewModel, Type type)
        {
            var viewType = _map[type];
            var view = _componentContext.Resolve(viewType) as IView;

            view.ViewModel = viewModel;

            if (view.WrapWithNavigationPage)
                return WrapWithNavigationPage((Page)view);

            return (Page)view;
        }

        private NavigationPage WrapWithNavigationPage(Page child)
        {
            var nav = new NavigationPage(child);

            nav.SetBinding(NavigationPage.IconProperty,  new Binding(nameof(Page.Icon),  source: child));
            nav.SetBinding(NavigationPage.TitleProperty, new Binding(nameof(Page.Title), source: child));

            nav.BarTextColor       = MaterialColors.PrimaryHueMidForeground;
            nav.BarBackgroundColor = MaterialColors.PrimaryHueMid;

            return nav;
        }
    }
}
