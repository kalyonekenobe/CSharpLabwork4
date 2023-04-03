using KMA.ProgrammingInCSharp.Labwork4.Tools;
using System;
using static KMA.ProgrammingInCSharp.Labwork4.Tools.ZodiacSigns;

namespace KMA.ProgrammingInCSharp.Labwork4.Models
{
	internal class Person
	{
		private bool _isAdult = false;
		private string _sunSign = string.Empty;
		private string _chineseSign = string.Empty;
		private bool _isBirthday = false;

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public DateTime DateOfBirth { get; set; }
		public bool IsAdult { get => _isAdult; }
		public string SunSign { get => _sunSign; }
		public string ChineseSign { get => _chineseSign; }
		public bool IsBirthday { get => _isBirthday; }

		public Person(string firstName, string lastName, string email, DateTime dateOfBirth)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			DateOfBirth = dateOfBirth;
			InitializeValues();
		}

		public Person(string firstName, string lastName, string email) : this(firstName, lastName, email, DateTime.Now) { }

		public Person(string firstName, string lastName, DateTime dateOfBirth) : this(firstName, lastName, string.Empty, dateOfBirth) { }

		public Person() : this(string.Empty, string.Empty, string.Empty) { }

		public void InitializeValues()
		{
			_isAdult = IsPersonAdult();
			_sunSign = GetSunSign();
			_chineseSign = GetChineseSign();
			_isBirthday = IsBirthdayToday();
		}

		private string GetSunSign()
		{
			string defaultValue = "Unknown";
			switch (DateOfBirth.Date)
			{
				case var date when (date >= new DateTime(date.Year, 12, 22) && date <= new DateTime(date.Year, 12, 31)) || (date >= new DateTime(date.Year, 1, 1) && date <= new DateTime(date.Year, 1, 20)):
					return WesternAstrologyZodiacSigns.Capricorn.ToString();
				case var date when date >= new DateTime(date.Year, 1, 21) && date <= new DateTime(date.Year, 2, 19):
					return WesternAstrologyZodiacSigns.Aquarius.ToString();
				case var date when date >= new DateTime(date.Year, 2, 20) && date <= new DateTime(date.Year, 3, 20):
					return WesternAstrologyZodiacSigns.Pisces.ToString();
				case var date when date >= new DateTime(date.Year, 3, 21) && date <= new DateTime(date.Year, 4, 20):
					return WesternAstrologyZodiacSigns.Aries.ToString();
				case var date when date >= new DateTime(date.Year, 4, 20) && date <= new DateTime(date.Year, 5, 20):
					return WesternAstrologyZodiacSigns.Taurus.ToString();
				case var date when date >= new DateTime(date.Year, 5, 21) && date <= new DateTime(date.Year, 6, 21):
					return WesternAstrologyZodiacSigns.Gemini.ToString();
				case var date when date >= new DateTime(date.Year, 6, 22) && date <= new DateTime(date.Year, 7, 22):
					return WesternAstrologyZodiacSigns.Cancer.ToString();
				case var date when date >= new DateTime(date.Year, 7, 23) && date <= new DateTime(date.Year, 8, 22):
					return WesternAstrologyZodiacSigns.Leo.ToString();
				case var date when date >= new DateTime(date.Year, 8, 23) && date <= new DateTime(date.Year, 9, 22):
					return WesternAstrologyZodiacSigns.Virgo.ToString();
				case var date when date >= new DateTime(date.Year, 9, 23) && date <= new DateTime(date.Year, 10, 22):
					return WesternAstrologyZodiacSigns.Libra.ToString();
				case var date when date >= new DateTime(date.Year, 10, 23) && date <= new DateTime(date.Year, 11, 22):
					return WesternAstrologyZodiacSigns.Scorpio.ToString();
				case var date when date >= new DateTime(date.Year, 11, 23) && date <= new DateTime(date.Year, 12, 21):
					return WesternAstrologyZodiacSigns.Sagittarius.ToString();
				default:
					return defaultValue;
			}
		}

		private string GetChineseSign() => ((ChineseAstrologyZodiacSigns)((DateOfBirth.Year - 4) % 12)).ToString();

		private bool IsPersonAdult() => DateTimeTools.GetYearsDifference(DateOfBirth, DateTime.Today) >= 18;

		private bool IsBirthdayToday() => DateTime.Now.Day == DateOfBirth.Day && DateTime.Now.Month == DateOfBirth.Month;
	}
}