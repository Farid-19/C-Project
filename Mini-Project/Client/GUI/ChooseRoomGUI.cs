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
using Client;
using Newtonsoft.Json.Linq;
using NetworkLibrary;

namespace Client.GUI
{

    public partial class ChooseRoomGUI : Form
    {
        Client client;

        public ChooseRoomGUI(String username, Client c)
        {
            InitializeComponent();
            _welcomeLabel.Text = "Welcome " + username + " !";

            if (_roomListBox.SelectedIndex == -1)
            {
                _connectButton.Enabled = false;
            }
            client = c;

            client.requestRooms();
            client.OnReceivedJSON += UpdateJSON;
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
            JObject changeRoomPacket = new JObject(
                new JProperty("CMD", "changeRoom"),
                new JProperty("Room", _roomListBox.SelectedIndex.ToString()));


            var json = changeRoomPacket.ToString();

            byte[] data = Packet.CreateByteData(json);
            client.Send(data);


            ChatRoomGUI chatGUI = new ChatRoomGUI();
            chatGUI.Show();

        }

        public void UpdateJSON(JObject j)
        {
            client.OnReceivedJSON -= UpdateJSON;
            foreach(JToken token in j["Data"])
            {
                Chatroom chatroom = token.ToObject<Chatroom>();
                _roomListBox.BeginInvoke((Action)(() => _roomListBox.Items.Add(chatroom)));
                
            }
        }
    }
}
