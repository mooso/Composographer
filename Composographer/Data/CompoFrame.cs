﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Composographer.Data
{
	public sealed class CompoFrame
	{
		private double _x;
		private double _y;
		private double _width;
		private double _height;
		private BitmapImage _image;
		private string _imageFilePath;

		public CompoFrame(double x, double y, double width, double height)
		{
			_x = x;
			_y = y;
			_width = width;
			_height = height;
		}

		public double X { get { return _x; } set { _x = value; } }
		public double Y { get { return _y; } set { _y = value; } }
		public double Width { get { return _width; } set { _width = value; } }
		public double Height { get { return _height; } set { _height = value; } }

		public void SetImage(string imageFilePath, BitmapImage image)
		{
			_image = image;
			_imageFilePath = imageFilePath;
		}

		public BitmapImage Image { get { return _image; } }
	}
}
