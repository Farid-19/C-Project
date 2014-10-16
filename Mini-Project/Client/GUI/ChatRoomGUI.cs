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

namespace Client.GUI
{
    public partial class ChatRoomGUI : Form
    {
        public ChatRoomGUI(String name)
        {
            
            InitializeComponent();
            _chatRoomLabel.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
