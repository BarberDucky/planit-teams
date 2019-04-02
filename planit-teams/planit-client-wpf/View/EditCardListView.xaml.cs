using planit_client_wpf.Base;
using planit_client_wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace planit_client_wpf.View
{
    /// <summary>
    /// Interaction logic for EditCardListView.xaml
    /// </summary>
    public partial class EditCardListView : UserControl
    {
        public EditCardListView()
        {
            InitializeComponent();
        }

        void MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null && DataContext is EditCardListViewModel)
            {
                var vm = DataContext as EditCardListViewModel;
                vm.MessageBoxRequest += MessageBoxRequest;
            }
        }
    }
}
