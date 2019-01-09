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
            this.SuspendLayout();
            // 
            // getBoards
            // 
            this.getBoards.Location = new System.Drawing.Point(29, 26);
            this.getBoards.Name = "getBoards";
            this.getBoards.Size = new System.Drawing.Size(253, 44);
            this.getBoards.TabIndex = 0;
            this.getBoards.Text = "Get all boards";
            this.getBoards.UseVisualStyleBackColor = true;
            this.getBoards.Click += new System.EventHandler(this.getBoards_Click);
            // 
            // getBoardsTextBox
            // 
            this.getBoardsTextBox.Location = new System.Drawing.Point(29, 100);
            this.getBoardsTextBox.Name = "getBoardsTextBox";
            this.getBoardsTextBox.Size = new System.Drawing.Size(253, 605);
            this.getBoardsTextBox.TabIndex = 1;
            this.getBoardsTextBox.Text = "";
            // 
            // getBoard
            // 
            this.getBoard.Location = new System.Drawing.Point(335, 26);
            this.getBoard.Name = "getBoard";
            this.getBoard.Size = new System.Drawing.Size(226, 44);
            this.getBoard.TabIndex = 2;
            this.getBoard.Text = "Get board";
            this.getBoard.UseVisualStyleBackColor = true;
            this.getBoard.Click += new System.EventHandler(this.getBoard_Click);
            // 
            // boardIdTextbox
            // 
            this.boardIdTextbox.Location = new System.Drawing.Point(335, 118);
            this.boardIdTextbox.Name = "boardIdTextbox";
            this.boardIdTextbox.Size = new System.Drawing.Size(226, 22);
            this.boardIdTextbox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Board Id:";
            // 
            // getBoardTextBox
            // 
            this.getBoardTextBox.Location = new System.Drawing.Point(337, 167);
            this.getBoardTextBox.Name = "getBoardTextBox";
            this.getBoardTextBox.Size = new System.Drawing.Size(223, 191);
            this.getBoardTextBox.TabIndex = 5;
            this.getBoardTextBox.Text = "";
            // 
            // boardChangesTextbox
            // 
            this.boardChangesTextbox.Location = new System.Drawing.Point(335, 373);
            this.boardChangesTextbox.Name = "boardChangesTextbox";
            this.boardChangesTextbox.Size = new System.Drawing.Size(225, 332);
            this.boardChangesTextbox.TabIndex = 6;
            this.boardChangesTextbox.Text = "";
            // 
            // updateBoardButton
            // 
            this.updateBoardButton.Location = new System.Drawing.Point(622, 26);
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
            this.label2.Location = new System.Drawing.Point(621, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Board Id:";
            // 
            // updateIdBoardTextbox
            // 
            this.updateIdBoardTextbox.Location = new System.Drawing.Point(622, 118);
            this.updateIdBoardTextbox.Name = "updateIdBoardTextbox";
            this.updateIdBoardTextbox.Size = new System.Drawing.Size(226, 22);
            this.updateIdBoardTextbox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(621, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "New Board Name:";
            // 
            // updateNameBoardTextbox
            // 
            this.updateNameBoardTextbox.Location = new System.Drawing.Point(622, 167);
            this.updateNameBoardTextbox.Name = "updateNameBoardTextbox";
            this.updateNameBoardTextbox.Size = new System.Drawing.Size(226, 22);
            this.updateNameBoardTextbox.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 717);
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
    }
}

