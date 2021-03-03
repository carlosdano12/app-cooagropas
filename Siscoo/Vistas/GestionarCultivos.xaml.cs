using Siscoo.comunicaciones;
using Siscoo.clases;
using Siscoo.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siscoo.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarCultivos : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        readonly CultivoManager cultivoManager = new CultivoManager();
        List<Cultivo> cultivos = new List<Cultivo>();
        public GestionarCultivos(LoginResponseDto aso)
        {
            InitializeComponent();
            asociado = aso;

            llenaLista();

            cultivos_lv.RefreshCommand = new Command(() =>
            {
                llenaLista();
                cultivos_lv.IsRefreshing = false;
            });
        }

        public async void llenaLista()
        {
            cultivos = new List<Cultivo>();
            var response = await cultivoManager.GetAll(asociado.accessToken);
            if (response.code == "OK")
            {
                var culList = JsonConvert.DeserializeObject<List<Cultivo>>(response.message);

                foreach (Cultivo cultivo in culList)
                {
                    if (cultivo.id_cultivo != "")
                    {
                        cultivos.Add(cultivo);
                    }
                }
                cultivos = cultivos.OrderBy(o => o.nombre).ToList();
                cultivos_lv.ItemsSource = cultivos;
            }
            else
            {
                if (response.code == "Unauthorized")
                {
                    Navigation.PushAsync(new MainPage());
                }
                else
                {
                    Console.WriteLine("error llenaListaSiembra():" + response.message);
                    await DisplayAlert("Error", "Ocurrio algun problema al listar los sembrados", "OK");
                }
            }
        }

        private void cultivos_lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cultivo = (Cultivo)e.SelectedItem;
            Console.WriteLine("culSend: " + cultivo.nombre + " culSendNia: " + cultivo.niameIdNiame);
            Navigation.PushAsync(new RegistrarCultivo(asociado, cultivo));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Cultivo cultivo = new Cultivo();
            cultivo.id_cultivo = "0";
            Navigation.PushAsync(new RegistrarCultivo(asociado, cultivo));
        }

    }
}