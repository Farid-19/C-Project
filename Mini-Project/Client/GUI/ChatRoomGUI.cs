using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkLibrary;

namespace Client.GUI
{
    public partial class ChatRoomGUI : Form
    {
        Chatroom room;

        public ChatRoomGUI(Chatroom r)
        {
            room = r;
            
            InitializeComponent();
            _chatRoomLabel.Text = room.Name;
            Initialize();
            _chatLogBox.AppendText(room.users. + " joined the room!");
            _chatLogBox.AppendText(Environment.NewLine);


        }

        public void Initialize()
        {
            foreach (User user in room.users)
            {
                _userListBox.Items.Add(user.Name);
            }

            foreach (KeyValuePair<User, String> pair in room.Messages) 
            {
                _chatLogBox.AppendText(pair.Key + pair.Value);
                _chatLogBox.AppendText(Environment.NewLine);
            }
        }
    }
}
