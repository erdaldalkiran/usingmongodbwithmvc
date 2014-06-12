namespace Test.Rentals
{
    using MongoDB.Bson;

    using NUnit.Framework;

    using RealEstate.Rentals;

    [TestFixture]
    public class RentalTests : AssertionHelper
    {
        [Test]
        public void ToDocument_RentalWithPrice_PriceRepresentedAsDouble()
        {
            // Arrange
            var rental = new Rental { Price = 1 };

            // Act

            var document = rental.ToBsonDocument();

            // Assert

            Expect(document["Price"].BsonType, Is.EqualTo(BsonType.Double));

        }

        [Test]
        public void ToDocument_RentalWithAnId_IdIsRepresentedAsAnObjectId()
        {
            // Arrange
            var rental = new Rental { Id = ObjectId.GenerateNewId().ToString() };

            var document = rental.ToBsonDocument();
            // Assert

            Expect(document["_id"].BsonType, Is.EqualTo(BsonType.ObjectId));

        }
    }
}
