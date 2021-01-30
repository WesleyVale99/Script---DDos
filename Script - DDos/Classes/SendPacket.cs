using System;
using System.IO;
using System.Text;

namespace Script___DDos.Classes
{
    public class SendPacket
    {

        public MemoryStream stream;
        public SendPacket()
        {
            stream = new MemoryStream();
        }
        public void WriteC(byte v) { stream.WriteByte(v); }
        public void WriteB(byte[] v) { stream.Write(v, 0, v.Length); } //Value
        public void WriteD(int v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteUD(uint v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteH(short v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteH(ushort v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteF(double v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteQ(long v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteQ(ulong v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteS(string name, int count)
        {
            if (name == null)
                name = "";
            WriteB(Encoding.GetEncoding(1251).GetBytes(name));
            WriteB(new byte[count - name.Length]);
        }
        public void Close()
        {
            try
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
                stream = null;
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}

