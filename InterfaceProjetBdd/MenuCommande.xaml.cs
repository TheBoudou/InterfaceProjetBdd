using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace InterfaceProjetBdd
{
    /// <summary>
    /// Logique d'interaction pour MenuCommande.xaml
    /// </summary>
    public partial class MenuCommande : Window
    {
        MySqlConnection Connexion;
        string connectionstring;
        string éléments;
        string requete;
        string commandesql;
        string part1;
        public MenuCommande(string connectionstring)
        {
            InitializeComponent();
            string commande = "Select * from commande;";
            this.connectionstring = connectionstring;
            FillGrid(connectionstring, commande);
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuPrincipal(connectionstring);
            Menu.Show();
            this.Close();
        }

        public void FillGrid(string connectionstring, string commande)
        {




            MySqlConnection connection = new MySqlConnection(connectionstring);
            connection.Open();
            MySqlCommand cmdSel = new MySqlCommand(commande, connection);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
            try
            {
                da.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {

            }
            connection.Close();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Element1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedelement = ((ComboBoxItem)Element1.SelectedItem).Content as string;
            this.part1 = selectedelement;
            if (selectedelement != "*" && selectedelement != " *" && selectedelement != " * ")
            {
                this.éléments = "num_commande," + selectedelement + " ";
            }
            else
            {
                this.éléments = selectedelement + " ";
            }
            this.requete = this.commandesql + this.éléments + " from commande ";
        }

        private void Element2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedelement = ((ComboBoxItem)Element2.SelectedItem).Content as string;
            if (selectedelement != part1 && selectedelement != "")
            {
                if (part1 != "*" && part1 != " *" && part1 != " * ")
                {
                    this.éléments = "num_commande," + part1 + ", " + selectedelement + " ";
                    this.requete = this.commandesql + this.éléments + " from commande ";
                }


            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            this.commandesql = "Select ";
            this.requete = this.commandesql + this.éléments + " from commande ";
            FillGrid(connectionstring, requete);
        }
    }
}
