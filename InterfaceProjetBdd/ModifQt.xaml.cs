using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    /// Logique d'interaction pour ModifQt.xaml
    /// </summary>
    public partial class ModifQt : Window
    {
        string id_magasin;
        string connectionstring;
        string table;
        string stocktable;
        string objet;
        public ModifQt(string connectionstring, string id_magasin)
        {
            InitializeComponent();
            this.connectionstring = connectionstring;
            this.id_magasin = id_magasin;
        }

        private void VérificationExistence()
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            // Créer une commande SQL pour récupérer les données à partir de la base de données
            MySqlCommand command = new MySqlCommand("SELECT id_fleur FROM fleur" , connection);

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
                    Object.Items.Add(name);
                }
            }

            // Fermer la connexion et le DataReader
            reader.Close();
            connection.Close();
        

    }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuStock(this.connectionstring);
            Menu.Show();
            this.Close();
        }

        private void SelectFleur_Click(object sender, RoutedEventArgs e)
        {
            this.table = "fleur";
            this.stocktable = "stockfleur";
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            // Effacer les éléments existants de la combobox
            Object.Items.Clear();

            // Créer une commande SQL pour récupérer les données à partir de la base de données
            MySqlCommand command = new MySqlCommand("SELECT id_fleur FROM "+this.table, connection);

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
                    Object.Items.Add(name);
                }
            }

            // Fermer la connexion et le DataReader
            reader.Close();
            connection.Close();
        }

        private void SelectAccessoire_Click(object sender, RoutedEventArgs e)
        {
            this.table = "accessoire";
            this.stocktable = "stockaccessoire";
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            // Effacer les éléments existants de la combobox
            Object.Items.Clear();

            // Créer une commande SQL pour récupérer les données à partir de la base de données
            MySqlCommand command = new MySqlCommand("SELECT id_accessoire FROM "+ this.table, connection);

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
                    Object.Items.Add(name);
                }
            }

            // Fermer la connexion et le DataReader
            reader.Close();
            connection.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Object.SelectedItem != null)
            {
                string selectedelement = Object.SelectedItem.ToString();
                this.objet = selectedelement;
            }
        }

        private void ModifButton_Click(object sender, RoutedEventArgs e)
        {
            int num;
            if (int.TryParse(Ajout.Text, out num)) // Conversion en entier
            {
                if (num >= 0)
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(this.connectionstring);
                        connection.Open();
                        string command = "UPDATE " + this.stocktable + " SET quantite = +" + num + " WHERE id_" + this.table + " = '" + this.objet + "' and id_magasin= '" + this.id_magasin + "';";
                        MySqlCommand cmdSel = new MySqlCommand(command, connection);
                        int rowsAffected = cmdSel.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Insertion Réussie", "Insertion Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            if (num == 0)
                            {
                                string command2 = "UPDATE " + this.table + " SET dispo_"+this.table+" = False WHERE id_" + this.table + " = '" + this.objet +"';";
                                MySqlCommand cmdSel2 = new MySqlCommand(command, connection);
                                
                            }

                        }
                        else
                        {
                            MessageBox.Show("Erreur d'insertion", "Insertion Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Une erreur est survenue : Erreur de Mise à Jour.", "Erreur Critique SQL", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Une erreur est survenue : Veuillez saisir une valeur correct.", "Erreur Critique Value", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Une erreur est survenue : Veuillez saisir une valeur correct.", "Erreur Critique Value", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
