using Siscoo.clases;
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
    public partial class GestionarCosecha : ContentPage
    {
        public GestionarCosecha(Cultivo cultivo)
        {
            InitializeComponent();
        }
    }
}