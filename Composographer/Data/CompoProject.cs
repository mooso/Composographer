using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Composographer.Data
{
	public sealed class CompoProject
	{
		private string _imageFilePath;
		private double _fullHeight;
		private double _fullWidth;
		private List<CompoFrame> _frames;
		private BitmapImage _image;
		private const double DefaultWidth = 24;

		private CompoProject(string imageFilePath, BitmapImage image,
			double fullHeight, double fullWidth,
			IEnumerable<CompoFrame> frames)
		{
			_imageFilePath = imageFilePath;
			_image = image;
			_fullHeight = fullHeight;
			_fullWidth = fullWidth;
			_frames = frames.ToList();
		}

		public string ImageFilePath { get { return _imageFilePath; } }
		public BitmapImage Image { get { return _image; } }

		public async static Task<CompoProject> NewFromImage(StorageFile imageFile)
		{
			var image = new BitmapImage();
			using (var stream = await imageFile.OpenReadAsync())
			{
				await image.SetSourceAsync(stream);
			}
			return new CompoProject(
				imageFilePath: imageFile.Path,
				image: image,
				fullHeight: image.PixelHeight * DefaultWidth / image.PixelWidth,
				fullWidth: DefaultWidth,
				frames: Enumerable.Empty<CompoFrame>()
			);
		}
	}
}
