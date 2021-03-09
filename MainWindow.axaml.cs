using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using net.holmedal;
using Avalonia.Interactivity;
using System;

namespace AvaloniaApplication3
{
    public class MainWindow : Window
    {
        public TextBox Username ;
        public TextBox Password;
        public TextBox Vlan;
        private static Client Cl;

        public MainWindow()
        {
            InitializeComponent();
            Username = this.FindControl<TextBox>("TxtUsername");
            Password = this.FindControl<TextBox>("TxtPassword");
            Vlan = this.FindControl<TextBox>("TxtVlan");
            Cl = new Client("https://api.holmedal.net");
            Cl.UserAvalable += (o, e) =>
            {
                if (e.Vlan.HasValue)
                {
                    Vlan.Text = e.Vlan.Value.ToString();
                }
                else
                {
                    Vlan.Text = "";
                }
                Password.Text = e.Password;
            };
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void BtnFind_Click(object s, RoutedEventArgs a)
        {
            Cl.GetUserAsync(Username.Text);
        }
        private void BtnAdd_Click(object s, RoutedEventArgs a)
        {

            Cl.AddUser(Username.Text, Password.Text, GetVlan(Vlan));
        }
        private void BtnUpdate_Click(object s, RoutedEventArgs a)
        {
            Cl.UpdateUser(Username.Text, Password.Text, GetVlan(Vlan));


        }
        private void BtnDelete_Click(Object sender, RoutedEventArgs e)
        {
            Cl.DeleteUser(Username.Text);
        }
        private static int? GetVlan(TextBox Vla)

        {
            if (Vla.Text != " ")
            {
                var a = int.TryParse(Vla.Text, out int Vl);
                if (a) return Vl;
                else
                    return null;
            }
            else return null;

        }
     }
}
