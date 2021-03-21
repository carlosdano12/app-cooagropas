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
    public partial class ModalImsumoCompra : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        readonly InsumosManager insumoManager = new InsumosManager();
        List<Insumo> insumos = new List<Insumo>();
        RegistrarCompra _root;
        public ModalImsumoCompra(LoginResponseDto aso, RegistrarCompra root)
        {
            InitializeComponent();
            asociado = aso;
            _root = root;
            llenaLista();

            insumos_lv.RefreshCommand = new Command(() =>
            {
                llenaLista();
                insumos_lv.IsRefreshing = false;
            });
        }

        private async void insumos_lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var insumo = (Insumo)e.SelectedItem;
            CompraDetalle compraDetalle = new CompraDetalle();
            Console.WriteLine("inusmo: " + insumo.nombre);
            string cantidad = await DisplayPromptAsync("Cantidad", "¿Cantidad de insumo en kg?");
            compraDetalle.insumoId = insumo.id;
            compraDetalle.cantidad = float.Parse(cantidad);
            compraDetalle.insumo = insumo;
            _root.addInsumo(compraDetalle);
            this.Navigation.PopModalAsync();
        }

        public async void llenaLista()
        {
            insumos = new List<Insumo>();
            var response = await insumoManager.GetAll(asociado.accessToken);
            if (response.code == "OK")
            {
                var isuList = JsonConvert.DeserializeObject<List<Insumo>>(response.message);

                foreach (Insumo insumo in isuList)
                {
                    if (insumo.id != "")
                    {
                        insumos.Add(insumo);
                    }
                }
                insumos = insumos.OrderBy(o => o.nombre).ToList();
                insumos_lv.ItemsSource = insumos;
            }
            else
            {
                if (response.code == "Unauthorized")
                {
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    Console.WriteLine("error llenaListaInsumos():" + response.message);
                    await DisplayAlert("Error", "Ocurrio algun problema al listar los insumos", "OK");
                }
            }
        }
    }
}