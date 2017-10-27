using RFID_WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFID_WebSite.Controllers
{
    public class SPCYController : Controller
    {
        //
        // GET: /SPCY/

        public ActionResult Config()
        {
            return View();
        }

        public ActionResult ErrorLog()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }

        public JsonResult GetConfigProd()
        {
            try
            {
                ResponseContent<List<string>> result = new ResponseContent<List<string>>();
                RFID_WebSite.Models.SPCYModels SPCYModelsData = new RFID_WebSite.Models.SPCYModels();
                List<string> objs = SPCYModelsData.GetConfigProd();
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

        public JsonResult GetErrorLogProd()
        {
            try
            {
                ResponseContent<List<string>> result = new ResponseContent<List<string>>();
                RFID_WebSite.Models.SPCYModels SPCYModelsData = new RFID_WebSite.Models.SPCYModels();
                List<string> objs = SPCYModelsData.GetErrorLogProd();
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

        public JsonResult GetConfig(string Step, string Prod_ID, string Parameter)
        {

            try
            {

                ResponseContent<List<SPCYModels.Config>> result = new ResponseContent<List<SPCYModels.Config>>();


                RFID_WebSite.Models.SPCYModels SPCYModelsData = new RFID_WebSite.Models.SPCYModels();
                List<RFID_WebSite.Models.SPCYModels.Config> objs = SPCYModelsData.GetConfig(Step,Prod_ID,Parameter);
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

        public JsonResult GetErrorLog(string BeginTime, string EndTime, string Prod_ID)
        {

            try
            {

                ResponseContent<List<SPCYModels.ErrorLog>> result = new ResponseContent<List<SPCYModels.ErrorLog>>();


                RFID_WebSite.Models.SPCYModels SPCYModelsData = new RFID_WebSite.Models.SPCYModels();
                List<RFID_WebSite.Models.SPCYModels.ErrorLog> objs = SPCYModelsData.GetErrorLog(BeginTime, EndTime, Prod_ID);
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
