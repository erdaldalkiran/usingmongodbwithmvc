// <copyright file="Rental.cs " company="Socar Turkey Petrol-Enerji Dağıtım A.Ş." owner="Erdal Dalkıran">
// <createDate>12/06/2014 11:24</createDate>
// </copyright>

namespace RealEstate.Rentals
{
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Rental
    {
        #region Fields

        public List<string> Address = new List<string>();

        #endregion

        #region Constructors and Destructors

        public Rental()
        {

        }
        public Rental(PostRental postRental)
        {
            NumberOfRooms = postRental.NumberOfRooms;
            Description = postRental.Description;
            Price = postRental.Price;
            Address = (postRental.Address ?? string.Empty).Split('\n').ToList();
        }

        #endregion

        #region Public Properties

        public string Description { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int NumberOfRooms { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }

        public string ImageId { get; set; }

        public bool HasImage()
        {
            return !string.IsNullOrEmpty(ImageId);
        }

        public IList<PriceAdjustment> Adjustments = new List<PriceAdjustment>();

        #endregion

        public void AdjustPrice(AdjustPrice adjustPrice)
        {
            Adjustments.Add(new PriceAdjustment(adjustPrice, Price));
            Price = adjustPrice.NewPrice;
        }
    }
}