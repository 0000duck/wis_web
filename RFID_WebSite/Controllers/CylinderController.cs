using RFID_WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFID_WebSite.Controllers
{
    public class CylinderController : Controller
    {

       
        //
        // GET: /Cylinder/

        public ActionResult Status()
        {
            return View();
        }

        public JsonResult GetCylinders()
        {

            try
            {

                ResponseContent<List<CylinderModels.Cylinder>> result = new ResponseContent<List<CylinderModels.Cylinder>>();
                CylinderModels ModelsData = new CylinderModels();
                List<CylinderModels.Cylinder> objs = ModelsData.GetCylinders();
                result.Status = "200";
                result.Message = objs;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                ResponseContent<string> result = new ResponseContent<string>();
                result.Status = "500";
                result.Message = e.Message + " " + e.StackTrace;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

    }
}
