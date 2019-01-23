using System;
using Microsoft.AspNet.SignalR.Client;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace ClientXamarinForms.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatPage : ContentPage
	{
		HubConnection hubConnection;
		IHubProxy hubProxy;

		public string UserName { get; set; }

		private string message;
		public string Message
		{
			get => message;
			set
			{
				message = value;
				OnPropertyChanged("Message");
			}
		}

		public ObservableCollection<MessageData> Messages { get; } = new ObservableCollection<MessageData>();

		public ChatPage()
		{
			InitializeComponent();

			BindingContext = this;

			hubConnection = new HubConnection("http://www.signalrtest.somee.com/signalr");
			hubProxy = hubConnection.CreateHubProxy("MyHub");

			hubProxy.On<string, string>("sendMessageClient", (name, message) => Messages.Add(new MessageData() { User = name, Message = message }));			
			hubConnection.Start();
		}

		private void BtnSend_Clicked(object sender, EventArgs e)
		{
			hubProxy.Invoke("SendMessageServer", UserName, Message);
			Message = String.Empty;
		}
	}

	public class MessageData
	{
		public string Message { get; set; }
		public string User { get; set; }
	}
}