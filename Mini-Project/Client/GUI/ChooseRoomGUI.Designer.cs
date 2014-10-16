namespace Client.GUI
{
    partial class ChooseRoomGUI
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
            this._roomListBox = new System.Windows.Forms.ListBox();
            this._welcomeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._connectButton = new System.Windows.Forms.Button();
            this._disconnectButton = new System.Windows.Forms.Button();
            this._updateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _roomListBox
            // 
            this._roomListBox.FormattingEnabled = true;
            this._roomListBox.Location = new System.Drawing.Point(15, 72);
            this._roomListBox.Name = "_roomListBox";
            this._roomListBox.Size = new System.Drawing.Size(464, 160);
            this._roomListBox.TabIndex = 0;
            this._roomListBox.SelectedIndexChanged += new System.EventHandler(this._roomListBox_SelectedIndexChanged);
            // 
            // _welcomeLabel
            // 
            this._welcomeLabel.AutoSize = true;
            this._welcomeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._welcomeLabel.Location = new System.Drawing.Point(12, 9);
            this._welcomeLabel.Name = "_welcomeLabel";
            this._welcomeLabel.Size = new System.Drawing.Size(66, 17);
            this._welcomeLabel.TabIndex = 1;
            this._welcomeLabel.Text = "Welcome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(185, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose a room!";
            // 
            // _connectButton
            // 
            this._connectButton.Location = new System.Drawing.Point(364, 295);
            this._connectButton.Name = "_connectButton";
            this._connectButton.Size = new System.Drawing.Size(115, 42);
            this._connectButton.TabIndex = 5;
            this._connectButton.Text = "Connect";
            this._connectButton.UseVisualStyleBackColor = true;
            this._connectButton.Click += new System.EventHandler(this._connectButton_Click);
            // 
            // _disconnectButton
            // 
            this._disconnectButton.Location = new System.Drawing.Point(16, 295);
            this._disconnectButton.Name = "_disconnectButton";
            this._disconnectButton.Size = new System.Drawing.Size(115, 42);
            this._disconnectButton.TabIndex = 7;
            this._disconnectButton.Text = "Quit";
            this._disconnectButton.UseVisualStyleBackColor = true;
            this._disconnectButton.Click += new System.EventHandler(this._disconnectButton_Click);
            // 
            // _updateButton
            // 
            this._updateButton.Location = new System.Drawing.Point(188, 295);
            this._updateButton.Name = "_updateButton";
            this._updateButton.Size = new System.Drawing.Size(115, 42);
            this._updateButton.TabIndex = 8;
            this._updateButton.Text = "Update";
            this._updateButton.UseVisualStyleBackColor = true;
            this._updateButton.Click += new System.EventHandler(this._updateButton_Click);
            // 
            // ChooseRoomGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 351);
            this.Controls.Add(this._updateButton);
            this.Controls.Add(this._disconnectButton);
            this.Controls.Add(this._connectButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._welcomeLabel);
            this.Controls.Add(this._roomListBox);
            this.Name = "ChooseRoomGUI";
            this.Text = "Choose a room";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox _roomListBox;
        private System.Windows.Forms.Label _welcomeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _connectButton;
        private System.Windows.Forms.Button _disconnectButton;
        private System.Windows.Forms.Button _updateButton;
    }
}