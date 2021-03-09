using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siscoo.clases;
using Siscoo.dtos;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siscoo.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarDiaControl : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        List<Insumo> insumos = new List<Insumo>();

        public GestionarDiaControl(LoginResponseDto aso)
        {
            InitializeComponent();
            asociado = aso;
        }

        private void btn_insumos(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new ModalInsumos(asociado, this));
        }

        public void llenaLista(Insumo insumo)
        {
            insumos.Add(insumo);
            insumos = insumos.OrderBy(o => o.nombre).ToList();
            insumos_lv.ItemsSource = insumos;
            textDescripcion.Text = insumos[0].nombre;
            Console.WriteLine("Llega insumo: " + insumos[0].nombre);
        }
    }
}