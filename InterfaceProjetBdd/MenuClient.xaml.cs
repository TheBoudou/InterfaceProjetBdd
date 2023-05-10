using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
    /// Logique d'interaction pour MenuClient.xaml
    /// </summary>
    public partial class MenuClient : Window
    {
        MySqlConnection Connexion;
        string connectionstring;
        string commandesql;
        string éléments;
        string requete;
        string where;
        string élémentwhere;
        public MenuClient(string connectionstring)
        {
            InitializeComponent();
            this.commandesql = "Select ";
            this.éléments = " * ";
            this.requete = this.commandesql + this.éléments + " from clients ";
            this.where = "id_client = ";
            this.élémentwhere = "";
            FillGrid(connectionstring,this.requete);
            AffichageComboBox1.Text = this.éléments;
            //this.Connexion = Connexion;
            
            this.connectionstring = connectionstring;
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuPrincipal(connectionstring);
            Menu.Show();
            this.Close();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        public void FillGrid(string connectionstring,string commande)
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
            catch(Exception ex)
            {

            }
            connection.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BoutonSelect_Click(object sender, RoutedEventArgs e)
        {
            this.commandesql = "Select ";
            this.requete = this.commandesql + this.éléments + " from clients ";
            FillGrid(connectionstring, requete);
        }
        private void Element1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedelement = ((ComboBoxItem)Element1.SelectedItem).Content as string;
            if (selectedelement != null)
            {
                AffichageComboBox1.Text = selectedelement;
            }
            if (selectedelement != "*" && selectedelement != " *" && selectedelement != " * ")
            {
                this.éléments = "id_client," + selectedelement + " ";
            }
            else
            {
                this.éléments = selectedelement+" ";
            }
            this.requete = this.commandesql + this.éléments + " from clients ";
            //FillGrid(connectionstring, this.requete);

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void BoutonSelectWhere_Click(object sender, RoutedEventArgs e)
        {
            this.requete = "Select " + this.éléments + " from clients where " + where + élémentwhere;
            FillGrid(connectionstring, requete);
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }

        private void Element2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedelement = ((ComboBoxItem)Element2.SelectedItem).Content as string;
            if (selectedelement != null)
            {
                AffichageComboBox2.Text = selectedelement;
            }
            if (selectedelement != AffichageComboBox1.Text && selectedelement != "")
            {
                if (AffichageComboBox1.Text != "*" && AffichageComboBox1.Text != " *" && AffichageComboBox1.Text != " * ")
                {
                    this.éléments = "id_client," + AffichageComboBox1.Text + ", " + selectedelement + " ";
                    this.requete = this.commandesql + this.éléments + " from clients ";
                }
               
                
            }
            
            //FillGrid(connectionstring, this.requete);
        }

        private void Element3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedelement = ((ComboBoxItem)Element3where.SelectedItem).Content as string;
            if (selectedelement != null)
            {
                AffichageComboBox3.Text = selectedelement;
                this.where = selectedelement + " = ";
            }
            
        }

        private void TextBox_TextChanged_4(object sender, TextChangedEventArgs e)
        {
            if (AffichageComboBox3.Text != null)
            {
                string text = AffichageComboBox3.Text;

            }
        }

        private void EntreeWhere_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EntreeWhere.Text != null)
            {
                string text = EntreeWhere.Text;
                this.élémentwhere = "\""+text+"\"";

            }
        }

        private void BoutonAjout_Click(object sender, RoutedEventArgs e)
        {
            var ajout = new Ajout_Client(connectionstring);
            ajout.Show();
            this.Close();
        }

        private void BoutonSuppr_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new SuppressionClient(connectionstring);
            Menu.Show();
            this.Close();
        }
    }
}
