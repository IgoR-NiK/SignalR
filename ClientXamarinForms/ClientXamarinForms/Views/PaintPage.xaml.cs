using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using TouchTracking;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientXamarinForms.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaintPage : ContentPage
	{
		HubConnection hubConnection;
		IHubProxy hubProxy;

		double width;
		double height;

		public bool IsConnect { get; set; } = false;

		Dictionary<long, (SKPath, SKPaint)> inProgressPaths = new Dictionary<long, (SKPath, SKPaint)>();
		List<(SKPath, SKPaint)> completedPaths = new List<(SKPath, SKPaint)>();

		List<Data> PointsToServer = new List<Data>();				
		string ColorPen { get; set; } = "#000000";

		public PaintPage()
		{
			InitializeComponent();
			canvasView.SizeChanged += (sender, e) =>
			{
				width = canvasView.CanvasSize.Width < 5 ? canvasView.Width : canvasView.CanvasSize.Width;
				height = canvasView.CanvasSize.Height < 5 ? canvasView.Height : canvasView.CanvasSize.Height;
			};

			hubConnection = new HubConnection("http://signalrtest.somee.com/signalr");
			hubProxy = hubConnection.CreateHubProxy("MyHub");

			hubProxy.On<List<Data>>("drawClient", e =>
			{
				foreach(var point in e)
				{
					var path = new SKPath();
					var paint = new SKPaint
					{
						Style = SKPaintStyle.Stroke,
						Color = SKColor.Parse(point.ColorPen),
						StrokeWidth = 3,
						StrokeCap = SKStrokeCap.Round,
						StrokeJoin = SKStrokeJoin.Round
					};

					path.MoveTo(ConvertFromServer(point.StartX, point.StartY));
					path.LineTo(ConvertFromServer(point.EndX, point.EndY));

					completedPaths.Add((path, paint));
					Device.BeginInvokeOnMainThread(() => canvasView.InvalidateSurface());
				}				
			});
			Connect();

			// Логика переподключения
			hubConnection.StateChanged += change =>
			{
				switch (change.NewState)
				{
					case ConnectionState.Connected:
						IsConnect = true;
						break;
					case ConnectionState.Reconnecting:
						IsConnect = false;
						break;
					case ConnectionState.Disconnected:
						IsConnect = false;
						Device.BeginInvokeOnMainThread(() => Device.StartTimer(TimeSpan.FromSeconds(5), () => { Connect(); return false; }));
						break;
				}
			};

			Device.StartTimer(TimeSpan.FromMilliseconds(75), () => { UpdateServerModel(); return true; });

			NavigationPage.SetHasNavigationBar(this, false);
			MessagingCenter.Subscribe<PalettePage, Color>(this, "ColorChanged", (page, color) =>
			{
				var r = (int)(color.R * 255);
				var g = (int)(color.G * 255);
				var b = (int)(color.B * 255);

				ColorPen = $"#{r.ToString("X2")}{g.ToString("X2")}{b.ToString("X2")}";
				boxView.BackgroundColor = color;
			});
		}

		private async void Connect()
		{
			try
			{
				await hubConnection.Start();
			}
			catch (Exception e) { }
		}

		private void UpdateServerModel()
		{
			if (IsConnect && PointsToServer.Count > 0)
			{
				hubProxy.Invoke("DrawServer", PointsToServer);
				PointsToServer.Clear();
			}
		}

		void OnTouchEffectAction(object sender, TouchActionEventArgs args)
		{
			switch (args.Type)
			{
				case TouchActionType.Pressed:
					if (!inProgressPaths.ContainsKey(args.Id))
					{
						var path = new SKPath();
						path.MoveTo(ConvertToPixel(args.Location));

						var paint = new SKPaint
						{
							Style = SKPaintStyle.Stroke,
							Color = SKColor.Parse(ColorPen),
							StrokeWidth = 3,
							StrokeCap = SKStrokeCap.Round,
							StrokeJoin = SKStrokeJoin.Round
						};

						inProgressPaths.Add(args.Id, (path, paint));
						canvasView.InvalidateSurface();
					}
					break;

				case TouchActionType.Moved:
					if (inProgressPaths.ContainsKey(args.Id))
					{
						var tuple = inProgressPaths[args.Id];
						tuple.Item1.LineTo(ConvertToPixel(args.Location));
						canvasView.InvalidateSurface();		
					}
					break;

				case TouchActionType.Released:
					if (inProgressPaths.ContainsKey(args.Id))
					{
						completedPaths.Add(inProgressPaths[args.Id]);

						// Отправляем на сервер после того, как закончили рисовать 
						var points = inProgressPaths[args.Id].Item1.Points;
						for (var i = 0; i < points.Length - 1; i++)
						{
							PointsToServer.Add(new Data()
							{
								StartX = (float)(points[i].X / (width / 400)),
								StartY = (float)(points[i].Y / (height / 400)),
								EndX = (float)(points[i + 1].X / (width / 400)),
								EndY = (float)(points[i + 1].Y / (height / 400)),
								ColorPen = ColorPen
							});
						}

						inProgressPaths.Remove(args.Id);
						canvasView.InvalidateSurface();
					}
					break;

				case TouchActionType.Cancelled:
					if (inProgressPaths.ContainsKey(args.Id))
					{
						inProgressPaths.Remove(args.Id);
						canvasView.InvalidateSurface();
					}
					break;
			}
		}

		SKPoint ConvertToPixel(TouchTrackingPoint pt)
		{
			return new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
							   (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));
		}

		SKPoint ConvertFromServer(float X, float Y)
		{
			return new SKPoint((float)(X * width / 400), (float)(Y * height / 400));
		}

		void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
		{
			var canvas = args.Surface.Canvas;
			canvas.Clear();

			for (var i = 0; i < completedPaths.Count; i++)
			{
				canvas.DrawPath(completedPaths[i].Item1, completedPaths[i].Item2);
			}

			foreach (var tuple in inProgressPaths.Values)
			{
				canvas.DrawPath(tuple.Item1, tuple.Item2);
			}
		}

		private async void BtnChangeColor_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new PalettePage());
		}
	}

	public class Data
	{
		public float StartX { get; set; }
		public float StartY { get; set; }
		public float EndX { get; set; }
		public float EndY { get; set; }
		public string ColorPen { get; set; }
	}
}