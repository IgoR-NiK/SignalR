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

		public bool IsConnect { get; set; } = false;
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
			hubConnection = new HubConnection("http://localhost:51188/signalr");
			hubProxy = hubConnection.CreateHubProxy("MyHub");

			hubProxy.On<string, string>("sendMessageClient", (name, message) => Messages.Add(new MessageData() { User = name, Message = message }));
			Connect();

			// Логика переподключения
			hubConnection.StateChanged += change =>
			{
				switch (change.NewState)
				{
					case ConnectionState.Connected:
						IsConnect = true;
						Messages.Add(new MessageData() { User = "Системное сообщение", Message = "Вы подключились к чату" });
						break;
					case ConnectionState.Reconnecting:
						IsConnect = false;
						Messages.Add(new MessageData() { User = "Системное сообщение", Message = "Произошло отключение от чата. Пытаемся возобновить связь..." });
						break;
					case ConnectionState.Disconnected:
						IsConnect = false;
						Device.StartTimer(TimeSpan.FromSeconds(5), () => { Connect(); return false; });
						break;
				}
			};

			SendComman = new Command(() =>
			{
				if (IsConnect)
				{
					hubProxy.Invoke("SendMessageServer", UserName, Message);
					Message = String.Empty;
				}
			}, () => IsConnect);
		}

		private async void Connect()
		{
			try
			{
				await hubConnection.Start();
			}
			catch (Exception e) { }
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
