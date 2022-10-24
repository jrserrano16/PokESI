using PokESI;
using Windows.System;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace PokESI
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += volver;
            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 320));
            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().VisibleBoundsChanged
           += MainPage_VisibleBoundsChanged;
        }

        private void MainPage_VisibleBoundsChanged(Windows.UI.ViewManagement.ApplicationView
       sender, object args)
        {
            var Width = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().VisibleBounds.Width;
            if (Width >= 900)
            {
                sView.DisplayMode = SplitViewDisplayMode.CompactInline;
                sView.IsPaneOpen = true;
            }
            else if (Width >= 360)
            {
                sView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                sView.IsPaneOpen = false;
            }
            else
            {
                sView.DisplayMode = SplitViewDisplayMode.Overlay;
                sView.IsPaneOpen = false;
            }
        }
        private void volver(object sender, BackRequestedEventArgs e)
        {
            if (fmmain.BackStack.Any())
            {
                fmmain.GoBack();

            }
            else
            {
                fmmain.Content = null;
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }



        private void irMisPokemon(object sender, RoutedEventArgs e)
        {
            fmmain.Navigate(typeof(PlantillaMisPokemon), this);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void Pokedex_Click(object sender, RoutedEventArgs e)
        {
            fmmain.Navigate(typeof(Pokedex), this);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void btn_Inicio_Click(object sender, RoutedEventArgs e)
        {
            fmmain.Content = null;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }


        public void irPokedex(Pokemon p)
        {

            fmmain.Navigate(typeof(InfoPokemon), p);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

        }

        private void btn_Combat_Click(object sender, RoutedEventArgs e)
        {
            fmmain.Navigate(typeof(Combate), this);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        public void irCombate(List<Pokemon> ps)
        {
            fmmain.Navigate(typeof(CombateFinal), ps);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void btn_Training_Click(object sender, RoutedEventArgs e)
        {
            fmmain.Navigate(typeof(EntrenamientoN1), this);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void btn_Training2_Click(object sender, RoutedEventArgs e)
        {
            fmmain.Navigate(typeof(EntrenamientoN2), this);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void btn_Capture_Click(object sender, RoutedEventArgs e)
        {
            fmmain.Navigate(typeof(MapaCaptura), this);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sView.IsPaneOpen = !sView.IsPaneOpen;
        }
    }

}