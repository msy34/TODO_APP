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
using System.Windows.Shapes;
using TODO.Domain;
using TODO.FRM;
using TODO.Helpers;

namespace TODO
{
    /// <summary>
    /// Interaction logic for BoardCard.xaml
    /// </summary>
    public partial class BoardCards : Window
    {
        public BoardCards()
        {
            InitializeComponent();
            statusCombobox.ItemsSource = Enum.GetValues(typeof(Status));
            FillBoardCard();
        }
        private  readonly ToDoContext _dbContext=new ToDoContext();
        CollectionViewSource boardViewSource;
        
        private void boardDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataRowView item = e.Row.Item as DataRowView;
            if (item != null)
            {

                DataRow row = item.Row;
                if (item["Status"] != DBNull.Value)
                {
                    e.Row.Item = ((Status)item["Status"]).ToString();

                }
            }
        }
        private void btnfiltre_Click(object sender, RoutedEventArgs e)
        {
            var query = _dbContext.Boards.Where(x => x.UserId == Globals.LoggedInUser.Id);
            if (!string.IsNullOrEmpty(searchName.Text))
            {
                query = query.Where(x => x.Name.Contains(searchName.Text));
            }
            if (!string.IsNullOrEmpty(searchDescription.Text))
            {
                query = query.Where(x => x.Description.Contains(searchDescription.Text));
            }
            if (statusCombobox.SelectedValue != null)
            {

                query = query.Where(x => x.Status==(Status)statusCombobox.SelectedValue);
            }
            this.boardDataGrid.ItemsSource = query.ToList();
        }
        private void FillBoardCard()
        {
            var datas= _dbContext.Boards.Where(x => x.UserId == Globals.LoggedInUser.Id);
            //TODO.ToDoDataSet toDoDataSet = ((TODO.ToDoDataSet) (this.FindResource("toDoDataSet")));
            //var boardCards = Enumerable.Where(toDoDataSet.BoardCards, x => x.UserId == Globals.LoggedInUser.Id);
            //var dt = datas  as ToDoDataSet.BoardCardsDataTable;
            //// Load data into the table BoardCards. You can modify this code as needed.
            //TODO.ToDoDataSetTableAdapters.BoardCardsTableAdapter toDoDataSetBoardCardsTableAdapter =
            //    new TODO.ToDoDataSetTableAdapters.BoardCardsTableAdapter();
            //toDoDataSetBoardCardsTableAdapter.Fill(toDoDataSet.BoardCards);
            //System.Windows.Data.CollectionViewSource boardCardsViewSource =
            //    ((System.Windows.Data.CollectionViewSource) (this.FindResource("boardCardsViewSource")));
            //boardCardsViewSource.View.MoveCurrentToFirst();
            this.boardDataGrid.ItemsSource = datas.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //object item = boardDataGrid.SelectedItem;

            //int id = int.Parse((boardDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            //_dbContext.Boards.Remove(_dbContext.Boards.FirstOrDefault(x => x.Id == id));
            //_dbContext.SaveChanges();
            CreateBoardCard createBoardCard = new CreateBoardCard();
            createBoardCard.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            object item = boardDataGrid.SelectedItem;

            int id = int.Parse((boardDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            var card = _dbContext.Boards.FirstOrDefault(x => x.Id == id);
            _dbContext.Boards.Remove(card);
            _dbContext.SaveChanges();
            FillBoardCard();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isCreate=false;
                object item = boardDataGrid.SelectedItem;

                int id = int.Parse((boardDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

                BoardCard boardCard = _dbContext.Boards.FirstOrDefault(x => x.Id == id);
                if (boardCard == null)
                {
                    isCreate = true;
                    boardCard =new BoardCard{UserId = Globals.LoggedInUser.Id};
                    boardCard.Deadline=DateTime.Now;
                    boardCard.CreatedDate=DateTime.Now;
                    boardCard.UpdatedDate=DateTime.Now;
                    
                }
                boardCard.Name = (boardDataGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                boardCard.Description = (boardDataGrid.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                if (isCreate)
                {
                    _dbContext.Boards.Add(boardCard);
                }
                _dbContext.SaveChanges();
                MessageBox.Show("Row Updated Successfully");
                this.boardDataGrid.Items.Clear();
                FillBoardCard();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

    }
}
