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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace InterfaceProjetBdd
{
    /// <summary>
    /// Logique d'interaction pour MenuPourClient.xaml
    /// </summary>
    /// 


    public partial class MenuPourClient : Window
    {
        string connectionstring;
        string id;
        string nom_client;
        string prenom_client;
        string num_tel_client;
        string email_client;
        string mdp_client;
        string adresse_facturation_client;
        string carte_credit_client;
        int nb_commandes;
        int nb_commandes_mois;
        string statut;

        public MenuPourClient(string connectionstring, string ID_client)
        {
            InitializeComponent();
            this.connectionstring = connectionstring;
            this.id = ID_client;

            MySqlConnection connection = new MySqlConnection(connectionstring);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT *"
                                   + " FROM clients"
                                   + " WHERE id_client ='"
                                   + this.id + "';";


            MySqlDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())   // parcours ligne par ligne
            {

                this.id = reader.GetString(0);
                this.nom_client = reader.GetString(1);
                this.prenom_client = reader.GetString(2);
                this.num_tel_client = reader.GetString(3);
                this.email_client = reader.GetString(4);
                this.mdp_client = reader.GetString(5);
                this.adresse_facturation_client = reader.GetString(6);
                this.carte_credit_client = reader.GetString(7);
                this.nb_commandes = reader.GetInt32(8);
                this.nb_commandes_mois = reader.GetInt32(9);
                this.statut = reader.GetString(10);

                W_id_client.Text = this.id;
                W_nom.Text = this.nom_client;
                W_prenom.Text = this.prenom_client;
                W_numTel.Text = this.num_tel_client;
                W_email.Text = this.email_client;
                W_mdp.Password = this.mdp_client;
                W_adresse.Text = this.adresse_facturation_client;
                W_carteCredit.Text = this.carte_credit_client;
                W_nb_commandes.Text = Convert.ToString(this.nb_commandes);
                W_nb_commandes_mois.Text = Convert.ToString(this.nb_commandes_mois);
                W_statut.Text = this.statut;
            }
        }

        private void Deconnexion_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MainWindow();
            Menu.Show();
            this.Close();
        }

      
        private void W_id_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void nom_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (W_nom.Text != null)
            {
                this.nom_client = W_nom.Text;      
            }
        }

        private void prenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (W_prenom.Text != null)
            {
                this.prenom_client = W_prenom.Text;
            }
        }

        private void numtel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (W_numTel.Text != null)
            {
                this.num_tel_client = W_numTel.Text;
            }
        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (W_email.Text != null)
            {
                this.email_client = W_email.Text;
            }
        }

   
        private void adresse_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (W_adresse.Text != null)
            {
                this.adresse_facturation_client = W_adresse.Text;
            }
        }

        private void carte_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (W_carteCredit.Text != null)
            {
                this.carte_credit_client = W_carteCredit.Text;
            }
        }

        private void W_nb_commandes_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void W_nb_commandes_mois_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void W_statut_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Sauvegarder_Click(object sender, RoutedEventArgs e)
        {

            this.mdp_client = W_mdp.Password;

            W_id_client.Text = this.id;
            W_nom.Text = this.nom_client;
            W_prenom.Text = this.prenom_client;
            W_numTel.Text = this.num_tel_client;
            W_email.Text = this.email_client;
            W_mdp.Password = this.mdp_client;
            W_adresse.Text = this.adresse_facturation_client;
            W_carteCredit.Text = this.carte_credit_client;
            W_nb_commandes.Text = Convert.ToString(this.nb_commandes);
            W_nb_commandes_mois.Text = Convert.ToString(this.nb_commandes_mois);
            W_statut.Text = this.statut;


            string update = "UPDATE clients SET "
                + "nom_client = '" + this.nom_client + "', "
                + "prenom_client = '" + this.prenom_client + "', "
                + "num_tel_client = '" + this.num_tel_client + "', "
                + "email_client = '" + this.email_client + "', "
                + "mdp_client = '" + this.mdp_client + "', "
                + "adresse_facturation_client = '" + this.adresse_facturation_client + "', "
                + "carte_credit_client = '" + this.carte_credit_client + "' "
                + "WHERE id_client='" + this.id + "';";

            MySqlConnection connection = new MySqlConnection(connectionstring);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = update;
            command.ExecuteReader();
            connection.Close();

            
        }
    }
}
