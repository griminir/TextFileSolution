using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace TextFileUI
{
    internal class Program
    {
        private static IConfiguration _config;
        private static string textFile;
        private static TextFileDataAccess db = new TextFileDataAccess();

        static void Main(string[] args)
        {
            InitializeConfiguration();
            textFile = _config.GetValue<string>("TextFile");

            ContactModel user1 = new ContactModel();
            user1.FirstName = "viktor";
            user1.LastName = "Degray";
            user1.EmailAddresses.Add("viktor@gmail.com");
            user1.EmailAddresses.Add("me@gmail.com");
            user1.PhoneNumbers.Add("12345678");
            user1.PhoneNumbers.Add("40400404");

            ContactModel user2 = new ContactModel();
            user2.FirstName = "sue";
            user2.LastName = "storm";
            user2.EmailAddresses.Add("sue@gmail.com");
            user2.EmailAddresses.Add("me@gmail.com");
            user2.PhoneNumbers.Add("89213725");
            user2.PhoneNumbers.Add("40400404");

            //CreateContact(user1);
            //CreateContact(user2);
            //GetAllContacts();

            //UpdateContactsFirstName("timmy");
            //GetAllContacts();

            //RemovePhoneNumberFromUser("12345678");
            //GetAllContacts();

            RemoveUser();
            GetAllContacts();

            Console.WriteLine("Done processing text file");
            Console.ReadLine();
        }

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }

        private static void GetAllContacts()
        {
            var contacts = db.ReadAllRecords(textFile);

            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.FirstName} {contact.LastName}");
            }

        }
        private static void CreateContact(ContactModel contact)
        {
            var contacts = db.ReadAllRecords(textFile);

            contacts.Add(contact);

            db.WriteAllRecords(contacts, textFile);
        }
        private static void UpdateContactsFirstName(string firstName)
        {
            var contacts = db.ReadAllRecords(textFile);
            contacts[0].FirstName = firstName; // simulates the UI chosing the first contact in the list
            db.WriteAllRecords(contacts, textFile);
        }
        private static void RemovePhoneNumberFromUser(string phoneNumber)
        {
            var contacts = db.ReadAllRecords(textFile);
            contacts[0].PhoneNumbers.Remove(phoneNumber); // simulates the UI chosing the first contact in the list
            db.WriteAllRecords(contacts, textFile);
        }
        private static void RemoveUser()
        {
            var contacts = db.ReadAllRecords(textFile);
            contacts.RemoveAt(0); // simulates the UI chosing the first contact in the list
            db.WriteAllRecords(contacts, textFile);
        }
    }
}
