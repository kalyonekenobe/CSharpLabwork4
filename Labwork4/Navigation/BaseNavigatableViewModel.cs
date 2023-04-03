using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KMA.ProgrammingInCSharp.Labwork4.Navigation
{
	internal abstract class BaseNavigatableViewModel<TObject> : INotifyPropertyChanged where TObject : Enum
	{
		private INavigatable<TObject>? _currentViewModel;
		public event PropertyChangedEventHandler? PropertyChanged;

		public INavigatable<TObject>? CurrentViewModel
		{
			get => _currentViewModel;
			private set
			{
				if (_currentViewModel == value)
					return;
				_currentViewModel = value;
				NotifyPropertyChanged(nameof(CurrentViewModel));
			}
		}

		internal void Navigate(TObject type)
		{
			if (CurrentViewModel is not null && CurrentViewModel.ViewType.Equals(type))
				return;

			INavigatable<TObject>? viewModel = GetViewModel(type);

			if (viewModel is null)
				return;

			CurrentViewModel = viewModel;
		}

		internal void Navigate<TArgument>(TObject type, TArgument argument)
		{
			if (CurrentViewModel is not null && CurrentViewModel.ViewType.Equals(type))
				return;

			INavigatable<TObject>? viewModel = GetViewModel(type, argument);

			if (viewModel is null)
				return;

			CurrentViewModel = viewModel;
		}

		private INavigatable<TObject>? GetViewModel(TObject type)
		{
			return CreateViewModel(type);
		}

		private INavigatable<TObject>? GetViewModel<TArgument>(TObject type, TArgument argument)
		{
			return CreateViewModel(type, argument);
		}

		protected abstract INavigatable<TObject>? CreateViewModel(TObject type);
		protected abstract INavigatable<TObject>? CreateViewModel<TArgument>(TObject type, TArgument argument);

		protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
