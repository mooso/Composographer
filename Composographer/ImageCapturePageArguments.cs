using Composographer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composographer
{
	public sealed class ImageCapturePageArguments
	{
		private readonly CompoProject _project;
		private readonly CompoFrame _targetFrame;

		public ImageCapturePageArguments(CompoProject project, CompoFrame targetFrame)
		{
			_project = project;
			_targetFrame = targetFrame;
		}

		public CompoProject Project { get { return _project; } }
		public CompoFrame TargetFrame { get { return _targetFrame; } }
	}
}
