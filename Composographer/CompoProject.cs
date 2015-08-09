using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Composographer
{
	sealed class CompoProject
	{
		private readonly Canvas _parentCanvas;
		private readonly Image _mainImage;
		private string _imageFilePath;
		private double _fullHeightInInches;
		private double _fullWidthInInches;
		private List<Rectangle> _frames;

		private CompoProject(Canvas parentCanvas, Image mainImage)
		{
			_parentCanvas = parentCanvas;
			_mainImage = mainImage;
		}

		public async static Task<CompoProject> NewFromImage(Canvas parentCanvas, StorageFile imageFile)
		{
			var image = new BitmapImage();
			using (var stream = await imageFile.OpenReadAsync())
			{
				await image.SetSourceAsync(stream);
			}
			Image mainImage = new Image() { Source = image };
			parentCanvas.Children.Add(mainImage);
			return new CompoProject(parentCanvas, mainImage);
		}
	}
}
