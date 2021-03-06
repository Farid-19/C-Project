﻿using System;
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
        User user; 
        public ChooseRoomGUI(String username, Client c)
        {
            
            InitializeComponent();
            _welcomeLabel.Text = "Welcome " + username;

            if (_roomListBox.SelectedIndex == -1)
            {
                _connectButton.Enabled = false;
            }
            client = c;

            user = new User(username, null);

            client.requestInfo("chatrooms");
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
            Chatroom room = (Chatroom)_roomListBox.SelectedItem;


            JObject changeRoomPacket = new JObject(
                new JProperty("CMD", "joinRoom"),
                new JProperty("Room", room.Name));

            

            var json = changeRoomPacket.ToString();

            byte[] data = Packet.CreateByteData(json);
            client.Send(data);

            room.AddUser(user);

            
            ChatRoomGUI chatGUI = new ChatRoomGUI(client, room, user);
            
            chatGUI.Text = room.Name;
            chatGUI.ShowDialog();

        }

        public void UpdateJSON(JObject j)
        {
            if (j["CMD"].ToString() != "requestinforesponse" ||
                j["Type"].ToString() != "chatrooms")
                return;

            client.OnReceivedJSON -= UpdateJSON;
            foreach(JToken token in j["Data"])
            {
                Chatroom chatroom = token.ToObject<Chatroom>();
                _roomListBox.BeginInvoke((Action)(() => _roomListBox.Items.Add(chatroom)));
                
            }
        }

        private void _updateButton_Click(object sender, EventArgs e)
        {
            if (_roomListBox.Items.Count != 0)
            {
                _roomListBox.Items.Clear();
            }
            client.requestInfo("chatrooms");
            client.OnReceivedJSON += UpdateJSON;
        }
    }
}
