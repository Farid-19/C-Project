using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json.Serialization;
namespace NetworkLibrary
{
    [Serializable]
    public class Chatroom
    {
        public string Name { get; set; }
        public List<String> Messages { get; set; }
        public ObservableCollection<User> users { get; set; }
        private readonly ReaderWriterLock usersLock = new ReaderWriterLock();
        private bool writeLog;

        public Chatroom(string name, bool writeMessagesToDisk = false)
        {
            writeLog = writeMessagesToDisk;
            Name = name;
            Messages = new List<string>();
            users = new ObservableCollection<User>();
            users.CollectionChanged += OnUsersListChanged;

                
        }

        public void addMessage(User s, string message)
        {
            string mes = s.Name + ": " + message;

            if (writeLog)
                WriteLog(mes);
            Messages.Add(mes);
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
            addMessage(this.users.First(x => x.Name == user), message);
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

        public void WriteLog(string leMessage)
        {
            string logFilePath = this.Name + @".txt";
            using (
                System.IO.StreamWriter file = new System.IO.StreamWriter(logFilePath, true))
            {
                
                DateTime n = DateTime.Now;
                string message = "[" + n.Day + ":" + n.Month + ":" + n.Year + "][" + n.Hour + ":" + n.Minute + ":" + n.Second + "]" + " " +
                                 leMessage;
                file.WriteLine(message);
            }
            
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
                 
                
                switch (argsChangedAction.Action)
                {
                        case NotifyCollectionChangedAction.Add:
                          
                            foreach (User newUser in argsChangedAction.NewItems)
                            {
                                JObject json = new JObject(new JProperty("Name", newUser.Name));
                                 if(existingUser == newUser)
                                    continue;
                                json.Add(new JProperty("CMD", "userjoined"));
                                if (writeLog)
                                    WriteLog(newUser + " joined the room.");
                                existingUser.send(json.ToString());
                            }

                        break;
                        case NotifyCollectionChangedAction.Remove:
                            foreach (User oldUser in argsChangedAction.OldItems)
                            {
                                JObject json = new JObject(new JProperty("Name", oldUser.Name));
                                json.Add(new JProperty("CMD", "userleft"));
                                existingUser.send(json.ToString());
                                if (writeLog)
                                    WriteLog(oldUser + " left the room.");
                            }
                            break;
                        default:
                            break;
                }

                
            }
        }
    }
}
