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

            sql = "SELECT * FROM books WHERE book_id="+bookid;
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                }
            }

        }
    }
}
