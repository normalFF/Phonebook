using System;
using Bogus;
using System.Collections.Generic;
using LibraryOOP;
using System.IO;
using System.Linq;

namespace DebugCode
{
	class Program
	{
		static PhoneBook phoneBook;
		static List<PhoneNumber> phoneNumber;

		static void Main(string[] args)
		{
			phoneBook = PhoneBook.GetPhoneBook();

			/*
			Faker faker = new("ru");
			phoneNumber = new List<PhoneNumber>();
			Random rn = new();

			for (int i = 0; i < 15; i++)
			{
				var phone = PhoneBook.CreatePhoneNumber(faker.Phone.PhoneNumber(), "Рабочий");
				phoneNumber.Add(phone);
				var gender = faker.Person.Gender;
				phoneBook.AddAbonent(faker.Name.FirstName(gender), faker.Name.LastName(gender), phone, new DateTime(rn.Next(1950, 2010), rn.Next(1, 12), rn.Next(1, 26)));
			}

			var abonents = phoneBook.Abonents.ToList();

			foreach (var item in abonents)
			{
				int num = rn.Next(1, 3);
				for (int j = 0; j < num; j++)
				{
					var phone = PhoneBook.CreatePhoneNumber(faker.Phone.PhoneNumber(), "Личный");
					phoneNumber.Add(phone);
					phoneBook.AddAbonentPhone(item, phone);
				}
			}

			phoneBook.CreateAbonentsGroup("group1", new List<Abonent>() { abonents[0], abonents[1], abonents[2], abonents[3] });
			phoneBook.CreateAbonentsGroup("group2", new List<Abonent>() { abonents[0], abonents[1], abonents[4], abonents[5] });
			phoneBook.CreateAbonentsGroup("group3", new List<Abonent>() { abonents[0], abonents[3], abonents[4], abonents[6] });

			Console.WriteLine(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "test.json");
			Console.WriteLine(phoneBook.SaveData(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "test.json"));
			*/


			phoneBook.LoadData(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "test.json");
			var collection = phoneBook.Abonents;

			foreach (var item in collection)
			{
				Console.WriteLine(item.ToString() + "\n");
			}

			var groups = phoneBook.AbonentsGroup;

			foreach (string item in groups)
			{
				Console.WriteLine(item + "\n");
				var collectionGroup = collection.Where(t => t.Groups.Contains(item)).ToList();
				foreach (var i in collectionGroup)
				{
					Console.WriteLine(i.ToString() + "\n");
				}
			}
		}
	}
}
