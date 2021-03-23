using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siscoo.dtos;
using Siscoo.clases;
using Siscoo.comunicaciones;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siscoo.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalNiames : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        readonly NiameManager niameManager = new NiameManager();
        List<Niame> niames = new List<Niame>();
        RegistrarTransporte _root;
        public ModalNiames(LoginResponseDto aso, RegistrarTransporte root)
        {
            InitializeComponent();
            asociado = aso;
            _root = root;
            llenaLista();

            niames_lv.RefreshCommand = new Command(() =>
            {
                llenaLista();
                niames_lv.IsRefreshing = false;
            });
        }

        private async void niames_lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var niame = (Niame)e.SelectedItem;
            TransporteDetalle transporteDetalle = new TransporteDetalle();
            Console.WriteLine("niame: " + niame.nombre);
            string cantidad = await DisplayPromptAsync("Cantidad", "¿Cantidad de niame a enviar en kg?");
            transporteDetalle.niameIdNiame = niame.id_niame;
            transporteDetalle.cantidad = float.Parse(cantidad);
            transporteDetalle.niame = niame;
            _root.addNiame(transporteDetalle);
            this.Navigation.PopModalAsync();
        }

        public async void llenaLista()
        {
            niames = new List<Niame>();
            var response = await niameManager.GetAll(asociado.accessToken);
            if (response.code == "OK")
            {
                var niameList = JsonConvert.DeserializeObject<List<Niame>>(response.message);

                foreach (Niame niame in niameList)
                {
                    if (niame.id_niame != "")
                    {
                        niames.Add(niame);
                    }
                }
                niames = niames.OrderBy(o => o.nombre).ToList();
                niames_lv.ItemsSource = niames;
            }
            else
            {
                if (response.code == "Unauthorized")
                {
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    Console.WriteLine("error llenaListaNiames():" + response.message);
                    await DisplayAlert("Error", "Ocurrio algun problema al listar los niames", "OK");
                }
            }
        }
    }
}