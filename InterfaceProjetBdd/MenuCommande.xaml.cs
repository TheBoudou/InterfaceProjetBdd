using Google.Protobuf.WellKnownTypes;
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
    /// Logique d'interaction pour MenuCommande.xaml
    /// </summary>
    public partial class MenuCommande : Window
    {
        MySqlConnection Connexion;
        string connectionstring;
        string éléments;
        string requete;
        string commandesql;
        string part1;
        string etat;
        string bouquet;
        public MenuCommande(string connectionstring)
        {
            InitializeComponent();
            string commande = "Select * from commande;";
            this.éléments = "*";
            this.connectionstring = connectionstring;
            FillGrid(connectionstring, commande);
            ObjectChange();
            ObjectChange2();
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuPrincipal(connectionstring);
            Menu.Show();
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
                dataGrid1.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {

            }
            connection.Close();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Element1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedelement = ((ComboBoxItem)Element1.SelectedItem).Content as string;
            this.part1 = selectedelement;
            if (selectedelement != "*" && selectedelement != " *" && selectedelement != " * ")
            {
                this.éléments = "num_commande," + selectedelement + " ";
            }
            else
            {
                this.éléments = selectedelement + " ";
            }
            this.requete = this.commandesql + this.éléments + " from commande ";
        }

        private void Element2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedelement = ((ComboBoxItem)Element2.SelectedItem).Content as string;
            if (selectedelement != part1 && selectedelement != "")
            {
                if (part1 != "*" && part1 != " *" && part1 != " * ")
                {
                    this.éléments = "num_commande," + part1 + ", " + selectedelement + " ";
                    this.requete = this.commandesql + this.éléments + " from commande ";
                }


            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            this.commandesql = "Select ";
            this.requete = this.commandesql + this.éléments + " from commande ";
            FillGrid(connectionstring, requete);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Client.SelectedItem != null)
            {
                string selectedelement = Client.SelectedItem.ToString();
                string c = selectedelement;
                this.requete="Select "+this.éléments+" from commande where id_client = '"+c+"';";
                FillGrid(connectionstring, this.requete);
            }
        }

        private void ObjectChange()
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            // Effacer les éléments existants de la combobox
            Client.Items.Clear();

            // Créer une commande SQL pour récupérer les données à partir de la base de données
            MySqlCommand command = new MySqlCommand("SELECT id_client FROM clients;", connection);

            // Exécuter la commande SQL et récupérer les données dans un DataReader
            MySqlDataReader reader = command.ExecuteReader();

            // Parcourir les enregistrements du DataReader et ajouter chaque élément à la combobox
            while (reader.Read())
            {
                string name = "";
                for (int i = 0; i < reader.FieldCount; i++)    // parcours cellule par cellule
                {
                    string valueAsString = reader.GetValue(i).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                    name = valueAsString;
                    Client.Items.Add(name);
                }
            }

            // Fermer la connexion et le DataReader
            reader.Close();
        }

        private void ModifButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.connectionstring);
                connection.Open();
                string command = "UPDATE commande SET etat_commande = '" + this.etat + "' WHERE num_commande = '" + Numcom.Text + "';";
                MySqlCommand cmdSel = new MySqlCommand(command, connection);
                int rowsAffected = cmdSel.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Modification réussie", "Modification Success", MessageBoxButton.OK, MessageBoxImage.Information);


                }
                else
                {
                    MessageBox.Show("Erreur de modification : Aucune commande à modifier", "Modification Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de modification ", "Modification Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Statut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Statut.SelectedItem != null)
            {
                string selectedelement = Statut.SelectedItem.ToString();
                string c = selectedelement.Replace("System.Windows.Controls.ComboBoxItem : ", "");
                this.etat = c;
            }
        }

        private void Statutselect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Statutselect.SelectedItem != null)
            {
                string selectedelement = Statutselect.SelectedItem.ToString();
                string ca = selectedelement.Replace("System.Windows.Controls.ComboBoxItem : ","");
                this.requete = "Select "+this.éléments+" from commande where etat_commande = '"+ca+"';";
                FillGrid(connectionstring, this.requete);
            }
        }

        private void Numcom_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ObjectChange2()
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            // Effacer les éléments existants de la combobox
            Client.Items.Clear();

            // Créer une commande SQL pour récupérer les données à partir de la base de données
            MySqlCommand command = new MySqlCommand("SELECT id_perso FROM bouquet_perso;", connection);

            // Exécuter la commande SQL et récupérer les données dans un DataReader
            MySqlDataReader reader = command.ExecuteReader();

            // Parcourir les enregistrements du DataReader et ajouter chaque élément à la combobox
            while (reader.Read())
            {
                string name = "";
                for (int i = 0; i < reader.FieldCount; i++)    // parcours cellule par cellule
                {
                    string valueAsString = reader.GetValue(i).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                    name = valueAsString;
                    BouquetPerso.Items.Add(name);
                }
            }

            // Fermer la connexion et le DataReader
            reader.Close();
        }

        private void Bouquet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BouquetPerso.SelectedItem != null)
            {
                string selectedelement = BouquetPerso.SelectedItem.ToString();
                string c = selectedelement.Replace("System.Windows.Controls.ComboBoxItem : ", "");
                this.bouquet = c;
            }
        }

        private void ModifButton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionstring);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "select id_bouquet from commande where num_commande = '" + numcomperso.Text + "';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string id_bouquet = "";
                while (reader.Read())                           // parcours ligne par ligne
                {
                    id_bouquet = reader.GetValue(0).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                }
                reader.Close();
                connection.Close();
                if (id_bouquet == "vide")
                {
                    try
                    {
                        connection.Open();
                        string command2 = "UPDATE commande SET id_perso = '" + this.bouquet + "' WHERE num_commande = '" + numcomperso.Text + "';";
                        MySqlCommand cmdSel = new MySqlCommand(command2, connection);
                        int rowsAffected = cmdSel.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            try
                            {
                                MySqlCommand command3 = connection.CreateCommand();
                                command3.CommandText = "select prix_max from bouquet_perso where id_perso = '" + this.bouquet + "';";
                                reader = command3.ExecuteReader();
                                float prixbouquet = 0;
                                while (reader.Read())                           // parcours ligne par ligne
                                {
                                    prixbouquet = Convert.ToSingle(reader.GetValue(0));  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                                }
                                reader.Close();
                                MySqlCommand command4 = connection.CreateCommand();
                                command4.CommandText = "select Statut from clients,commande where commande.id_client = clients.id_client and num_commande= '"+numcomperso.Text+"';";
                                reader = command4.ExecuteReader();
                                string statut = "";
                                while (reader.Read())                           // parcours ligne par ligne
                                {
                                    statut = reader.GetValue(0).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
                                }
                                reader.Close();
                                if (statut == "Or")
                                {
                                    prixbouquet = prixbouquet * 85 / 100;
                                }
                                else if (statut == "Bronze")
                                {
                                    prixbouquet = prixbouquet * 95 / 100;
                                }
                                string command5 = "UPDATE bouquet_perso SET prix_max = '" + prixbouquet + "' WHERE id_perso = '" + this.bouquet + "';";
                                MySqlCommand final = new MySqlCommand(command5, connection);
                                int rowsAffected2 = final.ExecuteNonQuery();
                                if (rowsAffected2 > 0)
                                {
                                    MessageBox.Show("Modification réussie", "Modification Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Erreur de modification du prix", "Modification Error 5", MessageBoxButton.OK, MessageBoxImage.Error);
                                }

                                    
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("Erreur de modification du prix", "Modification Error 4", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            
                            


                        }
                        else
                        {
                            MessageBox.Show("Erreur de modification : Aucune commande à modifier", "Modification Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur de modification ", "Modification Error 3", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Erreur de modification : La commande contient un bouquet standard ", "Modification Error 2", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erreur de modification ", "Modification Error 1", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
