using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Composographer
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();

			this.NavigationCacheMode = NavigationCacheMode.Required;
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.
		/// This parameter is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			// TODO: Prepare page for display here.

			// TODO: If your application contains multiple pages, ensure that you are
			// handling the hardware Back button by registering for the
			// Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
			// If you are using the NavigationHelper provided by some templates,
			// this event is handled for you.
			CoreApplication.GetCurrentView().Activated += MainPage_Activated;
    }

		private void MainPage_Activated(CoreApplicationView sender, IActivatedEventArgs args)
		{
			var openPickerArgs = args as FileOpenPickerContinuationEventArgs;
			if (openPickerArgs != null && openPickerArgs.Files.Count == 1)
			{
				Frame.Navigate(typeof(FramingPage), openPickerArgs.Files[0]);
			}
		}

		private void newProjectButton_Click(object sender, RoutedEventArgs e)
		{
			var picker = new FileOpenPicker()
			{
				ViewMode = PickerViewMode.Thumbnail,
				CommitButtonText = "Select framing image",
				SuggestedStartLocation = PickerLocationId.PicturesLibrary,
			};
			picker.FileTypeFilter.Add(".jpg");
			picker.PickSingleFileAndContinue();
		}
	}
}
