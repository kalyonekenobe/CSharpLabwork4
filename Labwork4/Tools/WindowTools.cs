using System.Windows;

namespace KMA.ProgrammingInCSharp.Labwork4.Tools
{
	public static class WindowTools
	{
		public static void SetWindowSize(int minWidth, int minHeight, int width, int height, int maxWidth, int maxHeight)
		{
			Application.Current.MainWindow.MinHeight = minHeight;
			Application.Current.MainWindow.Height = height;
			Application.Current.MainWindow.MaxHeight = maxHeight;
			Application.Current.MainWindow.MinWidth = minWidth;
			Application.Current.MainWindow.Width = width;
			Application.Current.MainWindow.MaxWidth = maxWidth;
		}

		public static void SetWindowToCenterOfScreen()
		{
			Application.Current.MainWindow.Left = (SystemParameters.PrimaryScreenWidth / 2) - (Application.Current.MainWindow.Width / 2);
			Application.Current.MainWindow.Top = (SystemParameters.PrimaryScreenHeight / 2) - (Application.Current.MainWindow.Height / 2);
		}
	}
}
