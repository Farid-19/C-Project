﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    public class User
    {
        public string Name { get; set; }

        public User()
        {
            
        }

        public User(string name)
        {
            Name = name;
        }
    }
}
