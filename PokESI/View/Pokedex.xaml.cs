using PokESI;
using System;
using System.Collections.Generic;
using System.Xml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace PokESI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Pokedex : Page
    {
        List<Pokemon> pokemonList = new List<Pokemon>();
        List<Pokemon> pokemonListAUX = new List<Pokemon>();
        MainPage padre;
        public Pokedex()
        {
            this.InitializeComponent();

            Controller.cargarPokedex(ventana, pokemonList, pokemonListAUX);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            padre = (MainPage)e.Parameter;
        }

        private void Panel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            padre.irPokedex(pokemonListAUX[ventana.SelectedIndex]);
        }

        private void tbx_Filtro_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            filterSearch();
        }
        private void filterSearch()
        {
            PlantillaPokedex poke;
            pokemonListAUX = new List<Pokemon>();
            ventana.Items.Clear();
            switch (cmb_Filter.SelectedIndex)
            {
                case 0:
                    foreach (Pokemon p in pokemonList)
                    {
                        pokemonListAUX.Add(p);
                        poke = new PlantillaPokedex(p);
                        ventana.Items.Add(poke);
                    }
                    break;
                case 1:
                    foreach (Pokemon p in pokemonList)
                    {
                        if (p.name.ToLower().Contains(tbx_Filtro.Text.ToLower()))
                        {
                            pokemonListAUX.Add(p);
                            poke = new PlantillaPokedex(p);
                            ventana.Items.Add(poke);
                        }

                    }
                    break;
                case 2:
                    foreach (Pokemon p in pokemonList)
                    {
                        if (p.types.Count == 2)
                        {
                            if (p.types[0].ToLower().Contains(tbx_Filtro.Text.ToLower()) ||
                                p.types[1].ToLower().Contains(tbx_Filtro.Text.ToLower()))
                            {
                                pokemonListAUX.Add(p);
                                poke = new PlantillaPokedex(p);
                                ventana.Items.Add(poke);

                            }
                        }
                        else
                        {
                            if (p.types[0].ToLower().Contains(tbx_Filtro.Text.ToLower()))
                            {
                                pokemonListAUX.Add(p);
                                poke = new PlantillaPokedex(p);
                                ventana.Items.Add(poke);

                            }
                        }
                    }
                    break;
                case 3:
                    foreach (Pokemon p in pokemonList)
                    {
                        if (p.generation.ToString().Contains(tbx_Filtro.Text))
                        {
                            pokemonListAUX.Add(p);
                            poke = new PlantillaPokedex(p);
                            ventana.Items.Add(poke);
                        }
                    }
                    break;
                case 4:
                    foreach (Pokemon p in pokemonList)
                    {
                        if (p.captured)
                        {
                            pokemonListAUX.Add(p);
                            poke = new PlantillaPokedex(p);
                            ventana.Items.Add(poke);
                        }
                    }
                    break;
                case 5:
                    foreach (Pokemon p in pokemonList)
                    {
                        if (p.starter)
                        {
                            pokemonListAUX.Add(p);
                            poke = new PlantillaPokedex(p);
                            ventana.Items.Add(poke);
                        }
                    }
                    break;
                case 6:
                    foreach (Pokemon p in pokemonList)
                    {
                        if (p.legendary)
                        {
                            pokemonListAUX.Add(p);
                            poke = new PlantillaPokedex(p);
                            ventana.Items.Add(poke);
                        }
                    }
                    break;
                case 7:
                    foreach (Pokemon p in pokemonList)
                    {
                        if (p.mythical)
                        {
                            pokemonListAUX.Add(p);
                            poke = new PlantillaPokedex(p);
                            ventana.Items.Add(poke);
                        }
                    }
                    break;
                case 8:
                    foreach (Pokemon p in pokemonList)
                    {
                        if (p.mega)
                        {
                            pokemonListAUX.Add(p);
                            poke = new PlantillaPokedex(p);
                            ventana.Items.Add(poke);
                        }
                    }
                    break;
                case 9:
                    foreach (Pokemon p in pokemonList)
                    {
                        if (p.ultraBeast)
                        {
                            pokemonListAUX.Add(p);
                            poke = new PlantillaPokedex(p);
                            ventana.Items.Add(poke);
                        }
                    }
                    break;
            }
        }
        private void cmb_Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbx_Filtro.IsEnabled = true;
            tbx_Filtro.Text = String.Empty;
            if (cmb_Filter.SelectedIndex==0 || cmb_Filter.SelectedIndex==4
                || cmb_Filter.SelectedIndex==5 || cmb_Filter.SelectedIndex==6
                || cmb_Filter.SelectedIndex==7 || cmb_Filter.SelectedIndex==8 || cmb_Filter.SelectedIndex==9)
            {
                filterSearch();
                tbx_Filtro.IsEnabled = false;
                tbx_Filtro.Text = "Select Filter";
            }






        }

        private void tbx_Filtro_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            tbx_Filtro.Text = String.Empty;
            tbx_Filtro.FontStyle = Windows.UI.Text.FontStyle.Normal;


        }


    }
}

