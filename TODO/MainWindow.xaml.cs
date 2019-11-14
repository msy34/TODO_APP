using System.Linq;
using System.Windows;
using TODO.Domain;
using TODO.FRM;
using TODO.Helpers;

namespace TODO
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

        private readonly ToDoContext dbContext = new ToDoContext();
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == txtEmail.Text && x.Password == password.Password);

            if (user == null)
            {
                MessageBox.Show("Email veya şifre hatalı");
            }

            Globals.LoggedInUser = user;
            BoardCards boardCards = new BoardCards();
            boardCards.Show();
            Close();
        }
        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            Close();
        }
    }
}
