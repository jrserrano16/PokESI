using PokESI;
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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace PokESI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PlantillaMisPokemon : Page
    {
        public static Pokemon p;
        public PlantillaMisPokemon()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            p = (Pokemon)e.Parameter;
            tbx_Level.Text = "Level "+p.level.ToString();
            pb_Experiencia.Value = p.experience;
            tbx_EXP.Text = "Exp. "+p.experience.ToString()+" / 100";
            pb_Health.Value = p.health;
            tbx_Health.Text = "Health "+p.health.ToString()+" / 100";

            tbx_Nombre.Text = p.name;
            switch (p.name)
            {
                case "Squirtle":
                    ucSquirtle squirtle = new ucSquirtle();
                    pokemon.Children.Add(squirtle);
                    break;
                case "Chimchar":
                    ucChimchar chimchar = new ucChimchar();
                    pokemon.Children.Add(chimchar);
                    break;
                case "Psyduck":
                    ucPsyduck psyduck = new ucPsyduck();
                    pokemon.Children.Add(psyduck);
                    break;
                case "Gengar":
                    ucGengar gengar = new ucGengar();
                    pokemon.Children.Add(gengar);
                    break;
                case "Jigglypuff":
                    ucJigglipuff jiggli = new ucJigglipuff();
                    pokemon.Children.Add(jiggli);
                    break;
                case "Zapdos":
                    ucZapdos zapdos = new ucZapdos();
                    pokemon.Children.Add(zapdos);
                    break;
                case "Snorlax":
                    ucSnorlax snorly = new ucSnorlax();
                    pokemon.Children.Add(snorly);
                    break;

            }



        }
    }
}
