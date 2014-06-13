namespace RealEstate.Controllers
{
    using System.Web.Mvc;

    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

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
            var result = Context.Rentals.Remove(Query.EQ("_id",new ObjectId(id)),RemoveFlags.Single,WriteConcern.Acknowledged);
            return RedirectToAction("Index");
        }


        public ActionResult Index()
        {
            var rentals = Context.Rentals;
            var model = rentals.FindAll();
            return View(model);
        }

        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(PostRental model)
        {
            var rental = new Rental(model);
            Context.Rentals.Insert(rental);
            return RedirectToAction("Index");
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