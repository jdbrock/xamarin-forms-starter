using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarterKit.Views;
using Xamarin.Forms;
using StarterKit.ViewModels;

namespace StarterKit.Services
{
    public class NavigationService : INavigationService
    {
        private Func<INavigation> _scopedNavigation;
        private Func<INavigation> _rootNavigation;

        private readonly IViewService _viewService;
        private readonly IKeyboardService _keyboardService;

        private Stack<KeyboardResizeMode> _keyboardResizeStack;

        public NavigationService(IViewService viewService, IKeyboardService keyboardService = null)
        {
            _viewService = viewService;
            _keyboardService = keyboardService;

            _keyboardResizeStack = new Stack<KeyboardResizeMode>();
        }

        private INavigation GetNavigation(bool scopeNavigation = true) =>
            (scopeNavigation ? _scopedNavigation ?? _rootNavigation : _rootNavigation)();

        public async Task<IViewModel> PopAsync()
        {
            Page view = await GetNavigation().PopAsync();

            if (view is IView)
                PopResizeMode((IView)view);
            
            return view.BindingContext as IViewModel;
        }

        public async Task<IViewModel> PopModalAsync()
        {
            Page view = await GetNavigation().PopModalAsync();

            if (view is IView)
                PopResizeMode((IView)view);
            
            return view.BindingContext as IViewModel;
        }

        public async Task PopToRootAsync()
        {
            // Assumes the root has a mode of 'none'.
            _keyboardResizeStack.Clear();
            _keyboardResizeStack.Push(KeyboardResizeMode.None);
            _keyboardService?.SetKeyboardResizeModeWhereAvailable(KeyboardResizeMode.None);

            await GetNavigation().PopToRootAsync();
        }

        public async Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> setStateAction = null, bool scopeNavigation = true)
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;

            var view = (Page)_viewService.Resolve<TViewModel>(out viewModel, setStateAction);

            if (view is IView)
                PushResizeMode((IView)view);

            await GetNavigation(scopeNavigation).PushAsync(view);

            return viewModel;
        }

        public async Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel, bool scopeNavigation = true)
            where TViewModel : class, IViewModel
        {
            var view = (Page)_viewService.Resolve(viewModel);

            if (view is IView)
                PushResizeMode((IView)view);

            await GetNavigation(scopeNavigation).PushAsync(view);

            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(Action<TViewModel> setStateAction = null, bool scopeNavigation = true)
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;

            var view = (Page)_viewService.Resolve<TViewModel>(out viewModel, setStateAction);

            if (view is IView)
                PushResizeMode((IView)view);

            await GetNavigation(scopeNavigation).PushModalAsync(view);

            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel, bool scopeNavigation = true)
            where TViewModel : class, IViewModel
        {
            var view = (Page)_viewService.Resolve(viewModel);

            if (view is IView)
                PushResizeMode((IView)view);

            await GetNavigation(scopeNavigation).PushModalAsync(view);

            return viewModel;
        }

        private void PopResizeMode(IView view)
        {
            var keyboardResizeMode = _keyboardResizeStack.Pop();
            _keyboardService?.SetKeyboardResizeModeWhereAvailable(keyboardResizeMode);
        }

        private void PushResizeMode(IView view)
        {
            var keyboardResizeMode = ((IView)view).KeyboardResizeMode;

            _keyboardResizeStack.Push(keyboardResizeMode);
            _keyboardService?.SetKeyboardResizeModeWhereAvailable(keyboardResizeMode);
        }

        public void SetRootNavigationContext(Func<INavigation> context) => _rootNavigation = context;

        public void SetScopedNavigationContext(Func<INavigation> context) => _scopedNavigation = context;
    }
}
