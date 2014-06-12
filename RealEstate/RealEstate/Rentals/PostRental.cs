// <copyright file="PostRental.cs " company="Socar Turkey Petrol-Enerji Dağıtım A.Ş." owner="Erdal Dalkıran">
// <createDate>12/06/2014 10:57</createDate>
// </copyright>
namespace RealEstate.Rentals
{
    using System.Collections.Generic;

    public class PostRental
    {
        public string Description { get; set; }

        public int NumberOfRooms { get; set; }

        public decimal Price { get; set; }

        public string Address { get; set; }
    }
}