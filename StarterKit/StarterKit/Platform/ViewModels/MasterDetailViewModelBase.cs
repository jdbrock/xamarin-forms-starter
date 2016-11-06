using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.ViewModels
{
    [ImplementPropertyChanged]
    public abstract class MasterDetailViewModelBase : ViewModelBase, IViewModelWithMasterDetail
    {
        public IViewModel Master { get; protected set; }
        public IViewModel Detail { get; protected set; }
    }
}
