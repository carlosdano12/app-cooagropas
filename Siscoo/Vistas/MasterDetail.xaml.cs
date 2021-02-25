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
    public partial class MasterDetail : MasterDetailPage
    {
        public MasterDetail(LoginResponseDto asociado)
        {
            InitializeComponent();
            this.Master = new Maestro(asociado);
            this.Detail = new NavigationPage(new Detalle());
            App.MasterD = this;
        }
    }
}