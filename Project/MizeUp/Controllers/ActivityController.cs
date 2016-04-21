using MizeUP.Filters;
using MizeUP.Internacionalization;
using MizeUP.Mappings;
using MizeUP.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MizeUP.Controllers
{
    [AuthenticationFilter]
    public class ActivityController : Controller
    {

        public ActionResult List()
        {
            return null;
        }

        public ActionResult Create() 
        {
            return View();
        }

        public ActionResult Edit() 
        {
            return null;
        }

        public ActionResult Delete() 
        {
            return null;
        }
        
    }
}