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

        List<Data> points;
		bool isConnect = false;
        bool isDrawing = false;
        float startX = 0;
        float startY = 0;
        string ColorPen = "#000000";
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            g = picArea.CreateGraphics();
            points = new List<Data>();

            hubConnection = new HubConnection("http://localhost:51188/signalr");
            hubProxy = hubConnection.CreateHubProxy("MyHub");
            
            hubProxy.On<string, string>("sendMessageClient", (name, message) => txtMessages.Invoke(new Action(() => txtMessages.Text += name + " : " + message + Environment.NewLine)));
            hubProxy.On<List<Data>>("drawClient", (e) =>
            {
                foreach(var point in e)
                {
                    DrawLine(point.StartX, point.StartY, point.EndX, point.EndY, point.ColorPen);
                }                
            });
			Connect();

			// Логика переподключения
			hubConnection.StateChanged += change =>
			{
				switch (change.NewState) {
					case Microsoft.AspNet.SignalR.Client.ConnectionState.Connected:
						isConnect = true;
						txtMessages.Invoke(new Action(() => txtMessages.Text += $"Системное сообщение : Вы подключились к чату {Environment.NewLine}"));
						break;
					case Microsoft.AspNet.SignalR.Client.ConnectionState.Reconnecting:
						isConnect = false;
						txtMessages.Invoke(new Action(() => txtMessages.Text += $"Системное сообщение : Произошло отключение от чата. Пытаемся возобновить связь... {Environment.NewLine}"));
						break;
					case Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected:
						isConnect = false;
						Invoke(new Action(() => new System.Threading.Timer(x => Connect(), null, 5000, Timeout.Infinite)));
						break;
				}
			};
		}

		private async void Connect()
		{
			try
			{
				await hubConnection.Start();
			}
			catch (Exception e) { }
		}
        
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
			if (isConnect)
			{
				hubProxy.Invoke("SendMessageServer", txtName.Text, txtMessage.Text);
				txtMessage.Clear();
			}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isConnect && points.Count > 0)
            {
                hubProxy.Invoke("DrawServer", points);
                points.Clear();
            }
        }

        private void DrawLine(float startX, float startY, float endX, float endY, string colorPen)
        {
            Color color = ColorTranslator.FromHtml(colorPen);
            try
            {
                g.DrawLine(new Pen(color, 1), startX, startY, endX, endY);
            }
            catch { }            
        }

        private void picArea_MouseDown(object sender, MouseEventArgs e)
        {            
            startX = e.X;
            startY = e.Y;
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
                    DrawLine(startX, startY, mouseX, mouseY, ColorPen);

                    Data data = new Data()
                    {
                        StartX = startX,
                        StartY = startY,
                        EndX = mouseX,
                        EndY = mouseY,
                        ColorPen = ColorPen
                    };

                    points.Add(data);                   

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
            
            ColorPen = ColorTranslator.ToHtml(colorDialog.Color);
            picSelectColor.BackColor = colorDialog.Color;
        }
    }
}
