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
    /// Logique d'interaction pour Ajout_Client.xaml
    /// </summary>
    public partial class Ajout_Client : Window
    {
        string connectionstring;
        public Ajout_Client(string connectionstring)
        {
            InitializeComponent();
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuClient(connectionstring);
            Menu.Show();
            this.Close();
        }
    }
}
