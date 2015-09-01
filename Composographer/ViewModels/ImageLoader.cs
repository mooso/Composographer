using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Composographer.ViewModels
{
	static class ImageLoader
	{
		public static async Task<WriteableBitmap> LoadScaled(string filePath, uint pixelWidth, uint pixelHeight)
		{
			var image = new WriteableBitmap((int)pixelWidth, (int)pixelHeight);
			if (pixelWidth == 0 || pixelHeight == 0)
			{
				return image;
			}
			var file = await StorageFile.GetFileFromPathAsync(filePath);
			using (var fileStream = await file.OpenReadAsync())
			{
				var decoder = await BitmapDecoder.CreateAsync(fileStream);
				var fitX = (double)pixelWidth / decoder.PixelWidth;
				var fitY = (double)pixelHeight / decoder.PixelHeight;
				var minScale = Math.Min(fitX, fitY);
				using (var inMemoryStream = new InMemoryRandomAccessStream())
				{
					var encoder = await BitmapEncoder.CreateForTranscodingAsync(inMemoryStream, decoder);
					encoder.BitmapTransform.ScaledHeight = (uint)(minScale * decoder.PixelHeight);
					encoder.BitmapTransform.ScaledWidth = (uint)(minScale * decoder.PixelWidth);
					await encoder.FlushAsync();
					inMemoryStream.Seek(0);
					await image.SetSourceAsync(inMemoryStream);
					return image;
				}
			}
		}
	}
}
