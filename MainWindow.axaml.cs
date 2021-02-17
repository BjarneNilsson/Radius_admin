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
        
       
        
        private static Client Cl;
        public MainWindow()
        {
            InitializeComponent();
            
           
            
            Cl = new Client("https://api.holmedal.net");
            Cl.UserAvalable += (o, e) => {
                if (e.Vlan.HasValue)
                {
                    Tb("TxtVlan", e.Vlan.Value.ToString());
                }
                else
                {
                    Tb("TxtVlan", "");
                }
                Tb("TxtPassword",e.Password);
            };
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
            

        }
        private void BtnFind_Click (object s, RoutedEventArgs a)
        {
            Cl.GetUserAsync(Tb(this,"TxtUsername"));
        }
        private void BtnAdd_Click(object s, RoutedEventArgs a)
        {
            
            Cl.AddUser(Tb(this,"TxtUsername"), Tb(this,"TxtPassword"),GetVlan(this));
        }        
        private void BtnUpdate_Click(object s,RoutedEventArgs a)
        {
            Cl.UpdateUser(Tb(this,"TxtUsername"), Tb(this,"TxtPassword"), GetVlan(this));

        }
        private void BtnDelete_Click(Object sender, RoutedEventArgs e)
        {
            Cl.DeleteUser(Tb(this,"TxtUsername"));
        }
        private static int? GetVlan(Window Win)
        {
            if (Win.FindControl<TextBox>("TxtVlan").Text != " ")
            {
                var a = int.TryParse(Win.FindControl<TextBox>("TxtVlan").Text, out int Vl);
                if (a) return Vl;
                else
                    return null;
            }
            else return null;

        }
        // Find xaml textbox by name
       
        private void Tb(String Box, String Text)
        {
            this.FindControl<TextBox>(Box).Text= Text;
        }
        public  string Tb(Window Win, string Box) {  return Win.FindControl<TextBox>(Box).Text;  }
    }
}
