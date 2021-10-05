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
using Npgsql;
using System.Data;

namespace my_library
{
    /// <summary>
    /// Interaction logic for AddNewBookWindow.xaml
    /// </summary>
    public partial class AddNewBookWindow : Window
    {
        public string sql = null;
        public NpgsqlCommand cmd;
        public NpgsqlConnection conn;
        string connstring = String.Format(@"Server=localhost;Port=5432;User Id=postgres;Password=password;Database=mylib");
        public string bookname;
        public string aufname;
        public string aulname;
        public int page;
        public string publisher;
        public string lang;
        public string genre;
        public string status;
        public string a = "\'";
        public string c = ",";

        public AddNewBookWindow()
        {
            InitializeComponent();
            conn = new NpgsqlConnection(connstring);
            conn.Open();
            sql = "SELECT genre FROM books;";
            cmd = new NpgsqlCommand(sql, conn);
            using(NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string gen = reader.GetString(0);
                    GenreComboBox.Items.Add(gen);
                }
            }
            conn.Close();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = null;
            PageTextBox.Text = null;
            GenreComboBox.SelectedItem = null;
            StatusComboBox.SelectedItem = null;
            FNameTextBox.Text = null;
            SNameTextBox.Text = null;
            PubTextBox.Text = null;
            LangTextBox.Text = null;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            conn.Open();

            bookname = NameTextBox.Text;
            aufname = FNameTextBox.Text;
            aulname = SNameTextBox.Text;
            page = Convert.ToInt32(PageTextBox.Text);
            publisher = PubTextBox.Text;
            lang = LangTextBox.Text;
            status = StatusComboBox.SelectedItem.ToString();
            genre = GenreComboBox.SelectedItem.ToString();
            sql = "INSERT INTO books(bookname,au_fname,au_lname,page,publisher,lang,genre,status) VALUES("+a+bookname+a+c+a+aufname+a+c+a+aulname+a+c+a+page+a+c+a+publisher+a+c+a+lang+a+c+a+genre+a+c+a+status+a+");";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("bookname", bookname);
            cmd.Parameters.AddWithValue("au_fname", aufname);
            cmd.Parameters.AddWithValue("au_lname", aulname);
            cmd.Parameters.AddWithValue("page", page);
            cmd.Parameters.AddWithValue("publisher", publisher);
            cmd.Parameters.AddWithValue("lang", lang);
            cmd.Parameters.AddWithValue("genre", genre);
            cmd.Parameters.AddWithValue("status", status);
            cmd.ExecuteScalar();
            cmd.CommandText = sql;
            MessageBox.Show(bookname + " has been successfully saved", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            conn.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
