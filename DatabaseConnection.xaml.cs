using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biometric_Log_Downloader
{
    /// <summary>
    /// Interaction logic for DatabaseConnection.xaml
    /// </summary>
    public partial class DatabaseConnection : Window
    {
        public DatabaseConnection()
        {
            InitializeComponent();
        }
        private void saveDBClick(object sender, RoutedEventArgs e)
        {
            dynamic jsonDBCon = new JObject();
            
            jsonDBCon.IP = DBIP.Text;
            jsonDBCon.Port = DBPort.Text;
            jsonDBCon.Database = DBName.Text;
            jsonDBCon.Username = DBUser.Text;
            jsonDBCon.Password = DBPass.Password;

            File.WriteAllText("C:\\Biometric_Log_Downloader\\DBConnection.json", jsonDBCon.ToString());

            MessageBox.Show("Database Connection Details Saved", "Save", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
        }
    }
}
