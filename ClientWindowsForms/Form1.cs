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

        Data data = new Data()
        {
            StartX = 0,
            StartY = 0,
            EndX = 0,
            EndY = 0,
            ColorPen = "#000000"
        };
        bool isDrawing = false;
        float startX = 0;
        float startY = 0;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            g = picArea.CreateGraphics();

            hubConnection = new HubConnection("http://localhost:51188/signalr");
            hubProxy = hubConnection.CreateHubProxy("MyHub");
            
            hubProxy.On<string>("sendMessageClient", (message) => txtMessage.Invoke(new Action(() => txtMessages.Text += message + Environment.NewLine)));
            hubProxy.On<Data>("drawClient", (data) =>
            {
                DrawLine(data.StartX, data.StartY, data.EndX, data.EndY, data.ColorPen);
            });
            hubConnection.Start();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            hubProxy.Invoke("SendMessageServer", txtMessage.Text);
            txtMessage.Clear();
        }

        private void DrawLine(float startX, float startY, float endX, float endY, string colorPen)
        {
            Color color = ColorTranslator.FromHtml(colorPen);
            g.DrawLine(new Pen(color, 1), startX, startY, endX, endY);
        }

        private void picArea_MouseDown(object sender, MouseEventArgs e)
        {            
            var mouseX = e.X;
            var mouseY = e.Y;
            startX = mouseX;
            startY = mouseY;
            isDrawing = true;
        }

        private void picArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                var mouseX = e.X;
                var mouseY = e.Y;
                if (!(mouseX == startX &&
                    mouseY == startY))
                {
                    DrawLine(startX, startY, mouseX, mouseY, data.ColorPen);

                    data.StartX = startX;
                    data.StartY = startY;
                    data.EndX = mouseX;
                    data.EndY = mouseY;

                    hubProxy.Invoke("DrawServer", data);

                    startX = mouseX;
                    startY = mouseY;
                }
            }
        }

        private void picArea_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void lblSelectColor_Click(object sender, EventArgs e)
        {
            SelectColor();
        }

        private void picSelectColor_Click(object sender, EventArgs e)
        {
            SelectColor();
        }

        private void SelectColor()
        {
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            
            data.ColorPen = ColorTranslator.ToHtml(colorDialog.Color);
            picSelectColor.BackColor = colorDialog.Color;
        }
    }
}
