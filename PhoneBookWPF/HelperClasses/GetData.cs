using System;
using Bogus;
using LibraryOOP;

namespace PhoneBookWPF
{
	internal static class GetData
	{
		public static void GetListAbonents(PhoneBook phoneBook, int count)
		{
			Faker faker = new Faker("ru");
			Random rn = new();

			for (int i = 0; i < count; i++)
			{
				PhoneNumber phoneNumber = new PhoneNumber(faker.Phone.PhoneNumber(), (PhoneType)rn.Next(0, 3));
				var gender = faker.Person.Gender;
				Abonent abonent = new Abonent(faker.Name.FirstName(gender), faker.Name.LastName(gender), phoneNumber, DateTime.Now);
				int num = rn.Next(1, 3);
				for (int j = 0; j < num; j++)
				{
					phoneNumber = new PhoneNumber(faker.Phone.PhoneNumber(), (PhoneType)rn.Next(0, 3));
					abonent.AddPhone(phoneNumber);
				}
				phoneBook.AddAbonent(abonent);
			}
		}
	}
}
