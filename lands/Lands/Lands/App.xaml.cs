namespace Lands
{
    using Views;  //CON RECONOCE LAS OTRAS PAGINAS
    using Xamarin.Forms;

    public partial class App : Application
    {
        #region Contructor  
        public App()
        {
            InitializeComponent();

            this.MainPage = new NavigationPage(new LoginPage()); //PUSIMOS COMO LA PRINCIPAL LOGIN PAGE
        }
        #endregion

        #region Metodos
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
