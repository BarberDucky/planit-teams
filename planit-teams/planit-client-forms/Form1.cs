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

        public Form1()
        {
            InitializeComponent();

        }

        private async void getBoards_Click(object sender, EventArgs e)
        {
            var list = await BoardService.GetAllBoards();
            getBoardsTextBox.Text = "";
            foreach (var el in list)
            {
                getBoardsTextBox.Text += el.ToString();
            }
        }

        private async void getBoard_Click(object sender, EventArgs e)
        {
            var board = (await BoardService.GetBoard(boardIdTextbox.Text));
            getBoardTextBox.Text = board.ToString();
            MQService.SubscribeToExchange(board["ExchangeName"].ToString(), (message) => {
                MessageBox.Show(message.ToString());
                return true;
            });
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
            await BoardService.PutBoard(id, newName);
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
