using Siscoo.clases;
using Siscoo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Siscoo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Asociado asociado = new Asociado() {
                id_asociado = 1,
                nombre = "",
                apellido = "",
                id_tipo_documento = 0,
                documento ="",
                contraseña = "",
                telefono = ""
            };
            Navigation.PushModalAsync(new MasterDetail(asociado));
        }
    }
}
