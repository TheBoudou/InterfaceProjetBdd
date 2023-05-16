using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

using Newtonsoft.Json;

using System.Xml.Serialization;
using System.Xml;
using System.ComponentModel.Design;

namespace InterfaceProjetBdd
{
    /// <summary>
    /// Logique d'interaction pour StatClient.xaml
    /// </summary>
    /// 


    public partial class Client
    {
        private string id_client;
        private string nom_client;
        private string prenom_client;
        private string num_tel_client;
        private string email_client;
        private string mdp_client;
        private string adresse_facturation_client;
        private string carte_credit_client;
        private int nb_commandes;
        private int nb_commandes_mois;
        private string statut;


        public Client(string id_client, string nom_client, string prenom_client, string num_tel_client, string email_client, string mdp_client, string adresse_facturation_client, string carte_credit_client, int nb_commandes, int nb_commandes_mois, string statut)
        {
            this.id_client = id_client;
            this.nom_client = nom_client;
            this.prenom_client = prenom_client;
            this.num_tel_client = num_tel_client;
            this.email_client = email_client;
            this.mdp_client = mdp_client;
            this.adresse_facturation_client = adresse_facturation_client;
            this.carte_credit_client = carte_credit_client;
            this.nb_commandes = nb_commandes;
            this.nb_commandes_mois = nb_commandes_mois;
            this.statut = statut;
        }

        public Client()
        {
    
        }

        public string IdClient
        {
            get { return id_client; }
            set { id_client = value; }
        }

        public string NomClient
        {
            get { return nom_client; }
            set { nom_client = value; }
        }

        public string PrenomClient
        {
            get { return prenom_client; }
            set { prenom_client = value; }
        }

        public string NumTelClient
        {
            get { return num_tel_client; }
            set { num_tel_client = value; }
        }

        public string EmailClient
        {
            get { return email_client; }
            set { email_client = value; }
        }

        public string MdpClient
        {
            get { return mdp_client; }
            set { mdp_client = value; }
        }

        public string AdresseFacturationClient
        {
            get { return adresse_facturation_client; }
            set { adresse_facturation_client = value; }
        }

        public string CarteCreditClient
        {
            get { return carte_credit_client; }
            set { carte_credit_client = value; }
        }

        public int NbCommandes
        {
            get { return nb_commandes; }
            set { nb_commandes = value; }
        }

        public int NbCommandesMois
        {
            get { return nb_commandes_mois; }
            set { nb_commandes_mois = value; }
        }

        public string Statut
        {
            get { return statut; }
            set { statut = value; }
        }

        override public string ToString()
          => "à écrire";
    }




    public partial class StatClient : Window
    {
        string connectionstring;
        public StatClient(string connectionstring)
        {
            InitializeComponent();
            //this.Connexion = Connexion;
            this.connectionstring = connectionstring;
        }



        private void AfficherPrettyJson(string nomFichier)
        {
            StreamReader reader = new StreamReader(nomFichier);
            JsonTextReader jreader = new JsonTextReader(reader);
            while (jreader.Read())
            {
                if (jreader.Value != null)
                {
                    if (jreader.TokenType.ToString() == "PropertyName")
                    {
                        Console.Write(jreader.Value + " : ");
                    }
                    else
                    {
                        Console.WriteLine(jreader.Value);
                    }
                }
                else
                {
                    // Console.WriteLine("Token:{0} ", jreader.TokenType.ToString());
                    if (jreader.TokenType.ToString() == "StartObject") Console.WriteLine("Nouvel objet\n--------------");
                    if (jreader.TokenType.ToString() == "EndObject") Console.WriteLine("-------------\n");
                    if (jreader.TokenType.ToString() == "StartArray") Console.WriteLine("Liste\n");
                }
            }
            jreader.Close();
            reader.Close();
        }



        private void SerialiserObjectToFileJson(MySqlConnection connection)
        {
            // création de la liste d'objets mesChats
            List<Client> clientsNonCommandes = new List<Client>();

            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT *"
                                   + " FROM clients"
                                   + " WHERE id_client NOT IN("
                                   + " SELECT id_client"
                                   + " FROM commande"
                                   + " WHERE date_commande > DATE_SUB(NOW(), INTERVAL 6 MONTH));";

            FillGrid(this.connectionstring, command.CommandText);
            MySqlDataReader reader;
            reader = command.ExecuteReader();

            string id_client;
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

            while (reader.Read())   // parcours ligne par ligne
            {

                id_client = reader.GetString(0);
                nom_client = reader.GetString(1);
                prenom_client = reader.GetString(2);
                num_tel_client = reader.GetString(3);
                email_client = reader.GetString(4);
                mdp_client = reader.GetString(5);
                adresse_facturation_client = reader.GetString(6);
                carte_credit_client = reader.GetString(7);
                nb_commandes = reader.GetInt32(8);
                nb_commandes_mois = reader.GetInt32(9);
                statut = reader.GetString(10);

                clientsNonCommandes.Add(new Client(id_client, nom_client, prenom_client, num_tel_client, email_client, mdp_client, adresse_facturation_client, carte_credit_client, nb_commandes, nb_commandes_mois, statut));
            }

            connection.Close();

            // instanciation des outils
            StreamWriter sw = new StreamWriter("clientsNonCommandes.json");
            JsonTextWriter writer = new JsonTextWriter(sw);

            // sérialisation
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Serialize(writer, clientsNonCommandes);

            // fermeture des writers
            writer.Close();
            sw.Close();

        }



        private void SerialiserXml(MySqlConnection connection) 
        {
            List<Client> clientsCommandes = new List<Client>();


            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT *"
                                + " FROM clients"
                                + " WHERE nb_commandes_mois>1 ;";

            FillGrid(this.connectionstring, command.CommandText);
            MySqlDataReader reader;
            reader = command.ExecuteReader();

            string id_client;
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

            while (reader.Read())   // parcours ligne par ligne
            {

                id_client = reader.GetString(0);
                nom_client = reader.GetString(1);
                prenom_client = reader.GetString(2);
                num_tel_client = reader.GetString(3);
                email_client = reader.GetString(4);
                mdp_client = reader.GetString(5);
                adresse_facturation_client = reader.GetString(6);
                carte_credit_client = reader.GetString(7);
                nb_commandes = reader.GetInt32(8);
                nb_commandes_mois = reader.GetInt32(9);
                statut = reader.GetString(10);

                Console.WriteLine(id_client + " : " + nom_client + ", " + prenom_client + ", " + num_tel_client + ", " + email_client + ", " + mdp_client + ", " + adresse_facturation_client + ", " + carte_credit_client + ", " + nb_commandes + ", " + nb_commandes_mois + ", " + statut);

                clientsCommandes.Add(new Client(id_client, nom_client, prenom_client, num_tel_client, email_client, mdp_client, adresse_facturation_client, carte_credit_client, nb_commandes, nb_commandes_mois, statut));
            }

            connection.Close();




            // Code pour sérialiser l'objet en XML dans un fichier ".xml"
            XmlSerializer xs = new XmlSerializer(typeof(List<Client>));  // l'outil de sérialisation
            StreamWriter wr = new StreamWriter("clientsCommandes.xml");  // accès en écriture à un fichier (texte)
            xs.Serialize(wr, clientsCommandes); // action de sérialiser en XML l'objet  
                                                // et d'écrire le résultat dans le fichier manipulé par wr
            wr.Close();
            Console.WriteLine("sérialisation dans fichier clientsCommandes.xml terminée");

            // vérifier le contenu du fichier ".xml" dans le dossier bin\Debug de Visual Studio.
        }


        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            var MenuStatistique = new MenuStatistique(connectionstring);
            MenuStatistique.Show();
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
            command.CommandText = commande2;

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

            command.CommandText = " SELECT nom_client, prenom_client,num_tel_client, email_client FROM clients"
            + " WHERE id_client=\"" + clients[0] + "\";";

            string nom = "";
            string prenom = "";
            string num = "";
            string emailClient = "";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                nom = reader.GetString(0);
                prenom = reader.GetString(1);
                num = reader.GetString(2);
                emailClient = reader.GetString(3);

            }

            reader.Close();

            Id_client.Text= clients[0];
            nom_client.Text = nom; ;
            prenom_client.Text = prenom;
            num_tel.Text = num;
            email.Text = emailClient;
            nb_commandes.Text = Convert.ToString(sommeAchats[0])+" €";

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



        private void JSON_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            SerialiserObjectToFileJson(connection);
            MessageBox.Show("Export en JSON réussi", "JSON Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void XML_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            SerialiserXml(connection);
            MessageBox.Show("Export en XML réussi", "XML Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void id_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void nom_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void prenom_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void num_tel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void nb_commandes_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
