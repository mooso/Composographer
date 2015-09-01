using Composographer.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Composographer.ViewModels
{
	public sealed class CompoProjectViewModel : INotifyPropertyChanged
	{
		private CompoProject _project;
		public event PropertyChangedEventHandler PropertyChanged;
		private ObservableCollection<CompoFrameViewModel> _frames = new ObservableCollection<CompoFrameViewModel>();
		private uint _pixelWidth;
		private uint _pixelHeight;
		private WriteableBitmap _image;
		private double _pixelsPerInch;
		private FitMode _fitMode;

		public CompoProjectViewModel()
		{
			_pixelHeight = _pixelWidth = 100;
		}

		public string ImageFilePath { get { return _project == null ? null : _project.ImageFilePath; } }
		public ImageSource Image { get { return _image; } }
		public double PixelsPerInch { get { return _pixelsPerInch; } }
		public ObservableCollection<CompoFrameViewModel> Frames { get { return _frames; } }

		public CompoProject Project
		{
			get { return _project; }
		}

		public async Task Scale(uint pixelWidth, uint pixelHeight)
		{
			_pixelWidth = pixelWidth;
			_pixelHeight = pixelHeight;
			CalculateFit();
			await RefreshImage();
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("Image"));
			}
			foreach (var frame in _frames)
			{
				await frame.ScaleFrame(_pixelsPerInch);
			}
		}

		public async Task LoadProject(CompoProject project)
		{
			_project = project;
			CalculateFit();
			await RefreshImage();
			if (project == null)
			{
				_frames.Clear();
			}
			else
			{
				_frames.Clear();
				foreach (var frame in project.Frames)
				{
					var frameModel = new CompoFrameViewModel();
					await frameModel.LoadFrame(frame, _pixelsPerInch);
					_frames.Add(frameModel);
				}
			}
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("ImageFilePath"));
				PropertyChanged(this, new PropertyChangedEventArgs("Image"));
				PropertyChanged(this, new PropertyChangedEventArgs("Frames"));
			}
		}

		private void CalculateFit()
		{
			if (_project == null || _project.FullHeight == 0 || _pixelHeight == 0)
			{
				_fitMode = FitMode.FitWidth;
				_pixelsPerInch = 0;
				return;
			}
			var pixelAspectRatio = (double)_pixelWidth / _pixelHeight;
			var imageAspectRatio = _project.FullWidth / _project.FullHeight;
			if (pixelAspectRatio >= imageAspectRatio)
			{
				_fitMode = FitMode.FitHeight;
				_pixelsPerInch = _pixelHeight / _project.FullHeight;
			}
			else
			{
				_fitMode = FitMode.FitWidth;
				_pixelsPerInch = _pixelWidth / _project.FullWidth;
			}
		}

		private async Task RefreshImage()
		{
			if (_project == null || _project.ImageFilePath == null)
			{
				_image = null;
				return;
			}
			_image = await ImageLoader.LoadScaled(_project.ImageFilePath, _pixelWidth, _pixelHeight);
    }
	}
}
