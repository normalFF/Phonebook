using System;
using NUnit.Framework;
using Bogus;
using LibraryOOP;
using System.Collections.Generic;

namespace AutomaticTestsNUnit
{
	[TestFixture]
	public class TestsPhoneBook
	{
		PhoneBook phoneBook;
		List<PhoneNumber> phoneNumber;

		[SetUp]
		public void Setup()
		{
			phoneBook = PhoneBook.GetPhoneBook();

			Faker faker = new("ru");
			phoneNumber = new List<PhoneNumber>();
			Random rn = new();

			for (int i = 0; i < 15; i++)
			{
				var phone = PhoneBook.CreatePhoneNumber(faker.Phone.PhoneNumber(), (PhoneType)rn.Next(0, 3));
				phoneNumber.Add(phone);
				var gender = faker.Person.Gender;
				phoneBook.AddAbonent(faker.Name.FirstName(gender), faker.Name.LastName(gender), phone);
			}

			var abonents = phoneBook.Abonents;

			foreach (var item in abonents)
			{
				int num = rn.Next(1, 3);
				for (int j = 0; j < num; j++)
				{
					var phone = PhoneBook.CreatePhoneNumber(faker.Phone.PhoneNumber(), (PhoneType)rn.Next(0, 3));
					phoneNumber.Add(phone);
					phoneBook.AddAbonentPhone(item, phone);
				}
			}
		}

		[Test]
		public void PrintInfoAbonents()
		{
			var collection = phoneBook.Abonents;

			foreach (var item in collection)
			{
				var phones = item.PhoneNumbers;

				Console.WriteLine(item);
				Console.WriteLine();
			}
		}

		[Test]
		public void PrintPhones()
		{
			Console.WriteLine(1);

			foreach (var item in phoneNumber)
			{
				Console.WriteLine(item.ToString());
			}
		}

		[Test]
		public void CreatePhones()
		{
			Faker faker = new();
			Random random = new();
			
			for (int i = 0; i < 30; i++)
			{
				var phone = faker.Phone.PhoneNumber();
				Console.WriteLine(phone);
				Console.WriteLine(PhoneNumber.IsCorrectPhone(phone));
			}
		}
	}
}