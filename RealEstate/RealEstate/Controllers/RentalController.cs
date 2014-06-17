namespace RealEstate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Antlr.Runtime.Misc;

    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.GridFS;
    using MongoDB.Driver.Linq;

    using RealEstate.App_Start;
    using RealEstate.Rentals;

    public class RentalController : Controller
    {
        #region Fields

        public readonly RealEstateContext Context = new RealEstateContext();

        #endregion

        #region Public Methods and Operators

        public ActionResult AdjustPrice(string id)
        {
            var model = GetRental(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult AdjustPrice(string id, AdjustPrice adjustPrice)
        {
            var rental = GetRental(id);
            rental.AdjustPrice(adjustPrice);
            Context.Rentals.Save(rental);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            var result = Context.Rentals.Remove(Query.EQ("_id", new ObjectId(id)), RemoveFlags.Single, WriteConcern.Acknowledged);
            return RedirectToAction("Index");
        }


        public ActionResult Index(RentalsFilter filter)
        {
            var rentals = FilterRentals(filter);
            //.OrderByDescending(x => x.Price);
            //.SetSortOrder(SortBy<Rental>.Descending(x => x.Price));
            var model = new RentalsList
                        {
                            Filter = filter,
                            Rentals = rentals
                        };
            return View(model);
        }

        private IEnumerable<Rental> FilterRentals(RentalsFilter filter)
        {
            var rentals = Context.Rentals.AsQueryable();

            if (filter.MinumumRoom.HasValue)
            {
                rentals = rentals.Where(x => x.NumberOfRooms >= filter.MinumumRoom.Value);
            }

            //if (filter.PriceLimit.HasValue)
            //{
            //    rentals = rentals.Where(x => x.Price < filter.PriceLimit.Value);
            //}


            if (filter.PriceLimit.HasValue)
            {
                var query = Query<Rental>.Where(x => x.Price < filter.PriceLimit.Value);
                rentals = rentals.Where(x => query.Inject());
            }

            return rentals.OrderBy(x => x.Price);
            ////return Context.Rentals.Find(query);
            //return Context.Rentals.AsQueryable().Where(x => x.Price < filter.PriceLimit.Value).AsEnumerable();
        }

        public ActionResult Post()
        {
            return View();
        }

        public string PriceDistribution()
        {
            return new QueryPriceDistribution().Run(Context.Rentals).ToJson();
        }

        [HttpPost]
        public ActionResult Post(PostRental model)
        {
            var rental = new Rental(model);
            Context.Rentals.Insert(rental);
            return RedirectToAction("Index");
        }

        public ActionResult AttachImage(string id)
        {
            var rental = GetRental(id);
            return View(rental);
        }

        [HttpPost]
        public ActionResult AttachImage(string id, HttpPostedFileBase file)
        {
            var rental = GetRental(id);
            var imageId = ObjectId.GenerateNewId();
            rental.ImageId = imageId.ToString();
            Context.Rentals.Save(rental);

            var options = new MongoGridFSCreateOptions
                          {
                              ContentType = file.ContentType,
                              Id = imageId
                          };
            var bytes = new byte[file.InputStream.Length];
            var byteCOunt = file.InputStream.Read(bytes, 0, (int)file.InputStream.Length);
            var base64Content = Convert.ToBase64String(bytes);
            Context.Database.GridFS.Upload(file.InputStream, file.FileName, options);
            return RedirectToAction("Index");
        }

        public FileResult GetImage(string id)
        {
            var image = Context.Database.GridFS.FindOneById(new ObjectId(id));
            if (image == null)
            {
                return null;
            }

            return this.File(image.OpenRead(), image.ContentType);
        }

        #endregion

        #region Methods

        private Rental GetRental(string id)
        {
            return Context.Rentals.FindOneById(new ObjectId(id));
        }

        #endregion
    }
}