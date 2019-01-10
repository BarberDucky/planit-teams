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
using planit_client_forms.DTOs;

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
            int userId = int.Parse(userIdTextBox.Text);

            var list = await BoardService.GetAllBoards(userId);
            getBoardsTextBox.Text = "";
            foreach (var el in list)
            {
                getBoardsTextBox.Text += el.ToString();
            }
        }

        private async void getBoard_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(userIdTextBox.Text);
            ReadBoardDTO board = (await BoardService.GetBoard(boardIdTextbox.Text, userId));
            if (board != null)
            {
                getBoardTextBox.Text = board.ToString();
                MQService.SubscribeToExchange(board.ExchangeName, (message) =>
                {
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
            int UserId = int.Parse(userIdTextBox.Text);

            UpdateBoardDTO updateData = new UpdateBoardDTO()
            {
                Name = newName,
                BoardId = int.Parse(id)
            };


            await BoardService.PutBoard(updateData, UserId);
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
            if (control.InvokeRequired)
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
            int userId = int.Parse(userIdTextBox.Text);

            var list = await CardListService.GetAllCardLists(boardIdTextbox.Text, userId);
            allListsTextBox.Text = "";
            foreach (var el in list)
            {
                allListsTextBox.Text += el.ToString();
            }
        }

        private async void getListButton_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(userIdTextBox.Text);

            var list = await CardListService.GetCardList(getListTextBox.Text, userId);
            if (list != null)
            {
                listTextBox.Text = list.ToString();
            }

        }

        private async void createListButton_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(userIdTextBox.Text);

            string color = listColorTextBox.Text;
            string name = listNameTextBox.Text;
            string boardId = listParentTextBox.Text;

            CreateCardListDTO createData = new CreateCardListDTO()
            {
                Name = name,
                Color = color,
                BoardId = int.Parse(boardId)
            };

            await CardListService.PostCardList(createData, userId);
        }

        private async void getAllCardsButton_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(userIdTextBox.Text);

            var list = await CardService.GetAllCards(boardIdTextbox.Text, userId);
            getAllCardsTextBox.Text = "";
            foreach (var el in list)
            {
                getAllCardsTextBox.Text += el.ToString();
            }
        }

        private async void getCardButton_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(userIdTextBox.Text);

            var list = await CardService.GetCard(cardIdTextBox.Text, userId);

            if (list != null)
            {
                cardTextBox.Text = list.ToString();
            }

        }

        private async void createCardTextBox_Click(object sender, EventArgs e)
        {
            string name = cardNameTextBox.Text;
            string description = cardDescTextBox.Text;
            DateTime dueDate = cardDueDatePicker.Value;
            string listId = cardParentIdTextBox.Text;

            int userId = int.Parse(userIdTextBox.Text);


            CreateCardDTO createData = new CreateCardDTO()
            {
                Name = name,
                Description = description,
                ListId = int.Parse(listId),
                UserId = userId,
                DueDate = dueDate
            };

            await CardService.PostCard(createData, userId);
        }
    }
}
