using KMA.ProgrammingInCSharp.Labwork4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp.Labwork4.Repositories
{
	internal class FileRepository
	{
		private static readonly string _baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CSharpLabwork4Storage");

		public FileRepository() 
		{ 
			if (!Directory.Exists(_baseFolder))
			{
				Directory.CreateDirectory(_baseFolder);
			}
		}

		public async Task CreateOrUpdateAsync(Person person)
		{
			var serializedObject = JsonSerializer.Serialize(person);

			using (StreamWriter streamWriter = new StreamWriter(Path.Combine(_baseFolder, person.Email), false))
			{
				await streamWriter.WriteAsync(serializedObject);	
			}
		}

		public async Task CreateOrUpdateAllAsync(IEnumerable<Person> list)
		{
			foreach (var person in list)
			{
				await CreateOrUpdateAsync(person);
			}
		}

		public async Task RemoveAsync(Person person)
		{
			if (File.Exists(Path.Combine(_baseFolder, person.Email)))
			{
				await Task.Run(() => File.Delete(Path.Combine(_baseFolder, person.Email)));
			}
		}

		public async Task<Person?> GetPersonAsync(string email)
		{
			string filePath = Path.Combine(_baseFolder, email);
			
			if (!File.Exists(filePath))
			{
				return null;
			}

			string serializedObject = string.Empty;
			using (StreamReader streamReader = new StreamReader(filePath)) 
			{
				serializedObject = await streamReader.ReadToEndAsync();
			}

			Person? person = JsonSerializer.Deserialize<Person>(serializedObject);
			
			if (person is not null)
			{
				person.InitializeValues();
			}

			return person;
		}

		public async Task<IEnumerable<Person>> GetAllAsync()
		{
			var list = new List<Person>();

			foreach (var file in Directory.EnumerateFiles(_baseFolder))
			{
				string serializedObject = string.Empty;

				using (StreamReader streamReader = new StreamReader(file))
				{
					serializedObject = await streamReader.ReadToEndAsync();
				}

				Person? person = JsonSerializer.Deserialize<Person>(serializedObject);
				if (person is not null)
				{
					person.InitializeValues();
					list.Add(person);
				}
			}

			return list;
		}

		public IEnumerable<Person> GetAll()
		{
			var list = new List<Person>();

			foreach (var file in Directory.EnumerateFiles(_baseFolder))
			{
				string serializedObject = string.Empty;

				using (StreamReader streamReader = new StreamReader(file))
				{
					serializedObject = streamReader.ReadToEnd();
				}

				Person? person = JsonSerializer.Deserialize<Person>(serializedObject);
				if (person is not null)
				{
					person.InitializeValues();
					list.Add(person);
				}
			}

			return list;
		}
	}
}
