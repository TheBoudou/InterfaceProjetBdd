using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InterfaceProjetBdd
{
    /// <summary>
    /// Logique d'interaction pour MenuStatistique.xaml
    /// </summary>
    public partial class MenuStatistique : Window
    {
        string connectionstring;
        public MenuStatistique(string connectionstring)
        {
            InitializeComponent();
            //this.Connexion = Connexion;
            this.connectionstring = connectionstring;
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuPrincipal(connectionstring);
            Menu.Show();
            this.Close();
        }


     
        



        private void StatClient_Click_1(object sender, RoutedEventArgs e)
        {
            var StatClient = new StatClient(connectionstring);
            StatClient.Show();
            this.Close();
        }

        private void StatProduits_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StatCommandes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StatMagasins_Click(object sender, RoutedEventArgs e)
        {
            var StatMagasin = new StatMagasin(connectionstring);
            StatMagasin.Show();
            this.Close();
        }

        private void StatBouquets_Click(object sender, RoutedEventArgs e)
        {
            var StatBouquet = new StatBouquet(connectionstring);
            StatBouquet.Show();
            this.Close();
        }

      





        // Calcul du prix moyen du bouquet acheté 

    }
}
