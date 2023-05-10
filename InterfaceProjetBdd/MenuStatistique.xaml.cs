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

        private void CalculMeilleurClient(string commande1, string commande2, string finrep)
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            string id_client;
            int prixBouquetPerso;
            int prixBouquetStd;
            List<string> clients = new List<string>();
            List<int> sommeAchats = new List<int>();

            // sélection de la somme des prix des commandes bouquet perso passées par chaque client
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commande1;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())   // parcours ligne par ligne
            {

                id_client = reader.GetString(0);
                prixBouquetPerso = reader.GetInt32(1);
                clients.Add(id_client);
                sommeAchats.Add(prixBouquetPerso);

            }

            reader.Close();

            //sélection de la somme des prix des commandes bouquet std passées par chaque client
            command = connection.CreateCommand();
            command.CommandText =commande2;

            reader = command.ExecuteReader();
            while (reader.Read())   // parcours ligne par ligne
            {

                id_client = reader.GetString(0);
                prixBouquetStd = reader.GetInt32(1);
                if (clients.Contains(id_client))
                {
                    int index = clients.IndexOf(id_client);
                    sommeAchats[index] += prixBouquetStd;
                }
                else
                {
                    clients.Add(id_client);
                    sommeAchats.Add(prixBouquetStd);
                }

            }

            reader.Close();


            for (int i = 0; i < sommeAchats.Count - 1; i++)
            {
                for (int j = i + 1; j < sommeAchats.Count; j++)
                {
                    if (sommeAchats[i] < sommeAchats[j])
                    {
                        int tempInt = sommeAchats[i];
                        sommeAchats[i] = sommeAchats[j];
                        sommeAchats[j] = tempInt;

                        string tempString = clients[i];
                        clients[i] = clients[j];
                        clients[j] = tempString;
                    }
                }
            }

            string nomClient = "Client : " + clients[0] + " pour un montant dépensé de " + sommeAchats[0] + " euros durant"+finrep;

            nom_client.Text = nomClient;
            connection.Close();
        }

        private void MeilleurClientMois_Click(object sender, RoutedEventArgs e)
        {
            string commande1 = " SELECT id_client, sum(prix_max) FROM commande C NATURAL JOIN bouquet_perso P"
                                   + " WHERE date_commande > '2023-05-01'"
                                   + " GROUP BY id_client;";

            string commande2 = " SELECT id_client, sum(prix) FROM commande C NATURAL JOIN bouquet_std S"
                                  + " WHERE date_commande > '2023-05-01'"
                                  + " GROUP BY id_client;";

            string finrep = "  le dernier mois.";

            CalculMeilleurClient(commande1, commande2, finrep);
        }

        private void MeilleurClientAnnee_Click(object sender, RoutedEventArgs e)
        {

            string commande1 = " SELECT id_client, sum(prix_max) FROM commande C NATURAL JOIN bouquet_perso P"
                                   + " WHERE date_commande > '2023-01-01'"
                                   + " GROUP BY id_client;";

            string commande2 = " SELECT id_client, sum(prix) FROM commande C NATURAL JOIN bouquet_std S"
                                  + " WHERE date_commande > '2023-01-01'"
                                  + " GROUP BY id_client;";

            string finrep = "  la dernière année.";

            CalculMeilleurClient(commande1, commande2, finrep);



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
                AffichagePrincipal.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {

            }
            connection.Close();
        }

        private void nom_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BouqueStdSucces_Click(object sender, RoutedEventArgs e)
        {

            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            string id_bouquet;
            int nbrBouquet;
      
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT id_bouquet, count(id_bouquet) FROM commande"
            + " WHERE id_bouquet<>'vide'"
            + " GROUP BY id_bouquet"
            + " ORDER BY count(id_bouquet) DESC;"; 

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();  
            id_bouquet = reader.GetString(0);
            nbrBouquet = reader.GetInt32(1);

            reader.Close();

            string nomClient = "Le bouquet standard ayant le plus de succès est le bouquet " + id_bouquet + " pour un nombre de commandes = " + nbrBouquet ;

            nom_client.Text = nomClient;
            connection.Close();
          
        }

        private void MasasinGrosCA_Click(object sender, RoutedEventArgs e)
        {

            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            string nom_magasin;
            int CA;

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT nom_magasin, chiffre_affaires FROM magasin"
            + " WHERE chiffre_affaires>= ALL(SELECT chiffre_affaires FROM magasin);";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();
            nom_magasin = reader.GetString(0);
            CA = reader.GetInt32(1);

            reader.Close();

            string nomClient = "Le magasin ayant le plus grand CA est " + nom_magasin + " pour un CA de = " + CA;

            nom_client.Text = nomClient;
            connection.Close();

            

        }




        // Calcul du prix moyen du bouquet acheté 

    }
}
