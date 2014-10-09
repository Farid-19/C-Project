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

        public void requestRooms()
        {
            JObject roomsPacket = new JObject(
                    new JProperty("CMD", "requestinfo"),
                    new JProperty("Type", "chatrooms"));
                

            var json = roomsPacket.ToString();

           

            byte[] data = Packet.CreateByteData(json);
            this.Send(data);
            
        }

        public void read()
        {
            while (true)
            {
                List<byte> allTheBytes = new List<byte>();

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
