using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit
{
    public interface IViewModel
    {
        event EventHandler StateChanged;
        void SetState<T>(Action<T> action) where T : class, IViewModel;
    }
}
