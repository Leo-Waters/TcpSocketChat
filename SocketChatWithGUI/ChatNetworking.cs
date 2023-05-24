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

    TcpListener server;
    TcpClient client;
    NetworkStream stream;

    TextBox output;

    public event EventHandler Disconnected;

    public Chat(TextBox textBox)
    {
        output = textBox;

    }


    public void CleanUp()
    {
        isRunning = false;
        if (stream!=null)
        {
            stream.Close();
            
            
        }
        if (client!=null)
        {
            client.Close();
        }
        if (server != null)
        {
            server.Stop();
        }
    }

    public bool StartClient()
    {
        try
        {
            // Connect to the server
            client = new TcpClient();
            client.Connect(ipAddress, port);
           
            stream = client.GetStream();

            // Start receiving messages from the server
            isRunning=true;
            Task.Run(() => ReceiveMessages());
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async void StartServer()
    {
        try
        {
            // Create a TCP/IP listener
            server = new TcpListener(ipAddress, port);

            // Start listening for client requests
            server.Start();
            // Accept a client connection
            client = await server.AcceptTcpClientAsync();
            output.Invoke((MethodInvoker)delegate { output.Text += "Client Connected!" + Environment.NewLine; });

            server.Stop();

            stream = client.GetStream();

            // Start receiving messages from the client
            isRunning = true;
            Task.Run(() => ReceiveMessages());
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: {0}", ex.Message);
            Disconnected?.Invoke(this,EventArgs.Empty);
        }

    }



    async Task ReceiveMessages()
    {
        try
        {
            while (isRunning)
            {
                byte[] buffer = new byte[client.ReceiveBufferSize];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    output.Invoke((MethodInvoker)delegate { output.Text +="Them: "+receivedMessage + Environment.NewLine; });

                }
                else
                {
                    // Connection closed by the other end
                    isRunning = false;
                    Disconnected?.Invoke(this, EventArgs.Empty);
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
}