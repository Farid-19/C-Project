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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._textBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this._userListBox = new System.Windows.Forms.ListBox();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 26);
            this.label1.TabIndex = 19;
            this.label1.Text = "Chatroom";
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
            this._textBox.Size = new System.Drawing.Size(675, 20);
            this._textBox.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(704, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 44);
            this.button1.TabIndex = 22;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // _userListBox
            // 
            this._userListBox.FormattingEnabled = true;
            this._userListBox.Location = new System.Drawing.Point(630, 37);
            this._userListBox.Name = "_userListBox";
            this._userListBox.ScrollAlwaysVisible = true;
            this._userListBox.Size = new System.Drawing.Size(190, 290);
            this._userListBox.TabIndex = 23;
            // 
            // ChatRoomGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 401);
            this.Controls.Add(this._userListBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._chatLogBox);
            this.Name = "ChatRoomGUI";
            this.Text = "ChatRoomGUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _chatLogBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _textBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox _userListBox;
    }
}