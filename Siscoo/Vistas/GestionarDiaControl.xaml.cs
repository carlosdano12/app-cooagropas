using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siscoo.clases;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siscoo.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarDiaControl : ContentPage
    {
        public GestionarDiaControl()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            List<Insumo> list = new List<Insumo>();
            Insumo in1 = new Insumo();
            in1.id = "1";
            in1.nombre = "Abono";
            Insumo in2 = new Insumo();
            in2.id = "2";
            in2.nombre = "Fertilizante";
            list.Add(in1);
            list.Add(in2);
            var option = await DisplayActionSheet("Seleccionar Insumo", "Cancelar", null, list.Select(insumo => insumo.nombre).ToArray());
            Console.WriteLine("eligio el insumo: " + option);
        }
    }
}