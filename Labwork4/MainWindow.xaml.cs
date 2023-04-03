using KMA.ProgrammingInCSharp.Labwork4.ViewModels;
using System.Windows;

namespace KMA.ProgrammingInCSharp.Labwork4
{
	public partial class MainWindow : Window
	{
		private MainWindowViewModel _viewModel;
		public MainWindow()
		{
			InitializeComponent();
			DataContext = _viewModel = new MainWindowViewModel();
		}
	}
}
