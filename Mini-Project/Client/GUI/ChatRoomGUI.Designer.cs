namespace Client.GUI
{
    partial class ChatRoomGUI
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
            this._chatLogBox = new System.Windows.Forms.TextBox();
            this._chatRoomLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._textBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this._userListBox = new System.Windows.Forms.ListBox();
            this._leaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _chatLogBox
            // 
            this._chatLogBox.Location = new System.Drawing.Point(12, 37);
            this._chatLogBox.Multiline = true;
            this._chatLogBox.Name = "_chatLogBox";
            this._chatLogBox.ReadOnly = true;
            this._chatLogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._chatLogBox.Size = new System.Drawing.Size(525, 291);
            this._chatLogBox.TabIndex = 17;
            // 
            // _chatRoomLabel
            // 
            this._chatRoomLabel.AutoSize = true;
            this._chatRoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._chatRoomLabel.Location = new System.Drawing.Point(12, 8);
            this._chatRoomLabel.Name = "_chatRoomLabel";
            this._chatRoomLabel.Size = new System.Drawing.Size(116, 26);
            this._chatRoomLabel.TabIndex = 19;
            this._chatRoomLabel.Text = "Chatroom";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(629, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 26);
            this.label2.TabIndex = 20;
            this.label2.Text = "Connected Users";
            // 
            // _textBox
            // 
            this._textBox.Location = new System.Drawing.Point(12, 356);
            this._textBox.Name = "_textBox";
            this._textBox.Size = new System.Drawing.Size(525, 20);
            this._textBox.TabIndex = 21;
            this._textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._textBox_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(557, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 44);
            this.button1.TabIndex = 22;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _userListBox
            // 
            this._userListBox.Enabled = false;
            this._userListBox.FormattingEnabled = true;
            this._userListBox.Location = new System.Drawing.Point(630, 37);
            this._userListBox.Name = "_userListBox";
            this._userListBox.ScrollAlwaysVisible = true;
            this._userListBox.Size = new System.Drawing.Size(190, 290);
            this._userListBox.TabIndex = 23;
            // 
            // _leaveButton
            // 
            this._leaveButton.Location = new System.Drawing.Point(704, 343);
            this._leaveButton.Name = "_leaveButton";
            this._leaveButton.Size = new System.Drawing.Size(116, 44);
            this._leaveButton.TabIndex = 24;
            this._leaveButton.Text = "Leave Room";
            this._leaveButton.UseVisualStyleBackColor = true;
            this._leaveButton.Click += new System.EventHandler(this._leaveButton_Click);
            // 
            // ChatRoomGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 401);
            this.Controls.Add(this._leaveButton);
            this.Controls.Add(this._userListBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._chatRoomLabel);
            this.Controls.Add(this._chatLogBox);
            this.Name = "ChatRoomGUI";
            this.Text = "ChatRoomGUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _chatLogBox;
        private System.Windows.Forms.Label _chatRoomLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _textBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox _userListBox;
        private System.Windows.Forms.Button _leaveButton;
    }
}