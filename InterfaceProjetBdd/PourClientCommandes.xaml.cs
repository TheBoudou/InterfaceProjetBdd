using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
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
    /// Logique d'interaction pour PourClientCommandes.xaml
    /// </summary>
    public partial class PourClientCommandes : Window
    {
        string connectionstring;
        string id;
        string bouquetStd;
        string description;
        string prixPerso;
        string id_bouquetStd;

        string quantite;
        string fleur;
        string accessoire;
        string prixstd;
        public PourClientCommandes(string connectionstring, string id)
        {
            InitializeComponent();
            this.id = id;
            this.connectionstring = connectionstring;


            string commande = "Select * from commande where id_client='"+this.id+"';";
            FillGrid(connectionstring, commande);
            ObjectChange();


        }




        private void ObjectChange()
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            // Effacer les éléments existants de la combobox
            Element1.Items.Clear();

            // Créer une commande SQL pour récupérer les données à partir de la base de données
            MySqlCommand command = new MySqlCommand("SELECT nom_bouquet FROM bouquet_std;", connection);

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
                    Element1.Items.Add(name);
                }
            }

            // Fermer la connexion et le DataReader
            reader.Close();
            connection.Close();
        }



        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            var PourClient = new MenuPourClient(this.connectionstring,this.id);
            PourClient.Show();
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
      
        

        private void Element1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Element1.SelectedItem != null)
            {
                this.bouquetStd = Element1.SelectedItem.ToString();
                Element1.Text = this.bouquetStd ;
       
            }

            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();

            //categorie

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT categorie FROM bouquet_std"
            + " WHERE nom_bouquet=\"" + this.bouquetStd + "\";";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string categoriestr = "";
            while (reader.Read())
            {
                categoriestr = reader.GetString(0);
            }
            
            reader.Close();

            Categorie.Text = categoriestr;

            //id_bouquetStd 

            command.CommandText = " SELECT id_bouquet FROM bouquet_std"
            + " WHERE nom_bouquet=\"" + this.bouquetStd + "\";";

         
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                this.id_bouquetStd = reader.GetString(0);
            }

            reader.Close();

          


            //prix std


            command.CommandText = " SELECT prix FROM bouquet_std"
            + " WHERE nom_bouquet=\"" + this.bouquetStd + "\";";

          
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                this.prixstd = reader.GetString(0);
            }
            
            reader.Close();

            Prix_std.Text = this.prixstd+" €";


            //composition

            command.CommandText = " SELECT quantite, id_fleur, id_accessoire  FROM bouquet_std NATURAL JOIN composition"
            + " WHERE nom_bouquet=\"" + this.bouquetStd + "\";";


      
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                this.quantite = reader.GetString(0);
                 this.fleur = reader.GetString(1);
                 this.accessoire = reader.GetString(2);
            }
            
            reader.Close();

            string composition = "";
            if (fleur == "vide")
            {
                composition += this.quantite + " " + this.accessoire;
            }
            else
            {
                composition += this.quantite + " " + this.fleur;
            }


            Composition.Text = composition;


            connection.Close();




        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Categorie_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Prix_std_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Composition_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Description.Text != null)
            {
                this.description = Description.Text;
            }
        }

        private void Prix_perso_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Prix_perso.Text != null && int.TryParse(Prix_perso.Text, out int prix))
            {
                this.prixPerso = Convert.ToString(prix);
            }
        }

        private void ValiderCommande_Click(object sender, RoutedEventArgs e)
        {
            if(this.id_bouquetStd!=null)
            {
                var validerStd = new ValiderStd(this.connectionstring, this.id, this.id_bouquetStd, this.quantite,this.fleur,this.accessoire,this.prixstd);
                validerStd.Show();
                this.Close();

            }
                     
            
        }

        private void ValiderCommandePerso_Click(object sender, RoutedEventArgs e)
        {
            string id_bouquetPerso = null;

            MySqlConnection connection = new MySqlConnection(this.connectionstring);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader reader;

            //trouver le bon id_perso :
            List<string> list_id_perso = new List<string>();
            command.CommandText = " SELECT id_perso FROM bouquet_perso;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                list_id_perso.Add(reader.GetString(0));
            }
            list_id_perso.Sort();
            id_bouquetPerso = list_id_perso[list_id_perso.Count - 2];
            string temp="";
            for (int i=0; i< id_bouquetPerso.Length-1; i++)
            {
                temp += id_bouquetPerso[i];
            }
            temp += Convert.ToChar(Convert.ToInt32(id_bouquetPerso[id_bouquetPerso.Length - 1]) + 1);
            id_bouquetPerso = temp;

            reader.Close();

            if (id_bouquetPerso != null)
            {
                var validerPerso = new ValiderPerso(this.connectionstring, this.id, id_bouquetPerso, this.prixPerso, this.description);
                validerPerso.Show();
                this.Close();

            }
        }
    }
}
