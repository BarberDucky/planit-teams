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
    }
}
