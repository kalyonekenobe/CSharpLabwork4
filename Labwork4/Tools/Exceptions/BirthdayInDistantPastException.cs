using System;

namespace KMA.ProgrammingInCSharp.Labwork4.Tools.Exceptions
{
	[Serializable]
	class BirthdayInDistantPastException : Exception
	{
		public static string DefaultMessage { get => $"You cannot set the birthday in the distant past (more than 135 years ago)."; }

		public BirthdayInDistantPastException() : base(DefaultMessage) { }

		public BirthdayInDistantPastException(string message) : base(message) { }

		public BirthdayInDistantPastException(string message, DateTime date) : base($"{message}\nDate that caused exception: {date.ToString("dd/MM/yyyy")}") { }
	}
}