using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_client_wpf.Model;

namespace planit_client_wpf.Base
{
    public interface IPanelContainer
    {
        void InstantiatePanel(ViewModelBase sidePanel, IPanelOwner owner);
    }
}
