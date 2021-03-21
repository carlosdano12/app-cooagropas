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
    public partial class GestionarCompras : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        readonly CompraManager compraManager = new CompraManager();
        List<Compra> compras = new List<Compra>();
        public GestionarCompras(LoginResponseDto aso)
        {
            InitializeComponent();
            asociado = aso;
            llenaLista();

            compra_lv.RefreshCommand = new Command(() =>
            {
                llenaLista();
                compra_lv.IsRefreshing = false;
            });
        }

        public async void llenaLista()
        {
            compras = new List<Compra>();
            var response = await compraManager.GetAll(asociado.accessToken);
            if (response.code == "OK")
            {
                var compraList = JsonConvert.DeserializeObject<List<Compra>>(response.message);

                foreach (Compra compra in compraList)
                {
                    if (compra.id != "")
                    {
                        compras.Add(compra);
                    }
                }
                compras = compras.OrderBy(o => o.fechaSolicitud).ToList();
                compra_lv.ItemsSource = compras;
            }
            else
            {
                if (response.code == "Unauthorized")
                {
                    Navigation.PushAsync(new MainPage());
                }
                else
                {
                    Console.WriteLine("error llenaListaCompras():" + response.message);
                    await DisplayAlert("Error", "Ocurrio algun problema al listar las compras", "OK");
                }
            }
        }

        private void compra_lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var compra = (Compra)e.SelectedItem;
            Navigation.PushAsync(new RegistrarCompra(asociado, compra));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Compra compra = new Compra();
            compra.id = "0";
            Navigation.PushAsync(new RegistrarCompra(asociado, compra));
        }
    }
}