using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    public class Chat
    {
        // Set the listening IP address and port
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 8080;
        bool isRunning = true;

        public void StartClient()
        {
            try
            {
                // Connect to the server
                TcpClient client = new TcpClient();
                client.Connect(ipAddress, port);

                // Start receiving messages from the server
                Task.Run(() => ReceiveMessages(client));

                // Start sending messages to the server
                Task.Run(() => SendMessages(client));
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: {0}", ex.Message);
            }
        }
        public void StartServer()
        {
            try
            {
                // Create a TCP/IP listener
                TcpListener server = new TcpListener(ipAddress, port);

                // Start listening for client requests
                server.Start();

                Console.WriteLine("Server listening on {0}:{1}", ipAddress, port);

                // Accept a client connection
                TcpClient client = server.AcceptTcpClient();

                // Start receiving messages from the client
                Task.Run(() => ReceiveMessages(client));

                // Start sending messages to the client
                Task.Run(() => SendMessages(client));
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: {0}", ex.Message);
            }

        }

        void ReceiveMessages(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                while (isRunning)
                {
                    byte[] buffer = new byte[client.ReceiveBufferSize];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine("Received message: {0}", receivedMessage);
                    }
                    else
                    {
                        // Connection closed by the other end
                        isRunning = false;
                        Console.WriteLine("Connection closed by the other end.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while receiving messages: {0}", ex.Message);
            }
        }

        void SendMessages(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                while (isRunning)
                {
                    string message = Console.ReadLine();

                    if (message.ToLower() == "exit")
                    {
                        // User wants to exit
                        isRunning = false;
                        client.Close();
                        break;
                    }
                    if (!string.IsNullOrEmpty(message))
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(message);
                        stream.Write(buffer, 0, buffer.Length);
                    }
           
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while sending messages: {0}", ex.Message);
            }
        }
    }



    private static async Task Main(string[] args)
    {
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        Process currentProcess = Process.GetCurrentProcess();
        currentProcess.PriorityClass = ProcessPriorityClass.Normal;

        Console.WriteLine("Tcp Socket Chat\n (1)Start Server\n (2)Start Client");

        Chat chat = new Chat();
        while (true)
        {
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.D1)
            {
                Console.Clear();
                chat.StartServer();
                break;

            }
            if (key.Key == ConsoleKey.D2)
            {
                Console.Clear();
                chat.StartClient();
                break;
            }
        }


        Console.WriteLine("press enter to exit....");
        Console.ReadLine();
        await Task.Delay(-1); // Keeps the application running
    }

}