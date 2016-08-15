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
    public abstract class TabViewModelBase : ViewModelBase, IViewModelWithTabs
    {
        public IList<IViewModel> Tabs { get; protected set; }

        public event EventHandler TabsChanged;

        protected void RaiseTabsChanged()
        {
            TabsChanged?.Invoke(this, EventArgs.Empty);
        }

        // Raised by PropertyChanged.Fody automatically.
        private void OnTabsChanged()
        {
            // Re-raise for consumers.
            RaiseTabsChanged();
        }
    }
}
