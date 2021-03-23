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
    public partial class RegistrarTransporte : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        List<TransporteDetalle> niames = new List<TransporteDetalle>();
        readonly TransporteManager transporteManager = new TransporteManager();
        Transporte transporte = new Transporte();
        public RegistrarTransporte(LoginResponseDto aso, Transporte transpor)
        {
            InitializeComponent();
            asociado = aso;
            transporte = transpor;
            if (transpor.id != "0")
            {
                llenarDatosDiaControl();
            }
        }
        public void llenarDatosDiaControl()
        {

            txtNota.Text = transporte.nota;
            niames = new List<TransporteDetalle>();
            niames = transporte.transporteDetalles.ToList();
            niames = niames.OrderBy(o => o.niame.nombre).ToList();
            niames_lv.ItemsSource = niames;

        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var viewCellSelected = sender as MenuItem;
            var niameToDelete = viewCellSelected?.BindingContext as TransporteDetalle;
            niames.Remove(niameToDelete);
            niames = niames.OrderBy(o => o.niame.nombre).ToList();
            niames_lv.ItemsSource = niames;
        }

        private void btnNiame_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new ModalNiames(asociado, this));
        }

        public void addNiame(TransporteDetalle niame)
        {
            niames.Add(niame);
            niames = niames.OrderBy(o => o.niame.nombre).ToList();
            niames_lv.ItemsSource = niames;
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {

            transporte.nota = txtNota.Text;
            transporte.transporteDetalles = niames.ToArray();
            if (transporte.id == "0")
            {
                var response = await transporteManager.Add(asociado.accessToken, transporte);
                Console.WriteLine("createTransporteCODE: " + response.code);
                if (response.code == "OK" || response.code == "Created")
                {
                    await DisplayAlert("Transporte", "Se registro el transporte correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    Console.WriteLine("erro addTransporte(): " + response.message);
                    await DisplayAlert("Error", "Ocurrio un error al registrar el transporte", "OK");
                }

            }
            else
            {
                transporte.nota = txtNota.Text;
                transporte.transporteDetalles = niames.ToArray();
                var response = await transporteManager.update(asociado.accessToken, transporte);
                Console.WriteLine("update transporte " + response.code);
                if (response.code == "OK" || response.code == "Created")
                {
                    await DisplayAlert("Transporte", "Se actualizo la informacion del transporte correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    Console.WriteLine("erro updateTransporte(): " + response.message);
                    await DisplayAlert("Error", "Ocurrio un error al actualizar el transporte", "OK");
                }

            }
        }
    }
}