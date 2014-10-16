using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NetworkLibrary;
using System.Threading;

namespace Client
{
    public class Client
    {
        private TcpClient client;
        private Thread thread;
        public delegate void receivedJSON(JObject json);
        public event receivedJSON OnReceivedJSON;

        public Client()
        {
            client = new TcpClient();
            thread = new Thread(read);
      
        }


        public void connectToServer(String ip)
        {
            client.Connect(ip, 9001);
            thread.Start();
        }

        public void requestInfo(String type) 
        {
            switch (type)
            {
                case "chatrooms":
                    JObject roomsPacket = new JObject(
                    new JProperty("CMD", "requestinfo"),
                    new JProperty("Type", type));
                    var json1 = roomsPacket.ToString();
                    byte[] data1 = Packet.CreateByteData(json1);
                    this.Send(data1);
                    break;

                case "users":
                    JObject usersPacket = new JObject(
                    new JProperty("CMD", "requestinfo"),
                    new JProperty("Type", type));
                    var json2 = usersPacket.ToString();
                    byte[] data2 = Packet.CreateByteData(json2);
                    this.Send(data2);
                    break;
            }

            
            
        }

        public void read()
        {
            List<byte> allTheBytes = new List<byte>();
            while (true)
            {
                

                byte[] buffer = new byte[512];
                int received = client.GetStream().Read(buffer, 0, buffer.Length);

                if (received > 0)
                {
                    byte[] rawData = new byte[received];
                    Array.Copy(buffer, 0, rawData, 0, received);
                    allTheBytes = allTheBytes.Concat(rawData).ToList();


                    int packetSize = Packet.getLengthOfPacket(allTheBytes);
                    if (packetSize == -1)
                    {
                        return;
                    }

                    JObject json = Packet.RetrieveJSON(packetSize, ref allTheBytes);

                    if (json != null && OnReceivedJSON != null)
                    {
                        OnReceivedJSON(json);
                    }
                }
            }
        }

        public void Send(byte[] data)
        {
            client.GetStream().WriteAsync(data, 0, data.Length);            
        }
    }
}
