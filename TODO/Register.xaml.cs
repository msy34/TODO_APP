using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TODO.Domain;
using TODO.FRM;

namespace TODO
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private readonly ToDoContext dbContext = new ToDoContext();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TxtEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                TxtEmail.Focus();
            }
            else if (!Regex.IsMatch(TxtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                TxtEmail.Select(0, TxtEmail.Text.Length);
                TxtEmail.Focus();
            }
            else
            {
                string firstname = TxtName.Text;
                string lastname = TxtSurname.Text;
                string email = TxtEmail.Text;
                string password = Password.Password;
                if (Password.Password.Length == 0)
                {
                    errormessage.Text = "Enter password.";
                    Password.Focus();
                }
                else if (PasswordRecover.Password.Length == 0)
                {
                    errormessage.Text = "Enter Confirm password.";
                    PasswordRecover.Focus();
                }
                else if (Password.Password != PasswordRecover.Password)
                {
                    errormessage.Text = "Confirm password must be same as password.";
                    PasswordRecover.Focus();
                }
                else
                {
                    var user = new User
                    {
                        Email = email,
                        Password = password,
                        CreatedDate = DateTime.Now,
                        Name = firstname,
                        Surname = lastname,
                        UpdatedDate = DateTime.Now
                    };
                    errormessage.Text = "";
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
            }
        }

    }
}
