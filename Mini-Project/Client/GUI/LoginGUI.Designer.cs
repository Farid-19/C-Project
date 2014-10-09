namespace Client.GUI
{
    partial class LoginGUI
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
            this._usernameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._connectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _usernameBox
            // 
            this._usernameBox.Location = new System.Drawing.Point(38, 87);
            this._usernameBox.Name = "_usernameBox";
            this._usernameBox.Size = new System.Drawing.Size(200, 20);
            this._usernameBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter your username";
            // 
            // _connectButton
            // 
            this._connectButton.Location = new System.Drawing.Point(53, 128);
            this._connectButton.Name = "_connectButton";
            this._connectButton.Size = new System.Drawing.Size(169, 53);
            this._connectButton.TabIndex = 2;
            this._connectButton.Text = "Connect";
            this._connectButton.UseVisualStyleBackColor = true;
            this._connectButton.Click += new System.EventHandler(this._connectButton_Click);
            // 
            // LoginGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 223);
            this.Controls.Add(this._connectButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._usernameBox);
            this.Name = "LoginGUI";
            this.Text = "Connect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _usernameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _connectButton;
    }
}