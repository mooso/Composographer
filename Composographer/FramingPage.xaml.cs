using Composographer.Data;
using Composographer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Composographer
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class FramingPage : Page
	{
		private readonly CompoProjectViewModel _viewModel = new CompoProjectViewModel();

		public FramingPage()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.
		/// This parameter is typically used to configure the page.</param>
		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			var file = e.Parameter as StorageFile;
			if (file != null)
			{
				_viewModel.Project = await CompoProject.NewFromImage(file);
				RefreshCanvas();
				HardwareButtons.BackPressed += HandleBackFromOpen;
			}
		}

		public CompoProjectViewModel ViewModel{ get { return _viewModel; } }

		private void RefreshCanvas()
		{
			_mainCanvas.Children.Clear();
			_mainCanvas.Children.Add(new Image() { Source = _viewModel.Image });
			foreach (var frame in _viewModel.Frames)
			{
				var frameRectangle = new Rectangle()
				{
					Width = frame.Width,
					Height = frame.Height,
					Stroke = new SolidColorBrush()
					{
						Color = Colors.White,						
					},
				};
				frameRectangle.SetValue(Canvas.LeftProperty, frame.X);
				frameRectangle.SetValue(Canvas.TopProperty, frame.Y);
				_mainCanvas.Children.Add(frameRectangle);
			}
		}

		private void HandleBackFromOpen(object sender, BackPressedEventArgs args)
		{
			if (Frame != null && Frame.IsEnabled)
			{
				Frame.Navigate(typeof(MainPage));
				args.Handled = true;
				HardwareButtons.BackPressed -= HandleBackFromOpen;
			}
		}
	}
}
