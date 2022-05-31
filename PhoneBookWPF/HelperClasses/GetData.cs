using System;
using Bogus;
using System.Linq;
using LibraryOOP;

namespace PhoneBookWPF
{
	internal static class GetData
	{
		public static void GetListAbonents(PhoneBook phoneBook, int count)
		{
			phoneBook = PhoneBook.GetPhoneBook();

			Faker faker = new("ru");
			Random rn = new();

			for (int i = 0; i < 15; i++)
			{
				var phone = PhoneBook.CreatePhoneNumber(faker.Phone.PhoneNumber(), "Рабочий");
				var gender = faker.Person.Gender;
				phoneBook.AddAbonent(faker.Name.FirstName(gender), faker.Name.LastName(gender), phone, DateTime.Now);
			}

			var abonents = phoneBook.Abonents;

			foreach (var item in abonents)
			{
				int num = rn.Next(1, 5);
				for (int j = 0; j < num; j++)
				{
					var phone = PhoneBook.CreatePhoneNumber(faker.Phone.PhoneNumber(), "Личный");
					phoneBook.AddAbonentPhone(item, phone);
				}
			}

			for (int i = 0; i < 4; i++)
			{
				foreach (var item in phoneBook.Abonents)
				{
					if (rn.Next(0, 3) == 1)
						phoneBook.AddAbonentsGroup($"group{i}", item);
				}
			}
		}
	}
}
