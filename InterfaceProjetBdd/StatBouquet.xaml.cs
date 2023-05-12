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
             
            }
            catch (Exception ex)
            {

            }
            connection.Close();
        }
        private void nom_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
