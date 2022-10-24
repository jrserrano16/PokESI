using PokESI;
using System.Collections.Generic;
using System;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace PokESI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MapaCaptura : Page
    {

        public static ImageSource agudo = new BitmapImage(new Uri("https://callejerode.com/images/mapas/mapa-agudo.jpg"));
        public static ImageSource calzada = new BitmapImage(new Uri("https://callejerode.com/images/mapas/mapa-calzada-de-calatrava.jpg"));
        public static ImageSource cr = new BitmapImage(new Uri("https://callejerode.com/images/mapas/mapa-ciudad-real.jpg"));
        public static ImageSource lasolana = new BitmapImage(new Uri("https://callejerode.com/images/mapas/mapa-la-solana.jpg"));
        public static ImageSource membrilla = new BitmapImage(new Uri("https://callejerode.com/images/mapas/mapa-membrilla.jpg"));
        public static ImageSource pokeball = new BitmapImage(new Uri("https://static.wikia.nocookie.net/espokemon/images/0/02/Pok%C3%A9_Ball_%28Ilustraci%C3%B3n%29.png/revision/latest/top-crop/width/360/height/450?cb=20090125150654"));
        public static ImageSource superball = new BitmapImage(new Uri("https://static.wikia.nocookie.net/espokemon/images/5/57/Super_Ball_%28Ilustraci%C3%B3n%29.png/revision/latest?cb=20160831125614"));
        public static ImageSource ultraball = new BitmapImage(new Uri("https://static.wikia.nocookie.net/espokemon/images/c/c9/Ultra_Ball_%28Ilustraci%C3%B3n%29.png/revision/latest?cb=20090125150713"));
        public static ImageSource masterball = new BitmapImage(new Uri("https://static.wikia.nocookie.net/espokemon/images/a/a9/Master_Ball_%28Ilustraci%C3%B3n%29.png/revision/latest?cb=20120802225204"));







        List<Pokemon> pokemonList = new List<Pokemon>();
        public static List<Pokemon> pokemonListAUX = new List<Pokemon>();
        GridView g = new GridView();
        Pokemon p1 = new Pokemon();
        Pokemon p2 = new Pokemon();
        Pokemon p3 = new Pokemon();
        Pokemon p4 = new Pokemon();
        Pokemon p5 = new Pokemon();
        Pokemon p6 = new Pokemon();
        Pokemon p7 = new Pokemon();
        Pokemon se = new Pokemon();
        public int intento = 0;
        public int range = 10;
        public int prob = 5;
        public int opcion;
        public MapaCaptura()
        {
            this.InitializeComponent();
            Controller.cargarPokedex(g, pokemonList, pokemonListAUX);
            cargarPokemon();
        }

        public void cargarPokemon()
        {
            pokemonListAUX.Clear();
            foreach (Pokemon p in pokemonList)
            {
                if (p.captured)
                {
                    pokemonListAUX.Add(p);
                }
            }
        }
        public void cargarUserControl(Pokemon p, RelativePanel pokemon)
        {
            se=p;
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
                case "Gengar":
                    ucGengar g = new ucGengar();
                    pokemon.Children.Add(g);
                    break;
                case "Jigglypuff":
                    ucJigglipuff jp = new ucJigglipuff();
                    pokemon.Children.Add(jp);
                    break;
                case "Zapdos":
                    ucZapdos z = new ucZapdos();
                    pokemon.Children.Add(z);
                    break;
                case "Snorlax":
                    ucSnorlax s = new ucSnorlax();
                    pokemon.Children.Add(s);
                    break;

            }
        }
        private void cmb_Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int opcion = cmb_Filter.SelectedIndex;
            if (cmb_Filter.SelectedIndex > -1)
            {
                img_Captura.Visibility = Windows.UI.Xaml.Visibility.Visible;
                img_Pokeball.Visibility = Windows.UI.Xaml.Visibility.Visible;
                findPokemon();
                if (opcion == 0)
                {
                    img_mapa.Source = agudo;

                }
                else if (opcion == 1)
                {
                    img_mapa.Source = calzada;
                }
                else if (opcion == 2)
                {
                    img_mapa.Source = cr;
                }
                else if (opcion == 3)
                {
                    img_mapa.Source = lasolana;
                }
                else if (opcion == 4)
                {
                    img_mapa.Source = membrilla;
                }
            }
        }

        private void img_p6_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            cargarUserControl(p6, pokemon);
        }
        public Pokemon searchPokemon(Image i)
        {
            var random = new Random();
            var value = random.Next(0, pokemonListAUX.Count);
            Pokemon pokerandom = pokemonListAUX[value];
            i.Source = new BitmapImage(new Uri(pokerandom.image));
            return pokerandom;
        }


        public void findPokemon()
        {
            p1 = searchPokemon(img_p1);
            p2 = searchPokemon(img_p2);
            p3 = searchPokemon(img_p3);
            p4 = searchPokemon(img_p4);
            p5 = searchPokemon(img_p5);
            p6 = searchPokemon(img_p6);
            p7 = searchPokemon(img_p7);
        }

        private void img_p1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            cargarUserControl(p1, pokemon);

        }

        private void img_p2_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            cargarUserControl(p2, pokemon);

        }

        private void img_p3_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            cargarUserControl(p3, pokemon);

        }

        private void img_p4_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            cargarUserControl(p4, pokemon);
        }

        private void img_p5_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            cargarUserControl(p5, pokemon);
        }

        private void img_p7_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            cargarUserControl(p7, pokemon);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indice = cm_Pokeball.SelectedIndex;
            if (indice > -1)
            {
                opcion = indice;
                if (indice == 0)
                {

                    img_Pokeball.Source = pokeball;

                }
                else if (indice == 1)
                {
                    img_Pokeball.Source = superball;
                }
                else if (indice == 2)
                {
                    img_Pokeball.Source = ultraball;
                }
                else if (indice == 3)
                {
                    img_Pokeball.Source = masterball;
                }
            }
        }
        private void capturar(int prob)
        {
            if (prob < range)
            {
                tbx_narrador.Text = "El pokemon ha escapado de la pokeball!";
            }
            else
            {
                tbx_narrador.Text = "El pokemon ha sido atrapado!";
                pokemon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Controller.pokemonCapturado(se);
                img_Pokeball.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }


        }
        private void img_Pokeball_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (intento < 5)
            {
                switch (opcion)
                {
                    case 0:
                        ++prob;
                        break;
                    case 1:
                        prob = prob+2;
                        break;
                    case 2:
                        prob = prob+3;
                        break;
                    case 3:
                        prob = prob+5;
                        break;
                }
                capturar(prob);
            }
            else
            {
                pokemon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                tbx_narrador.Text = "El pokemon ha huido!";
            }

        }
    }
}
