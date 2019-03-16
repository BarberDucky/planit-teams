using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace planit_client_wpf.Base
{
    public class ViewModelBase : BindableBase
    {
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;      

        protected virtual void ShowMessageBox(Action<MessageBoxResult> resultAction, string messageBoxText, string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.None, MessageBoxOptions options = MessageBoxOptions.None)
        {
            MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(resultAction, messageBoxText, caption, button, icon, defaultResult, options));
        }
    }
}
