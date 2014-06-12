// <copyright file="RentalController.cs " company="Socar Turkey Petrol-Enerji Dağıtım A.Ş." owner="Erdal Dalkıran">
// <createDate>12/06/2014 11:08</createDate>
// </copyright>
namespace RealEstate.Controllers
{
    using System.Web.Mvc;

    using RealEstate.App_Start;
    using RealEstate.Rentals;

    public class RentalController : Controller
    {
        public readonly RealEstateContext Context = new RealEstateContext();


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
    }
}