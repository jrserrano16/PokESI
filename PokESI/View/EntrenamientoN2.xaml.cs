using PokESI;
using System;
using System.Collections.Generic;
using System.Xml;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using static System.Net.Mime.MediaTypeNames;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace PokESI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class EntrenamientoN2 : Page
    {
        public static List<Test> listaTest = new List<Test>();
        public static List<Test> preguntas = new List<Test>();
        List<Pokemon> listaPokemons = new List<Pokemon>();
        List<Pokemon> listaPokemonsAUX = new List<Pokemon>();
        Pokemon p = new Pokemon();
        GridView g = new GridView();
        int indice = 0;
        int vida = 0;
        int nAciertos = 0;

        int fallos = 0;
        public EntrenamientoN2()
        {
            this.InitializeComponent();
            Controller.cargarPokedex(g, listaPokemons, listaPokemonsAUX);
            cargarPokemon();
            cargarPreguntas();
            cogerpreguntas();


        }

        private void mostrarPregunta(int indice)
        {
            lstbox.Items.Clear();
            if (indice < 10)
            {
                Test t = preguntas[indice];
                tbxPregunta.Text = t.pregunta;

                foreach (string p in t.respuestas)
                {
                    lstbox.Items.Add(p);
                }
            }

        }

        private void cogerpreguntas()
        {
            int pg = 0;
            preguntas.Clear();
            while (pg < 10)
            {

                var random = new Random();
                var value = random.Next(1, listaTest.Count);
                Test t = listaTest[value-1];
                preguntas.Add(t);
                pg++;
            }
        }

        public static void cargarPreguntas()
        {
            listaTest.Clear();
            // Cargar contenido de prueba
            XmlDocument docSol = new XmlDocument();
            int n = 0;
            docSol.Load("Pokemons.xml");
            XmlNode root = docSol.SelectSingleNode("PokeApp");
            XmlNode pokemonsRoot = root.SelectSingleNode("Trainings");
            foreach (XmlNode node in pokemonsRoot.ChildNodes)
            {
                Test newTest = new Test();
                newTest.numero = ++n;
                newTest.pregunta = node.Attributes["Question"].Value;
                newTest.solucion = node.Attributes["Solution"].Value;


                newTest.respuestas = new List<string>();

                string[] words = node.Attributes["Answer"].Value.Split(',');

                foreach (string t in words)
                {
                    newTest.respuestas.Add(t);
                }

                listaTest.Add(newTest);





            }

        }

        private void lstbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MessageDialog messageDialog;
            string message;

            if (lstbox.SelectedIndex>-1)
            {
                string respuesta = preguntas[indice].respuestas[lstbox.SelectedIndex];
                string solucion = preguntas[indice].solucion;
                if (respuesta.Equals(solucion))
                {
                    vida+=5;


                    if (pb_pokemon.Value+5 < 55)
                    {
                        pb_pokemon.Value = pb_pokemon.Value+5;

                    }


                    tbxVida.Text = "Health "+(pb_pokemon.Value)+" / 100";
                    num_Vida.Text = vida.ToString();
                    ++nAciertos;
                    num_Aciertos.Text = nAciertos.ToString();
                    message = "Correcto!!!!";
                    messageDialog = new MessageDialog(message, p.name+" ha recuperado 5 puntos de vida!");
                    _=messageDialog.ShowAsync();



                }
                else
                {
                    ++fallos;
                    nums_Fallos.Text = fallos.ToString();
                    message = p.name+" ha fallado en la respuesta";
                    messageDialog = new MessageDialog(message, "La respuesta correcta es: "+preguntas[indice].solucion);
                    _=messageDialog.ShowAsync();
                }
                if (indice < 9)
                {
                    ++indice;
                    tbx_num_Pregunta.Text = "Pregunta "+(indice+1).ToString()+" / 10";
                    mostrarPregunta(indice);
                }
                else
                {
                    string info = p.name+" ha recuperado "+vida +" puntos de vida";
                    Controller.showNotification(p, "Fin del juego...", info);
                    p.health = pb_pokemon.Value;
                    Controller.actualizarPokemon(p);
                }

            }

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



        private void cmb_Pokemon1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            p = listaPokemonsAUX[cmb_Pokemon1.SelectedIndex];
            tbxLevel.Text = "Level "+p.level.ToString();
            tbxVida.Text = "Health "+p.health+" / 100";
            pb_pokemon.Value = p.health;
            pokemon.Children.Add(elegirPokemon(p.name));
            cmb_Pokemon1.IsEnabled = false;
            mostrarPregunta(indice);
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


    }
}
