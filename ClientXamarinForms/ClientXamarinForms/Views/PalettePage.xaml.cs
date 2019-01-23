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
	public partial class PalettePage : ContentPage
	{
		public PalettePage ()
		{
			InitializeComponent ();

			var colors = new[,] { { Color.IndianRed, Color.Red, Color.DarkRed },
								  { Color.Yellow, Color.Orange, Color.DarkOrange },
								  { Color.Pink, Color.HotPink, Color.MediumVioletRed },
								  { Color.Plum, Color.MediumPurple, Color.DarkOrchid },
								  { Color.SkyBlue, Color.DodgerBlue, Color.MidnightBlue },
								  { Color.LightGreen, Color.SpringGreen, Color.DarkGreen },
								  { Color.LightGray, Color.Gray, Color.Black } };

			var scrollView = new ScrollView();
			var stackLayer = new StackLayout();

			stackLayer.Children.Add(new Label()
			{
				Text = "Палитра",
				FontSize = 28,
				TextColor = Color.DodgerBlue,
				VerticalOptions  = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Margin = new Thickness(0, 5)
			});
			for (var i = 0; i < colors.GetLength(0); i++)
			{
				var grid = new Grid
				{
					ColumnDefinitions = new ColumnDefinitionCollection()
					{
						new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
						new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
						new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
					}
				};

				for (var j = 0; j < colors.GetLength(1); j++)
				{
					var boxView = new BoxView()
					{
						BackgroundColor = colors[i, j],
						WidthRequest = 100,
						HeightRequest = 100,
						Margin = new Thickness(10)
					};

					boxView.GestureRecognizers.Add(new TapGestureRecognizer(async view =>
					{
						await Navigation.PopModalAsync();
						MessagingCenter.Send<PalettePage, View>(this, "ColorChanged", view);
					}));
					grid.Children.Add(boxView, j, 0);
				}

				stackLayer.Children.Add(grid);
			}

			scrollView.Content = stackLayer;
			Content = scrollView;
		}
	}
}