﻿using Siscoo.clases;
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
    public partial class RegistrarCosecha : ContentPage
    {
        public RegistrarCosecha(Cultivo cultivo)
        {
            InitializeComponent();
        }
    }
}