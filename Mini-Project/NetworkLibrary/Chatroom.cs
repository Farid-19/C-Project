using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    public class Chatroom
    {
        public string Name { get; set; }
        public Dictionary<User, string> Messages { get; set; }
        public List<User> users { get; set; }

        public Chatroom(string name)
        {
            Name = name;
        }
    }
}
