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
    public partial class ListaDiasControl : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        Cultivo cultivo = new Cultivo();
        readonly DiaControlManager diaControlManager = new DiaControlManager();
        List<DiaControl> diasControl = new List<DiaControl>();
        public ListaDiasControl(LoginResponseDto aso, Cultivo culti)
        {
            InitializeComponent();
            asociado = aso;
            cultivo = culti;

            llenaLista();

            diaControl_lv.RefreshCommand = new Command(() =>
            {
                llenaLista();
                diaControl_lv.IsRefreshing = false;
            });
        }

        private void diaControl_lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var diaControl = (DiaControl)e.SelectedItem;
            Navigation.PushAsync(new GestionarDiaControl(asociado, cultivo.id_cultivo, diaControl));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DiaControl diaControl = new DiaControl();
            diaControl.id = "0";
            Navigation.PushAsync(new GestionarDiaControl(asociado, cultivo.id_cultivo, diaControl));
        }

        public async void llenaLista()
        {
            diasControl = new List<DiaControl>();
            var response = await diaControlManager.GetAll(asociado.accessToken, cultivo.id_cultivo);
            if (response.code == "OK")
            {
                var diasList = JsonConvert.DeserializeObject<List<DiaControl>>(response.message);

                foreach (DiaControl diaControl in diasList)
                {
                    if (diaControl.id != "")
                    {
                        diasControl.Add(diaControl);
                    }
                }
                diasControl = diasControl.OrderBy(o => o.fechaControl).ToList();
                diaControl_lv.ItemsSource = diasControl;
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