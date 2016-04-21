using MizeUP.Internacionalization;
using MizeUP.Mappings;
using MizeUP.Models;
using MizeUP.Util;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MizeUP.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(contact);
                    transaction.Commit();
                }
            }

            return RedirectToAction("Index").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
        }

        [HttpGet]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}