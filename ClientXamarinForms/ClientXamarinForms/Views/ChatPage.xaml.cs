using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClientXamarinForms.ViewModels;

namespace ClientXamarinForms.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatPage : ContentPage
	{
		public ChatPage()
		{
			InitializeComponent();

			BindingContext = new ChatVM();
		}
	}
}