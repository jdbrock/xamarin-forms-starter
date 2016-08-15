using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.ViewModels
{
    public class SampleTabViewModel : TabViewModelBase
    {
        public SampleTabViewModel(SampleViewModel sampleTab)
        {
            Tabs = new List<IViewModel> { sampleTab };
        }
    }
}
