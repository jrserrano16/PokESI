using PokESI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class Combate : Page
    {
        List<Pokemon> pokemonList = new List<Pokemon>();
        List<Pokemon> pokemonListAUX = new List<Pokemon>();
        MainPage padre;
        GridView gv = new GridView();
        public Combate()
        {
            this.InitializeComponent();
            Controller.cargarPokedex(gv, pokemonList, pokemonListAUX);
            cargarPokemons();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            padre = (MainPage)e.Parameter;
        }





        public void cargarPokemons()
        {
            pokemonListAUX.Clear();
            foreach (Pokemon p in pokemonList)
            {
                if (p.combat && p.health>0)
                    pokemonListAUX.Add(p);
            }
            cmb_Pokemon1.ItemsSource = pokemonListAUX;
            cmb_Pokemon2.ItemsSource = pokemonListAUX;
        }

        public void cargarUserControl(Pokemon p, RelativePanel pokemon)
        {
            pokemon.Children.Clear();
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
            }
        }
        private void cmb_Pokemon1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cargarUserControl(pokemonListAUX[cmb_Pokemon1.SelectedIndex], pokemon1);
        }

        private void cmb_Pokemon2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cargarUserControl(pokemonListAUX[cmb_Pokemon2.SelectedIndex], pokemon2);
        }
        public bool mensajesError()
        {
            if (cmb_Pokemon1.SelectedIndex == -1)
            {
                string message = "El primer pokemon no fue seleccionado...";
                MessageDialog messageDialog = new MessageDialog(message, "No se puede comenzar combate");
                _=messageDialog.ShowAsync();
                return false;
            }
            else if (cmb_Pokemon2.SelectedIndex == -1)
            {
                string message = "El segundo pokemon no fue seleccionado...";
                MessageDialog messageDialog = new MessageDialog(message, "No se puede comenzar combate");
                _=messageDialog.ShowAsync();
                return false;
            }
            else if (cmb_Pokemon2.SelectedIndex == cmb_Pokemon1.SelectedIndex)
            {
                string message = "El mismo pokemon no puede luchar contra él mismo...";
                MessageDialog messageDialog = new MessageDialog(message, "No se puede comenzar combate");
                _=messageDialog.ShowAsync();
                return false;
            }
            return true;
        }
        private void btn_1vs1_Click(object sender, RoutedEventArgs e)
        {
            if (mensajesError())
            {
                Pokemon p = new Pokemon();
                p.description = 1.ToString();
                List<Pokemon> luchadores = new List<Pokemon>();
                luchadores.Add(pokemonListAUX[cmb_Pokemon1.SelectedIndex]);
                luchadores.Add(pokemonListAUX[cmb_Pokemon2.SelectedIndex]);
                luchadores.Add(p);
                padre.irCombate(luchadores);
            }


        }

        private void btn_vcCPU_Click(object sender, RoutedEventArgs e)
        {
            if (mensajesError())
            {
                Pokemon p = new Pokemon();
                p.description = 2.ToString();
                List<Pokemon> luchadores = new List<Pokemon>();
                luchadores.Add(pokemonListAUX[cmb_Pokemon1.SelectedIndex]);
                luchadores.Add(pokemonListAUX[cmb_Pokemon2.SelectedIndex]);
                luchadores.Add(p);
                padre.irCombate(luchadores);
            }
        }
    }
}
