using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace InterfaceProjetBdd
{
    /// <summary>
    /// Logique d'interaction pour MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        MySqlConnection Connexion;
        string connectionstring;
        public MenuPrincipal(string connectionstring)
        {
            InitializeComponent();
            //this.Connexion = Connexion;
            this.connectionstring = connectionstring;
        }

        private void Button_Click(object sender, RoutedEventArgs e)//client
        {
            var MenuClient = new MenuClient(connectionstring);
            MenuClient.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Deconnection_Click(object sender, RoutedEventArgs e)
        {
            var Menuconnect = new MainWindow();
            Menuconnect.Show();
            
            this.Close();

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var MenuCommande = new MenuCommande(connectionstring);
            MenuCommande.Show();
            this.Close();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            var MenuStat = new MenuStatistique(connectionstring);
            MenuStat.Show();
            this.Close();
        }

        private void Stock_click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuStock(connectionstring);
            Menu.Show();
            this.Close();
        }
    }
}
