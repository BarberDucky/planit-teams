using planit_client_wpf.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { SetProperty(ref currentViewModel, value); }
        }

        public MainWindowViewModel()
        {
            CurrentViewModel = new LoginViewModel();
            MessengerBus.MessengerBusInstance.OpenHomeWindowDelegate += InstantiateHome;
            MessengerBus.MessengerBusInstance.OpenLoginWindowDelegate += InstantiateLogin;
            MessengerBus.MessengerBusInstance.OpenRegisterWindowDelegate += InstantiateRegister;
        }

        public void InstantiateHome()
        {
            CurrentViewModel = new HomeViewModel();
        }

        public void InstantiateLogin()
        {
            CurrentViewModel = new LoginViewModel();
        }

        public void InstantiateRegister()
        {
            CurrentViewModel = new RegisterViewModel();
        }
    }
}
