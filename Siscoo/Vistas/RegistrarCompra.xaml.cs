using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siscoo.dtos;
using Siscoo.clases;
using Siscoo.comunicaciones;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siscoo.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarCompra : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        List<CompraDetalle> insumos = new List<CompraDetalle>();
        readonly CompraManager compraManager = new CompraManager();
        Compra compra = new Compra();
        public RegistrarCompra(LoginResponseDto aso, Compra comp)
        {
            InitializeComponent();
            asociado = aso;
            compra = comp;
            if (comp.id != "0")
            {
                llenarDatosDiaControl();
            }
        }

        public void llenarDatosDiaControl()
        {

            txtNota.Text = compra.nota;
            insumos = new List<CompraDetalle>();
            insumos = compra.compraDetalles.ToList();
            insumos = insumos.OrderBy(o => o.insumo.nombre).ToList();
            insumos_lv.ItemsSource = insumos;

        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var viewCellSelected = sender as MenuItem;
            var insumoToDelete = viewCellSelected?.BindingContext as CompraDetalle;
            insumos.Remove(insumoToDelete);
            insumos = insumos.OrderBy(o => o.insumo.nombre).ToList();
            insumos_lv.ItemsSource = insumos;
        }

        private void btnInsumo_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new ModalImsumoCompra(asociado, this));
        }

        public void addInsumo(CompraDetalle insumo)
        {
            insumos.Add(insumo);
            insumos = insumos.OrderBy(o => o.insumo.nombre).ToList();
            insumos_lv.ItemsSource = insumos;
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {

            compra.nota = txtNota.Text;
            compra.compraDetalles = insumos.ToArray();
            if (compra.id == "0")
            {
                var response = await compraManager.Add(asociado.accessToken, compra);
                Console.WriteLine("createCompraCODE: " + response.code);
                if (response.code == "OK" || response.code == "Created")
                {
                    await DisplayAlert("Solicitud de compra", "Se registro la solicitud de compra correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    Console.WriteLine("erro addCompra(): " + response.message);
                    await DisplayAlert("Error", "Ocurrio un error al registrar la solicitud de compra", "OK");
                }

            }
            else
            {
                compra.nota = txtNota.Text;
                compra.compraDetalles = insumos.ToArray();
                var response = await compraManager.update(asociado.accessToken, compra);
                Console.WriteLine("update compra " + response.code);
                if (response.code == "OK" || response.code == "Created")
                {
                    await DisplayAlert("Solicitud de compra", "Se actualizo la informacion de la solicitud correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    Console.WriteLine("erro updateCompra(): " + response.message);
                    await DisplayAlert("Error", "Ocurrio un error al actualizar la solicitud", "OK");
                }

            }

        }
    }
}