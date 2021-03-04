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
    public partial class GestionarCosecha : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        Cultivo cultivo = new Cultivo();
        readonly CosechaManager cosechaManager = new CosechaManager();
        List<Cosecha> cosechas = new List<Cosecha>();
        public GestionarCosecha(LoginResponseDto aso, Cultivo culti)
        {
            InitializeComponent();
            asociado = aso;
            cultivo = culti;

            llenaLista();

            cosecha_lv.RefreshCommand = new Command(() =>
            {
                llenaLista();
                cosecha_lv.IsRefreshing = false;
            });
        }

        private void cosechas_lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cosecha = (Cosecha)e.SelectedItem;
            Navigation.PushAsync(new RegistrarCosecha(asociado, cosecha, cultivo.id_cultivo));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Cosecha cosecha = new Cosecha();
            cosecha.id = "0";
            Navigation.PushAsync(new RegistrarCosecha(asociado, cosecha, cultivo.id_cultivo));
        }

        public async void llenaLista()
        {
            cosechas = new List<Cosecha>();
            var response = await cosechaManager.GetAll(asociado.accessToken, cultivo.id_cultivo);
            if (response.code == "OK")
            {
                var coseList = JsonConvert.DeserializeObject<List<Cosecha>>(response.message);

                foreach (Cosecha cosecha in coseList)
                {
                    if (cosecha.id != "")
                    {
                        cosechas.Add(cosecha);
                    }
                }
                cosechas = cosechas.OrderBy(o => o.fecha_inicio_cosecha).ToList();
                cosecha_lv.ItemsSource = cosechas;
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