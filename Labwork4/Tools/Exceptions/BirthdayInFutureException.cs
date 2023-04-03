using System;

namespace KMA.ProgrammingInCSharp.Labwork4.Tools.Exceptions
{
	[Serializable]
	class BirthdayInFutureException : Exception
	{
		public static string DefaultMessage { get => $"You cannot set the birthday in the future."; }

		public BirthdayInFutureException() : base(DefaultMessage) { }

		public BirthdayInFutureException(string message) : base(message) { }

		public BirthdayInFutureException(string message, DateTime date) : base($"{message}\nDate that caused exception: {date.ToString("dd/MM/yyyy")}") { }
	}
}