using Autofac;
using PropertyChanged;
using StarterKit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.ComponentModel;
using StarterKit.ViewModels;

namespace StarterKit.Views
{
    public abstract class MasterDetailViewBase<T> : MasterDetailPage, IView<T>
        where T : IViewModelWithMasterDetail
    {
        private T _viewModel;
        public T ViewModel
        {
            get { return _viewModel; }
            set { var oldViewModel = _viewModel; _viewModel = value; BindingContext = _viewModel; OnViewModelReplaced(oldViewModel, _viewModel); }
        }

        object IView.ViewModel { get { return ViewModel; } set { ViewModel = (T)value; } }

        private IViewService _viewService;

        public MasterDetailViewBase(IViewService viewService)
        {
            _viewService = viewService;
        }

        public virtual void OnViewModelReplaced(IViewModelWithMasterDetail oldViewModel, IViewModelWithMasterDetail newViewModel)
        {
            if (oldViewModel == newViewModel)
                return;

            if (oldViewModel as INotifyPropertyChanged != null)
                ((INotifyPropertyChanged)oldViewModel).PropertyChanged -= OnPropertyChanged;

            if (newViewModel as INotifyPropertyChanged != null)
                ((INotifyPropertyChanged)newViewModel).PropertyChanged += OnPropertyChanged;

            Master = (Page)_viewService.Resolve(ViewModel.Master, ViewModel.Master.GetType());
            Detail = (Page)_viewService.Resolve(ViewModel.Detail, ViewModel.Detail.GetType());

            OnViewModelRefreshed(this, EventArgs.Empty);
        }

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Detail))
            {
                if (ViewModel.Detail == null)
                    Detail = null;
                else
                    Detail = (Page)_viewService.Resolve(ViewModel.Detail, ViewModel.Detail.GetType());
            }

            if (e.PropertyName == nameof(ViewModel.Master))
            {
                if (ViewModel.Master == null)
                    Master = null;
                else
                    Master = (Page)_viewService.Resolve(ViewModel.Master, ViewModel.Master.GetType());
            }
        }

        public virtual void OnViewModelRefreshed(object sender, EventArgs args) { }

        public virtual bool WrapWithNavigationPage { get; } = false;

        public virtual KeyboardResizeMode KeyboardResizeMode { get; } = KeyboardResizeMode.None;
    }
}
