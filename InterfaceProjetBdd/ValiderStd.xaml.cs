using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls.Crypto;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour ValiderStd.xaml
    /// </summary>
    public partial class ValiderStd : Window
    {
        string connectionstring;
        string id;
        string num_commande;
        string date_commande;
        string adresse_livraison;
        string message;
        string date_livraison;
        string etat_commande;
        string id_bouquet;
        string nom_magasin;
        string id_magasin;
        string prix;
        string statut;

        string quantite;
        string fleur;
        string accessoire;

        string connectionstringroot = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";

        public ValiderStd(string connectionstring, string id, string id_bouquet, string quantite, string fleur, string accessoire,string prixstd)
        {
            InitializeComponent();
            this.connectionstring = connectionstring;
            this.id = id;
            this.id_bouquet = id_bouquet;
            
            this.quantite = quantite;
            this.fleur = fleur;
            this.accessoire = accessoire;
            this.prix = prixstd;

            ObjectChange();
        }

        private void W_adresse_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (W_adresse.Text != null)
            {
                this.adresse_livraison = W_adresse.Text;
            }
        }

        private void W_message_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (W_message.Text != null)
            {
                this.message = W_message.Text;
            }
        }
        private void ObjectChange()
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstringroot);
            connection.Open();

            // Effacer les éléments existants de la combobox
            Element1.Items.Clear();

            // Créer une commande SQL pour récupérer les données à partir de la base de données
            MySqlCommand command = new MySqlCommand("SELECT nom_magasin FROM magasin;", connection);

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

        private void Element1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Element1.SelectedItem != null)
            {
                this.nom_magasin = Element1.SelectedItem.ToString();
                Element1.Text = this.nom_magasin;


                MySqlConnection connection = new MySqlConnection(this.connectionstringroot);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = " SELECT id_magasin FROM magasin"
                + " WHERE nom_magasin=\"" + this.nom_magasin + "\";";

                MySqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    this.id_magasin = reader.GetString(0);
                }

                reader.Close();
            

            }
        }

        private void Confirmer_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(this.connectionstringroot);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader reader;

            //num commande 
            List<int> num_commande = new List<int>();
            command.CommandText = " SELECT num_commande FROM commande;";
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                num_commande.Add(Convert.ToInt32(reader.GetString(0)));
            }
            num_commande.Sort();
            this.num_commande = Convert.ToString(num_commande[num_commande.Count - 1]+1);
            reader.Close();

            // date commande :
           this.date_commande = DateTime.Now.ToString("yyyy-MM-dd");

           

            //date_livraison
            DateTime? selectedDate = Date.SelectedDate;
            if (selectedDate.HasValue)
            {
               
                this.date_livraison = selectedDate.Value.ToString("yyyy-MM-dd");
                
            }


            //stock gestion + etat commande
            int quantiteStock = 0;

            if(this.fleur==null)
            {
                command.CommandText = " SELECT quantite FROM stockfleur WHERE id_fleur='"+this.fleur+"' AND id_magasin='"+this.id_magasin+"';";
            }
            else
            {
                command.CommandText = " SELECT quantite FROM stockaccessoire WHERE id_accessoire='" + this.accessoire + "' AND id_magasin='" + this.id_magasin + "';";
            }
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                quantiteStock=reader.GetInt32(0);
            }
            reader.Close();

            quantiteStock -= Convert.ToInt32(this.quantite);
            if (quantiteStock<0)
            {
                this.etat_commande = "VINV";
            }
            else
            {
                this.etat_commande = "CC";
            }





            //check statut update + prix en fonction de statut
            int nb_commandes_mois=0;
            command.CommandText = " SELECT nb_commandes_mois FROM clients WHERE id_client='"+this.id+"';";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                nb_commandes_mois=reader.GetInt32(0);
            }
            reader.Close ();

            if(nb_commandes_mois>5)
            {
                this.statut = "OR";
                this.prix=Convert.ToString(0.85*Convert.ToInt32(this.prix));
            }
            else
            {
                if(nb_commandes_mois>1)
                { this.statut = "BRONZE";
                  this.prix = Convert.ToString(0.95 * Convert.ToInt32(this.prix));
                }
                else { this.statut = "vide"; }
            }

            command.CommandText = "UPDATE clients SET statut = '" + this.statut+"' WHERE id_client = '"+this.id+"';";
            command.ExecuteNonQuery();



            //achat dans 
            //command.CommandText = " INSERT INTO achat_dans (id_client, id_magasin) VALUES ('"+this.id+"', '"+this.id_magasin+"');";
            //command.ExecuteNonQuery();


            // final
            //stock
            if (this.fleur == null)
            {
                command.CommandText = "UPDATE stockaccessoire SET quantite = " + quantiteStock + " WHERE id_accessoire = '" + this.accessoire + "' AND id_magasin = '" + this.id_magasin + "';";
            }
            else
            {
                command.CommandText = "UPDATE stockfleur SET quantite = " + quantiteStock + " WHERE id_fleur = '" + this.fleur + "' AND id_magasin = '" + this.id_magasin + "';";
            }
            
            command.ExecuteNonQuery();

            //insertion commande
            command.CommandText = "INSERT INTO `fleurs`.`commande` (`num_commande`,`date_commande`,`adresse_livraison`,`message`,`date_livraison`,`etat_commande`,`id_client`,`id_perso`,`id_bouquet`,`prix_commande`)"
            +"VALUES ('"+this.num_commande+ "', '"+this.date_commande+ "', '"+this.adresse_livraison+ "', '"+this.message+ "', '"+this.date_livraison+ "', '"+this.etat_commande+ "', '"+this.id+ "', 'vide', '"+this.id_bouquet+ "', '"+this.prix+"');";
            command.ExecuteNonQuery();

            //incrementer commande et commande mois

            command.CommandText = "UPDATE clients SET nb_commandes = nb_commandes + 1 WHERE id_client='"+this.id+"';";
            command.ExecuteNonQuery();

            command.CommandText = "UPDATE clients SET nb_commandes_mois = nb_commandes_mois + 1 WHERE id_client='" + this.id + "';";
            command.ExecuteNonQuery();

            //augmenter CA du magasin
            command.CommandText = "UPDATE magasin SET chiffre_affaires = chiffre_affaires + " + this.prix + " WHERE id_magasin='" + this.id_magasin + "';";
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Commande validée", "Order Validation", MessageBoxButton.OK, MessageBoxImage.Information);

            var PourClientCommande = new PourClientCommandes(connectionstring, id);
            PourClientCommande.Show();
            this.Close();
        }
    }
}
