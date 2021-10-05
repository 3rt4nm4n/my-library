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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace my_library
{
    /// <summary>
    /// Interaction logic for UpdateBookWindow.xaml
    /// </summary>
    public partial class UpdateBookWindow : Window
    {

        public string sql = null;
        public NpgsqlCommand cmd;
        public NpgsqlConnection conn;
        public NpgsqlDataReader reader;
        string connstring = String.Format(@"Server=localhost;Port=5432;User Id=postgres;Password=password;Database=mylib");
        public int bookid;
        public UpdateBookWindow()
        {
            InitializeComponent();
            conn = new NpgsqlConnection(connstring);
            sql = "SELECT * FROM books WHERE book_id="+bookid;
            conn.Open();
            cmd = new NpgsqlCommand(sql, conn);
            
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string gen = reader.GetString(0);
                    GenreComboBox.Items.Add(gen);
                }
            }
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    NameTextBox.Text = reader.GetString(1);
                    FNameTextBox.Text = reader.GetString(2);
                    SNameTextBox.Text = reader.GetString(3);
                    PageTextBox.Text = reader.GetString(4);
                    PubTextBox.Text = reader.GetString(5);
                    LangTextBox.Text = reader.GetString(6);
                    GenreComboBox.SelectedItem = reader.GetString(7);
                    StatusComboBox.SelectedItem = reader.GetString(8);
                }
            }
            conn.Close();
        }
    }
}
