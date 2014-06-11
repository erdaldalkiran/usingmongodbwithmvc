namespace Test
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.IO;

    using NUnit.Framework;


    public class BsonDocumentTests
    {
        public BsonDocumentTests()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }
        [Test]
        public void EmptyDocument()
        {
            var document = new BsonDocument();
            Console.WriteLine(document);
        }

        [Test]
        public void AddElement()
        {
            var person = new BsonDocument
                         {
                             { "firstName", new BsonString("bob") },
                             { "lastName", new BsonString("mock") },
                             { "isAlive",true}
                         };

            Console.WriteLine(person);
        }

        [Test]
        public void EmbededDocument()
        {
            var person = new BsonDocument
                         {
                             {
                                 "contact", new BsonDocument
                                            {
                                                {"phone","555-555-22-22"},
                                                {"age", 23},
                                                {"email","erda.asd@asd.com"}
                                            }
                             }
                         };

            Console.WriteLine(person);

        }

        [Test]
        public void BsonValueConversions()
        {
            var person = new BsonDocument
                         {
                             {"age",29}
                         };

            Console.WriteLine((double)person["age"] + 10);
            Console.WriteLine(person["age"].IsInt32);
            Console.WriteLine(person["age"].IsString    );
        }

    }
}
