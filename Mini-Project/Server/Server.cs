using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetworkLibrary;
using Newtonsoft.Json.Linq;
using NetworkLibrary;
namespace Server
{
    class Server
    {

        protected static int inputRow = 25;
        protected static int inputCol = 7;

        private ConcurrentBag<Chatroom> chatrooms;
        private ConcurrentBag<User> users;

        private ConcurrentDictionary<User, TcpClient> userConnections;
        private ConcurrentDictionary<User, Chatroom> usersChatRoom;
        private Chatroom defaultChatroom;

        public Server()
        {
            chatrooms = new ConcurrentBag<Chatroom>();
            users = new ConcurrentBag<NetworkLibrary.User>();
            userConnections = new ConcurrentDictionary<NetworkLibrary.User, TcpClient>();
            usersChatRoom = new ConcurrentDictionary<NetworkLibrary.User, NetworkLibrary.Chatroom>();
            defaultChatroom = new Chatroom("Room 1");
            chatrooms.Add(defaultChatroom);

        }

        public void Start()
        {
            var listener = new TcpListener(IPAddress.Any, 9001);
            var token = new CancellationToken();

            var serverip = Dns.GetHostEntry(Dns.GetHostName())
    .AddressList.First(o => o.AddressFamily == AddressFamily.InterNetwork)
    .ToString();

            Console.WriteLine("Chat server IP: {0}", serverip);
            listener.Start();
            Console.WriteLine("READY! Now listening.");
            while (!token.IsCancellationRequested)
            {
                TcpClient client = listener.AcceptTcpClient();

                Thread thread = new Thread(() => HandleClient(client, token));
                thread.Start();
            }
            Console.WriteLine("");
        }

        public async void HandleClient(TcpClient obj, CancellationToken token)
        {
            var client = obj as TcpClient;
            bool keepReading = true;
            List<byte> allTheBytes = new List<byte>();

            while (keepReading)
            {
                byte[] buffer = new byte[512];
                int received = await client.GetStream().ReadAsync(buffer, 0, buffer.Length, token);

                if (received > 0)
                {
                    byte[] rawData = new byte[received];
                    Array.Copy(buffer, 0, rawData, 0, received);
                    allTheBytes = allTheBytes.Concat(rawData).ToList();


                    int packetSize = Packet.getLengthOfPacket(allTheBytes);
                    if (packetSize == -1)
                        continue;

                    JObject json = Packet.RetrieveJSON(packetSize, ref allTheBytes);

                    if (json == null)
                        continue;

                    switch (json["CMD"].ToString())
                    {
                        case "identity":
                            handleIdentity(json, client);
                            break;
                        case "requestinfo":
                            HandleFreedomOfInformationRequest(json, client);
                            break;
                    }
                }
                else // client (probably) disconnected.
                    keepReading = false;

            }



        }

        private void send(User u, string s)
        {
            TcpClient client;
            userConnections.TryGetValue(u, out client);
            if(client == null)
                throw new InvalidOperationException("User not found in the userConnection dictionary.");
            send(client, s);
        }

        private void send(TcpClient client, string s)
        {
            byte[] data = Packet.CreateByteData(s);

            //no await. Fire and forget
            client.GetStream().WriteAsync(data ,0, data.Length);
        }

        private void handleIdentity(JObject json, TcpClient client)
        {
            User newUser = new User()
            {
                Name = json["Name"].ToString()
            };

            users.Add(newUser);
            userConnections.AddOrUpdate(newUser, client, (u, c) => c);
            usersChatRoom.AddOrUpdate(newUser, defaultChatroom, (u, c) => c);
        }

        private void HandleFreedomOfInformationRequest(JObject j, TcpClient c)
        {
            switch (j["type"].ToString().ToLower())
            {
                case "chatrooms":
                    JArray allRooms = JArray.FromObject(chatrooms);
                    send(c, new JObject(new JProperty("CMD", "requestinforesponse"),
                        new JProperty("Type", "chatrooms"),
                        new JProperty("Data", allRooms)).ToString());
                        break;
            }
        }


       
    }
}
