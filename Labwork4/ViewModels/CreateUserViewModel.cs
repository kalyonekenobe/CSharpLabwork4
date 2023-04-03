using KMA.ProgrammingInCSharp.Labwork4.Models;
using KMA.ProgrammingInCSharp.Labwork4.Navigation;
using KMA.ProgrammingInCSharp.Labwork4.Repositories;
using KMA.ProgrammingInCSharp.Labwork4.Tools;
using KMA.ProgrammingInCSharp.Labwork4.Tools.Exceptions;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.ProgrammingInCSharp.Labwork4.ViewModels
{
	internal class CreateUserViewModel : INotifyPropertyChanged, INavigatable<MainNavigationTypes>
	{
		private PersonCandidate _personCandidate;
		private RelayCommand<object>? _createCommand;
		private RelayCommand<object>? _gotoUserListCommand;
		private Action _gotoUserList;
		private bool _isEnabled;
		private FileRepository _fileRepository;

		public event PropertyChangedEventHandler? PropertyChanged;

		public string FirstName
		{
			get { return _personCandidate.FirstName; }
			set
			{
				_personCandidate.FirstName = value;
				NotifyPropertyChanged(nameof(FirstName));
			}
		}

		public string LastName
		{
			get { return _personCandidate.LastName; }
			set
			{
				_personCandidate.LastName = value;
				NotifyPropertyChanged(nameof(LastName));
			}
		}

		public string Email
		{
			get { return _personCandidate.Email; }
			set
			{
				_personCandidate.Email = value;
				NotifyPropertyChanged(nameof(Email));
			}
		}

		public DateTime? DateOfBirth
		{
			get { return _personCandidate.DateOfBirth; }
			set
			{
				_personCandidate.DateOfBirth = value;
				NotifyPropertyChanged(nameof(DateOfBirth));
			}
		}

		public bool IsEnabled
		{
			get => _isEnabled;
			private set
			{
				_isEnabled = value;
				NotifyPropertyChanged(nameof(IsEnabled));
			}
		}

		public RelayCommand<object> CreateCommand
		{
			get
			{
				return _createCommand ??= new RelayCommand<object>(_ => Create(), CanExecute);
			}
		}

		public RelayCommand<object> GotoUserListCommand
		{
			get
			{
				return _gotoUserListCommand ??= new RelayCommand<object>(_ => GotoUserList());
			}
		}

		public MainNavigationTypes ViewType
		{
			get => MainNavigationTypes.CreateUser;
		}

		public CreateUserViewModel(Action gotoUserList)
		{
			_isEnabled = true;
			_personCandidate = new PersonCandidate();
			_gotoUserList = gotoUserList;
			_fileRepository = new FileRepository();
			WindowTools.SetWindowSize(600, 380, 600, 380, 700, 380); 
			WindowTools.SetWindowToCenterOfScreen();
		}

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private async void Create()
		{
			if (!CheckAllFieldsAreNotEmpty())
			{
				return;
			}

			try
			{
				CheckAllFieldsAreValid();
				IsEnabled = false;
				Person person = await Task.Run(() => new Person(FirstName, LastName, Email, (DateTime)DateOfBirth!));

				if (await UserExsists(Email))
				{
					MessageBox.Show($"Person with such email already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				if (person.IsBirthday)
				{
					MessageBox.Show($"Happy birthday to {FirstName} {LastName}!", "Greetings", MessageBoxButton.OK, MessageBoxImage.Information);
				}

				await CreateUser(person);
				GotoUserList();
			}
			catch (BirthdayInFutureException exception)
			{
				MessageBox.Show($"Birthday value error: {exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			catch (BirthdayInDistantPastException exception)
			{
				MessageBox.Show($"Birthday value error: {exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			catch (InvalidEmailFormatException exception)
			{
				MessageBox.Show($"Invalid email format error: {exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			catch (Exception exception)
			{
				MessageBox.Show($"User information processing error: {exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			finally
			{
				IsEnabled = true;
			}
		}

		private bool CheckAllFieldsAreNotEmpty()
		{
			if (string.IsNullOrWhiteSpace(FirstName))
			{
				MessageBox.Show("User first name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			if (string.IsNullOrWhiteSpace(LastName))
			{
				MessageBox.Show("User last name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			if (string.IsNullOrWhiteSpace(Email))
			{
				MessageBox.Show("User email cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			if (IsDatePickerValueEmpty(DateOfBirth))
			{
				MessageBox.Show("User date of birth cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			return true;
		}

		private void CheckAllFieldsAreValid()
		{
			if (DateOfBirth is null)
			{
				throw new Exception("Date of birth is null!");
			}

			int userAge = DateTimeTools.GetYearsDifference((DateTime)DateOfBirth, DateTime.Now);

			if (userAge < 0)
			{
				throw new BirthdayInFutureException(BirthdayInFutureException.DefaultMessage, (DateTime)DateOfBirth);
			}

			if (userAge > 135)
			{
				throw new BirthdayInDistantPastException(BirthdayInDistantPastException.DefaultMessage, (DateTime)DateOfBirth);
			}

			if (!Regex.Match(Email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$").Success)
			{
				throw new InvalidEmailFormatException(InvalidEmailFormatException.DefaultMessage, Email);
			}
		}

		private bool IsDatePickerValueEmpty(DateTime? value) => value is null;

		private async Task CreateUser(Person person)
		{
			await _fileRepository.CreateOrUpdateAsync(person);
		}

		private async Task<bool> UserExsists(string email)
		{
			Person? person = await _fileRepository.GetPersonAsync(email);
			return person is not null;
		}

		private void GotoUserList()
		{
			_gotoUserList.Invoke();
		}

		private bool CanExecute(object obj)
		{
			return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(Email) && !IsDatePickerValueEmpty(DateOfBirth) && IsEnabled;
		}
	}
}