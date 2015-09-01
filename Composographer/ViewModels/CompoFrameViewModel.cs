using Composographer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Composographer.ViewModels
{
	public sealed class CompoFrameViewModel : INotifyPropertyChanged
	{
		private CompoFrame _frame;
		private double _pixelsPerInch;
		private WriteableBitmap _image;

		public event PropertyChangedEventHandler PropertyChanged;

		public CompoFrameViewModel()
		{
		}

		public uint X { get { return _frame == null ? 0 : ScaleToPixels(_frame.X); } }
		public uint Y { get { return _frame == null ? 0 : ScaleToPixels(_frame.Y); } }
		public uint Width { get { return _frame == null ? 0 : ScaleToPixels(_frame.Width); } }
		public uint Height { get { return _frame == null ? 0 : ScaleToPixels(_frame.Height); } }
		public ImageSource Image { get { return _image; } }
		public CompoFrame Frame { get { return _frame; } }

		private uint ScaleToPixels(double inches)
		{
			return (uint)(inches * _pixelsPerInch);
		}

		public async Task LoadFrame(CompoFrame frame, double pixelsPerInch)
		{
			_frame = frame;
			await ScaleFrame(pixelsPerInch);
		}

		public async Task ScaleFrame(double pixelsPerInch)
		{
			_pixelsPerInch = pixelsPerInch;
			if (_frame != null && _frame.ImageFilePath != null)
			{
				_image = await ImageLoader.LoadScaled(_frame.ImageFilePath, (uint)(_frame.Width * pixelsPerInch),
					(uint)(_frame.Height * pixelsPerInch));
			}
			else
			{
				_image = null;
			}
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("X"));
				PropertyChanged(this, new PropertyChangedEventArgs("Y"));
				PropertyChanged(this, new PropertyChangedEventArgs("Image"));
			}
		}
	}
}
