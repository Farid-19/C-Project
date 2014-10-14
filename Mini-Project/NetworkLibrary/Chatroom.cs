using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NetworkLibrary
{
    [Serializable]
    public class Chatroom
    {
        public string Name { get; set; }
        public Dictionary<User, string> Messages { get; set; }
        public ObservableCollection<User> users { get; set; }
        private readonly ReaderWriterLock usersLock = new ReaderWriterLock();

        public Chatroom(string name)
        {
            Name = name;
            Messages = new Dictionary<User, string>();
            users = new ObservableCollection<User>();
            users.CollectionChanged += OnUsersListChanged;
        }



        public void AddUser(User u)
        {
            usersLock.AcquireReaderLock(-1);
            users.Add(u);
            usersLock.ReleaseReaderLock();
        }

        public void RemoveUser(User u)
        {
            usersLock.AcquireWriterLock(-1);
            users.Remove(u);
            usersLock.ReleaseReaderLock();
            
        }

        public void BroadCast(string message, string sender)
        {
            BroadCast(message, sender, users.Where(x => x.isConnected).ToList());
        }

        public void sendUserMessage(string message,User sender)
        {
            BroadCast(message, sender.Name, users.Where(x => x.isConnected && x != sender).ToList());
        }

        private void BroadCast(string message, string user, IEnumerable<User> users)
        {
            JObject json = new JObject(new JProperty("CMD", "newchatmessage"),
            new JProperty("Message", message),
            new JProperty("User", user));
            usersLock.AcquireReaderLock(-1);
            foreach (User u in users)
            {
                u.send(json.ToString());
            }
            usersLock.ReleaseReaderLock();
        }

        private void BroadCast(string message)
        {
            JObject json = new JObject(new JProperty("CMD", "newchatmessage"),
            new JProperty("Message", message));
            usersLock.AcquireReaderLock(-1);
            foreach (User u in users.Where(x => x.isConnected))
            {
                u.send(json.ToString());
            }
            usersLock.ReleaseReaderLock();
        }

        public override string ToString()
        {
            usersLock.AcquireReaderLock(-1);
            String retval = String.Format("{0} - {1} Users", Name, users.Count);
            usersLock.ReleaseReaderLock();
            return retval;

        }


        public void OnUsersListChanged(object obj, NotifyCollectionChangedEventArgs argsChangedAction)
        {

            //if (users.Count < 2)
            //    return;
            String message;

            
            switch (argsChangedAction.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (User newUser in argsChangedAction.NewItems)
                    {
                        message = String.Format("{0} has joined the conversation..", newUser.Name);
                        BroadCast(message);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (User newUser in argsChangedAction.NewItems)
                    {
                        
                        message = String.Format("{0} has left...", newUser.Name);
                        BroadCast(message);
                    }
                    break;
                default:
                    return;
                    break;
            }

            
            
        }
    }
}
