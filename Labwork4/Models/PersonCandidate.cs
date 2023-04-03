using System;

namespace KMA.ProgrammingInCSharp.Labwork4.Models
{
	internal class PersonCandidate
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public DateTime? DateOfBirth { get; set; }
	}
}
