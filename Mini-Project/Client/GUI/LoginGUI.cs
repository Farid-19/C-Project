using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GUI
{
    public partial class LoginGUI : Form
    {

        public LoginGUI()
        {
            InitializeComponent();
        }

        private void _connectButton_Click(object sender, EventArgs e)
        {
            if (_usernameBox.Text == "")
            {
                MessageBox.Show("Please enter your username!");
            }
            else
            {
                this.Hide();

                ChooseRoomGUI chooseGUI = new ChooseRoomGUI(_usernameBox.Text);
                chooseGUI.Show();
            }
        }
    }
}
