// <copyright file="RealEstateContext.cs " company="Socar Turkey Petrol-Enerji Dağıtım A.Ş." owner="Erdal Dalkıran">
// <createDate>10/06/2014 18:31</createDate>
// </copyright>

namespace RealEstate.App_Start
{
    using MongoDB.Driver;

    using RealEstate.Properties;

    public class RealEstateContext
    {
        #region Fields

        public MongoDatabase Database;

        #endregion

        #region Constructors and Destructors

        public RealEstateContext()
        {
            var client = new MongoClient(Settings.Default.RealEstate);
            var server = client.GetServer();
            Database = server.GetDatabase(Settings.Default.RealEstateDataBaseName);
        }

        #endregion
    }
}