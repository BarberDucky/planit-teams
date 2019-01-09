using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using planit_client_forms.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace planit_client_forms
{
    public partial class Form1 : Form
    {
        MQ.MQService MQService = new MQ.MQService();
        private delegate void SetControlPropertyThreadSafeDel(Control control, string propertyName, string val);

        public Form1()
        {
            InitializeComponent();

        }

        private async void getBoards_Click(object sender, EventArgs e)
        {
            var list = await BoardService.GetAllBoards(1);
            getBoardsTextBox.Text = "";
            foreach (var el in list)
            {
                getBoardsTextBox.Text += el.ToString();
            }
        }

        private async void getBoard_Click(object sender, EventArgs e)
        {
            int userId = 1;
            var board = (await BoardService.GetBoard(boardIdTextbox.Text, userId));
            if(board!=null)
            {
                getBoardTextBox.Text = board.ToString();
                MQService.SubscribeToExchange(board["ExchangeName"].ToString(), (message) => {
                    // var noviString = boardChangesTextbox + message.ToString();
                    // SetControlPropertyThreadSafe(boardChangesTextbox, "Text", message.ToString());
                    AddChanges(boardChangesTextbox, message.ToString());
                    return true;
                });
            }
            else
            {
                MessageBox.Show("No permission for this board!");
            }
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Form1.showMessage("Hello");
        }

        public static void showMessage(string message)
        {
            MessageBox.Show(message);
        }

        private async void updateBoardButton_Click(object sender, EventArgs e)
        {
            string id = updateIdBoardTextbox.Text;
            string newName = updateNameBoardTextbox.Text;
            int UserId = 1;
            await BoardService.PutBoard(id, newName, UserId);
        }

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, string value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDel(SetControlPropertyThreadSafe),
                    new object[] { control, propertyName, control.Text + value });
                
            }
            else
            {
                control.GetType().InvokeMember(propertyName,
                    System.Reflection.BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { control.Text + value }
                    );
            }
            
        }

        private void AddChanges(Control control, string message)
        {
            if(control.InvokeRequired)
            {
                control.BeginInvoke(new MethodInvoker(delegate ()
                {
                    control.Text += $"\n {message}";
                }));
            }
            else
            {
                control.Text += message;
            }
        }

        private async void getAllListsButton_Click(object sender, EventArgs e)
        {
            var list = await CardListService.GetAllCardLists();
            allListsTextBox.Text = "";
            foreach (var el in list)
            {
                allListsTextBox.Text += el.ToString();
            }
        }

        private async void getListButton_Click(object sender, EventArgs e)
        {
            var list = (await CardListService.GetCardList(getListTextBox.Text));
            listTextBox.Text = list.ToString();
        }

        private async void createListButton_Click(object sender, EventArgs e)
        {
            string color = listColorTextBox.Text;
            string name = listNameTextBox.Text;
            string boardId = listParentTextBox.Text;
            await CardListService.PostCardList(name, color, boardId);
        }

        private async void getAllCardsButton_Click(object sender, EventArgs e)
        {
            var list = await CardService.GetAllCards();
            getAllCardsTextBox.Text = "";
            foreach (var el in list)
            {
                getAllCardsTextBox.Text += el.ToString();
            }
        }

        private async void getCardButton_Click(object sender, EventArgs e)
        {
            var list = (await CardService.GetCard(cardIdTextBox.Text));
            cardTextBox.Text = list.ToString();
        }

        private async void createCardTextBox_Click(object sender, EventArgs e)
        {
            string name = cardNameTextBox.Text;
            string description = cardDescTextBox.Text;
            DateTime dueDate = cardDueDatePicker.Value;
            string listId = cardParentIdTextBox.Text;
            string userId = cardUserIdTextBox.Text;
            await CardService.PostCardList(name, description, listId, userId, dueDate);
        }
    }
}
