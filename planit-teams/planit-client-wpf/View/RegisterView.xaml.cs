﻿using planit_client_wpf.Base;
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
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();            
        }

        void RegisterView_MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window wind = Window.GetWindow(this);
            wind.WindowState = WindowState.Normal;
            try
            {
                var vm = (RegisterViewModel)DataContext;
                vm.MessageBoxRequest += RegisterView_MessageBoxRequest;
            }
            catch (Exception)
            {
                MessageBox.Show("Error on message box request");
            }
        }
    }
}
