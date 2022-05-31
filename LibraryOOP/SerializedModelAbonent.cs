using System;

namespace LibraryOOP
{
	[Serializable]
	class SerializedModelAbonent
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string DateOfBirth { get; set; }
		public string Residence { get; set; }
		public SerializedModelPhone[] Phones { get; set; }
		public string[] Groups { get; set; }
	}
}
