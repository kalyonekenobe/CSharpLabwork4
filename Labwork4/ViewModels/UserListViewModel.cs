using KMA.ProgrammingInCSharp.Labwork4.Models;
using KMA.ProgrammingInCSharp.Labwork4.Navigation;
using KMA.ProgrammingInCSharp.Labwork4.Services;
using KMA.ProgrammingInCSharp.Labwork4.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp.Labwork4.ViewModels
{
	internal class UserListViewModel : INotifyPropertyChanged, INavigatable<MainNavigationTypes>
	{
		private ObservableCollection<Person> _users;
		private UserService _userService;
		private RelayCommand<object>? _gotoCreateUserCommand;
		private RelayCommand<object>? _gotoEditUserCommand;
		private RelayCommand<object>? _removeUserCommand;
		private bool _isEnabled = true;
		private OrderBy _orderByValue;
		private Action _gotoCreateUser;
		private Action<Person> _gotoEditUser;

		public enum OrderBy { FirstName, LastName, Email, DateOfBirth, IsAdult, SunSign, ChineseSign, IsBirthdayToday }

		public event PropertyChangedEventHandler? PropertyChanged;

		public RelayCommand<object> GotoCreateUserCommand
		{
			get
			{
				return _gotoCreateUserCommand ??= new RelayCommand<object>(_ => GotoCreateUser());
			}
		}

		public RelayCommand<object> GotoEditUserCommand
		{
			get
			{
				return _gotoEditUserCommand ??= new RelayCommand<object>(email => GotoEditUser((string?)email ?? string.Empty), CanExecuteEditUser);
			}
		}

		public RelayCommand<object> RemoveUserCommand
		{
			get
			{
				return _removeUserCommand ??= new RelayCommand<object>(email => RemoveUser((string?)email ?? string.Empty), CanExecuteRemoveUser);
			}
		}

		public bool IsEnabled
		{
			get => _isEnabled;
			set
			{
				_isEnabled = value;
				NotifyPropertyChanged(nameof(IsEnabled));
			}
		}

		public ObservableCollection<Person> Users
		{
			get => _users;
			set
			{
				_users = value;
				NotifyPropertyChanged(nameof(Users));
			}
		}

		public int OrderByValue
		{
			get => (int)_orderByValue;
			set
			{
				_orderByValue = (OrderBy)value;
				IsEnabled = false;
				Users = new ObservableCollection<Person>(GetSortedUserList());
				IsEnabled = true;
				NotifyPropertyChanged(nameof(OrderByValue));
			}
		}

		public MainNavigationTypes ViewType
		{
			get => MainNavigationTypes.UserList;
		}

		public UserListViewModel(Action gotoCreateUser, Action<Person> gotoEditUser) 
		{
			_userService = new UserService();
			_gotoCreateUser = gotoCreateUser;
			_gotoEditUser = gotoEditUser;
			_users = new ObservableCollection<Person>();
			InitializeUserList();
			WindowTools.SetWindowSize(1200, 600, 1200, 600, 1400, 800);
			WindowTools.SetWindowToCenterOfScreen();
		}

		private async void InitializeUserList()
		{
			IsEnabled = false;
			List<Person> users = (await _userService.GetAllUsersAsync()).ToList();
			if (users.Count == 0)
			{
				users.AddRange(_userService.GetDefaultUsersList());
				await _userService.SaveAllUsersAsync(users);
			}
			Users = new ObservableCollection<Person>(users);
			OrderByValue = (int)OrderBy.FirstName;
			IsEnabled = true;
		}

		private void GotoCreateUser()
		{
			_gotoCreateUser.Invoke();
		}

		private void GotoEditUser(string? email)
		{
			Person? person = Users.FirstOrDefault(user => user.Email == email);
			if (person is null)
			{
				return;
			}
			_gotoEditUser.Invoke(person);
		}

		private async void RemoveUser(string email)
		{
			Person? person = await Task.Run(() => Users.FirstOrDefault(user => user.Email == email));
			if (person is not null)
			{
				Users.Remove(person);
				await _userService.RemoveUserAsync(person);
			}
		}

		private bool CanExecuteEditUser(object obj)
		{
			return IsEnabled && Users.Count > 0;
		}

		private bool CanExecuteRemoveUser(object obj)
		{
			return IsEnabled && Users.Count > 0;
		}

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private IEnumerable<Person> GetSortedUserList()
		{
			switch (_orderByValue)
			{
				case OrderBy.FirstName:
					return Users.OrderBy(user => user.FirstName).ThenBy(user => user.LastName)
								.ThenBy(user => user.Email).ThenBy(user => user.DateOfBirth)
								.ThenBy(user => user.IsAdult).ThenBy(user => Enum.Parse(typeof(ZodiacSigns.WesternAstrologyZodiacSigns), user.SunSign))
								.ThenBy(user => Enum.Parse(typeof(ZodiacSigns.ChineseAstrologyZodiacSigns), user.ChineseSign)).ThenBy(user => user.IsBirthday);
				case OrderBy.LastName:
					return Users.OrderBy(user => user.LastName).ThenBy(user => user.FirstName)
								.ThenBy(user => user.Email).ThenBy(user => user.DateOfBirth)
								.ThenBy(user => user.IsAdult).ThenBy(user => Enum.Parse(typeof(ZodiacSigns.WesternAstrologyZodiacSigns), user.SunSign))
								.ThenBy(user => Enum.Parse(typeof(ZodiacSigns.ChineseAstrologyZodiacSigns), user.ChineseSign)).ThenBy(user => user.IsBirthday);
				case OrderBy.Email:
					return Users.OrderBy(user => user.Email).ThenBy(user => user.FirstName)
								.ThenBy(user => user.LastName).ThenBy(user => user.DateOfBirth)
								.ThenBy(user => user.IsAdult).ThenBy(user => Enum.Parse(typeof(ZodiacSigns.WesternAstrologyZodiacSigns), user.SunSign))
								.ThenBy(user => Enum.Parse(typeof(ZodiacSigns.ChineseAstrologyZodiacSigns), user.ChineseSign)).ThenBy(user => user.IsBirthday);
				case OrderBy.DateOfBirth:
					return Users.OrderBy(user => user.DateOfBirth).ThenBy(user => user.FirstName)
						        .ThenBy(user => user.LastName).ThenBy(user => user.Email)
								.ThenBy(user => user.IsAdult).ThenBy(user => Enum.Parse(typeof(ZodiacSigns.WesternAstrologyZodiacSigns), user.SunSign))
								.ThenBy(user => Enum.Parse(typeof(ZodiacSigns.ChineseAstrologyZodiacSigns), user.ChineseSign)).ThenBy(user => user.IsBirthday);
				case OrderBy.IsAdult:
					return Users.OrderBy(user => user.IsAdult).ThenBy(user => user.FirstName)
								.ThenBy(user => user.LastName).ThenBy(user => user.Email)
								.ThenBy(user => user.DateOfBirth).ThenBy(user => Enum.Parse(typeof(ZodiacSigns.WesternAstrologyZodiacSigns), user.SunSign))
								.ThenBy(user => Enum.Parse(typeof(ZodiacSigns.ChineseAstrologyZodiacSigns), user.ChineseSign)).ThenBy(user => user.IsBirthday);
				case OrderBy.SunSign:
					return Users.OrderBy(user => Enum.Parse(typeof(ZodiacSigns.WesternAstrologyZodiacSigns), user.SunSign)).ThenBy(user => user.FirstName)
								.ThenBy(user => user.LastName).ThenBy(user => user.Email)
								.ThenBy(user => user.DateOfBirth).ThenBy(user => user.IsAdult)
								.ThenBy(user => Enum.Parse(typeof(ZodiacSigns.ChineseAstrologyZodiacSigns), user.ChineseSign)).ThenBy(user => user.IsBirthday);
				case OrderBy.ChineseSign:
					return Users.OrderBy(user => Enum.Parse(typeof(ZodiacSigns.ChineseAstrologyZodiacSigns), user.ChineseSign)).ThenBy(user => user.FirstName)
								.ThenBy(user => user.LastName).ThenBy(user => user.Email)
								.ThenBy(user => user.DateOfBirth).ThenBy(user => user.IsAdult)
								.ThenBy(user => Enum.Parse(typeof(ZodiacSigns.WesternAstrologyZodiacSigns), user.SunSign)).ThenBy(user => user.IsBirthday);
				case OrderBy.IsBirthdayToday:
					return Users.OrderBy(user => user.IsBirthday).ThenBy(user => user.FirstName)
								.ThenBy(user => user.FirstName).ThenBy(user => user.Email).ThenBy(user => user.DateOfBirth)
								.ThenBy(user => user.IsAdult).ThenBy(user => Enum.Parse(typeof(ZodiacSigns.WesternAstrologyZodiacSigns), user.SunSign))
								.ThenBy(user => Enum.Parse(typeof(ZodiacSigns.ChineseAstrologyZodiacSigns), user.ChineseSign));
				default:
					return Users;
			}
		}
	}
}
