using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siscoo.clases;
using Siscoo.dtos;
using Siscoo.comunicaciones;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siscoo.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarTransporte : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        readonly TransporteManager transporteManager = new TransporteManager();
        List<Transporte> transportes = new List<Transporte>();
        public GestionarTransporte(LoginResponseDto aso)
        {
            InitializeComponent();
            asociado = aso;

            llenaLista();

            transporte_lv.RefreshCommand = new Command(() =>
            {
                llenaLista();
                transporte_lv.IsRefreshing = false;
            });
        }

        private void transporte_lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var transporte = (Transporte)e.SelectedItem;
            Navigation.PushAsync(new RegistrarTransporte(asociado, transporte));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Transporte transporte = new Transporte();
            transporte.id = "0";
            Navigation.PushAsync(new RegistrarTransporte(asociado, transporte));
        }

        public async void llenaLista()
        {
            transportes = new List<Transporte>();
            var response = await transporteManager.GetAll(asociado.accessToken);
            if (response.code == "OK")
            {
                var transpoteList = JsonConvert.DeserializeObject<List<Transporte>>(response.message);

                foreach (Transporte transporte in transpoteList)
                {
                    if (transporte.id != "")
                    {
                        transportes.Add(transporte);
                    }
                }
                transportes = transportes.OrderBy(o => o.fechaSolicitud).ToList();
                transporte_lv.ItemsSource = transportes;
            }
            else
            {
                if (response.code == "Unauthorized")
                {
                    Navigation.PushAsync(new MainPage());
                }
                else
                {
                    Console.WriteLine("error llenaListaCosecha():" + response.message);
                    await DisplayAlert("Error", "Ocurrio algun problema al listar los sembrados", "OK");
                }
            }
        }
    }
}