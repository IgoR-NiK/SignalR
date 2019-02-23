using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientXamarinForms.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaintPage : ContentPage
	{
		public PaintPage()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);
			MessagingCenter.Subscribe<PalettePage, Color>(this, "ColorChanged", (page, color) => boxView.BackgroundColor = color);
		}

		private async void BtnChangeColor_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new PalettePage());
		}
	}
}