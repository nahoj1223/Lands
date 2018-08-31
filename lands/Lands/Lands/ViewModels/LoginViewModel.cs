namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Atributos
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Propiedades
        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get { return this.password; }                //ESTAS DOS SON PARA REFRESCAR EL CONTROL, PERO PRIMERO SE NOMBRAN EN ATRIBUTOS
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunning  //todo lo que empiece por Is es Bool
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login() // LA PALABRA ASYNC CONVIERTE EL METODO EN ASYNCRONO
        {
            if (String.IsNullOrEmpty(this.Email))  //VALIDA SI EL CAMPO ESTA VACIO, NO USAMOS IF (this.Email == "")
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", //TITULO DEL MENSAJE
                    "Por favor ingrese un Email",  // MENSAJE
                    "Accept");  //TIPO DE BOTON
                return;
            }

            if (String.IsNullOrEmpty(this.Password))  //VALIDA SI EL CAMPO ESTA VACIO, NO USAMOS IF (this.Password == "")
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", //TITULO DEL MENSAJE
                    "Por favor ingrese la contraseña",  // MENSAJE
                    "Accept");  //TIPO DE BOTON
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            if(this.Email != "jhoanestiven2@gmail.com" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error", //TITULO DEL MENSAJE
                    "El Email o la Contraseña son incorrectos",  // MENSAJE
                    "Accept");  //TIPO DE BOTON
                this.Password = string.Empty;
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                "Ok", //TITULO DEL MENSAJE
                "Sesion Iniciada",  // MENSAJE
                "Accept");  //TIPO DE BOTON
            return;

        }
        #endregion

        #region Construcores
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
        }
        #endregion
    }
}
