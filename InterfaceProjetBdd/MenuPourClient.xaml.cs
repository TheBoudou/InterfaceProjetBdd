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
    /// Logique d'interaction pour MenuPourClient.xaml
    /// </summary>
    public partial class MenuPourClient : Window
    {
        string connectionstring;
        string id;
        public MenuPourClient(string connectionstring,string ID_client)
        {
            InitializeComponent();
            this.connectionstring = connectionstring;
            this.id = ID_client;
        }

        private void Deconnexion_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MainWindow();
            Menu.Show();
            this.Close();
        }
    }
}
