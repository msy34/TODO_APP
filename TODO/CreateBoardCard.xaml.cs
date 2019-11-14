using System;
using System.Collections.Generic;
using System.Data;
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
using TODO.Helpers;

namespace TODO
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CreateBoardCard : Window
    {
        public CreateBoardCard()
        {
            InitializeComponent();
            cmbStatus.ItemsSource = Enum.GetValues(typeof(Status));
        }
        private readonly ToDoContext _dbContext = new ToDoContext();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TxtName.Text.Length == 0)
            {
                errormessage.Text = "Enter an naeme.";
                TxtName.Focus();
            }
            else if (TxtDescription.Text.Length == 0)
            {
                errormessage.Text = "Enter an description.";
                TxtDescription.Focus();
            }
            else
            {
                string name = TxtName.Text;
                string description = TxtDescription.Text;
                var status = cmbStatus.SelectedValue;
                var deadline = dtdeadline.DisplayDate;

                var boardCard = new BoardCard
                {
                    Description = description,
                    Name = name,
                    Status = (Status) status,
                    Deadline = deadline,
                    UserId = Globals.LoggedInUser.Id,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                errormessage.Text = "";
                _dbContext.Boards.Add(boardCard);
                _dbContext.SaveChanges();
                MessageBox.Show("Added Successfully");
                BoardCards boardCards = new BoardCards();
                boardCards.Show();
                this.Close();
            }
        }

    }
}
