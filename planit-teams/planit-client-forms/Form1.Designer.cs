namespace planit_client_forms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.getBoards = new System.Windows.Forms.Button();
            this.getBoardsTextBox = new System.Windows.Forms.RichTextBox();
            this.getBoard = new System.Windows.Forms.Button();
            this.boardIdTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.getBoardTextBox = new System.Windows.Forms.RichTextBox();
            this.boardChangesTextbox = new System.Windows.Forms.RichTextBox();
            this.updateBoardButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.updateIdBoardTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.updateNameBoardTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listColorTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listNameTextBox = new System.Windows.Forms.TextBox();
            this.createListButton = new System.Windows.Forms.Button();
            this.listTextBox = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.getListTextBox = new System.Windows.Forms.TextBox();
            this.getListButton = new System.Windows.Forms.Button();
            this.allListsTextBox = new System.Windows.Forms.RichTextBox();
            this.getAllListsButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.listParentTextBox = new System.Windows.Forms.TextBox();
            this.labelWhatever = new System.Windows.Forms.Label();
            this.cardParentIdTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cardDescTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cardNameTextBox = new System.Windows.Forms.TextBox();
            this.createCardTextBox = new System.Windows.Forms.Button();
            this.cardTextBox = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cardIdTextBox = new System.Windows.Forms.TextBox();
            this.getCardButton = new System.Windows.Forms.Button();
            this.getAllCardsTextBox = new System.Windows.Forms.RichTextBox();
            this.getAllCardsButton = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.cardDueDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.userIdTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // getBoards
            // 
            this.getBoards.Location = new System.Drawing.Point(12, 12);
            this.getBoards.Name = "getBoards";
            this.getBoards.Size = new System.Drawing.Size(253, 44);
            this.getBoards.TabIndex = 0;
            this.getBoards.Text = "Get all boards";
            this.getBoards.UseVisualStyleBackColor = true;
            this.getBoards.Click += new System.EventHandler(this.getBoards_Click);
            // 
            // getBoardsTextBox
            // 
            this.getBoardsTextBox.Location = new System.Drawing.Point(12, 62);
            this.getBoardsTextBox.Name = "getBoardsTextBox";
            this.getBoardsTextBox.Size = new System.Drawing.Size(253, 344);
            this.getBoardsTextBox.TabIndex = 1;
            this.getBoardsTextBox.Text = "";
            // 
            // getBoard
            // 
            this.getBoard.Location = new System.Drawing.Point(271, 12);
            this.getBoard.Name = "getBoard";
            this.getBoard.Size = new System.Drawing.Size(226, 44);
            this.getBoard.TabIndex = 2;
            this.getBoard.Text = "Get board";
            this.getBoard.UseVisualStyleBackColor = true;
            this.getBoard.Click += new System.EventHandler(this.getBoard_Click);
            // 
            // boardIdTextbox
            // 
            this.boardIdTextbox.Location = new System.Drawing.Point(272, 80);
            this.boardIdTextbox.Name = "boardIdTextbox";
            this.boardIdTextbox.Size = new System.Drawing.Size(226, 22);
            this.boardIdTextbox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(271, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Board Id:";
            // 
            // getBoardTextBox
            // 
            this.getBoardTextBox.Location = new System.Drawing.Point(274, 129);
            this.getBoardTextBox.Name = "getBoardTextBox";
            this.getBoardTextBox.Size = new System.Drawing.Size(223, 277);
            this.getBoardTextBox.TabIndex = 5;
            this.getBoardTextBox.Text = "";
            // 
            // boardChangesTextbox
            // 
            this.boardChangesTextbox.Location = new System.Drawing.Point(775, 435);
            this.boardChangesTextbox.Name = "boardChangesTextbox";
            this.boardChangesTextbox.Size = new System.Drawing.Size(376, 306);
            this.boardChangesTextbox.TabIndex = 6;
            this.boardChangesTextbox.Text = "";
            // 
            // updateBoardButton
            // 
            this.updateBoardButton.Location = new System.Drawing.Point(503, 12);
            this.updateBoardButton.Name = "updateBoardButton";
            this.updateBoardButton.Size = new System.Drawing.Size(235, 44);
            this.updateBoardButton.TabIndex = 7;
            this.updateBoardButton.Text = "Update Board";
            this.updateBoardButton.UseVisualStyleBackColor = true;
            this.updateBoardButton.Click += new System.EventHandler(this.updateBoardButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(502, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Board Id:";
            // 
            // updateIdBoardTextbox
            // 
            this.updateIdBoardTextbox.Location = new System.Drawing.Point(503, 104);
            this.updateIdBoardTextbox.Name = "updateIdBoardTextbox";
            this.updateIdBoardTextbox.Size = new System.Drawing.Size(226, 22);
            this.updateIdBoardTextbox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(502, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "New Board Name:";
            // 
            // updateNameBoardTextbox
            // 
            this.updateNameBoardTextbox.Location = new System.Drawing.Point(503, 153);
            this.updateNameBoardTextbox.Name = "updateNameBoardTextbox";
            this.updateNameBoardTextbox.Size = new System.Drawing.Size(226, 22);
            this.updateNameBoardTextbox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(506, 523);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "List Color:";
            // 
            // listColorTextBox
            // 
            this.listColorTextBox.Location = new System.Drawing.Point(507, 543);
            this.listColorTextBox.Name = "listColorTextBox";
            this.listColorTextBox.Size = new System.Drawing.Size(226, 22);
            this.listColorTextBox.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(506, 474);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "List Name:";
            // 
            // listNameTextBox
            // 
            this.listNameTextBox.Location = new System.Drawing.Point(507, 494);
            this.listNameTextBox.Name = "listNameTextBox";
            this.listNameTextBox.Size = new System.Drawing.Size(226, 22);
            this.listNameTextBox.TabIndex = 20;
            // 
            // createListButton
            // 
            this.createListButton.Location = new System.Drawing.Point(506, 424);
            this.createListButton.Name = "createListButton";
            this.createListButton.Size = new System.Drawing.Size(235, 44);
            this.createListButton.TabIndex = 19;
            this.createListButton.Text = "Create card list";
            this.createListButton.UseVisualStyleBackColor = true;
            this.createListButton.Click += new System.EventHandler(this.createListButton_Click);
            // 
            // listTextBox
            // 
            this.listTextBox.Location = new System.Drawing.Point(277, 543);
            this.listTextBox.Name = "listTextBox";
            this.listTextBox.Size = new System.Drawing.Size(223, 129);
            this.listTextBox.TabIndex = 17;
            this.listTextBox.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(274, 474);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "List Id:";
            // 
            // getListTextBox
            // 
            this.getListTextBox.Location = new System.Drawing.Point(275, 494);
            this.getListTextBox.Name = "getListTextBox";
            this.getListTextBox.Size = new System.Drawing.Size(226, 22);
            this.getListTextBox.TabIndex = 15;
            // 
            // getListButton
            // 
            this.getListButton.Location = new System.Drawing.Point(274, 424);
            this.getListButton.Name = "getListButton";
            this.getListButton.Size = new System.Drawing.Size(226, 44);
            this.getListButton.TabIndex = 14;
            this.getListButton.Text = "Get card list";
            this.getListButton.UseVisualStyleBackColor = true;
            this.getListButton.Click += new System.EventHandler(this.getListButton_Click);
            // 
            // allListsTextBox
            // 
            this.allListsTextBox.Location = new System.Drawing.Point(12, 474);
            this.allListsTextBox.Name = "allListsTextBox";
            this.allListsTextBox.Size = new System.Drawing.Size(253, 299);
            this.allListsTextBox.TabIndex = 13;
            this.allListsTextBox.Text = "";
            // 
            // getAllListsButton
            // 
            this.getAllListsButton.Location = new System.Drawing.Point(12, 424);
            this.getAllListsButton.Name = "getAllListsButton";
            this.getAllListsButton.Size = new System.Drawing.Size(253, 44);
            this.getAllListsButton.TabIndex = 12;
            this.getAllListsButton.Text = "Get all card lists";
            this.getAllListsButton.UseVisualStyleBackColor = true;
            this.getAllListsButton.Click += new System.EventHandler(this.getAllListsButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(506, 571);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 17);
            this.label7.TabIndex = 25;
            this.label7.Text = "Parent Board Id:";
            // 
            // listParentTextBox
            // 
            this.listParentTextBox.Location = new System.Drawing.Point(507, 591);
            this.listParentTextBox.Name = "listParentTextBox";
            this.listParentTextBox.Size = new System.Drawing.Size(226, 22);
            this.listParentTextBox.TabIndex = 24;
            // 
            // labelWhatever
            // 
            this.labelWhatever.AutoSize = true;
            this.labelWhatever.Location = new System.Drawing.Point(1266, 156);
            this.labelWhatever.Name = "labelWhatever";
            this.labelWhatever.Size = new System.Drawing.Size(83, 17);
            this.labelWhatever.TabIndex = 38;
            this.labelWhatever.Text = "Card List Id:";
            // 
            // cardParentIdTextBox
            // 
            this.cardParentIdTextBox.Location = new System.Drawing.Point(1267, 176);
            this.cardParentIdTextBox.Name = "cardParentIdTextBox";
            this.cardParentIdTextBox.Size = new System.Drawing.Size(226, 22);
            this.cardParentIdTextBox.TabIndex = 37;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1266, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 17);
            this.label9.TabIndex = 36;
            this.label9.Text = "Card Description:";
            // 
            // cardDescTextBox
            // 
            this.cardDescTextBox.Location = new System.Drawing.Point(1267, 128);
            this.cardDescTextBox.Name = "cardDescTextBox";
            this.cardDescTextBox.Size = new System.Drawing.Size(226, 22);
            this.cardDescTextBox.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1266, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 17);
            this.label10.TabIndex = 34;
            this.label10.Text = "Card Name:";
            // 
            // cardNameTextBox
            // 
            this.cardNameTextBox.Location = new System.Drawing.Point(1267, 79);
            this.cardNameTextBox.Name = "cardNameTextBox";
            this.cardNameTextBox.Size = new System.Drawing.Size(226, 22);
            this.cardNameTextBox.TabIndex = 33;
            // 
            // createCardTextBox
            // 
            this.createCardTextBox.Location = new System.Drawing.Point(1266, 12);
            this.createCardTextBox.Name = "createCardTextBox";
            this.createCardTextBox.Size = new System.Drawing.Size(235, 44);
            this.createCardTextBox.TabIndex = 32;
            this.createCardTextBox.Text = "Create card";
            this.createCardTextBox.UseVisualStyleBackColor = true;
            this.createCardTextBox.Click += new System.EventHandler(this.createCardTextBox_Click);
            // 
            // cardTextBox
            // 
            this.cardTextBox.Location = new System.Drawing.Point(1037, 130);
            this.cardTextBox.Name = "cardTextBox";
            this.cardTextBox.Size = new System.Drawing.Size(223, 129);
            this.cardTextBox.TabIndex = 31;
            this.cardTextBox.Text = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1034, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 17);
            this.label11.TabIndex = 30;
            this.label11.Text = "Card Id:";
            // 
            // cardIdTextBox
            // 
            this.cardIdTextBox.Location = new System.Drawing.Point(1035, 81);
            this.cardIdTextBox.Name = "cardIdTextBox";
            this.cardIdTextBox.Size = new System.Drawing.Size(226, 22);
            this.cardIdTextBox.TabIndex = 29;
            // 
            // getCardButton
            // 
            this.getCardButton.Location = new System.Drawing.Point(1034, 12);
            this.getCardButton.Name = "getCardButton";
            this.getCardButton.Size = new System.Drawing.Size(226, 44);
            this.getCardButton.TabIndex = 28;
            this.getCardButton.Text = "Get card";
            this.getCardButton.UseVisualStyleBackColor = true;
            this.getCardButton.Click += new System.EventHandler(this.getCardButton_Click);
            // 
            // getAllCardsTextBox
            // 
            this.getAllCardsTextBox.Location = new System.Drawing.Point(775, 62);
            this.getAllCardsTextBox.Name = "getAllCardsTextBox";
            this.getAllCardsTextBox.Size = new System.Drawing.Size(253, 344);
            this.getAllCardsTextBox.TabIndex = 27;
            this.getAllCardsTextBox.Text = "";
            // 
            // getAllCardsButton
            // 
            this.getAllCardsButton.Location = new System.Drawing.Point(775, 12);
            this.getAllCardsButton.Name = "getAllCardsButton";
            this.getAllCardsButton.Size = new System.Drawing.Size(253, 44);
            this.getAllCardsButton.TabIndex = 26;
            this.getAllCardsButton.Text = "Get all cards";
            this.getAllCardsButton.UseVisualStyleBackColor = true;
            this.getAllCardsButton.Click += new System.EventHandler(this.getAllCardsButton_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1266, 211);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 17);
            this.label12.TabIndex = 40;
            this.label12.Text = "Card Due Date:";
            // 
            // cardDueDatePicker
            // 
            this.cardDueDatePicker.Location = new System.Drawing.Point(1267, 232);
            this.cardDueDatePicker.Name = "cardDueDatePicker";
            this.cardDueDatePicker.Size = new System.Drawing.Size(200, 22);
            this.cardDueDatePicker.TabIndex = 43;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1156, 546);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 17);
            this.label8.TabIndex = 45;
            this.label8.Text = "User Id:";
            // 
            // userIdTextBox
            // 
            this.userIdTextBox.Location = new System.Drawing.Point(1157, 566);
            this.userIdTextBox.Name = "userIdTextBox";
            this.userIdTextBox.Size = new System.Drawing.Size(226, 22);
            this.userIdTextBox.TabIndex = 44;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1514, 788);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.userIdTextBox);
            this.Controls.Add(this.cardDueDatePicker);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.labelWhatever);
            this.Controls.Add(this.cardParentIdTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cardDescTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cardNameTextBox);
            this.Controls.Add(this.createCardTextBox);
            this.Controls.Add(this.cardTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cardIdTextBox);
            this.Controls.Add(this.getCardButton);
            this.Controls.Add(this.getAllCardsTextBox);
            this.Controls.Add(this.getAllCardsButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listParentTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listColorTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listNameTextBox);
            this.Controls.Add(this.createListButton);
            this.Controls.Add(this.listTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.getListTextBox);
            this.Controls.Add(this.getListButton);
            this.Controls.Add(this.allListsTextBox);
            this.Controls.Add(this.getAllListsButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.updateNameBoardTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.updateIdBoardTextbox);
            this.Controls.Add(this.updateBoardButton);
            this.Controls.Add(this.boardChangesTextbox);
            this.Controls.Add(this.getBoardTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.boardIdTextbox);
            this.Controls.Add(this.getBoard);
            this.Controls.Add(this.getBoardsTextBox);
            this.Controls.Add(this.getBoards);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getBoards;
        private System.Windows.Forms.RichTextBox getBoardsTextBox;
        private System.Windows.Forms.Button getBoard;
        private System.Windows.Forms.TextBox boardIdTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox getBoardTextBox;
        private System.Windows.Forms.RichTextBox boardChangesTextbox;
        private System.Windows.Forms.Button updateBoardButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox updateIdBoardTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox updateNameBoardTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox listColorTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox listNameTextBox;
        private System.Windows.Forms.Button createListButton;
        private System.Windows.Forms.RichTextBox listTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox getListTextBox;
        private System.Windows.Forms.Button getListButton;
        private System.Windows.Forms.RichTextBox allListsTextBox;
        private System.Windows.Forms.Button getAllListsButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox listParentTextBox;
        private System.Windows.Forms.Label labelWhatever;
        private System.Windows.Forms.TextBox cardParentIdTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox cardDescTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox cardNameTextBox;
        private System.Windows.Forms.Button createCardTextBox;
        private System.Windows.Forms.RichTextBox cardTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox cardIdTextBox;
        private System.Windows.Forms.Button getCardButton;
        private System.Windows.Forms.RichTextBox getAllCardsTextBox;
        private System.Windows.Forms.Button getAllCardsButton;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker cardDueDatePicker;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox userIdTextBox;
    }
}

