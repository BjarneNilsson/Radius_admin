using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using net.holmedal;
using Avalonia.Interactivity;
using System;
using Radius.Models;

namespace AvaloniaApplication3
{

    public class MainWindow : Window
    {
#nullable enable
        private static Client Cl;
        private readonly UserViewModel  UVM;

        public MainWindow()
        {

            InitializeComponent();
            DataContext = new Radius.Models.UserViewModel();
            UVM = DataContext as UserViewModel;
            Cl = new Client("https://api.holmedal.net");
            Cl.UserAvalable += UserAvalable;
            
            
#if DEBUG
            this.AttachDevTools();
#endif
        }

       

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        #region Event hanlers for buttons
        private void BtnFind_Click(object s, RoutedEventArgs a)
        {
            Cl.GetUserAsync(UVM.UserName);
        }
        private void BtnAdd_Click(object s, RoutedEventArgs a)
        {

            Cl.AddUser(UVM.UserName, UVM.Password, UVM.Vl);
        }
        private void BtnUpdate_Click(object s, RoutedEventArgs a)
        {
            Cl.UpdateUser(UVM.UserName, UVM.Password, UVM.Vl);


        }
        private void BtnDelete_Click(Object sender, RoutedEventArgs e)
        {
            Cl.DeleteUser(UVM.UserName);
        }
    
        #endregion
        private void UserAvalable( object? o, UserAvalableArgs e) 
        {
            if (e.Vlan.HasValue)
            {
                UVM.Vlan = e.Vlan.Value.ToString();
            }
            else
            {
                UVM.Vlan = "";
            }
            UVM.Password = e.Password;
            
        }
    }

}
