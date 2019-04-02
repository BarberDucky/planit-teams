using planit_client_wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Base
{
    public abstract class PanelOwnerViewModel : ViewModelBase, IPanelOwner
    {
        protected bool HasPanelOpen { get; set; }
        protected ViewModelBase OpenPanel { get; set; }
        protected IPanelContainer Container { get; set; }

        protected PanelOwnerViewModel(IPanelContainer container)
        {
            Container = container;
            HasPanelOpen = false;
        }

        public void InstantiatePanel(ViewModelBase panel)
        {
            Container.InstantiatePanel(panel, this);
            HasPanelOpen = true;
            OpenPanel = panel;
        }

        public void DestroyPanelObject()
        {
            if (OpenPanel != null && OpenPanel is IDisposable)
            {
                IDisposable dis = OpenPanel as IDisposable;
                dis.Dispose();
            }
            OpenPanel = null;
        }

        public virtual void DestroyPanel()
        {
            if(HasPanelOpen == true)
            {
                HasPanelOpen = false;
                Container.InstantiatePanel(new EmptyViewModel(), null);
                DestroyPanelObject();
            }
        }

        public void NotifyPanelClosed()
        {
            HasPanelOpen = false;
            DestroyPanelObject();
            OpenPanel = null;
        }
    }
}
