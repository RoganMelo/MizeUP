using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MizeUP.Util
{
    public class TempDataActionResult : ActionResult
    {
        private readonly ActionResult _actionResult;
        private readonly string _message;
        private readonly string _classAlert;
        private readonly string _classGlyphicon;

        public TempDataActionResult(ActionResult actionResult, string message, string classAlert, string classGlyphicon)
        {
            _actionResult = actionResult;
            _message = message;
            _classAlert = classAlert;
            _classGlyphicon = classGlyphicon;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.Controller.TempData["Message"] = _message;
            context.Controller.TempData["ClassAlert"] = _classAlert;
            context.Controller.TempData["ClassGlyphicon"] = _classGlyphicon;
            _actionResult.ExecuteResult(context);
        }
    }
}