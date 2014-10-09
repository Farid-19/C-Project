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
        protected static int origRow;
        protected static int origCol;

        protected static int inputRow = 25;
        protected static int inputCol = 7;

        private List<Chatroom> rooms;
        private List<User> users;
        private ConcurrentDictionary<User, TcpClient> userConnections;
        

        public Server()
        {
            rooms= new List<Chatroom>();
            
            

            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            Console.CursorTop = inputRow;
            Console.CursorLeft = inputCol;

        }

        public async void Start()
        {
            var listener = new TcpListener(IPAddress.Any, 9001);
            var token = new CancellationToken();
            
            while (!token.IsCancellationRequested)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();

                Thread thread = new Thread(() => handleClient(client, token));
                thread.Start();
            }
        }

        public async void handleClient(TcpClient obj, CancellationToken token)
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
                }
                else // client (probably disconnected.
                    keepReading = false;

            }



        }

        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
            Console.CursorTop = inputRow;
            Console.CursorLeft = inputCol;
        }


       
    }
}
