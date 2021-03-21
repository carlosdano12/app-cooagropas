using Siscoo.dtos;
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
    public partial class Maestro : ContentPage
    {
        LoginResponseDto asociado = new LoginResponseDto();
        public Maestro(LoginResponseDto aso)
        {
            InitializeComponent();
            asociado = aso;
        }

        private void btn_gestion_cultivo_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            App.MasterD.Detail.Navigation.PushAsync(new GestionarCultivos(asociado));
        }

        private void btn_gestion_transporte_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            App.MasterD.Detail.Navigation.PushAsync(new GestionarTransportes());
        }

        private void btn_solicitud_compra_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            App.MasterD.Detail.Navigation.PushAsync(new GestionarCompras(asociado));
        }
    }
}