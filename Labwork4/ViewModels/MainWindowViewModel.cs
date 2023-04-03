using KMA.ProgrammingInCSharp.Labwork4.Models;
using KMA.ProgrammingInCSharp.Labwork4.Navigation;
using System.Windows;

namespace KMA.ProgrammingInCSharp.Labwork4.ViewModels
{
	internal class MainWindowViewModel : BaseNavigatableViewModel<MainNavigationTypes>, INavigatable<ApplicationNavigationTypes>
	{
		private bool _isEnabled = true;

		public bool IsEnabled
		{
			get
			{
				return _isEnabled;
			}
			set
			{
				_isEnabled = value;
				NotifyPropertyChanged(nameof(IsEnabled));
			}
		}

		public ApplicationNavigationTypes ViewType
		{
			get => ApplicationNavigationTypes.Main;
		}

		public MainWindowViewModel()
		{
			Navigate(MainNavigationTypes.UserList);
		}

		protected override INavigatable<MainNavigationTypes>? CreateViewModel(MainNavigationTypes type)
		{
			switch (type)
			{
				case MainNavigationTypes.UserList:
					return new UserListViewModel(() => Navigate(MainNavigationTypes.CreateUser), person => Navigate(MainNavigationTypes.EditUser, person));
				case MainNavigationTypes.CreateUser:
					return new CreateUserViewModel(() => Navigate(MainNavigationTypes.UserList));
				default:
					return null;
			}
		}

		protected override INavigatable<MainNavigationTypes>? CreateViewModel<TArgument>(MainNavigationTypes type, TArgument argument)
		{
			switch (type)
			{
				case MainNavigationTypes.EditUser:
					return new EditUserViewModel((argument as Person)!, () => Navigate(MainNavigationTypes.UserList));
				default:
					return null;
			}
		}
	}
}
