using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.ViewModels
{
    public abstract class ViewModelBase : IViewModel
    {
        public event EventHandler StateChanged;

        public void SetState<T>(Action<T> action) where T : class, IViewModel
        {
            action(this as T);
            RaiseStateChanged();
        }

        protected void RaiseStateChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
