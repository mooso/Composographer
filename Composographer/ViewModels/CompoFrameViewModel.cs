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

		public event PropertyChangedEventHandler PropertyChanged;

		public CompoFrameViewModel(CompoFrame frame)
		{
			_frame = frame;
		}

		public double X { get { return _frame.X; } }
		public double Y { get { return _frame.Y; } }
		public double Width { get { return _frame.Width; } }
		public double Height { get { return _frame.Height; } }
	}
}
