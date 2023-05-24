using System;

namespace SocketChatWithGUI
{
    public partial class Form1 : Form
    {
        bool IsConnected = false;
        Chat chat;
        public Form1()
        {
            InitializeComponent();
            chat = new Chat(OutputBox);
            chat.Disconnected += Chat_Disconnected;
            ConnectedUI(false);
            this.FormClosing += Form1_FormClosing;
        }

        private void Chat_Disconnected(object? sender, EventArgs e)
        {
            AddMessage("Disconnected!!");
            chat.CleanUp();
            IsConnected= false;
            ConnectedUI(false);
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            chat.CleanUp();
            chat.Disconnected -= Chat_Disconnected;
        }

        void ConnectedUI(bool Connected)
        {
            IPBox.ReadOnly = Connected;
            Connect.Text = Connected ? "Disconnect" : "Connect";
            Send.Enabled = Connected;
            InputBox.Enabled = Connected;
            Host.Visible = !Connected;
        }


        private void Host_Click(object sender, EventArgs e)
        {
            OutputBox.Text = "";
            IsConnected = true;
            chat.StartServer();
            AddMessage("Listerning for client on port: 8080 ");
            ConnectedUI(true);
        }
        private void Connect_Click(object sender, EventArgs e)
        {
           
            IsConnected = !IsConnected;
            ConnectedUI(IsConnected);
            if (IsConnected)
            {
                OutputBox.Text = "";
                AddMessage("Connecting to " + IPBox.Text + ":8080");
                if (chat.StartClient())
                {
                    AddMessage("Connected!!");
                }
                else
                {
                    AddMessage("Failed to find host!!");
                    IsConnected = false;
                    ConnectedUI(IsConnected);
                }
            }
            else
            {
                chat.CleanUp();

                AddMessage("Disconnected!");
            }

        }


        private void Send_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(InputBox.Text))
            {
                if (chat.isRunning)
                {
                    AddMessage("You: " + InputBox.Text);
                    chat.SendMessage(InputBox.Text);
                    InputBox.Text = "";
                }

                

            }
        }

        void AddMessage(string message)
        {
            OutputBox.Text +=message+ Environment.NewLine;
        }


    }
}