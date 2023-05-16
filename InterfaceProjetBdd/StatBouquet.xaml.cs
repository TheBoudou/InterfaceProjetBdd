using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InterfaceProjetBdd
{
    /// <summary>
    /// Logique d'interaction pour StatBouquet.xaml
    /// </summary>
    public partial class StatBouquet : Window
    {
        string connectionstring;
        public StatBouquet(string connectionstring)
        {
            InitializeComponent();
            //this.Connexion = Connexion;
            this.connectionstring = connectionstring;
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            var MenuStatistique = new MenuStatistique(connectionstring);
            MenuStatistique.Show();
            this.Close();
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

            string nomClient = "Le bouquet standard ayant le plus de succès est le bouquet " + id_bouquet + " pour un nombre de commandes = " + nbrBouquet;

            nom_client.Text = nomClient;
            connection.Close();
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

        private void Fleur_failure_Click(object sender, RoutedEventArgs e)
        {
          


            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            //union
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT F.id_fleur, COUNT(F.id_fleur)"
+ " FROM fleur F"
+ " WHERE F.id_fleur IN ("
    + " SELECT Co.id_fleur "
    + " FROM commande C, composition Co, bouquet_perso P"
    + " WHERE C.id_perso=P.id_perso AND Co.id_perso=P.id_perso"
    + " UNION "
    + " SELECT Co.id_fleur "
    + " FROM commande C, composition Co, bouquet_std S"
    + " WHERE C.id_bouquet=S.id_bouquet AND Co.id_bouquet=S.id_bouquet)"
    + " AND F.id_fleur <> 'vide'"
+ " GROUP BY F.id_fleur"
+ " ORDER BY COUNT(F.id_fleur) DESC;";

            FillGrid(connectionstring, command.CommandText);
            
            connection.Close();
        }

        private void ClientsMemeStd_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            string id_bouquet;
            int nbrBouquet;

            //auto-jointure et union
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT C1.id_bouquet, C1.id_client, C2.id_client FROM commande C1, commande C2"
           + " WHERE C1.id_bouquet=C2.id_bouquet "
            + " AND C1.id_client<C2.id_client"
           + " AND C1.id_bouquet<>'vide'"
+ " UNION"
 + " SELECT C1.id_perso, C1.id_client, C2.id_client FROM commande C1, commande C2"
            + " WHERE C1.id_perso=C2.id_perso "
           + " AND C1.id_client<C2.id_client "
           + " AND C1.id_perso<>'vide';"; 
            

            FillGrid(connectionstring, command.CommandText);
            connection.Close();
        }

        private void Accessoire_failure_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            

            //union
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT A.id_accessoire, COUNT(A.id_accessoire)"
+ " FROM accessoire A"
+  " WHERE A.id_accessoire IN ("
    + " SELECT Co.id_accessoire "
    + " FROM commande C, composition Co, bouquet_perso P"
    + " WHERE C.id_perso=P.id_perso AND Co.id_perso=P.id_perso"
    + " UNION "
    + " SELECT Co.id_accessoire "
    + " FROM commande C, composition Co, bouquet_std S"
    + " WHERE C.id_bouquet=S.id_bouquet AND Co.id_bouquet=S.id_bouquet)"
    + " AND A.id_accessoire <> 'vide'"
+ " GROUP BY A.id_accessoire"
+ " ORDER BY COUNT(A.id_accessoire) DESC;";

            FillGrid(connectionstring, command.CommandText);
           
            connection.Close();
        }

        private void CAEngendreSucces_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            string id_bouquet;
            int CA;

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT id_bouquet, count(id_bouquet), sum(prix_commande) FROM commande"
            + " WHERE id_bouquet<>'vide'"
            + " GROUP BY id_bouquet"
            + " ORDER BY count(id_bouquet) DESC;";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();
            id_bouquet = reader.GetString(0);
            CA = reader.GetInt32(2);

            reader.Close();

            string nomClient = "Le bouquet standard " + id_bouquet + " ayant le plus de succès a engendré un CA = " + CA+"€";

            nom_client.Text = nomClient;
            connection.Close();
        }
    }
}
