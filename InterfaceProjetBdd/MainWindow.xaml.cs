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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace InterfaceProjetBdd
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {

        string password;
        string user;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //connection
        {
            user = ID.Text;
            password = mdp.Password;
            string databasesql = "fleurs";
            string connectionstring = "";
            if (user == "root")
            {
                connectionstring = "SERVER=localhost;PORT=3306;DATABASE=" + databasesql + ";UID=" + user + ";PASSWORD=" + password + ";";
            }
            else
            {
                connectionstring = "SERVER=localhost;PORT=3306;DATABASE=" + databasesql + ";UID=bozo;PASSWORD=bozo;";
            }
            MySqlConnection Connexion = new MySqlConnection(connectionstring);
            try
            {
                Connexion.Open();
                if (Connexion.State == ConnectionState.Open)
                {
                    if (user == "root")
                    {
                        // Connection successful
                        MessageBox.Show("Connection réussie avec l'utilisateur : " + ID.Text, "Connection Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        var Menu = new MenuPrincipal(connectionstring);
                        Menu.Show();
                        Connexion.Close();
                        this.Close();
                    }
                    else
                    {
                        MySqlCommand command = Connexion.CreateCommand();
                        command.CommandText = "select id_client from clients where mdp_client = '" + password + "';";
                        MySqlDataReader reader;
                        reader = command.ExecuteReader();
                        string id = "";
                        while (reader.Read())                           // parcours ligne par ligne
                        {
                            id = reader.GetValue(0).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                        }
                        reader.Close();
                        if (id != null)
                        {
                            MessageBox.Show("Connection réussie avec l'utilisateur : " + ID.Text, "Connection Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            var Menu = new MenuPourClient(connectionstring,user);
                            Menu.Show();
                            Connexion.Close();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Une erreur est survenue : Mauvais Login ou Mot de passe, réssayez.", "Erreur Critique Client", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    
                }
            }


            catch (Exception ex)
            {
                // Exception occurred, handle it here
                MessageBox.Show("Une erreur est survenue : Mauvais Login ou Mot de passe, réssayez.", "Erreur Critique", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
    } 

}
