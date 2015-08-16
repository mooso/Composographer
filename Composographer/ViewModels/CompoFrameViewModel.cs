using Composographer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composographer.ViewModels
{
	public sealed class CompoFrameViewModel : INotifyPropertyChanged
	{
		private readonly CompoFrame _frame;
		private readonly double _pixelsPerInch;

		public event PropertyChangedEventHandler PropertyChanged;

		public CompoFrameViewModel(CompoFrame frame, double pixelsPerInch)
		{
			_frame = frame;
			_pixelsPerInch = pixelsPerInch;
		}

		public double X { get { return _frame.X * _pixelsPerInch; } }
		public double Y { get { return _frame.Y * _pixelsPerInch; } }
		public double Width { get { return _frame.Width * _pixelsPerInch; } }
		public double Height { get { return _frame.Height * _pixelsPerInch; } }
	}
}
