using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoggingSystem;
using LoggingSystem.Models;
using LogingFramework.Models;

namespace LogingFramework.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        LogSystem _loggingSystm = new LogSystem(new Guid(), new LogPublisherFactory());


        public ActionResult Index()
        {
            return View(new IndexViewModel{Message = "Unset Message"});
        }

        [HttpPost]
        public ActionResult MultiButtonForm(string command )
        {

            var model = new IndexViewModel();

            switch (command)
            {
                case "HELLO":
                    {
                        model.Message = "This means Hello";
                        break;
                    }
                case "BYE":
                    {
                        model.Message = "This means Goodby";
                        break;
                    }
            }

            _loggingSystm.Log(LoggingLevel.Info, model.Message, null);
            return View("Index", model);

        }
    }
}
