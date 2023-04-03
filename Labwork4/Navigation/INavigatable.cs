using System;

namespace KMA.ProgrammingInCSharp.Labwork4.Navigation
{
	internal interface INavigatable<TObject> where TObject : Enum
	{
		TObject ViewType
		{
			get;
		}
	}
}
