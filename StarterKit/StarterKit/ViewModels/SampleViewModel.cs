using PropertyChanged;
using StarterKit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StarterKit.ViewModels
{
    [ImplementPropertyChanged]
    public class SampleViewModel : ViewModelBase
    {
        public ICommand DoSomethingCommand { get; }

        private INavigationService _navigation;

        public SampleViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            DoSomethingCommand = new Command(async() => await DoSomething());
        }

        private async Task DoSomething()
        {
            await _navigation.PushAsync<SampleViewModel2>();
        }
    }
}
