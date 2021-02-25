using Siscoo.clases;
using Siscoo.Vistas;
using Siscoo.comunicaciones;
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
        readonly LoginManager loginManager = new LoginManager();
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Login(object sender, EventArgs e)
        {
            string documento = txtDocumento.Text;
            string contrsena = txtContrasena.Text;
            var login = await loginManager.login(documento, contrsena);
            Console.WriteLine("entra Acces:"+login.accessToken+ "nombre: "+ login.nombre);
            Navigation.PushModalAsync(new MasterDetail(login));
        }

    }
}
