using System.Text;
using RingingBloom.Common;

namespace RingingBloom
{
    class HelperFunctions
    {
        public static string ReadNullTerminatedString(BinaryReader br)
        {
            List<byte> stringC = new List<byte>();
            byte newByte = br.ReadByte();
            while(newByte != 0)
            {
                stringC.Add(newByte);
                newByte = br.ReadByte();
            }
            return Encoding.ASCII.GetString(stringC.ToArray());
        }

        public static void WriteNullTerminatedString(BinaryWriter bw, string str)
        {
            bw.Write(str.ToCharArray());
            bw.Write((byte)0);
        }

        public static string ReadUniNullTerminatedString(BinaryReader br)
        {
            List<byte> stringC = new List<byte>();
            byte newByte = br.ReadByte();
            byte newByte2 = br.ReadByte();
            while (newByte != 0 || newByte2 != 0)
            {
                stringC.Add(newByte);
                stringC.Add(newByte2);
                newByte = br.ReadByte();
                newByte2 = br.ReadByte();
            }
            return Encoding.Unicode.GetString(stringC.ToArray());
        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }
    }
}
