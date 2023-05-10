using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.X86;
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
    /// Logique d'interaction pour Ajout_Client.xaml
    /// </summary>
    public partial class Ajout_Client : Window
    {
        string connectionstring;
        public Ajout_Client(string connectionstring)
        {
            InitializeComponent();
            this.connectionstring = connectionstring;
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuClient(connectionstring);
            Menu.Show();
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (nom_client.Text == "" || num_tel_client.Text == "" || email_client.Text == "" || mdp_client.Text == "" || carte_credit_client.Text == "" || adresse_facturation_client.Text == "")
            {
                MessageBox.Show("Vous devez remplir tous les champs nécéssaires (nom, téléphone, mail, mot de passe, carte et adresse", "Sumbission Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(connectionstring);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "select email_client from clients where email_client = '" + email_client.Text + "';";
                MySqlDataReader readermail;
                readermail = command.ExecuteReader();
                string mail = "";
                while (readermail.Read())                           // parcours ligne par ligne
                {
                    mail = readermail.GetValue(0).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                }
                readermail.Close();
                if (mail == "")
                {
                    MySqlCommand command1 = connection.CreateCommand();
                    command1.CommandText = "SELECT Max(CAST(SUBSTRING(id_client, 2) AS UNSIGNED)) AS id_int FROM clients;"; // exemple de requete bien-sur !
                    object reader = command1.ExecuteScalar();
                    int valueAsInt = Convert.ToInt32(reader);  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                    int nb = valueAsInt;
                    nb += 1;
                    string id_client = "C" + nb;
                    string commande = "INSERT INTO `fleurs`.`clients` (`id_client`,`nom_client`,`prenom_client`,`num_tel_client`,`email_client`,`mdp_client`,`adresse_facturation_client`,`carte_credit_client`,`nb_commandes`,`nb_commandes_mois`,`Statut`)Values('" + id_client + "','" + nom_client.Text + "','" + prenom_client.Text + "' , '" + num_tel_client.Text + "','" + email_client.Text + "','" + mdp_client.Text + "','" + adresse_facturation_client.Text + "','" + carte_credit_client.Text + "', '0', '0', 'Non');";

                    MySqlCommand cmdSel = new MySqlCommand(commande, connection);
                    int rowsAffected = cmdSel.ExecuteNonQuery();

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
                    MessageBox.Show("Mail déjà utilisé","Mail Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                connection.Close();
            }

            



            
        }

        private void nom_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void prenom_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void num_tel_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void email_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void mdp_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void adresse_facturation_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void carte_credit_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
