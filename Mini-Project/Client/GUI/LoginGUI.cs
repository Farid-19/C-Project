using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using NetworkLibrary;

namespace Client.GUI
{
    public partial class LoginGUI : Form
    {
        private Client client;

        public LoginGUI()
        {
            InitializeComponent();
            client = new Client();
         
        }

        private void _connectButton_Click(object sender, EventArgs e)
        {
            if (_usernameBox.Text == "" || _serverIPBox.Text == "")
            {
                MessageBox.Show("Please enter username/server IP!");
            }
            else
            {
                client.connectToServer(_serverIPBox.Text);

                JObject identityPacket = new JObject(
                    new JProperty("CMD", "identity"),
                    new JProperty("Name", _usernameBox.Text));


                var json = identityPacket.ToString();

                byte[] data = Packet.CreateByteData(json);
                client.Send(data);
                
                

                this.Hide();

                ChooseRoomGUI chooseGUI = new ChooseRoomGUI(_usernameBox.Text);
                chooseGUI.Show();
            }
        }
    }
}
