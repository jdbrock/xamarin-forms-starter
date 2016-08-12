using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.ViewModels
{
    [ImplementPropertyChanged]
    public class SampleViewModel2 : ViewModelBase
    {
        public string Greeting { get; set; }

        public SampleViewModel2()
        {
            Greeting = "Greetings, earthling.";
        }
    }
}
