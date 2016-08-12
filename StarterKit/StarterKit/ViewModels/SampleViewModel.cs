using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.ViewModels
{
    [ImplementPropertyChanged]
    public class SampleViewModel : ViewModelBase
    {
        public string Greeting { get; set; }

        public SampleViewModel()
        {
            Greeting = "Greetings, earthling.";
        }
    }
}
