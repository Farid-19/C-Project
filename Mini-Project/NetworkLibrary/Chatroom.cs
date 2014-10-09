﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NetworkLibrary
{
    [Serializable]
    public class Chatroom
    {
        public string Name { get; set; }
        public Dictionary<User, string> Messages { get; set; }
        public List<User> users { get; set; }

        public Chatroom(string name)
        {
            Name = name;
        }

        public void AddUser(User u)
        {
            users.Add(u);
        }

        public void RemoveUser(User u)
        {
            users.Remove(u);
        }

        public void BroadCast(string message, string user, ConcurrentDictionary<User, TcpClient> userClients)
        {
            JObject json = new JObject(new JProperty("CMD", "newchatmessage"), 
                new JProperty("Message", message),
                new JProperty("User", user));
            foreach (User u in users)
            {
                u.send(json.ToString());
            }
        }
    }
}
