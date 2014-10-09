using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NetworkLibrary;

namespace Client
{
    class Client
    {
        private TcpClient client;

        public Client()
        {
            client = new TcpClient();
            
        }


        public void connectToServer(String ip)
        {
            client.Connect(ip, 9001);
        }

        public void loadRooms()
        {
            JObject roomsPacket = new JObject(
                    new JProperty("CMD", "requestinfo"),
                    new JProperty("Type", "chatrooms"));
                

            var json = roomsPacket.ToString();

            Packet.CreateByteData(json);
            
        }

        public void Send(byte[] data)
        {
            client.GetStream().WriteAsync(data, 0, data.Length);            
        }
    }
}
