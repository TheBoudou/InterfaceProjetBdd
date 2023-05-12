using Google.Protobuf.WellKnownTypes;
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
        string etat;
        public MenuCommande(string connectionstring)
        {
            InitializeComponent();
            string commande = "Select * from commande;";
            this.éléments = "*";
            this.connectionstring = connectionstring;
            FillGrid(connectionstring, commande);
            ObjectChange();
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Client.SelectedItem != null)
            {
                string selectedelement = Client.SelectedItem.ToString();
                string c = selectedelement;
                this.requete="Select "+this.éléments+" from commande where id_client = '"+c+"';";
                FillGrid(connectionstring, this.requete);
            }
        }

        private void ObjectChange()
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            // Effacer les éléments existants de la combobox
            Client.Items.Clear();

            // Créer une commande SQL pour récupérer les données à partir de la base de données
            MySqlCommand command = new MySqlCommand("SELECT id_client FROM clients;", connection);

            // Exécuter la commande SQL et récupérer les données dans un DataReader
            MySqlDataReader reader = command.ExecuteReader();

            // Parcourir les enregistrements du DataReader et ajouter chaque élément à la combobox
            while (reader.Read())
            {
                string name = "";
                for (int i = 0; i < reader.FieldCount; i++)    // parcours cellule par cellule
                {
                    string valueAsString = reader.GetValue(i).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                    name = valueAsString;
                    Client.Items.Add(name);
                }
            }

            // Fermer la connexion et le DataReader
            reader.Close();
        }

        private void ModifButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.connectionstring);
                connection.Open();
                string command = "UPDATE commande SET etat_commande = '" + this.etat + "' WHERE num_commande = '" + Numcom.Text + "';";
                MySqlCommand cmdSel = new MySqlCommand(command, connection);
                int rowsAffected = cmdSel.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Modification réussie", "Modification Success", MessageBoxButton.OK, MessageBoxImage.Information);


                }
                else
                {
                    MessageBox.Show("Erreur de modification : Aucune commande à modifier", "Modification Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de modification ", "Modification Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Statut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Statut.SelectedItem != null)
            {
                string selectedelement = Statut.SelectedItem.ToString();
                string c = selectedelement.Replace("System.Windows.Controls.ComboBoxItem : ", "");
                this.etat = c;
            }
        }

        private void Statutselect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Statutselect.SelectedItem != null)
            {
                string selectedelement = Statutselect.SelectedItem.ToString();
                string ca = selectedelement.Replace("System.Windows.Controls.ComboBoxItem : ","");
                Console.Write(ca);
                this.requete = "Select "+this.éléments+" from commande where etat_commande = '"+ca+"';";
                FillGrid(connectionstring, this.requete);
            }
        }

        private void Numcom_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
