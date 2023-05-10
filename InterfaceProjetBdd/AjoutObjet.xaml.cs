using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    /// Logique d'interaction pour AjoutObjet.xaml
    /// </summary>
    public partial class AjoutObjet : Window
    {
        string table;
        string connectionstring;
        public AjoutObjet(string connectionstring)
        {
            InitializeComponent();
            this.connectionstring = connectionstring;
            this.table = "fleur";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ID.Text == "" || Prix.Text == "" )
            {
                MessageBox.Show("Vous devez remplir tous les champs nécéssaires (nom objet,Prix)", "Sumbission Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int num;
                if (int.TryParse(Prix.Text, out num)) // Conversion en entier
                {
                    if (num >= 0)
                    {
                        MySqlConnection connection = new MySqlConnection(connectionstring);
                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "select id_" + this.table + " from " + this.table + " where id_" + this.table + " = '" + ID.Text + "';";
                        MySqlDataReader readerid;
                        readerid = command.ExecuteReader();
                        string id = "";
                        while (readerid.Read())                           // parcours ligne par ligne
                        {
                            id = readerid.GetValue(0).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                        }
                        readerid.Close();
                        if (id == "")
                        {
                            string commande = "INSERT INTO `fleurs`.`" + this.table + "` (`id_" + this.table + "`,`prix_" + this.table + "`,`dispo_" + this.table + "`)Values('" + ID.Text + "','" + num + "',True);";
                            MySqlCommand cmdSel = new MySqlCommand(commande, connection);
                            int rowsAffected = cmdSel.ExecuteNonQuery();


                            // Récupération des données de la première table
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
                                    MySqlCommand Insert = new MySqlCommand("INSERT INTO `fleurs`.`stock" + this.table+"` (`id_"+this.table+"`, `id_magasin`, `quantite`)VALUES('"+ID.Text+"','"+idval+"' ,"+0+");", conn);
                                    Insert.ExecuteNonQuery();
                                    conn.Close();
                                }


                                // Ajout d'une entrée dans la deuxième table pour chaque entrée de la première table
                                
                            }

                            // Fermeture de la connexion
                            connection.Close();

                            // vérification du nombre de lignes affectées par l'insertion
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Insertion Réussie", "Insertion Success", MessageBoxButton.OK, MessageBoxImage.Information);

                            }
                            else
                            {
                                MessageBox.Show("Erreur d'insertion", "Insertion Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Object existant", "Existence Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }


                        connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Une erreur est survenue : Veuillez saisir un prix correct.", "Erreur Critique Prix", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Une erreur est survenue : Veuillez saisir un prix correct.", "Erreur Critique Prix", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedelement = ((ComboBoxItem)Table.SelectedItem).Content as string;
            if (selectedelement != null)
            {
                this.table = selectedelement;
            }
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuStock(connectionstring);
            Menu.Show();
            this.Close();
        }
    }
}
