using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Biometric_Log_Downloader
{
    /// <summary>
    /// Interaction logic for AddDeleteReader.xaml
    /// </summary>
    public partial class AddDeleteReader : Window
    {
        public AddDeleteReader()
        {
            InitializeComponent();
        }

        private void readerAddClick(object sender, RoutedEventArgs e)
        {
            if (readerIP.Text.Trim() == "" || readerDescription.Text.Trim() == "")
            {
                MessageBox.Show("Reader IP/Description can't be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes);
            }
            else
            {
                readerList.Items.Add(readerIP.Text + "-" + readerDescription.Text);

                //Write to File
                JArray jArray = new JArray();
                
                foreach (var item in readerList.Items)
                {
                    JObject jObject = new JObject();
                    jObject.Add(new JProperty("IP", item.ToString().Split("-")[0]));
                    jObject.Add(new JProperty("Description", item.ToString().Split("-")[1]));
                    jArray.Add(jObject);
                }
                var jsonString = JsonConvert.SerializeObject(jArray, Formatting.Indented);

                File.WriteAllText("C:\\Biometric_Log_Downloader\\Readers.json", jsonString);

                ((MainWindow)System.Windows.Application.Current.MainWindow).ReaderListMain.ItemsSource = ((MainWindow)System.Windows.Application.Current.MainWindow).LoadReaderStatusData();
            }
        }

        private void readerDeleteClick(object sender, RoutedEventArgs e)
        {
            if (readerList.SelectedIndex == -1)
            {
                MessageBox.Show("Select an Item first", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes);
            }
            else
            {
                readerList.Items.RemoveAt(readerList.Items.IndexOf(readerList.SelectedItem));

                //Write to File
                //Write to File
                JArray jArray = new JArray();

                foreach (var item in readerList.Items)
                {
                    JObject jObject = new JObject();
                    jObject.Add(new JProperty("IP", item.ToString().Split("-")[0]));
                    jObject.Add(new JProperty("Description", item.ToString().Split("-")[1]));
                    jArray.Add(jObject);
                }
                var jsonString = JsonConvert.SerializeObject(jArray, Formatting.Indented);

                File.WriteAllText("C:\\Biometric_Log_Downloader\\Readers.json", jsonString);

                ((MainWindow)System.Windows.Application.Current.MainWindow).ReaderListMain.ItemsSource = ((MainWindow)System.Windows.Application.Current.MainWindow).LoadReaderStatusData();
            }

        }

    }
}
