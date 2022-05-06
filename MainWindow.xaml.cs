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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FireSharp.Config;
using FireSharp;
using FireSharp.Response;

namespace WpfApp1
{
    public class UserData
    {
        public string Age { get; set; }
        public string Name { get; set; }
        public string Faculty { get; set; }
        public string Married { get; set; }

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int id { get; set; }

        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = Connection.key,
            BasePath = Connection.db
        };

        FirebaseClient client;

        public MainWindow()
        {
            InitializeComponent();
            client = new FirebaseClient(config);
            //majd kell max id, mert annak hiányában még nem jó
            //id = 10;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Soooon....");
            // Collection Name: TestData
            // Document (table) name: PushData


            try
            {
                // set data
                await client.SetAsync("TestData/TestCRUD/data1", textBox1.Text);
                await client.SetAsync("TestData/TestCRUD/data2", textBox2.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void btn_document_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // set document
                await client.PushAsync("TestData/PushData", textBox1.Text);
                MessageBox.Show("OK");

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void btn_query_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // query from firebase
                FirebaseResponse response = await client.GetAsync("TestData/TestCRUD/data1");
                string st = response.ResultAs<string>();
                MessageBox.Show(st);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void btn_UserData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // insert userdata  to firebase
                UserData userData = new UserData { Age = textBox1.Text, Faculty = textBox2.Text, Married = textBox3.Text, Name = textBox4.Text };
                await client.SetAsync("TestData/UserData/vmi" + id, userData);
                id += 1;

                MessageBox.Show("Done\n" + id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void btn_getObject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = await client.GetAsync("TestData/UserData/vmi");
                UserData userData = response.ResultAs<UserData>();
                MessageBox.Show($"{userData.Name}\n{userData.Faculty}\n{userData.Age}\n");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
