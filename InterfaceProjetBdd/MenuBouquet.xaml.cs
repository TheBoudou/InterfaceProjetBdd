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
    /// Logique d'interaction pour MenuBouquet.xaml
    /// </summary>
    public partial class MenuBouquet : Window
    {
        string connectionstring;
        public MenuBouquet(string connectionstring)
        {
            InitializeComponent();
            this.connectionstring = connectionstring;
        }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            var Menu = new MenuPrincipal(connectionstring);
            Menu.Show();
            this.Close();
        }
    }
}
