using Siscoo.clases;
using Siscoo.dtos;
using Siscoo.comunicaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siscoo.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarCosecha : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        Cosecha cosecha = new Cosecha();
        readonly CosechaManager cosechaManager = new CosechaManager();
        string cultivoId;
        public RegistrarCosecha(LoginResponseDto aso, Cosecha cose, string idCultivo)
        {
            InitializeComponent();
            asociado = aso;
            cosecha = cose;
            cultivoId = idCultivo;
            if (cosecha.id != "0")
            {
                llenarDatosCultivo();
            }
        }
        public void llenarDatosCultivo()
        {

            inicio_cosecha_DtPick.Date = cosecha.fecha_inicio_cosecha;
            fin_cosecha_DtPick.Date = cosecha.fecha_fin_cosecha;
            txtKgCosecha.Text = cosecha.kg_cosechados.ToString();
            txtKgCosechaBien.Text = cosecha.kg_cosechados_bien.ToString();
            txtCostoCosecha.Text = cosecha.costo_total_cosecha.ToString();

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            DateTime inicio = inicio_cosecha_DtPick.Date;
            DateTime fin = fin_cosecha_DtPick.Date;
            float kgCosechados = 0;
            float kgCosechaBien = 0;
            float costo = 0;
            if (txtKgCosecha.Text != null)
            {
                kgCosechados = float.Parse(txtKgCosecha.Text);
            }

            if (txtKgCosechaBien.Text != null)
            {
                kgCosechaBien = float.Parse(txtKgCosechaBien.Text);
            }

            if (txtCostoCosecha.Text != null)
            {
                costo = float.Parse(txtCostoCosecha.Text);
            }
            if (cosecha.id == "0")
            {
                var response = await cosechaManager.Add(asociado.accessToken, cultivoId, cosecha.asociadoIdAsociado, inicio, fin, kgCosechados, kgCosechaBien, costo);
                Console.WriteLine("createCosechaCODE: " + response.code);
                if (response.code == "OK" || response.code == "Created")
                {
                    await DisplayAlert("Cosecha", "Se registro la cosecha correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    Console.WriteLine("erro addCosecha(): " + response.message);
                    await DisplayAlert("Error", "Ocurrio un error al registrar la cosecha", "OK");
                }

            }
            else
            {
                var response = await cosechaManager.update(asociado.accessToken, cosecha.id, cosecha.cultivoIdCultivo, cosecha.asociadoIdAsociado, inicio, fin, kgCosechados, kgCosechaBien, costo);
                if (response.code == "OK" || response.code == "Created")
                {
                    await DisplayAlert("Cosecha", "Se actualizo la informacion de la cosecha correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    Console.WriteLine("erro updateCosecha(): " + response.message);
                    await DisplayAlert("Error", "Ocurrio un error al registrar la cosecha", "OK");
                }

            }
        }
    }
}