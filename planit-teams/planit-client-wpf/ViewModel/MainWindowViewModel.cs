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
            CurrentViewModel = new LoginViewModel(InstantiateHome, InstantiateRegister);
        }

        public void InstantiateHome()
        {
            CurrentViewModel = new HomeViewModel(InstantiateLogin);
        }

        public void InstantiateLogin()
        {
            CurrentViewModel = new LoginViewModel(InstantiateHome, InstantiateRegister);
        }

        public void InstantiateRegister()
        {
            CurrentViewModel = new RegisterViewModel(InstantiateLogin);
        }
    }
}
