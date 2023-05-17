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
    /// Logique d'interaction pour MenuStock.xaml
    /// </summary>
    public partial class MenuStock : Window
    {
        string connectionstring;
        string magasin;
        string commandefleur;
        string commandeaccessoire;
        string id_magasin;
        public MenuStock(string connectionstring)
        {
            InitializeComponent();
            this.connectionstring = connectionstring;
            this.magasin = "Fleuriste du coin";
            this.id_magasin = RetourID_mag(connectionstring, this.magasin);
            this.commandefleur = "select id_fleur,quantite from stockfleur where stockfleur.id_magasin='" + this.id_magasin+"';";
            this.commandeaccessoire= "select id_accessoire,quantite from stockaccessoire where stockaccessoire.id_magasin='" + this.id_magasin + "';";
            FillGrid1(connectionstring, commandefleur);
            FillGrid2(connectionstring, commandeaccessoire);
            FillGrid3(connectionstring);
        }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuPrincipal(connectionstring);
            Menu.Show();
            this.Close();
        }//retourne au menu principal

        private string RetourID_mag(string connectionstring,string nom_mag)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select id_magasin from magasin where nom_magasin = '" + this.magasin + "';";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string id_mag = "";
            while (reader.Read())                           // parcours ligne par ligne
            {
                id_mag = reader.GetValue(0).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
            }
            reader.Close();
            return id_mag;
        }//prend le nom du magasin et renvoie son ID

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }//rien ne se passe

        public void FillGrid1(string connectionstring, string commande)
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
        }//Remplit la datagrid du stock

        public void FillGrid2(string connectionstring, string commande)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            connection.Open();
            MySqlCommand cmdSel = new MySqlCommand(commande, connection);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
            try
            {
                da.Fill(dt);
                dataGrid2.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {

            }
            connection.Close();
        }//Remplit la datagrid du stock

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedelement = ((ComboBoxItem)Magasin.SelectedItem).Content as string;
            if (selectedelement != null)
            {
                this.magasin = selectedelement;
                this.id_magasin = RetourID_mag(this.connectionstring, this.magasin);
                this.commandefleur = "select id_fleur,quantite from stockfleur where stockfleur.id_magasin='" + this.id_magasin + "';";
                this.commandeaccessoire = "select id_accessoire,quantite from stockaccessoire where stockaccessoire.id_magasin='" + this.id_magasin + "';";
                FillGrid1(this.connectionstring, this.commandefleur);
                FillGrid2(this.connectionstring, this.commandeaccessoire);
            }
        }//selection du magasin

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Modif_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new ModifQt(connectionstring, id_magasin);
            Menu.Show();
            this.Close();
        }

        private void Ajout_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new AjoutObjet(connectionstring);
            Menu.Show();
            this.Close();
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new SupprimerObjet (connectionstring);
            Menu.Show();
            this.Close();

        }

        private void dataGrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void FillGrid3(string connectionstring)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            connection.Open();
            string commande = "Select nom_magasin,id_fleur,quantite from stockfleur,magasin where quantite < 5 and magasin.id_magasin=stockfleur.id_magasin" +" UNION "+ "Select nom_magasin,id_accessoire,quantite from stockaccessoire,magasin where quantite < 5 and magasin.id_magasin=stockaccessoire.id_magasin order by nom_magasin;";
            MySqlCommand cmdSel = new MySqlCommand(commande, connection);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
            try
            {
                da.Fill(dt);

                dataGrid3.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {

            }
            connection.Close();
        }
    }
}
