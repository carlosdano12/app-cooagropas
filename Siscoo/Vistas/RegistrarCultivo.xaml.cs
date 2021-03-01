using Siscoo.clases;
using Siscoo.dtos;
using Siscoo.comunicaciones;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siscoo.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarCultivo : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        Cultivo cultivo1 = new Cultivo();
        readonly NiameManager niameManager = new NiameManager();
        readonly CultivoManager cultivoManager = new CultivoManager();
        List<Niame> niames = new List<Niame>();
        public RegistrarCultivo(LoginResponseDto aso, Cultivo cultivo)
        {
            InitializeComponent();
            asociado = aso;
            cultivo1 = cultivo;
            llenaLista();
            if (cultivo.id_cultivo != "")
            {
                llenarDatosCultivo();
            }
            else {
                btnRegistrar.IsVisible = false;
                btnCosechar.IsVisible = false;
            
            }

        }

        //llena lista de ñames
        public async void llenaLista()
        {
            niames = new List<Niame>();
            var niameList = await niameManager.GetAll();

            foreach (Niame niame in niameList)
            {
                if (niame.id_niame != null)
                {
                    niames.Add(niame);
                }
            }
            niames = niames.OrderBy(o => o.nombre).ToList();
            tipo_niame_Pk.ItemsSource = niames;
            if(cultivo1.id_niame != null)
            {
                for (int i = 0; i < niames.Count; i++)
                {
                    if (niames[i].id_niame == cultivo1.id_niame)
                    {
                        tipo_niame_Pk.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {

            if (tipo_niame_Pk.SelectedIndex >= 0)
            {
                string nombre = txtNombre.Text;
                var niame = (Niame)tipo_niame_Pk.SelectedItem;
                DateTime inicio = inicio_siembra_DtPick.Date;
                DateTime fin = fin_siembra_DtPick.Date;
                float hectareas = 0;
                float kg = 0;
                float costo = 0;
                if (txtHectareasSembradas.Text != null)
                {
                    hectareas = float.Parse(txtHectareasSembradas.Text);
                }

                if (txtKgEsperaCosechar.Text != null) {
                    kg = float.Parse(txtKgEsperaCosechar.Text);
                }

                if (txtCostoSiembra.Text != null) {
                    costo = float.Parse(txtCostoSiembra.Text);
                }

                if (cultivo1.id_cultivo == "0")
                {
                    var response = await cultivoManager.Add(asociado.accessToken, nombre, niame.id_niame, inicio, fin, hectareas, kg, costo, false);
                    if (response.code == "OK")
                    {
                        await DisplayAlert("Cultivo", "Se registro la siembra correctamente", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        Console.WriteLine("erro addSiembra(): " + response.message);
                        await DisplayAlert("Error", "Ocurrio un error al registrar la siembra", "OK");
                    }
                    
                }else {
                    Console.WriteLine("CULDITO:" + cultivo1.id_cultivo);
                    var result = await cultivoManager.update(cultivo1.id_cultivo, asociado.accessToken, nombre, niame.id_niame, inicio, fin, hectareas, kg, costo, false);
                    Cultivo cultivo = (Cultivo)result;
                    if (cultivo.id_cultivo != "")
                    {
                        await DisplayAlert("Cultivo", "Se actualizo la informacion de la siembra correctamente", "OK");
                        await Navigation.PopAsync();
                    }
                }
            }
            else {
                await DisplayAlert("ATENCION!!!", "DEBE SELECCIONAR EL TIPO DE ÑAME", "OK");
            }

        }
        //esta funcion llena los datos del cultivo que se selecciono
        public void llenarDatosCultivo() {

            txtNombre.Text = cultivo1.nombre;
            inicio_siembra_DtPick.Date = cultivo1.fecha_inicio_siembra;
            fin_siembra_DtPick.Date = cultivo1.fecha_fin_siembra;
            txtHectareasSembradas.Text = cultivo1.hectareas_sembradas.ToString();
            txtKgEsperaCosechar.Text = cultivo1.kg_espera_cosechar.ToString();
            txtCostoSiembra.Text = cultivo1.costo_total_siembra.ToString();
            
        }

        private void btnCosechar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GestionarCosecha(cultivo1));
        }
    }
}