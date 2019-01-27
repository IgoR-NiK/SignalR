using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ClientXamarinForms.ViewModels
{
	class ChatVM : INotifyPropertyChanged
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

		public Command SendComman { get; }

		public ChatVM()
		{
			hubConnection = new HubConnection("http://www.signalrtest.somee.com/signalr");
			hubProxy = hubConnection.CreateHubProxy("MyHub");

			hubProxy.On<string, string>("sendMessageClient", (name, message) => Messages.Add(new MessageData() { User = name, Message = message }));
			hubConnection.Start();

			SendComman = new Command(() =>
			{
				if (hubConnection.State == ConnectionState.Connected)
				{
					hubProxy.Invoke("SendMessageServer", UserName, Message);
					Message = String.Empty;
				}
			});
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public class MessageData
	{
		public string Message { get; set; }
		public string User { get; set; }
	}
}
