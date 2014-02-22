using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogingFramework.Models;

namespace LogingFramework.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

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

            return View("Index", model);

        }
    }
}
