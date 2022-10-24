using System;
using System.Collections.Generic;
using System.Timers;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace PokESI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CombateFinal : Page
    {
        DispatcherTimer dtAtaque;
        List<Pokemon> luchadores;
        public static Pokemon p1;
        public static Pokemon p2;
        public static UserControl ucpokemon1;
        public static UserControl ucpokemon2;
        public static int mode;

        public CombateFinal()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            luchadores = (List<Pokemon>)e.Parameter;
            p1 = luchadores[0];
            p2 = luchadores[1];
            mode = Convert.ToInt32(luchadores[2].description);
            cargarUserControl(p1, 1, pokemon1, btn1_pk1, btn1_pk2, btn1_pk3);
            tbxLevel1.Text = p1.name +" - lvl. "+p1.level.ToString();
            pb_pokemon1.Value = p1.health;
            tbxHealth1.Text = pb_pokemon1.Value +" / 100";


            cargarUserControl(p2, 2, pokemon2, btn2_pk1, btn2_pk2, btn2_pk3);
            tbxLevel2.Text = p2.name +" - lvl. "+p2.level.ToString();
            pb_pokemon2.Value = p2.health;
            tbxHealth2.Text = pb_pokemon2.Value +" / 100";
            if (mode == 2)
            {
                btn2_pk1.IsEnabled = false;
                btn2_pk2.IsEnabled = false;
                btn2_pk3.IsEnabled = false;
                btn2_pk4.IsEnabled = false;
            }


        }
        void ataqueMaquina(Pokemon p, ProgressBar pb2, ProgressBar pb1, UserControl uc, TextBlock tb, RelativePanel rp)
        {
            var random = new Random();
            var value = random.Next(1, 4);
            seleccionAtaque(value, p, pb2, pb1, uc, tb, rp, null);
            dtAtaque.Stop();


        }



        public void cargarUserControl(Pokemon p, int type, RelativePanel pokemon, Button b1, Button b2, Button b3)
        {
            pokemon.Name = p.name;

            pokemon.Children.Clear();
            if (type==1)
            {
                switch (p.name)
                {
                    case "Squirtle":

                        ucpokemon1 = new ucSquirtle();
                        b1.Content = p.abilities[0];
                        b2.Content = p.abilities[1];
                        b3.IsEnabled = false;
                        b3.Visibility = Visibility.Collapsed;


                        break;
                    case "Chimchar":
                        ucpokemon1 = new ucChimchar();
                        b1.Content = p.abilities[0];
                        b2.Content = p.abilities[1];
                        b3.Content = p.abilities[2];

                        break;
                    case "Psyduck":
                        ucpokemon1 = new ucPsyduck();
                        b1.Content = p.abilities[0];
                        b2.Content = p.abilities[1];
                        b3.IsEnabled = false;
                        b3.Visibility = Visibility.Collapsed;
                        break;
                }
                pokemon.Children.Add(ucpokemon1);
            }
            else
            {
                switch (p.name)
                {
                    case "Squirtle":

                        ucpokemon2 = new ucSquirtle();
                        b1.Content = p.abilities[0];
                        b2.Content = p.abilities[1];
                        b3.IsEnabled = false;
                        b3.Visibility = Visibility.Collapsed;


                        break;
                    case "Chimchar":
                        ucpokemon2 = new ucChimchar();
                        b1.Content = p.abilities[0];
                        b2.Content = p.abilities[1];
                        b3.Content = p.abilities[2];

                        break;
                    case "Psyduck":
                        ucpokemon2 = new ucPsyduck();
                        b1.Content = p.abilities[0];
                        b2.Content = p.abilities[1];
                        b3.IsEnabled = false;
                        b3.Visibility = Visibility.Collapsed;
                        break;
                }
                pokemon.Children.Add(ucpokemon2);
            }


        }
        public void ataque(Pokemon p, ProgressBar pb2, ProgressBar pb1, TextBlock tb, RelativePanel rp, string at, string type, double power)
        {
            Storyboard animacion = new Storyboard();
            Ataque ataque;
            if (p.name.Equals(rp.Name))
            {
                animacion = (Storyboard)ucpokemon1.Resources[at];
                animacion.Begin();
                ataque = new Ataque(at, type, power);
                Controller.atacar(ataque, p1, p2, tb, pb2, pb1, tbx_Narrador, animacion, ucpokemon2, grid2);
            }
            else
            {
                animacion = (Storyboard)ucpokemon2.Resources[at];
                animacion.Begin();
                ataque = new Ataque(at, type, power);
                Controller.atacar(ataque, p2, p1, tb, pb2, pb1, tbx_Narrador, animacion, ucpokemon1, grid2);
            }
        }

        private void btn1_pk1_Click(object sender, RoutedEventArgs e)
        {
            seleccionAtaque(1, p1, pb_pokemon2, pb_pokemon1, ucpokemon2, tbxHealth2, pokemon1, btn1_pk4);
            if (mode==2)
            {

                esperaAtaque();

            }
            disableButtons(btn1_pk1, btn1_pk2, btn1_pk3, btn1_pk4, btn2_pk1, btn2_pk2, btn2_pk3, btn2_pk3);

        }

        private void btn1_pk2_Click(object sender, RoutedEventArgs e)
        {
            seleccionAtaque(2, p1, pb_pokemon2, pb_pokemon1, ucpokemon2, tbxHealth2, pokemon1, btn1_pk4);
            if (mode==2)
            {

                esperaAtaque();

            }
            disableButtons(btn1_pk1, btn1_pk2, btn1_pk3, btn1_pk4, btn2_pk1, btn2_pk2, btn2_pk3, btn2_pk3);
        }

        private void btn1_pk3_Click(object sender, RoutedEventArgs e)
        {
            seleccionAtaque(3, p1, pb_pokemon2, pb_pokemon1, ucpokemon2, tbxHealth2, pokemon1, btn1_pk4);
            if (mode==2)
            {

                esperaAtaque();

            }
            disableButtons(btn1_pk1, btn1_pk2, btn1_pk3, btn1_pk4, btn2_pk1, btn2_pk2, btn2_pk3, btn2_pk3);
        }

        private void btn1_pk4_Click(object sender, RoutedEventArgs e)
        {
            seleccionAtaque(4, p1, pb_pokemon1, pb_pokemon2, ucpokemon1, tbxHealth1, pokemon1, btn1_pk4);
            if (mode==2)
            {
                esperaAtaque();
            }
            disableButtons(btn1_pk1, btn1_pk2, btn1_pk3, btn1_pk4, btn2_pk1, btn2_pk2, btn2_pk3, btn2_pk3);

        }

        private void esperaAtaque()
        {
            dtAtaque = new DispatcherTimer();
            dtAtaque.Interval = TimeSpan.FromMilliseconds(7500);
            dtAtaque.Tick += espera;
            dtAtaque.Start();
        }

        private void espera(object sender, object e)
        {
            ataqueMaquina(p2, pb_pokemon1, pb_pokemon1, ucpokemon1, tbxHealth1, pokemon1);
        }

        public void curar(UserControl uc, TextBlock tb, ProgressBar pb, Pokemon p)
        {
            Storyboard st = (Storyboard)uc.Resources["Curar"];
            st.Begin();
            tb.Text = pb.Value+20 +" / 100";
            pb.Value += 20;
            tbx_Narrador.Text = p.name.ToUpper() +" has been healed and has recovered 20 life points";

        }

        private void btn2_pk1_Click(object sender, RoutedEventArgs e)
        {
            seleccionAtaque(1, p2, pb_pokemon1, pb_pokemon2, ucpokemon1, tbxHealth1, pokemon1, btn2_pk4);

            disableButtons(btn2_pk1, btn2_pk2, btn2_pk3, btn2_pk4, btn1_pk1, btn1_pk2, btn1_pk3, btn1_pk4);

        }

        private void btn2_pk2_Click(object sender, RoutedEventArgs e)
        {
            seleccionAtaque(2, p2, pb_pokemon1, pb_pokemon2, ucpokemon1, tbxHealth1, pokemon1, btn2_pk4);
            disableButtons(btn2_pk1, btn2_pk2, btn2_pk3, btn2_pk4, btn1_pk1, btn1_pk2, btn1_pk3, btn1_pk4);
        }

        private void btn2_pk3_Click(object sender, RoutedEventArgs e)
        {
            seleccionAtaque(3, p2, pb_pokemon1, pb_pokemon2, ucpokemon1, tbxHealth1, pokemon1, btn2_pk4);
            disableButtons(btn2_pk1, btn2_pk2, btn2_pk3, btn2_pk4, btn1_pk1, btn1_pk2, btn1_pk3, btn1_pk4);
        }

        private void btn2_pk4_Click(object sender, RoutedEventArgs e)
        {
            seleccionAtaque(4, p2, pb_pokemon2, pb_pokemon1, ucpokemon2, tbxHealth2, pokemon2, btn2_pk4);
            disableButtons(btn2_pk1, btn2_pk2, btn2_pk3, btn2_pk4, btn1_pk1, btn1_pk2, btn1_pk3, btn1_pk4);
        }

        public void disableButtons(Button b1, Button b2, Button b3, Button b4, Button b5, Button b6, Button b7, Button b8)
        {
            if (mode == 1)
            {
                b1.IsEnabled = false;
                b2.IsEnabled = false;

                b3.IsEnabled = false;
                b4.IsEnabled = false;

                b5.IsEnabled = true;
                b6.IsEnabled = true;
                if (b7.Visibility == Visibility.Visible)
                {
                    b7.IsEnabled = true;
                }
                if (b8.Visibility == Visibility.Visible)
                {
                    b8.IsEnabled = true;
                }

            }
        }
        public void seleccionAtaque(int a, Pokemon p, ProgressBar pb, ProgressBar pb2, UserControl uc, TextBlock h, RelativePanel rp, Button b)
        {

            switch (p.name)
            {
                case "Squirtle":
                    if (a==1)
                    {
                        ataque(p, pb, pb2, h, rp, "Waterpistol", "Water", 5);
                    }
                    else if (a==2)
                    {
                        ataque(p, pb, pb2, h, rp, "Bubbles", "Water", 8);
                    }
                    else if (a==3)
                    {
                        tbx_Narrador.Text = p.name+" failed the attack!";
                    }
                    else if (a==4)
                    {
                        curar(uc, h, pb, p);
                        b.Visibility = Visibility.Collapsed;
                    }

                    break;
                case "Chimchar":
                    if (a==1)
                    {
                        ataque(p, pb, pb2, h, rp, "FirePunch", "Fire", 5);
                    }
                    else if (a==2)
                    {
                        ataque(p, pb, pb2, h, rp, "FlameThrower", "Fire", 8);
                    }
                    else if (a==3)
                    {
                        ataque(p, pb, pb2, h, rp, "BlazeKick", "Water", 6);
                    }
                    else if (a==4)
                    {
                        curar(uc, h, pb, p);
                        b.Visibility = Visibility.Collapsed;
                    }

                    break;
                case "Psyduck":
                    if (a==1)
                    {
                        ataque(p, pb, pb2, h, rp, "Waterpistol", "Water", 5);
                    }
                    else if (a==2)
                    {
                        ataque(p, pb, pb2, h, rp, "TailMovement", "Psychic", 7);
                    }
                    else if (a==3)
                    {
                        tbx_Narrador.Text = p.name+" failed the attack!";
                    }
                    else if (a==4)
                    {
                        curar(uc, h, pb, p);
                        b.Visibility = Visibility.Collapsed;
                    }
                    break;
            }
        }

    }
}

