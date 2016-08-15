using Autofac;
using PropertyChanged;
using StarterKit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StarterKit.Views
{
    public abstract class TabViewBase<T> : TabbedPage, IView<T>
        where T : IViewModelWithTabs
    {
        private T _viewModel;
        public T ViewModel
        {
            get { return _viewModel; }
            set { var oldViewModel = _viewModel; _viewModel = value; BindingContext = _viewModel; OnViewModelReplaced(oldViewModel, _viewModel); }
        }

        object IView.ViewModel { get { return ViewModel; } set { ViewModel = (T)value; } }

        private IViewService _viewService;

        public TabViewBase(IViewService viewService)
        {
            _viewService = viewService;
        }

        public virtual void OnViewModelReplaced(IViewModelWithTabs oldViewModel, IViewModelWithTabs newViewModel)
        {
            if (oldViewModel != null)
            {
                oldViewModel.StateChanged -= OnViewModelRefreshed;
                oldViewModel.TabsChanged -= OnTabsChanged;
            }

            if (newViewModel != null)
            {
                newViewModel.StateChanged += OnViewModelRefreshed;
                newViewModel.TabsChanged += OnTabsChanged;
            }

            OnViewModelRefreshed(this, EventArgs.Empty);
            OnTabsChanged(this, EventArgs.Empty);
        }

        private void OnTabsChanged(object sender, EventArgs e)
        {
            if (ViewModel?.Tabs == null)
                return;

            Children.Clear();

            foreach (var tab in ViewModel.Tabs)
            {
                var view = (Page)_viewService.Resolve(tab, tab.GetType());
                Children.Add(view);
            }
        }

        public virtual void OnViewModelRefreshed(object sender, EventArgs args) { }

        public virtual bool WrapWithNavigationPage { get; } = false;
    }
}
