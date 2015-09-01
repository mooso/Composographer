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
			_mainCanvas.SizeChanged += async (sender, args) =>
			{
				await _viewModel.Scale((uint)args.NewSize.Width, (uint)args.NewSize.Height);
			};
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.
		/// This parameter is typically used to configure the page.</param>
		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			StorageFile file;
			CompoProject project;
			if ((file = e.Parameter as StorageFile) != null)
			{
				project = await CompoProject.NewFromImage(file);
			}
			else
			{
				project = e.Parameter as CompoProject;
      }
			if (project != null)
			{
				await _viewModel.LoadProject(project);
				RefreshCanvas();
			}
			HardwareButtons.BackPressed += HandleBackFromOpen;
		}

		public CompoProjectViewModel ViewModel{ get { return _viewModel; } }

		private void RefreshCanvas()
		{
			_mainCanvas.Children.Clear();
			var mainImage = new Image() { Source = _viewModel.Image };
			mainImage.PointerPressed += MainImage_PointerPressed;
			_mainCanvas.Children.Add(mainImage);
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
				if (frame.Image != null)
				{
					var frameImage = new Image()
					{
						Width = frame.Width,
						Height = frame.Height,
						Source = frame.Image,
					};
					frameImage.SetValue(Canvas.LeftProperty, frame.X);
					frameImage.SetValue(Canvas.TopProperty, frame.Y);
					_mainCanvas.Children.Add(frameImage);
				}
			}
		}

		private void MainImage_PointerPressed(object sender, PointerRoutedEventArgs e)
		{
			var position = e.GetCurrentPoint(_mainCanvas).Position;
			foreach (var frame in _viewModel.Frames)
			{
				if (position.X > frame.X && position.X < (frame.X + frame.Width)
					&& position.Y > frame.Y && position.Y < (frame.Y + frame.Height))
				{
					Frame.Navigate(typeof(ImageCapturePage), new ImageCapturePageArguments(_viewModel.Project, frame.Frame));
				}
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
