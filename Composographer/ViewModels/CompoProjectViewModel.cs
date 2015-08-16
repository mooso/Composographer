using Composographer.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Composographer.ViewModels
{
	public sealed class CompoProjectViewModel : INotifyPropertyChanged
	{
		private CompoProject _project;
		public event PropertyChangedEventHandler PropertyChanged;
		private ObservableCollection<CompoFrameViewModel> _frames;

		public CompoProjectViewModel()
		{
		}

		public string ImageFilePath { get { return _project == null ? null : _project.ImageFilePath; } }
		public BitmapImage Image { get { return _project == null ? null : _project.Image; } }
		public ObservableCollection<CompoFrameViewModel> Frames { get { return _frames; } }

		public CompoProject Project
		{
			get { return _project; }
			set
			{
				_project = value;
				_frames = new ObservableCollection<CompoFrameViewModel>(value.Frames.Select(f => new CompoFrameViewModel(f, value.PixelsPerInch)));
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ImageFilePath"));
					PropertyChanged(this, new PropertyChangedEventArgs("Image"));
					PropertyChanged(this, new PropertyChangedEventArgs("Frames"));
				}
			}
		}
	}
}
