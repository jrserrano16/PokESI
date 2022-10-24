using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace PokESI
{
    public sealed partial class ucChimchar : UserControl
    {
        public ucChimchar()
        {
            this.InitializeComponent();
        }
        void patadaIgnea_Click(object sender, object e)
        {
            Storyboard sbPatadaIgnea = (Storyboard)this.Resources["PatadaIgnea"];
            sbPatadaIgnea.Begin();
        }

        void lanzallamas_Click(object sender, object e)
        {
            Storyboard sbLanzallamas = (Storyboard)this.Resources["Lanzallamas"];
            sbLanzallamas.Begin();
        }

        void puñoFuego_Click(object sender, object e)
        {
            Storyboard sbPuñoFuego = (Storyboard)this.Resources["PuñoFuego"];
            sbPuñoFuego.Begin();
        }




        void activar_superEnergia(object sender, object e)
        {
            Storyboard sbSuperEnergia = (Storyboard)this.Resources["SuperEnergia"];
            sbSuperEnergia.Begin();
        }



    }
}