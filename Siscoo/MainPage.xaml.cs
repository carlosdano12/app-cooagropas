using Siscoo.clases;
using Siscoo.Vistas;
using Siscoo.comunicaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Siscoo.dtos;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Siscoo
{
    public partial class MainPage : ContentPage
    {
        readonly LoginManager loginManager = new LoginManager();
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Login(object sender, EventArgs e)
        {
            string documento = txtDocumento.Text;
            string contrsena = txtContrasena.Text;
            var response = await loginManager.login(documento, contrsena);
            if(response.code == "OK")
            {
                var login = JsonConvert.DeserializeObject<LoginResponseDto>(response.message);
                Navigation.PushModalAsync(new MasterDetail(login));
            }else
            {
                await DisplayAlert("Login fallo", "Documento y/o contraseña no validos", "OK");
            }
            
        }

    }
}
