using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWindowsForms
{
    public partial class Form1 : Form
    {
        HubConnection hubConnection;
        IHubProxy hubProxy;
        SynchronizationContext context;

        public Form1()
        {
            InitializeComponent();

            hubConnection = new HubConnection("http://localhost:51188/signalr");
            hubProxy = hubConnection.CreateHubProxy("MyHub");
            context = SynchronizationContext.Current;

            hubProxy.On<string>("sendMessageClient", (message) =>
              context.Post(delegate
              {
                  txtMessages.Text += message + Environment.NewLine;
              }, null));
            hubConnection.Start();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            hubProxy.Invoke("SendMessageServer", txtMessage.Text);
            txtMessage.Clear();
        }
    }
}
