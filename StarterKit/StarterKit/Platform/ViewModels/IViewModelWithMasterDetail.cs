using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.ViewModels
{
    public interface IViewModelWithMasterDetail : IViewModel
    {
        IViewModel Master { get; }
        IViewModel Detail { get; }
    }
}
