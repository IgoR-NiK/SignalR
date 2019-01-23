using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClientXamarinForms.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ClientXamarinForms
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			MainPage = new TabbedPage()
			{
				Children =
				{
					new ChatPage() { Title = "Чат" },
					new NavigationPage(new PaintPage()) { Title = "Рисовалка" }
				}
			};
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
