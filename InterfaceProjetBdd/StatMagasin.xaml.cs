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

                FillGrid(connectionstring, command.CommandText);
                
                connection.Close();



            
        }


        
        private void nom_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CAinf_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            string nom_magasin;
            int CA;

            //requete synchronisee
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT M.nom_magasin, M.chiffre_affaires FROM magasin M"
            + " WHERE M.chiffre_affaires < (SELECT avg(M1.chiffre_affaires) FROM magasin M1);";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();
            nom_magasin = reader.GetString(0);
            CA = reader.GetInt32(1);

            reader.Close();

            FillGrid(connectionstring, command.CommandText);

            connection.Close();
        }
    }
}
