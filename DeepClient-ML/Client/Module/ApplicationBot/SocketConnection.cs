using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeepCore.Client.Module.ApplicationBot
{
    internal class SocketConnection
    {
        private static List<Socket> ServerHandlers = new List<Socket>();
        private static Socket _listenerSocket;
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        public static List<Socket> ServerHandler = new List<Socket>();
        public static void SendCommandToClients(string Command)
        {
            DeepConsole.Log("Server", string.Format("Sending Message ({0})",Command));
            List<Socket> list = (from s in ServerHandlers
                                 where s != null
                                 select s).ToList<Socket>();
            foreach (Socket socket in list)
            {
                try
                {
                    socket.Send(Encoding.ASCII.GetBytes(Command));
                }
                catch
                {
                }
            }
        }
        public static void OnClientReceiveCommand(string Command)
        {
            DeepConsole.Log("Bot",(string.Format("Received Message ({0})",Command)));
            Bot.ReceiveCommand(Command);
        }
        public static void StartServer()
        {
            ServerHandlers.Clear();
            Task.Run(new Action(HandleServer));
        }
        public static void StopServer()
        {
            _cancellationTokenSource.Cancel();
            _listenerSocket?.Close();
            _listenerSocket = null;
            lock (ServerHandlers)
            {
                foreach (var client in ServerHandlers)
                {
                    client.Close();
                }
                ServerHandlers.Clear();
            }
            DeepConsole.Log("Server", "Server stopped.");
        }
        private static void HandleServer()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry("localhost");
            IPAddress ipaddress = hostEntry.AddressList[0];
            IPEndPoint localEP = new IPEndPoint(ipaddress, 11000);
            try
            {
                Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(localEP);
                socket.Listen(10);
                DeepConsole.Log("Server", "Waiting for connections...");
                for (;;)
                {
                    ServerHandlers.Add(socket.Accept());
                }
            }
            catch (Exception ex)
            {
                DeepConsole.Log("Server", ex.ToString());
            }
        }
        public static void Client()
        {
            Task.Run(new Action(HandleClient));
        }
        public static void HandleClient()
        {
            byte[] array = new byte[1024];
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry("localhost");
                IPAddress ipaddress = hostEntry.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipaddress, 11000);
                Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socket.Connect(remoteEP);
                    DeepConsole.Log("Bot", "Socket connected to {0}" + socket.RemoteEndPoint.ToString());
                    for (; ; )
                    {
                        int count = socket.Receive(array);
                        OnClientReceiveCommand(Encoding.ASCII.GetString(array, 0, count));
                    }
                }
                catch (ArgumentNullException ex)
                {
                    DeepConsole.Log("Bot", "ArgumentNullException : {0}" + ex.ToString());
                }
                catch (SocketException ex2)
                {
                    DeepConsole.Log("Bot", "SocketException : {0}" + ex2.ToString());
                }
                catch (Exception ex3)
                {
                    DeepConsole.Log("Bot", "Unexpected exception : {0}" + ex3.ToString());
                }
            }
            catch (Exception ex4)
            {
                DeepConsole.Log("Bot", ex4.ToString());
            }
        }
    }
}