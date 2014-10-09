using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NetworkLibrary
{
    [Serializable]
    public class User
    {
        public string Name { get; set; }

        [NonSerialized] private TcpClient client;
        public bool isConnected { get { if (client == null) return false; else return client.Connected}}

        public User()
        {
                
        }

        public void send(string b)
        {
            byte[] data = Packet.CreateByteData(b);
            client.GetStream().WriteAsync(data, 0, data.Length);
        }

        public User(string name, TcpClient client)
        {
            this.client = client;
            Name = name;
        }
    }
}
