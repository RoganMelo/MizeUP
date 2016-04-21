using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MizeUP.Util
{
    public static class ActionResultExtensions
    {
        public static ActionResult WithMessage(this ActionResult actionResult, string message, string classAlert, string classGlyphicon)
        {
            return new TempDataActionResult(actionResult, message, classAlert, classGlyphicon);
        }
    }
}