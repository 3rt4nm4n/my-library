using System;
using System.Collections.Generic;
using System.Data;
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
using Npgsql;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace my_library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        private string sql = null;
        string connstring = String.Format(@"Server=localhost;Port=5432;User Id=postgres;Password=password;Database=mylib");
        private DataTable dt;
        public string selected = null;
        public bool check=false;
        private readonly ICollection<Books> item;
        private Books selecteditem;
       
        public void Init(string s)
        {
            conn.Open();
            cmd = new NpgsqlCommand(s, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            BooksDataGrid.DataContext = dt.DefaultView;
            DataContext = this;
            conn.Close();
        }

        public class Books
        {
            public string Name
            {
                get; set;
            }
            
        }

        public MainWindow()
        {
            InitializeComponent();
            this.item = new ObservableCollection<Books>();
            
            conn = new NpgsqlConnection(connstring);
            try {
                
                sql = "SELECT bookname,genre, page, status FROM books ORDER BY book_id ASC;";
                Init(sql);
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }
        }

        private void AddNewBookButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewBookWindow anbw = new AddNewBookWindow();
            anbw.Show();
            this.Close();
        }

        private void ReadBooksButton_Click(object sender, RoutedEventArgs e)
        {
            sql = "SELECT bookname, genre, page, status FROM books WHERE status=\'Read\' ORDER BY book_id ASC;";
            Init(sql);

        }
        private void ReadingBooksButton_Click(object sender, RoutedEventArgs e)
        {
            sql = "SELECT bookname,genre,page,status FROM books WHERE status=\'Reading\' ORDER BY book_id ASC;";
            Init(sql);
        }

        private void UnreadBooksButton_Click(object sender, RoutedEventArgs e)
        {
            sql = "SELECT bookname, genre, page, status FROM books WHERE status=\'Unread\' ORDER BY book_id ASC;";
            Init(sql);
        }

        private void UpdateBookButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateBookWindow ubw = new UpdateBookWindow();
            ubw.Show();
            this.Close();
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BooksDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BookDetailsWindow bdw = new BookDetailsWindow();
            bdw.Show();
        }

        private void BooksDataGrid_Selected(object sender, RoutedEventArgs e)
        {
            
            selected=BooksDataGrid.SelectedItem.ToString();

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myGrid.Width = e.NewSize.Width;
            myGrid.Height = e.NewSize.Height;

            double xc = 1, yc = 1; //xc = changed width referring to x axis, yc = changed width referring to y axis

            if (e.PreviousSize.Width != 0 && myGrid.Width > 500)

                xc = (e.NewSize.Width / e.PreviousSize.Width);

                
            if (e.PreviousSize.Height != 0 && myGrid.Height > 450)

                yc = (e.NewSize.Height / e.PreviousSize.Height);
                

                

            ScaleTransform scale = new ScaleTransform(myGrid.LayoutTransform.Value.M11 * xc, myGrid.LayoutTransform.Value.M22 * yc);
            myGrid.LayoutTransform = scale;
            myGrid.UpdateLayout();
            
        }

        
    }
}
