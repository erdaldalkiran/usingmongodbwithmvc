// <copyright file="HomeController.cs " company="Socar Turkey Petrol-Enerji Dağıtım A.Ş." owner="Erdal Dalkıran">
// <createDate>10/06/2014 18:32</createDate>
// </copyright>

namespace RealEstate.Controllers
{
    using System.Web.Mvc;

    using RealEstate.App_Start;

    public class HomeController : Controller
    {
        #region Fields

        public RealEstateContext Context = new RealEstateContext();

        #endregion

        #region Public Methods and Operators

        public ActionResult About()
        {
            Context.Database.GetStats();
            return Json(Context.Database.Server.BuildInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}