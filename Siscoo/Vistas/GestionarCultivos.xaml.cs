using Siscoo.comunicaciones;
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
    public partial class GestionarCultivos : ContentPage
    {
        Asociado asociado = new Asociado();
        readonly CultivoManager cultivoManager = new CultivoManager();
        List<Cultivo> cultivos = new List<Cultivo>();
        public GestionarCultivos(Asociado aso)
        {
            InitializeComponent();
            asociado = aso;

            llenaLista();

            cultivos_lv.RefreshCommand = new Command(() => {
                llenaLista();
                cultivos_lv.IsRefreshing = false;
            });
        }

        public async void llenaLista()
        {
            cultivos = new List<Cultivo>();
            var culList = await cultivoManager.GetAll(1);

            foreach (Cultivo cultivo in culList)
            {
                if (cultivo.id_cultivo != 0)
                {
                    cultivos.Add(cultivo);
                }
            }
            cultivos = cultivos.OrderBy(o => o.nombre).ToList();
            cultivos_lv.ItemsSource = cultivos;
        }

        private void cultivos_lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cultivo = (Cultivo)e.SelectedItem;
            Navigation.PushAsync(new RegistrarCultivo(asociado, cultivo));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Cultivo cultivo = new Cultivo();
            Navigation.PushAsync(new RegistrarCultivo(asociado, cultivo));
        }
    }
}