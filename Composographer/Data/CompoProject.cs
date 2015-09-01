using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
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
		private const double DefaultWidth = 24;

		private CompoProject(string imageFilePath,
			double fullHeight, double fullWidth,
			IEnumerable<CompoFrame> frames)
		{
			_imageFilePath = imageFilePath;
			_fullHeight = fullHeight;
			_fullWidth = fullWidth;
			_frames = frames.ToList();
		}

		public string ImageFilePath { get { return _imageFilePath; } }
		public double FullWidth { get { return _fullWidth; } }
		public double FullHeight { get { return _fullHeight; } }
		public IEnumerable<CompoFrame> Frames { get { return _frames; } }

		public async static Task<CompoProject> NewFromImage(StorageFile imageFile)
		{
			double height;
			using (var fileStream = await imageFile.OpenReadAsync())
			{
				var decoder = await BitmapDecoder.CreateAsync(fileStream);
				height = decoder.PixelHeight * DefaultWidth / decoder.PixelWidth;
			}
			return new CompoProject(
				imageFilePath: imageFile.Path,
				fullHeight: height,
				fullWidth: DefaultWidth,
				frames: Gridify(DefaultWidth, height, 2, 2)
			);
		}

		private static IEnumerable<CompoFrame> Gridify(double width, double height, int numHorizontal, int numVertical)
		{
			double frameWidth = width / numHorizontal,
				frameHeight = height / numVertical;
			for (int row = 0; row < numVertical; row++)
				for (int column = 0; column < numHorizontal; column++)
				{
					yield return new CompoFrame(
						x: column * frameWidth,
						y: row * frameHeight,
						width: frameWidth,
						height: frameHeight);
				}
		}
	}
}
