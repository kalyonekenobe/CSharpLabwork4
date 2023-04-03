using KMA.ProgrammingInCSharp.Labwork4.Models;
using KMA.ProgrammingInCSharp.Labwork4.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp.Labwork4.Services
{
	internal class UserService
	{
		private static FileRepository _fileRepository = new FileRepository();

		public async Task<IEnumerable<Person>> GetAllUsersAsync()
		{
			return await _fileRepository.GetAllAsync();
		}

		public async Task RemoveUserAsync(Person person)
		{
			await _fileRepository.RemoveAsync(person);
		}

		public async Task SaveUserAsync(Person user)
		{
			await _fileRepository.CreateOrUpdateAsync(user);
		}

		public async Task SaveAllUsersAsync(IEnumerable<Person> users)
		{
			await _fileRepository.CreateOrUpdateAllAsync(users);
		}

		public List<Person> GetDefaultUsersList()
		{
			return new List<Person>()
			{
				new Person("Leanne", "Graham", "graham@gmail.com", new DateTime(2008, 10, 10)),
				new Person("Oleksandr", "Igumnov", "oleksandr@gmail.com", new DateTime(2004, 09, 03)),
				new Person("Volodymyr", "Gavriliuk", "vova@gmail.com", new DateTime(2003, 12, 23)),
				new Person("Ervin", "Howell", "ervin@gmail.com", new DateTime(1991, 06, 12)),
				new Person("Clementine", "Bauch", "clementine@gmail.com", new DateTime(2002, 01, 31)),
				new Person("Patricia", "Lebsack", "patricia@gmail.com", new DateTime(1987, 05, 21)),
				new Person("Vlada", "Igumnova", "vlada@gmail.com", new DateTime(2014, 09, 13)),
				new Person("Chelsey", "Dietrich", "chelsey@gmail.com", new DateTime(2011, 11, 25)),
				new Person("Oleksandr", "Ivanov", "oleksandrivanov@gmail.com", new DateTime(1979, 08, 08)),
				new Person("Vita", "Sushytska", "vita@gmail.com", new DateTime(2013, 10, 17)),
				new Person("Dennis", "Schulist", "dennis@gmail.com", new DateTime(2013, 04, 24)),
				new Person("Alex", "Smith", "alex@gmail.com", new DateTime(1998, 03, 27)),
				new Person("John", "Dohn", "john@gmail.com", new DateTime(1976, 06, 13)),
				new Person("Adolf", "Kitler", "adolf@gmail.com", new DateTime(1987, 07, 16)),
				new Person("Arnold", "Aboba", "arnold@gmail.com", new DateTime(1984, 10, 25)),
				new Person("Valeriy", "Zhmyshenko", "valeriy@gmail.com", new DateTime(1999, 11, 13)),
				new Person("Victoria", "Terentieva", "victoria@gmail.com", new DateTime(1978, 12, 10)),
				new Person("Alex", "Lytvynchuk", "alexlitvin@gmail.com", new DateTime(2006, 08, 19)),
				new Person("Artem", "Osovskiy", "artem@gmail.com", new DateTime(2018, 05, 05)),
				new Person("Oleksandr", "Zimbabve", "oleksandrzimbabve@gmail.com", new DateTime(1956, 05, 03)),
				new Person("Zina", "Osmanova", "zina@gmail.com", new DateTime(1995, 02, 07)),
				new Person("Petr", "Slovak", "petr@gmail.com", new DateTime(2005, 08, 13)),
				new Person("Max", "Torvald", "max@gmail.com", new DateTime(2022, 05, 18)),
				new Person("Victoria", "Shevchenko", "victoriashevchenko@gmail.com", new DateTime(1981, 06, 26)),
				new Person("Anna", "Baum", "annabaum@gmail.com", new DateTime(1956, 05, 04)),
				new Person("Helen", "Mardeburg", "helen@gmail.com", new DateTime(1945, 03, 03)),
				new Person("Patrick", "Bateman", "patrick@gmail.com", new DateTime(2016, 11, 15)),
				new Person("Kurtis", "Weissnat", "weissnat@gmail.com", new DateTime(2001, 12, 06)),
				new Person("Nicholas", "Runolfsdottir", "nicholas@gmail.com", new DateTime(2000, 01, 18)),
				new Person("Glenna", "Reichert", "glenna@gmail.com", new DateTime(2009, 08, 29)),
				new Person("Clementina", "DuBuque", "dubuque@gmail.com", new DateTime(1955, 09, 28)),
				new Person("Bill", "James", "billjames@gmail.com", new DateTime(1998, 09, 05)),
				new Person("Anton", "Defeald", "antondefeald@gmail.com", new DateTime(1991, 12, 31)),
				new Person("Antony", "Floyd", "antonyfloyd@gmail.com", new DateTime(1977, 11, 01)),
				new Person("Volodymyr", "Kurbatov", "vovakurbatov@gmail.com", new DateTime(1973, 07, 09)),
				new Person("Mykola", "Balashov", "mykola@gmail.com", new DateTime(1964, 06, 05)),
				new Person("John", "Brentford", "johnbrentford@gmail.com", new DateTime(1985, 12, 04)),
				new Person("George", "Vashington", "george@gmail.com", new DateTime(2015, 04, 14)),
				new Person("Elizabeth", "Matvienko", "elizabeth@gmail.com", new DateTime(2007, 11, 17)),
				new Person("Iryna", "Nazar", "irynanazar@gmail.com", new DateTime(2004, 02, 05)),
				new Person("Jenifer", "Berinchi", "jenifer@gmail.com", new DateTime(1955, 09, 28)),
				new Person("Samantha", "Holand", "samanthaholand@gmail.com", new DateTime(1998, 09, 05)),
				new Person("Johnatan", "Filinsky", "johnatanfilinsky@gmail.com", new DateTime(1991, 12, 31)),
				new Person("Alina", "Glina", "alinaglina@gmail.com", new DateTime(1977, 11, 01)),
				new Person("Oleksiy", "Soplyakin", "oleksiysoplyakin@gmail.com", new DateTime(1973, 07, 09)),
				new Person("Andriy", "Kuropatkin", "andriykuropatkin@gmail.com", new DateTime(1964, 06, 05)),
				new Person("Anatoliy", "Shayba", "anatoliyshayba@gmail.com", new DateTime(1985, 12, 04)),
				new Person("Sergiy", "Goebbels", "sergiygoebbels@gmail.com", new DateTime(2015, 04, 14)),
				new Person("Oleksandra", "Shpak", "oleksandrashpak@gmail.com", new DateTime(2007, 11, 17)),
				new Person("Hrystyna", "Ptah", "hrystynaptah@gmail.com", new DateTime(2004, 02, 05))
			};
		}
	}
}
