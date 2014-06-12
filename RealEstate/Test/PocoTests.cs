namespace Test
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Bson;
    using MongoDB.Bson.IO;
    using MongoDB.Bson.Serialization.Attributes;

    using NUnit.Framework;

    public class PocoTests
    {
        public PocoTests()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }

        public class Person
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public List<string> Address = new List<string>();

            public Contact Contact = new Contact();

            [BsonElement("ActualName")]
            public string RenameElement { get; set; }

        }

        public class Contact
        {
            public string Email { get; set; }
            public string Phone { get; set; }

            public string Ignore
            {
                get
                {
                    return Email + Phone;
                }
            }
        }

        [Test]
        public void Automatic()
        {
            var person = new Person
                         {
                             Name = "Erdal",
                             Age = 29,
                             Contact = new Contact
                                       {
                                           Email = "adasd@sadasd.com",
                                           Phone = "5531-123-123"
                                       }
                         };

            person.Address.Add("Beşiktaş");
            person.Address.Add("İstanbul");
            Console.WriteLine(person.ToJson());

        }
    }
}
