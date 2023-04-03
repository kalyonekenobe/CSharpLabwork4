using System;

namespace KMA.ProgrammingInCSharp.Labwork4.Tools
{
	internal class DateTimeTools
	{
		public static int GetYearsDifference(DateTime begin, DateTime end)
		{
			var yearsDifference = end.Year - begin.Year;
			return yearsDifference - (begin.Date > end.AddYears(-yearsDifference) ? 1 : 0);
		}
	}
}