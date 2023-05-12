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
    /// Logique d'interaction pour MenuBouquet.xaml
    /// </summary>
    public partial class MenuBouquet : Window
    {
        string connectionstring;
        string type_bouquet;
        string type;
        string emptytype;
        string emptytypebouquet;
        string objet;
        string composant;
        public MenuBouquet(string connectionstring)
        {
            InitializeComponent();
            this.connectionstring = connectionstring;
            FillGrid1(connectionstring);
            FillGrid2(connectionstring);
            this.type_bouquet = "perso";
            this.emptytypebouquet = "bouquet";

        }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuPrincipal(connectionstring);
            Menu.Show();
            this.Close();
        }
        public void FillGrid1(string connectionstring)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            connection.Open();
            string commande = "Select * from bouquet_std ;";
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
        public void FillGrid2(string connectionstring)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            connection.Open();
            string commande = "Select * from bouquet_perso ;";
            MySqlCommand cmdSel = new MySqlCommand(commande, connection);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
            try
            {
                da.Fill(dt);
                dataGrid2.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {

            }
            connection.Close();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void FillGrid3(string connectionstring)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            connection.Open();
            string commande = "Select id_accessoire, quantite from composition where id_" + this.type_bouquet + " = '" + idbouquet.Text + "' and id_fleur='vide' Union select id_fleur, quantite from composition where id_" + this.type_bouquet + " = '" + idbouquet.Text + "' and id_accessoire='vide';";
            MySqlCommand cmdSel = new MySqlCommand(commande, connection);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
            try
            {
                da.Fill(dt);

                dataGrid3.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {

            }
            connection.Close();
        }

        private void idbouquet_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillGrid3(connectionstring);
            SelectComposant();
        }

        private void Typebouquet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedelement = ((ComboBoxItem)Typebouquet.SelectedItem).Content as string;
            if (selectedelement != null)
            {
                string c = selectedelement.Replace("System.Windows.Controls.ComboBoxItem : ", "");
                this.type_bouquet = c;
                if (this.type_bouquet == "perso")
                {
                    this.emptytypebouquet = "bouquet";
                }
                else
                {
                    this.emptytypebouquet = "perso";
                }

            }
        }

        private void dataGrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SelectComposant()
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            // Effacer les éléments existants de la combobox
            Composant.Items.Clear();
            try
            {
                // Créer une commande SQL pour récupérer les données à partir de la base de données
                MySqlCommand command = new MySqlCommand("Select id_accessoire from composition where id_" + this.type_bouquet + " = '" + idbouquet.Text + "' and id_fleur='vide' Union select id_fleur from composition where id_" + this.type_bouquet + " = '" + idbouquet.Text + "' and id_accessoire='vide';", connection);

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
                        Composant.Items.Add(name);
                    }
                }

                // Fermer la connexion et le DataReader
                reader.Close();
                connection.Close();
            }
            catch
            {

            }


        }



        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedelement = ((ComboBoxItem)Type.SelectedItem).Content as string;
            if (selectedelement != null)
            {
                string c = selectedelement.Replace("System.Windows.Controls.ComboBoxItem : ", "");
                this.type = c;
                if (this.type == "fleur")
                {
                    this.emptytype = "accessoire";
                }
                else
                {
                    this.emptytype = "fleur";
                }
                MySqlConnection connection = new MySqlConnection(this.connectionstring);
                connection.Open();

                // Effacer les éléments existants de la combobox
                Objet.Items.Clear();

                // Créer une commande SQL pour récupérer les données à partir de la base de données
                MySqlCommand command = new MySqlCommand("SELECT id_" + this.type + " FROM " + this.type + ";", connection);

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
                        Objet.Items.Add(name);
                    }
                }

                // Fermer la connexion et le DataReader
                reader.Close();
                connection.Close();
            }
        }

        private void Ajout_Click(object sender, RoutedEventArgs e)
        {
            if (idbouquet.Text == "" || Quantite.Text == "")
            {
                MessageBox.Show("Vous devez remplir tous les champs nécéssaires (Nom bouquet, Quantite)", "Sumbission Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int num;
                if (int.TryParse(Quantite.Text, out num)) // Conversion en entier
                {
                    if (num >= 0)
                    {
                        try
                        {
                            MySqlConnection connection = new MySqlConnection(this.connectionstring);
                            connection.Open();
                            string command = "INSERT INTO `fleurs`.`composition` (`id_" + this.type + "`,`quantite`,`id_" + this.emptytype + "`,`id_" + this.type_bouquet + "`,`id_" + this.emptytypebouquet + "`)Values('" + this.objet + "','" + num + "','vide','"+idbouquet.Text+"','vide');";
                            MySqlCommand cmdSel = new MySqlCommand(command, connection);
                            int rowsAffected = cmdSel.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {

                                MessageBox.Show("Ajout Réussie", "Add Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                FillGrid3(connectionstring);

                            }

                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Erreur d'Ajout", "Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }

                    }
                    else
                    {
                        MessageBox.Show("Une erreur est survenue : Veuillez saisir une quantite correct.", "Erreur Critique", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

        private void Objet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Objet.SelectedItem != null)
            {
                string selectedelement = Objet.SelectedItem.ToString();
                this.objet = selectedelement;
            }
            
        }

        private void Composant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Composant.SelectedItem != null)
            {
                string selectedelement = Composant.SelectedItem.ToString();
                this.composant = selectedelement;
            }
            
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.connectionstring);
                connection.Open();
                string command = "Delete from composition where id_" + this.type_bouquet + " = '" + idbouquet.Text + "'and id_fleur= '"+this.composant+"' or id_accessoire = '"+this.composant+"';";
                MySqlCommand cmdSel = new MySqlCommand(command, connection);
                int rowsAffected = cmdSel.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Suppression Réussie", "Suppression Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillGrid3(connectionstring);

                }
                else
                {
                    MessageBox.Show("Erreur de suppression", "Suppression Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Une erreur est survenue : Erreur de de Suppression. ", "Erreur Critique SQL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    }


