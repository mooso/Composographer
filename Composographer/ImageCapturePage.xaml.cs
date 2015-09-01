using Composographer.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Composographer
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ImageCapturePage : Page
	{
		private readonly MediaCapture _mediaCapture = new MediaCapture();
		private CompoProject _project;
		private CompoFrame _targetFrame;

		public ImageCapturePage()
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
			var myArgs = (ImageCapturePageArguments)e.Parameter;
			_project = myArgs.Project;
			_targetFrame = myArgs.TargetFrame;
			await _mediaCapture.InitializeAsync();
			_imageCapture.Source = _mediaCapture;
			_mediaCapture.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
			_mediaCapture.SetRecordRotation(VideoRotation.Clockwise90Degrees);
			await _mediaCapture.StartPreviewAsync();
			HardwareButtons.BackPressed += (sender, args) => Frame.Navigate(typeof(FramingPage), _project);
		}

		private async void CaptureButton_Click(object sender, RoutedEventArgs e)
		{
			await _mediaCapture.StopPreviewAsync();
			var imageFormat = ImageEncodingProperties.CreateJpeg();
			var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("FramingPhoto.jpg", CreationCollisionOption.GenerateUniqueName);
			await _mediaCapture.CapturePhotoToStorageFileAsync(imageFormat, file);
			_targetFrame.SetImage(file.Path);
			Frame.Navigate(typeof(FramingPage), _project);
		}
	}
}
