using Composographer.Data;
using System;
using System.Collections.Generic;
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

		public CompoProjectViewModel()
		{
		}

		public string ImageFilePath { get { return _project == null ? null : _project.ImageFilePath; } }
		public BitmapImage Image { get { return _project == null ? null : _project.Image; } }

		public CompoProject Project
		{
			get { return _project; }
			set
			{
				_project = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ImageFilePath"));
					PropertyChanged(this, new PropertyChangedEventArgs("Image"));
				}
			}
		}
	}
}
