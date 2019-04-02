using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Base
{
    public interface IPanelOwner
    {
        void InstantiatePanel(ViewModelBase panel);
        void DestroyPanel();
        void DestroyPanelObject();
        void NotifyPanelClosed();
    }
}
