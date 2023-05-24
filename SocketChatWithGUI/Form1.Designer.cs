namespace SocketChatWithGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.Connect = new System.Windows.Forms.Button();
            this.Host = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OutputBox
            // 
            this.OutputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputBox.Location = new System.Drawing.Point(4, 33);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputBox.Size = new System.Drawing.Size(784, 372);
            this.OutputBox.TabIndex = 0;
            // 
            // InputBox
            // 
            this.InputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputBox.Location = new System.Drawing.Point(4, 415);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(681, 23);
            this.InputBox.TabIndex = 1;
            // 
            // Send
            // 
            this.Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Send.Location = new System.Drawing.Point(691, 414);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(97, 23);
            this.Send.TabIndex = 2;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // IPBox
            // 
            this.IPBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IPBox.Location = new System.Drawing.Point(4, 4);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(570, 23);
            this.IPBox.TabIndex = 1;
            this.IPBox.Text = "127.0.0.1";
            // 
            // Connect
            // 
            this.Connect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Connect.Location = new System.Drawing.Point(588, 4);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(97, 23);
            this.Connect.TabIndex = 2;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Host
            // 
            this.Host.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Host.Location = new System.Drawing.Point(691, 4);
            this.Host.Name = "Host";
            this.Host.Size = new System.Drawing.Size(97, 23);
            this.Host.TabIndex = 2;
            this.Host.Text = "Host";
            this.Host.UseVisualStyleBackColor = true;
            this.Host.Click += new System.EventHandler(this.Host_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Host);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.IPBox);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.OutputBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox OutputBox;
        private TextBox InputBox;
        private Button Send;
        private TextBox IPBox;
        private Button Connect;
        private Button Host;
    }
}