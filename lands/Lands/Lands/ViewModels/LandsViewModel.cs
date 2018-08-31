namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {

        #region Servicios
        private ApiService apiService;
        #endregion

        #region Atributos
        private ObservableCollection<Land> lands;
        private bool isRefreshing;
        private string filter;
        private List<Land> landsList;
        #endregion

        #region Propiedades
        public ObservableCollection<Land> Lands
        {
            get { return this.lands; }                //ESTAS DOS SON PARA REFRESCAR EL CONTROL, PERO PRIMERO SE NOMBRAN EN ATRIBUTOS
            set { SetValue(ref this.lands, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }                //ESTAS DOS SON PARA REFRESCAR EL CONTROL, PERO PRIMERO SE NOMBRAN EN ATRIBUTOS
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }                //ESTAS DOS SON PARA REFRESCAR EL CONTROL, PERO PRIMERO SE NOMBRAN EN ATRIBUTOS
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }

        #endregion

        #region Constructores
        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region Metodos
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            this.landsList = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(this.landsList);
            this.IsRefreshing = false;
        }
        #endregion

        #region Comandos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.filter))
            {
                this.Lands = new ObservableCollection<Land>(
                    this.landsList);
            }
            else
            {
                this.Lands = new ObservableCollection<Land>(
                    this.landsList.Where(
                        l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                             l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion
    }
}
