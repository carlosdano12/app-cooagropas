using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siscoo.clases;
using Siscoo.dtos;
using Siscoo.comunicaciones;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siscoo.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarDiaControl : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        List<DiaControlInsumo> insumos = new List<DiaControlInsumo>();
        readonly DiaControlManager diaControlManager = new DiaControlManager();
        DiaControl diaControl = new DiaControl();
        string cultivoId;

        public GestionarDiaControl(LoginResponseDto aso, string idCultivo, DiaControl dia)
        {
            InitializeComponent();
            asociado = aso;
            diaControl = dia;
            cultivoId = idCultivo;
            if (diaControl.id != "0")
            {
                llenarDatosDiaControl();
            }
        }

        private void btn_insumos(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new ModalInsumos(asociado, this));
        }

        public void addInsumo(DiaControlInsumo insumo)
        {
            insumos.Add(insumo);
            insumos = insumos.OrderBy(o => o.insumo.nombre).ToList();
            insumos_lv.ItemsSource = insumos;
            Console.WriteLine("Llega insumo: " + insumos[0].insumo.nombre);
        }

        public void llenarDatosDiaControl()
        {

            fecha_control_DtPick.Date = diaControl.fechaControl;
            textDescripcion.Text = diaControl.descripcion;
            insumos = new List<DiaControlInsumo>();
            insumos = diaControl.diasControlInsumos.ToList();
            insumos = insumos.OrderBy(o => o.insumo.nombre).ToList();
            insumos_lv.ItemsSource = insumos;

        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            diaControl.fechaControl = fecha_control_DtPick.Date;
            diaControl.descripcion = textDescripcion.Text;
            diaControl.cultivoIdCultivo = cultivoId;
            diaControl.diasControlInsumos = insumos.ToArray();
            if (diaControl.id == "0")
            {
                var response = await diaControlManager.Add(asociado.accessToken, diaControl);
                Console.WriteLine("createDiaControlCODE: " + response.code);
                if (response.code == "OK" || response.code == "Created")
                {
                    await DisplayAlert("Cultivo", "Se registro el día de control correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    Console.WriteLine("erro addDiaControl(): " + response.message);
                    await DisplayAlert("Error", "Ocurrio un error al registrar el día de control", "OK");
                }

            }
            else
            {
                diaControl.fechaControl = fecha_control_DtPick.Date;
                diaControl.descripcion = textDescripcion.Text;
                diaControl.cultivoIdCultivo = cultivoId;
                diaControl.diasControlInsumos = insumos.ToArray();
                var response = await diaControlManager.update(asociado.accessToken, diaControl);
                Console.WriteLine("update día de control " + response.code);
                if (response.code == "OK" || response.code == "Created")
                {
                    await DisplayAlert("Día de control", "Se actualizo la informacion del día de control correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    Console.WriteLine("erro updateDiaControl(): " + response.message);
                    await DisplayAlert("Error", "Ocurrio un error al actualizar el día de control", "OK");
                }

            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var viewCellSelected = sender as MenuItem;
            var insumoToDelete = viewCellSelected?.BindingContext as DiaControlInsumo;
            insumos.Remove(insumoToDelete);
            insumos = insumos.OrderBy(o => o.insumo.nombre).ToList();
            insumos_lv.ItemsSource = insumos;
        }
    }
}