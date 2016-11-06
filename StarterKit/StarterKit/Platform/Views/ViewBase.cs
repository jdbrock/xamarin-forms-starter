using Autofac;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using StarterKit.ViewModels;

namespace StarterKit.Views
{
    public abstract class ViewBase<T> : ContentPageEx, IView<T>
        where T : IViewModel
    {
        private T _viewModel;
        public T ViewModel
        {
            get { return _viewModel; }
            set { var oldViewModel = _viewModel; _viewModel = value; BindingContext = _viewModel; OnViewModelReplaced(oldViewModel, _viewModel); }
        }

        object IView.ViewModel { get { return ViewModel; } set { ViewModel = (T)value; } }

        public virtual void OnViewModelReplaced(IViewModel oldViewModel, IViewModel newViewModel)
        {
            if (oldViewModel != null)
                oldViewModel.StateChanged -= OnViewModelRefreshed;

            if (newViewModel != null)
                newViewModel.StateChanged += OnViewModelRefreshed;

            OnViewModelRefreshed(this, EventArgs.Empty);
        }

        public virtual void OnViewModelRefreshed(object sender, EventArgs args) { }

        public virtual bool WrapWithNavigationPage { get; } = false;

        public virtual KeyboardResizeMode KeyboardResizeMode { get; } = KeyboardResizeMode.None;
    }
}
