using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFID_WebSite.Models;
using Newtonsoft.Json;

namespace RFID_WebSite.Controllers
{
    public class BCRecipeBodyController : Controller
    {
        //
        // GET: /BCRecipeBody/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Query()
        {
            return View();
        }

        public JsonResult GetRecipeBodyValue(string bcInfo, string eqpInfo, string ED03Info)
        {

            try
            {
                RFID_WebSite.Models.BCModels.BCInfo bcObj = JsonConvert.DeserializeObject<RFID_WebSite.Models.BCModels.BCInfo>(bcInfo);
                RFID_WebSite.Models.BCModels.ED03 ed03Obj = JsonConvert.DeserializeObject<RFID_WebSite.Models.BCModels.ED03>(ED03Info);
                RFID_WebSite.Models.BCModels.EQP eqpObj = JsonConvert.DeserializeObject<RFID_WebSite.Models.BCModels.EQP>(eqpInfo);
                ResponseContent<List<RFID_WebSite.Models.BCModels.RecipeBodyParseData>> result = new ResponseContent<List<RFID_WebSite.Models.BCModels.RecipeBodyParseData>>();


                RFID_WebSite.Models.BCModels BCModelsData = new RFID_WebSite.Models.BCModels();
                List<RFID_WebSite.Models.BCModels.RecipeBodyParseData> repValList = BCModelsData.GetRecipeBodyParseData(bcObj, eqpObj, ed03Obj);
                result.Status = "200";
                result.Message = repValList;
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

        public JsonResult CreateBCCmd(string bcInfo, string recipeInfo)
        {

            try
            {
                RFID_WebSite.Models.BCModels.BCInfo bcObj = JsonConvert.DeserializeObject<RFID_WebSite.Models.BCModels.BCInfo>(bcInfo);
                RFID_WebSite.Models.BCModels.Recipe repObj = JsonConvert.DeserializeObject<RFID_WebSite.Models.BCModels.Recipe>(recipeInfo);
                ResponseContent<string> result = new ResponseContent<string>();


                RFID_WebSite.Models.BCModels BCModelsData = new RFID_WebSite.Models.BCModels();
                BCModelsData.CreateBCCmd(bcObj, repObj);
                result.Status = "200";
                result.Message = "";
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

        public JsonResult GetED03(string bcInfo,string eqpInfo)
        {

            try
            {
                RFID_WebSite.Models.BCModels.BCInfo bcObj = JsonConvert.DeserializeObject<RFID_WebSite.Models.BCModels.BCInfo>(bcInfo);
                RFID_WebSite.Models.BCModels.EQP eqpObj = JsonConvert.DeserializeObject<RFID_WebSite.Models.BCModels.EQP>(eqpInfo);
                ResponseContent<RFID_WebSite.Models.BCModels.ED03> result = new ResponseContent<RFID_WebSite.Models.BCModels.ED03>();


                RFID_WebSite.Models.BCModels BCModelsData = new RFID_WebSite.Models.BCModels();
                RFID_WebSite.Models.BCModels.ED03 objs = BCModelsData.GetED03(bcObj, eqpObj);
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

        public JsonResult GetEQPList(string bcInfo)
        {

            try
            {
                RFID_WebSite.Models.BCModels.BCInfo bcObj = JsonConvert.DeserializeObject<RFID_WebSite.Models.BCModels.BCInfo>(bcInfo);
                ResponseContent<List<BCModels.EQP>> result = new ResponseContent<List<BCModels.EQP>>();


                RFID_WebSite.Models.BCModels BCModelsData = new RFID_WebSite.Models.BCModels();
                List<RFID_WebSite.Models.BCModels.EQP> objs = BCModelsData.GetEQP(bcObj);
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

        public JsonResult GetPPIDList(string bcInfo,string ppid)
        {

            try
            {
                RFID_WebSite.Models.BCModels.BCInfo bcObj = JsonConvert.DeserializeObject<RFID_WebSite.Models.BCModels.BCInfo>(bcInfo);
                ResponseContent<List<BCModels.PPID>> result = new ResponseContent<List<BCModels.PPID>>();


                RFID_WebSite.Models.BCModels BCModelsData = new RFID_WebSite.Models.BCModels();
                List<RFID_WebSite.Models.BCModels.PPID> objs = BCModelsData.GetPPID(bcObj, ppid);
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

        public JsonResult GetBCList()
        {
            
            try
            {
                ResponseContent<List<BCModels.BCInfo>> result = new ResponseContent<List<BCModels.BCInfo>>();
                RFID_WebSite.Models.BCModels BCModelsData = new RFID_WebSite.Models.BCModels();
                List<RFID_WebSite.Models.BCModels.BCInfo> objs = BCModelsData.GetBCInfo();
                result.Status = "200";
                result.Message = objs;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                ResponseContent<string> result = new ResponseContent<string>();
                result.Status = "500";
                result.Message = e.Message+" "+e.StackTrace;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetRecipeBody(string bcName, string bcIP)
        {
           
            try
            {
                ResponseContent<List<BCModels.RecipeBody>> result = new ResponseContent<List<BCModels.RecipeBody>>();
                RFID_WebSite.Models.BCModels BCModelsData = new RFID_WebSite.Models.BCModels();
                List<RFID_WebSite.Models.BCModels.RecipeBody> objs = BCModelsData.GetRecipeBody(bcName, bcIP);
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
