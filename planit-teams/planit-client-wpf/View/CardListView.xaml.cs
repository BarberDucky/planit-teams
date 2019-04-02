using planit_client_wpf.Base;
using planit_client_wpf.ViewModel;
using planit_client_wpf.Model;
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
    /// Interaction logic for CardListView.xaml
    /// </summary>
    public partial class CardListView : UserControl
    {
        public CardListView()
        {
            InitializeComponent();
        }

        void MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null && DataContext is CardListViewModel)
            {
                var vm = DataContext as CardListViewModel;
                vm.MessageBoxRequest += MessageBoxRequest;
            }
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            MessageBox.Show(e.Data.GetData(DataFormats.Text).ToString());
        }

        private void listbox_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData(DataFormats.StringFormat, ((ReadCard)(((ListBox)sender).SelectedItem)).CardId);

                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }
    }
}
