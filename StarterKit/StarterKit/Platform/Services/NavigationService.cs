using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StarterKit.Services
{
    public class NavigationService : INavigationService
    {
        private Func<INavigation> _scopedNavigation;
        private Func<INavigation> _rootNavigation;

        private readonly IViewService _viewService;

        public NavigationService(IViewService viewService)
        {
            _viewService = viewService;
        }

        private INavigation GetNavigation(bool scopeNavigation = true) =>
            (scopeNavigation ? _scopedNavigation ?? _rootNavigation : _rootNavigation)();

        public async Task<IViewModel> PopAsync()
        {
            Page view = await GetNavigation().PopAsync();
            return view.BindingContext as IViewModel;
        }

        public async Task<IViewModel> PopModalAsync()
        {
            Page view = await GetNavigation().PopAsync();
            return view.BindingContext as IViewModel;
        }

        public async Task PopToRootAsync()
        {
            await GetNavigation().PopToRootAsync();
        }

        public async Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> setStateAction = null, bool scopeNavigation = true)
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = (Page)_viewService.Resolve<TViewModel>(out viewModel, setStateAction);
            await GetNavigation(scopeNavigation).PushAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel, bool scopeNavigation = true)
            where TViewModel : class, IViewModel
        {
            var view = (Page)_viewService.Resolve(viewModel);
            await GetNavigation(scopeNavigation).PushAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(Action<TViewModel> setStateAction = null, bool scopeNavigation = true)
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = (Page)_viewService.Resolve<TViewModel>(out viewModel, setStateAction);
            await GetNavigation(scopeNavigation).PushModalAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel, bool scopeNavigation = true)
            where TViewModel : class, IViewModel
        {
            var view = (Page)_viewService.Resolve(viewModel);
            await GetNavigation(scopeNavigation).PushModalAsync(view);
            return viewModel;
        }

        public void SetRootNavigationContext(Func<INavigation> context) => _rootNavigation = context;

        public void SetScopedNavigationContext(Func<INavigation> context) => _scopedNavigation = context;
    }
}
