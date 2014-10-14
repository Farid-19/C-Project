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

        /// <summary>
        /// Sends a chatmessage to all users/clients in an given collection.
        /// </summary>
        /// <param name="message">Message to send</param>
        /// <param name="user">Name of the user to use for the message</param>
        /// <param name="users">Collection of users to send the message to</param>
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

        /// <summary>
        /// Sends a chatmessage to all users/clients in the chatroom.
        /// </summary>
        /// <param name="message"></param>
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


        /// <summary>
        ///  Notifies existing clients/users if a user leaves or joins this chatroom.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="argsChangedAction"></param>
        private void OnUsersListChanged(object obj, NotifyCollectionChangedEventArgs argsChangedAction)
        {
            foreach (User existingUser in users.Where(x => x.isConnected))
            {
                foreach (User newUser in argsChangedAction.NewItems)
                {
                    JObject json = new JObject(new JProperty("Name", newUser.Name));

                    switch (argsChangedAction.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            json.Add(new JProperty("CMD", "userjoined"));
                            break;
                        case NotifyCollectionChangedAction.Remove:
                            json.Add(new JProperty("CMD", "userleft"));
                            break;
                        default:
                            continue;
                            break;
                    }

                    existingUser.send(json.ToString());
                }
            }
        }
    }
}
