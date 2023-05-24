using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;
using SocketChatWithGUI;

public class Chat
{
    // Set the listening IP address and port
    IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
    int port = 8080;
    public bool isRunning = false;
    bool IsServer = false;
    TcpListener server;
    TcpClient TcpHost;
    List<TcpClient> clients;

    TextBox output;

    public event EventHandler Disconnected;

    public Chat(TextBox textBox)
    {
        output = textBox;

    }


    public void CleanUp()
    {
        isRunning = false;

        if (clients != null)
        {
            foreach (var client in clients)
            {
                client.Close();
            }
            clients.Clear();
        }
        if (server != null)
        {
            server.Stop();
        }
    }

    public bool StartClient()
    {
        IsServer = false;
        try
        {
            // Connect to the server
            TcpClient client = new TcpClient();
            client.Connect(ipAddress, port);
            TcpHost = client;
             // Start receiving messages from the server
             isRunning =true;
            Task.Run(() => ReceiveMessages(client));
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async void StartServer()
    {
        IsServer = true;
        try
        {
            clients = new List<TcpClient>();
            // Create a TCP/IP listener
            server = new TcpListener(ipAddress, port);

            // Start listening for client requests
            server.Start();
            isRunning = true;

            while (isRunning == true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                output.Invoke((MethodInvoker)delegate { output.Text += "Client Connected!" + Environment.NewLine; });

                clients.Add(client);

                // Start receiving messages from the client

                Task.Run(() => ReceiveMessages(client));
            }
            // Accept a client connection

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: {0}", ex.Message);
            Disconnected?.Invoke(this,EventArgs.Empty);
        }

    }



    async Task ReceiveMessages(TcpClient client)
    {
        NetworkStream stream= client.GetStream();
        try
        {
            while (isRunning)
            {
                byte[] buffer = new byte[client.ReceiveBufferSize];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    output.Invoke((MethodInvoker)delegate { output.Text +=receivedMessage + Environment.NewLine; });
                    if (IsServer)
                    {
                        BroadcastMessage(receivedMessage, client);
                    }

                }
                else
                {
                    if (!IsServer)
                    {
                        // Connection closed by the other end
                        isRunning = false;
                        Disconnected?.Invoke(this, EventArgs.Empty);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while receiving messages: {0}", ex.Message);
            Disconnected?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SendMessage(string message)
    {
        if (!IsServer)
        {
            NetworkStream stream = TcpHost.GetStream();
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while sending messages: {0}", ex.Message);
                Disconnected?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            BroadcastMessage(message, null);
        }

    }

    void BroadcastMessage(string message, TcpClient sender)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);

        foreach (TcpClient client in clients)
        {
            if (client != sender)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while broadcasting message: {0}", ex.Message);
                }
            }
        }
    }
}