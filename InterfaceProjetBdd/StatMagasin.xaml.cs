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
    /// Logique d'interaction pour StatMagasin.xaml
    /// </summary>
    public partial class StatMagasin : Window
    {
        string connectionstring;
        public StatMagasin(string connectionstring)
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
