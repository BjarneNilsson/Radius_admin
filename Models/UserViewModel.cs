using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Radius.Models
{
#nullable enable
    class UserViewModel : INotifyPropertyChanged
    {
        private string _UserName ="";
        private string _password ="";
        private string _Vlan="";
        private int? _VL;
        private string _Message="";
        private bool _IsValid = true;
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsValid
        {
            get { return _IsValid; }
            set
            {
               
                _IsValid = value;
                OnPropertyChanged();
            }
        }
        public int? Vl { get { return _VL; } }
        public string UserName
        {
            get { return _UserName; }
            set
            {
                
                if (value != _UserName)
                {
                    _UserName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            set
            {
                _password = value;
                OnPropertyChanged();
            }
            get { return _password; }
        }
        public string Vlan
        {
            get { return _Vlan; }
            set
            {
                this.IsValid = false;
                this.Message = "";
                if (value != string.Empty)
                {
                    var a = int.TryParse(value, out int q);
                    if (a)
                    {
                        this._Vlan = value;
                        this.IsValid = true;
                        this._VL = q;

                    }
                    else { this.Message = "Invalid Vlan" + value +"."; }

                }

                else 
                {
                    
                    _Vlan = "";
                    _VL = null;
                    this.IsValid = true; 
                }
            
            
               
                
                OnPropertyChanged();
            }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value;
                OnPropertyChanged();
            }
        }
#nullable enable
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
    }
}
