using Autofac;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StarterKit.Views
{
    public abstract class ViewPageBase<T> : ContentPage where T : IViewModel
    {
        private T _viewModel;
        public T ViewModel
        {
            get { return _viewModel; }
            set { var oldViewModel = _viewModel; _viewModel = value; BindingContext = _viewModel; OnViewModelReplaced(oldViewModel, _viewModel); }
        }

        public virtual void OnViewModelReplaced(IViewModel oldViewModel, IViewModel newViewModel)
        {
            if (oldViewModel != null)
                oldViewModel.StateChanged -= OnViewModelRefreshed;

            if (newViewModel != null)
                newViewModel.StateChanged += OnViewModelRefreshed;

            OnViewModelRefreshed(this, EventArgs.Empty);
        }

        public virtual void OnViewModelRefreshed(object sender, EventArgs args) { }
    }
}
