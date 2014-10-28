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
using NetworkLibrary;
using Newtonsoft.Json.Linq;

namespace Client.GUI
{
    public partial class ChatRoomGUI : Form
    {
        Chatroom room;
        private Client client;
        private User user;
        public ChatRoomGUI(Client c, Chatroom r, User s)
        {
            room = r;
            user = s;
            client = c;
            InitializeComponent();
            _chatRoomLabel.Text = room.Name;
            _userListBox.Items.Add(s);
            addMessage(s.Name + " has joined the room.");
            Initialize();
            client.OnReceivedJSON += jsonReceived;

        }

        public void Initialize()
        {
            foreach (User user in room.users)
            {
                if(user.Name == this.user.Name)
                    continue;
                _userListBox.Items.Add(user);
            }

            foreach (string message in room.Messages)
            {
                addMessage(message);
            }
        }

        private void addMessage(string message)
        {
            _chatLogBox.AppendText( message);
            _chatLogBox.AppendText(Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_textBox.Text == "")
            {
                MessageBox.Show("Please enter your message!");
            }
            else
            {
                JObject json = new JObject(new JProperty("CMD", "newchatmessage"),
                new JProperty("Message", _textBox.Text),
                new JProperty("User", user.Name));
                addMessage(user.Name + ": " + _textBox.Text);
                client.Send(Packet.CreateByteData(json.ToString()));
                _textBox.Text = "";
            }
           
        }

        private void _textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                if (_textBox.Text == "")
                {
                    MessageBox.Show("Please enter your message!");
                }
                else
                {
                    JObject json = new JObject(new JProperty("CMD", "newchatmessage"),
                    new JProperty("Message", _textBox.Text),
                    new JProperty("User", user.Name));
                    addMessage(user.Name + ": " + _textBox.Text);
                    client.Send(Packet.CreateByteData(json.ToString()));
                    _textBox.Text = "";
                }
           
            }
        }

        private void jsonReceived(JObject json)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => jsonReceived(json)));
                return;
            }

            JToken username;

            //if (!json.TryGetValue("User", out username))
            //    return;

            //Als de begruiker niet bestaat
            //if (!_userListBox.Items.Cast<User>().ToList().Exists(x => x.Name == username.ToString()))
            //    return;

            switch (json["CMD"].ToString().ToLower())
            {
                case "newchatmessage":
                    addMessage(json["User"] + ": " + json["Message"].ToString());
                    break;
                case "userjoined":
                    _userListBox.Items.Add(new User(json["Name"].ToString()));
                    addMessage(json["Name"].ToString() + " has joined the room." );
                    break;
                case "userleft":
                    List<User> a = _userListBox.Items.Cast<User>().ToList();
                    
                        
                    a.Remove(a.First(x => x.Name == json["Name"].ToString()));
                    addMessage(json["Name"].ToString() + " has left the room.");
                    _userListBox.Items.Clear();
                    foreach (var user in a)
                    {
                        _userListBox.Items.Add(user);
                    }
                    break;
            }
        }

        private void _leaveButton_Click(object sender, EventArgs e)
        {
            JObject json = new JObject(new JProperty("CMD", "leaveRoom"),
                new JProperty("Room", room.Name));
            client.Send(Packet.CreateByteData(json.ToString()));

            this.Close();
        }

        
    }
}
