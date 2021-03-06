﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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

        private ConcurrentBag<Chatroom> chatrooms;
        private ConcurrentBag<User> users;

        private ConcurrentDictionary<User, Chatroom> usersChatRoom;
        private ConcurrentDictionary<TcpClient, User> clientUsers;

        public Server()
        {
            chatrooms = new ConcurrentBag<Chatroom>();
            users = new ConcurrentBag<NetworkLibrary.User>();
            
            usersChatRoom = new ConcurrentDictionary<NetworkLibrary.User, NetworkLibrary.Chatroom>();
            clientUsers = new ConcurrentDictionary<TcpClient, User>();
            Chatroom defaultChatroom = new Chatroom("The Commissariat",true);
            Chatroom c1 = new Chatroom("The White council", true);
            Chatroom c2 = new Chatroom("League of Nations",true);
            Chatroom c3 = new Chatroom("Ministerie van koloniën", true);
            Chatroom c4 = new Chatroom("QueueAge",true);
            Chatroom c5 = new Chatroom("Het Gemeentehuis",true);
            chatrooms.Add(defaultChatroom);
            chatrooms.Add(c1);
            chatrooms.Add(c2);
            chatrooms.Add(c3);
            chatrooms.Add(c4);
            chatrooms.Add(c5);

        }

        public void Start()
        {
            var listener = new TcpListener(IPAddress.Any, 9001);
            var token = new CancellationToken();

            var serverip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(o => o.AddressFamily == AddressFamily.InterNetwork).ToString();

            Console.WriteLine("Chat server IP: {0}", serverip);
            listener.Start();
            Console.WriteLine("READY! Now listening.");
            while (!token.IsCancellationRequested)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine(String.Format("A client with IP {0} connected", client.Client.RemoteEndPoint));

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
                        case "newchatmessage":
                            HandleNewchatmessage(json);
                            break;
                        case "changeRoom":
                            HandleChangeRoom(json, client);
                            break;
                        case "joinRoom":
                            JoinRoom(json, client);
                            break;

                        case "leaveRoom":
                            LeaveRoom(json, client);
                            break;
                    }
                }
                else // client (probably) disconnected.
                    keepReading = false;

            }



        }

        private void JoinRoom(JObject j, TcpClient r)
        {
            string room = j["Room"].ToString();
            User s = clientUsers.GetOrAdd(r , z => null);

            if (s == null)
            {
                return;
            }

            Chatroom roomToAddTo = chatrooms.First(w => w.Name == j["Room"].ToString());
            usersChatRoom.AddOrUpdate(s, roomToAddTo, (z, c) => c);
            roomToAddTo.AddUser(s);
            string t = ";";
        }

        private void LeaveRoom(JObject j, TcpClient r)
        {
            
            string room = j["Room"].ToString();
            User s = clientUsers.GetOrAdd(r, z => null);

            if (s == null)
            {
                return;
            }

            User removUser = null;
            User userToRemove = clientUsers.GetOrAdd(r, removUser);
            if (userToRemove == null)
                return;

            Chatroom removed;
            bool succes = usersChatRoom.TryRemove(userToRemove, out removed);
            removed.RemoveUser(userToRemove);
            Console.WriteLine(String.Format("{0} has left {1}", userToRemove.Name, removed.Name));
        }

	    private void HandleNewchatmessage(JObject json)
        {
            string message = json["Message"].ToString();
            User user = users.First(x => x.Name == json["User"].ToString());
            Chatroom room;
            usersChatRoom.TryGetValue(user, out room);
	        Console.WriteLine(String.Format("Got a new chatmessage in room {1}: {0}", message, room.Name));
            if (room != null) room.sendUserMessage(message, user);
        }

        private void broadcast(string message, string user)
        {
            foreach(Chatroom room in chatrooms)
                room.BroadCast(message, user);
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
                Name = json["Name"].ToString(),
                client = client
            };

            AddNewUser(client, newUser);
        }

        private void HandleChangeRoom(JObject json, TcpClient client)
        {
            //
        }

        private void HandleFreedomOfInformationRequest(JObject j, TcpClient c)
        {
            JObject json;
            switch (j["Type"].ToString().ToLower())
            {
                case "chatrooms":
                    JArray allRooms = JArray.FromObject(chatrooms.ToList());
                    json = new JObject(new JProperty("CMD", "requestinforesponse"),
                        new JProperty("Type", "chatrooms"),
                        new JProperty("Data", allRooms));
                    send(c, json.ToString());
                        break;
                case "users":
                     JArray allUsers = JArray.FromObject(users.ToList());
                     json  = new JObject(new JProperty("CMD", "requestinforesponse"),
                         new JProperty("Type", "users"),
                         new JProperty("Data", allUsers));
                     break;

                default:
                    return;
            }
            send(c, json.ToString());

        }

        private void AddNewUser(TcpClient t, User u)
        {
            clientUsers.AddOrUpdate(t, u, (z, q) => q);
            users.Add(u);
            //usersChatRoom.AddOrUpdate(u, defaultChatroom, (z, c) => c);
            //defaultChatroom.AddUser(u);
        }

        


       
    }
}
