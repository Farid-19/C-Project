using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NetworkLibrary
{
    public class Packet
    {
        public static int getLengthOfPacket(List<byte> buffer)
        {
            if (buffer.Count < 4) return -1;
            int t = BitConverter.ToInt32(buffer.ToArray(), 0);
            return t;
        }

        public static JObject RetrieveJSON(int packetSize, ref List<byte> buffer)
        {
            if (buffer.Count < packetSize + 4) return null;
            return JObject.Parse(Encoding.UTF8.GetString(GetPacketBytes(packetSize, ref buffer).ToArray()));
        }

        private static List<byte> GetPacketBytes(int packetSize, ref List<byte> buffer)
        {
            List<byte> jsonData = buffer.GetRange(4, packetSize);
            buffer.RemoveRange(0, packetSize + 4);
            return jsonData;
        }
        public static byte[] CreateByteData(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            byte[] length = BitConverter.GetBytes(bytes.Length);
            byte[] data = length.Concat(bytes).ToArray();
            return data;
        }
    }
}
