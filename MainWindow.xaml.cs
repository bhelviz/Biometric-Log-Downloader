using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Biometric_Log_Downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void aboutClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Biometric Log Downloader v1.0\n© DTG BHEL-HPVP", "About", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
        }

        private void addDeleteReaderClick(object sender, RoutedEventArgs e) {
            AddDeleteReader addDelRdr = new AddDeleteReader();
            string jsonStr = File.ReadAllText("C:\\Biometric_Log_Downloader\\Readers.json");
            JArray jsonArr = JArray.Parse(jsonStr);
            
            foreach (JObject obj in jsonArr)
            {
                //MessageBox.Show((string)obj["IP"]);
                addDelRdr.readerList.Items.Add((string)obj["IP"]+"-"+(string)obj["Description"]);
            }
            addDelRdr.ShowDialog();
        }

        private void databaseConnectionClick(object sender, RoutedEventArgs e)
        {
            DatabaseConnection dbCon = new DatabaseConnection();
            
            string jsonStr = File.ReadAllText("C:\\Biometric_Log_Downloader\\DBConnection.json");
            JObject jsonObj = JObject.Parse(jsonStr);
            dbCon.DBIP.Text = jsonObj["IP"].ToString();
            dbCon.DBPort.Text = jsonObj["Port"].ToString();
            dbCon.DBName.Text = jsonObj["Database"].ToString();
            dbCon.DBUser.Text = jsonObj["Username"].ToString();
            dbCon.DBPass.Password = jsonObj["Password"].ToString();
            dbCon.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Create C:\Biometric_Log_Downloader if not created
            string filepath = @"C:\Biometric_Log_Downloader\";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);

                filepath = @"C:\Biometric_Log_Downloader\Readers.json";
                                
                FileInfo fi = new FileInfo(filepath);

                try
                {
                    // Create a new file
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.Write("[]");
                    }
                  
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }

                filepath = @"C:\Biometric_Log_Downloader\DBConnection.json";

                fi = new FileInfo(filepath);

                try
                {
                    // Create a new file
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.Write("{\"IP\":\"\",\"Port\":\"\",\"Database\":\"\",\"Username\":\"\",\"Password\":\"\"}");
                    }

                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }

                filepath = @"C:\Biometric_Log_Downloader\AutoDownloadTimings.json";

                fi = new FileInfo(filepath);

                try
                {
                    // Create a new file
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.Write("[]");
                    }

                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }
            }
            ReaderListMain.ItemsSource = LoadReaderStatusData();
        }

        public List<ReaderStatus> LoadReaderStatusData()
        {
            int i = 0;
            List<ReaderStatus> readers = new List<ReaderStatus>();

            string jsonStr = File.ReadAllText("C:\\Biometric_Log_Downloader\\Readers.json");
            JArray jsonArr = JArray.Parse(jsonStr);

            foreach (JObject obj in jsonArr)
            {
                 
                readers.Add(new ReaderStatus()
                {
                    SL = ++i,
                    IP = (string)obj["IP"],
                    Description = (string)obj["Description"],
                    Status = "-"
                });
            }

            return readers;
        }

    }

    public class ReaderStatus {
        public int SL { get; set; }
        public string IP { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }

}

