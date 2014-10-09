using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client.GUI
{

    public partial class ChooseRoomGUI : Form
    {
        Client client = new Client();

        public ChooseRoomGUI(String username)
        {
            InitializeComponent();
            _welcomeLabel.Text = "Welcome " + username + " !";

            if (_roomListBox.SelectedIndex == -1)
            {
                _connectButton.Enabled = false;
            }

            client.connectToServer();
            client.loadRooms();
        }

        

       

        private void _roomListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_roomListBox.SelectedIndex != -1)
            {
                _connectButton.Enabled = true;
            }
            else
            {
                _connectButton.Enabled = false;
            }
        }

        private void _disconnectButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Log out and quit the application?", "Alert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void _connectButton_Click(object sender, EventArgs e)
        {
            
            ChatRoomGUI chatGUI = new ChatRoomGUI();
            chatGUI.Show();
        }
    }
}
