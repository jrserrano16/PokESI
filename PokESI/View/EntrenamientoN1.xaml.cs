using PokESI;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace PokESI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class EntrenamientoN1 : Page
    {

        List<Pokemon> listaPokemons = new List<Pokemon>();
        List<Pokemon> listaPokemonsAUX = new List<Pokemon>();
        GridView g = new GridView();
        Pokemon p = new Pokemon();
        Pokemon pacierto = new Pokemon();
        int experiencia = 0;
        int nAciertos = 0;
        int niveles = 0;
        int fallos = 0;
        public EntrenamientoN1()
        {
            this.InitializeComponent();
            Controller.cargarPokedex(g, listaPokemons, listaPokemonsAUX);
            cargarPokemon();


        }

        private void pokemonRandom()
        {

            var random = new Random();
            var value = random.Next(1, listaPokemons.Count);
            pacierto = listaPokemons[value];
            img_Pokemon.Source = new BitmapImage(new Uri(pacierto.image));

        }

        public void cargarPokemon()
        {
            listaPokemonsAUX.Clear();
            cmb_Pokemon1.ItemsSource = listaPokemonsAUX;
            foreach (Pokemon p in listaPokemons)
            {
                if (p.captured)
                {
                    listaPokemonsAUX.Add(p);
                }
            }

            cmb_Pokemon1.ItemsSource = listaPokemonsAUX;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }

        public void cargarPokemon(Pokemon p)
        {
            tbxLevel.Text = p.name + " - lvl. " + p.level.ToString();
            tbxExperience.Text = p.experience + " / 100";
            pb_pokemon.Value = p.experience;

        }

        private void cmb_Pokemon1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pokemonRandom();
            calcularc.IsEnabled = true;
            tbxRespuesta.IsEnabled = true;
            p = listaPokemonsAUX[cmb_Pokemon1.SelectedIndex];
            tbxLevel.Text = "Level "+p.level.ToString();
            tbxExperience.Text = "Exp. "+p.experience+" / 100";
            pb_pokemon.Value = p.experience;
            pokemon.Children.Add(elegirPokemon(p.name));
            cmb_Pokemon1.IsEnabled = false;
        }

        private UserControl elegirPokemon(string name)
        {
            UserControl ucPokemon = new UserControl();
            switch (name)
            {
                case "Squirtle":
                    ucPokemon = new ucSquirtle();
                    break;
                case "Chimchar":
                    ucPokemon = new ucChimchar();
                    break;
                case "Psyduck":
                    ucPokemon = new ucPsyduck();
                    break;
                case "Gengar":
                    ucPokemon = new ucGengar();
                    break;
                case "Jigglypuff":
                    ucPokemon = new ucJigglipuff();
                    break;
                case "Zapdos":
                    ucPokemon = new ucZapdos();
                    break;
                case "Snorlax":
                    ucPokemon = new ucSnorlax();
                    break;
            }
            return ucPokemon;
        }

        private void calcularc_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog messageDialog;
            if (pacierto.name.ToLower().Equals(tbxRespuesta.Text.ToLower()))
            {
                experiencia+=15;

                if (pb_pokemon.Value+15 >= 100)
                {
                    pb_pokemon.Value = pb_pokemon.Value+15-100;
                    ++niveles;
                    tbxExperience.Text = "Exp. "+pb_pokemon.Value+" / 100";
                }
                else
                {
                    pb_pokemon.Value+=15;
                    tbxExperience.Text = "Exp. "+(pb_pokemon.Value)+" / 100";
                }

                num_Niveles.Text = niveles.ToString();
                tbxLevel.Text = "Level "+(p.level+niveles);
                num_Experiencia.Text = experiencia.ToString();
                ++nAciertos;
                tbxRespuesta.Text = string.Empty;
                num_Aciertos.Text = nAciertos.ToString();
                string message = "Correcto!!!!";
                messageDialog = new MessageDialog(message, p.name+" ha ganado 15 puntos de experiencia!");
                _=messageDialog.ShowAsync();
                pokemonRandom();

            }
            else
            {
                tbxRespuesta.Text = string.Empty;
                ++fallos;
                nums_Fallos.Text = fallos.ToString();
                string message = p.name+" ha fallado en la respuesta";
                messageDialog = new MessageDialog(message, "La respuesta correcta es: "+pacierto.name);
                _=messageDialog.ShowAsync();


                if (fallos == 1)
                {
                    vida3.Opacity = 0.2;
                    pokemonRandom();

                }
                else if (fallos == 2)
                {
                    vida2.Opacity = 0.2;
                    pokemonRandom();
                }
                if (fallos == 3)
                {
                    vida1.Opacity = 0.2;


                    calcularc.IsEnabled = false;
                    Controller.actualizarPokemon(experiencia, p);
                    string info = p.name+" ha conseguido "+experiencia +" puntos de experiencia";
                    showNotification(p, "Fin del juego...", info);
                }


            }
        }
        public static void showNotification(Pokemon p, string t1, string t2)
        {

            new ToastContentBuilder()
                 .AddArgument("action", "Favoritos")
                 .AddArgument("conversationId", 9813)
                 .AddCustomTimeStamp(new DateTime(2017, 04, 15, 19, 45, 00, DateTimeKind.Utc))
                 .AddText(t1)
                 .AddText(t2)
                 .AddInlineImage(new Uri(p.image))
                 .AddAppLogoOverride(new Uri("ms-appx:///Assets/descubir.png"), ToastGenericAppLogoCrop.Default)

                 .Show();
        }

        private void tbxRespuesta_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxRespuesta.Text = string.Empty;
        }
    }
}
