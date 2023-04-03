using System;

namespace KMA.ProgrammingInCSharp.Labwork4.Tools.Exceptions
{
	class InvalidEmailFormatException : Exception
	{
		public static string DefaultMessage { get => $"Invalid format of the email address."; }

		public InvalidEmailFormatException() : base(DefaultMessage) { }

		public InvalidEmailFormatException(string message) : base(message) { }

		public InvalidEmailFormatException(string message, string email) : base($"{message}\nEmail that caused exception: {email}") { }
	}
}