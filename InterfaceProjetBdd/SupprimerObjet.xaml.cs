using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Logique d'interaction pour SupprimerObjet.xaml
    /// </summary>
    public partial class SupprimerObjet : Window
    {
        string connectionstring;
        string table;
        string objet;
        public SupprimerObjet(string connectionstring)
        {
            InitializeComponent();
            this.connectionstring = connectionstring;
        }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuStock(connectionstring);
            Menu.Show();
            this.Close();
        }

        private void ObjectChange()
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            // Effacer les éléments existants de la combobox
            Object.Items.Clear();

            // Créer une commande SQL pour récupérer les données à partir de la base de données
            MySqlCommand command = new MySqlCommand("SELECT id_"+this.table+" FROM " + this.table, connection);

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
        }

        private void FleurSelect_Click(object sender, RoutedEventArgs e)
        {
            this.table = "fleur";
            ObjectChange();
        }

        private void AccessoireSelect_Click(object sender, RoutedEventArgs e)
        {
            this.table = "accessoire";
            ObjectChange();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Object.SelectedItem != null)
            {
                string selectedelement = Object.SelectedItem.ToString();
                this.objet = selectedelement;
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.connectionstring);
                connection.Open();
                string command = "Delete from " + this.table + " where id_" + this.table + " = '" + this.objet + "';";
                MySqlCommand cmdSel = new MySqlCommand(command, connection);
                int rowsAffected = cmdSel.ExecuteNonQuery();

                MySqlCommand cmd = new MySqlCommand("SELECT id_magasin FROM magasin", connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Parcours des données de la première table
                while (reader.Read())
                {
                    string idval = "";
                    for (int i = 0; i < reader.FieldCount; i++)    // parcours cellule par cellule
                    {
                        string valueAsString = reader.GetValue(i).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                        idval = valueAsString;
                        MySqlConnection conn = new MySqlConnection(connectionstring);
                        conn.Open();
                        MySqlCommand Suppr = new MySqlCommand("Delete from stock" + this.table + " where id_"+this.table+" = '"+this.objet+"';", conn);
                        Suppr.ExecuteNonQuery();
                        conn.Close();
                    }


                    // Ajout d'une entrée dans la deuxième table pour chaque entrée de la première table

                }

                // Fermeture de la connexion
                connection.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Suppression Réussie", "Suppression Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    

                }
                else
                {
                    MessageBox.Show("Erreur de suppression", "Suppression Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Une erreur est survenue : Erreur de de Suppression. ", "Erreur Critique SQL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
