﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFID_WebSite.Models;
using Newtonsoft.Json;//byEASY for datatable to json
using System.Data;
using System.Net;

namespace RFID_WebSite.Controllers
{
    public class HomeController : Controller
    {
        string hostNameStr = "";

        //protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        //{
        //    hostNameStr = requestContext.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        //    if (hostNameStr==null)
        //    {
        //        hostNameStr = requestContext.HttpContext.Request.UserHostName;
        //    }
        //    // do something based on 'hostname' value
        //    // ....

        //    base.Initialize(requestContext);
        //}

        public string DetermineCompName(string IP)
        {
            IPAddress myIP = IPAddress.Parse(IP);
            IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
            List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
            return compName.First();
        }

        public JsonResult AddTagMapping(string realID)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                RFID_WebSite.Models.ContainerModels ModelsData = new RFID_WebSite.Models.ContainerModels();
                ModelsData.AddTagMapping(realID);
                result.Status = "200";
                result.Message = "Success";
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

        public JsonResult GetTagMappingByRealID(string realID)
        {
            try
            {
                ResponseContent<Structure.RF_TagMapping> result = new ResponseContent<Structure.RF_TagMapping>();
                RFID_WebSite.Models.ContainerModels ModelsData = new RFID_WebSite.Models.ContainerModels();
                Structure.RF_TagMapping objs = ModelsData.GetTagMappingByRealID(realID);
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

        public JsonResult UpdateAntSetting(string eachSetting)
        {
            try
            {
                Structure.AntSetting infoObj = JsonConvert.DeserializeObject<Structure.AntSetting>(eachSetting);
                RFID_WebSite.Models.GateInfoModel AntSettingData = new GateInfoModel();
                AntSettingData.UpdateAntSetting(infoObj);

                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetAreaList(string Fab)
        {
            try
            {
                RFID_WebSite.Models.GateInfoModel AntSettingData = new GateInfoModel();
                List<string> objs = AntSettingData.GetAreaList(Fab);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetAntSetting(string Fab,string Area)
        {
            try
            {
                RFID_WebSite.Models.GateInfoModel AntSettingData = new GateInfoModel();
                List<Structure.AntSetting> objs = AntSettingData.GetAntSetting(Fab,Area);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetAreaCount()
        {
            try
            {
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.areaCount objs = containerData.GetAreaCount();

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        //byEASY
        public JsonResult GetCarDetail(String DivPoint)
        {
            try
            {

                RFID_WebSite.Models.ContainerModels cardata = new ContainerModels();
                DataTable carDT = cardata.getCarDetail(DivPoint);
                string objs = JsonConvert.SerializeObject(carDT, Formatting.Indented);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetShipTo(string carID)
        {
            try
            {
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                string shipTo = containerData.GetShipTo(carID);

                return Json(shipTo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetContainerInfo(string containerID, string carID)
        {
            try
            {
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.containerInfo objs = containerData.GetContainerInfo(containerID, carID);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetHostName()
        {
            hostNameStr = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (hostNameStr == null)
            {
                hostNameStr = HttpContext.Request.UserHostName;
            }
            // hostNameStr = HttpContext.Request.Headers["REMOTE_ADDR"];
            string hostName = DetermineCompName(hostNameStr);
            //System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            return Json(hostName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddContainerInfo(string jsonObj)
        {
            try
            {
                Structure.ResponseObj result = new Structure.ResponseObj();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.containerInfo infoObj = JsonConvert.DeserializeObject<Structure.containerInfo>(jsonObj);
                infoObj.start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (infoObj.ID.Equals(""))
                {
                    infoObj.ID = infoObj.carID;
                }
                containerData.AddInfo(infoObj);
                result.status = "OK";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Structure.ResponseObj result = new Structure.ResponseObj();
                result.status = "NG";
                result.detail = e.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        //byEASY
        public JsonResult updateContainerInfo(String jsonObj)
        {
            try
            {
                Structure.ResponseObj result = new Structure.ResponseObj();
                Structure.containerInfo infoObj = JsonConvert.DeserializeObject<Structure.containerInfo>(jsonObj);
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                infoObj.end = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                containerData.updateInfo(infoObj);
                result.status = "OK";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Structure.ResponseObj result = new Structure.ResponseObj();
                result.status = "NG";
                result.detail = e.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult getSourceList()
        {
            try
            {

                RFID_WebSite.Models.VendorListModel ModelData = new VendorListModel();
                DataTable DT = ModelData.GetSource();
                string objs = JsonConvert.SerializeObject(DT, Formatting.Indented);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult DeleteSource(string input)
        {
            try
            {

                RFID_WebSite.Models.VendorListModel ModelData = new VendorListModel();

                string objs = ModelData.DeleteSource(JsonConvert.DeserializeObject<Structure.Source>(input));

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult UpdateSource(string input)
        {
            try
            {

                RFID_WebSite.Models.VendorListModel ModelData = new VendorListModel();
                
                string objs = ModelData.ModifySource(JsonConvert.DeserializeObject<Structure.Source>(input));

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        //byEASY
        public JsonResult getVendorList()
        {
            try
            {

                RFID_WebSite.Models.VendorListModel ModelData = new VendorListModel();
                DataTable DT = ModelData.getVendorlistModel();
                string objs = JsonConvert.SerializeObject(DT, Formatting.Indented);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //byEASY
        public JsonResult getVendorOnly(String key)
        {
            try
            {

                RFID_WebSite.Models.VendorListModel ModelData = new VendorListModel();
                DataTable DT = ModelData.getVendorOnlyModel(key);
                string objs = JsonConvert.SerializeObject(DT, Formatting.Indented);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult AddContainerHistory(string jsonObj)
        {
            try
            {
                Structure.ResponseObj result = new Structure.ResponseObj();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.containerHis hisObj = JsonConvert.DeserializeObject<Structure.containerHis>(jsonObj);
                hisObj.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (hisObj.container_id.Equals(""))
                {
                    hisObj.container_id = hisObj.car_id;
                }
                containerData.AddHistory(hisObj);
                result.status = "OK";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Structure.ResponseObj result = new Structure.ResponseObj();
                result.status = "NG";
                result.detail = e.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetContainerHistory(string containerID, string carID, string fab, string start, string end)
        {
            try
            {
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                List<Structure.containerHis> objs = containerData.GetContainerHistory(containerID, carID, fab, start, end);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetTagHistory(string area, string gateID, string fab, string antType, string hideTagType, string start, string end)
        {
            try
            {
                RFID_WebSite.Models.TagModels TagModelsData = new TagModels();
                List<Structure.TagHis> objs = TagModelsData.GeTagHistory(area, gateID, fab, antType, hideTagType, start, end);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetSearchTagHistory(string area, string gateID, string fab, string antType, string tagID, string start, string end)
        {
            try
            {
                RFID_WebSite.Models.TagModels TagModelsData = new TagModels();
                List<Structure.TagHis> objs = TagModelsData.SearchTagHistory(area, gateID, fab, antType, tagID, start, end);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetContainerList()
        {
            try
            {
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                List<string> objs = containerData.GetContainerList();

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetDriverNameList()
        {
            try
            {
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                List<string> objs = containerData.GetDriverNameList();

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetDriverInfo(string name)
        {
            try
            {
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.driverInfo objs = containerData.GetDriverInfo(name);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetCarIDList()
        {
            try
            {
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                List<string> objs = containerData.GetCarIDList();

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetDriverInfoByCarID(string carID)
        {
            try
            {
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.driverInfo objs = containerData.GetDriverInfoByCarID(carID);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetContainer(string id)
        {
            try
            {
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.containerInfo objs = containerData.GetContainer(id);

                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetLayout(string Name)
        {
            try
            {
                RFID_WebSite.Models.LayoutModel layoutData = new LayoutModel();
                List<Structure.layoutObj> objs = new List<Structure.layoutObj>();
                objs = layoutData.GetLayout(Name);
                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SaveLayout(string Name, string listObject)
        {
            try
            {
                Structure.ResponseObj responseObj = new Structure.ResponseObj();
                List<Structure.layoutObj> objs = new List<Structure.layoutObj>();
                RFID_WebSite.Models.LayoutModel layoutData = new LayoutModel();

                objs = JsonConvert.DeserializeObject<List<Structure.layoutObj>>(listObject);

                layoutData.StoreLayout(Name, objs);
                responseObj.status = "OK";
                return Json(responseObj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                Structure.ResponseObj responseObj = new Structure.ResponseObj();
                responseObj.status = "ERROR";
                responseObj.detail = e.Message;

                return Json(responseObj, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCustCaption(string FAB, string AREA)
        {
            try
            {
                RFID_WebSite.Models.GateInfoModel GateData = new GateInfoModel();
                List<Structure.CaptionInfo> CaptionList = GateData.GetCaptionList(FAB, AREA);

                return Json(CaptionList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.StackTrace, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetGateData(string FAB, string AREA,string GATE)
        {
            try
            {
                RFID_WebSite.Models.GateInfoModel GateData = new GateInfoModel();
                Structure.RF_ANTCURRENT Gatainfo = new Structure.RF_ANTCURRENT();

                Gatainfo = GateData.GetEachGate(FAB,AREA,GATE);

                return Json(Gatainfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.StackTrace, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetGateStatus()
        {
            try
            {
                RFID_WebSite.Models.GateInfoModel GateData = new GateInfoModel();
                List<Structure.RF_ANTCURRENT> Gatainfolist = new List<Structure.RF_ANTCURRENT>();

                Gatainfolist = GateData.GetGateStatus();

                return Json(Gatainfolist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.StackTrace, JsonRequestBehavior.AllowGet);
            }

        }

        // byEASY
        public JsonResult GateInfo(string FAB, string AREA)
        {
            try
            {
                RFID_WebSite.Models.GateInfoModel GateData = new GateInfoModel();
                List<Structure.RF_CONTAINERINFO> Gatainfolist = new List<Structure.RF_CONTAINERINFO>();

                Gatainfolist = GateData.GetGateInfo(FAB, AREA);

                return Json(Gatainfolist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.StackTrace, JsonRequestBehavior.AllowGet);
            }

        }
        //byEASY
        public JsonResult getVendorDetail(string guestid)
        {
            try
            {
                RFID_WebSite.Models.VendorListModel vendorlist = new VendorListModel();
                DataTable vendorData = vendorlist.getVendorData(guestid);
                string objs = JsonConvert.SerializeObject(vendorData, Formatting.Indented);

                return Json(objs, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {
                return Json(e.StackTrace, JsonRequestBehavior.AllowGet);
            }

        }
        //byEASY
        public JsonResult DeleteVendorData(string jsonObj)
        {
            try
            {
                Structure.ResponseObj result = new Structure.ResponseObj();
                RFID_WebSite.Models.VendorListModel vendordelete = new VendorListModel();
                Structure.vendorManagement Obj = JsonConvert.DeserializeObject<Structure.vendorManagement>(jsonObj);
                Obj.UPDATETIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                vendordelete.DeleteVendor(Obj);
                result.status = "OK";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Structure.ResponseObj result = new Structure.ResponseObj();
                result.status = "NG";
                result.detail = e.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        //byEASY
        public JsonResult ModifyVendorData(string jsonObj)
        {
            try
            {
                Structure.ResponseObj result = new Structure.ResponseObj();
                RFID_WebSite.Models.VendorListModel vendormodify = new VendorListModel();
                Structure.vendorManagement Obj = JsonConvert.DeserializeObject<Structure.vendorManagement>(jsonObj);
                Obj.UPDATETIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                vendormodify.ModifyVendor(Obj);
                result.status = "OK";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Structure.ResponseObj result = new Structure.ResponseObj();
                result.status = "NG";
                result.detail = e.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ImportVendorData(string jsonObj)
        {

            RFID_WebSite.Models.VendorListModel vendormodify = new VendorListModel();
            List<Structure.vendorManagement> Obj = JsonConvert.DeserializeObject<List<Structure.vendorManagement>>(jsonObj);

            for (int i = 0; i < Obj.Count; i++)
            {
                try
                {
                    if (Obj[i].VENDORID != null)
                    {
                        Obj[i].UPDATETIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        vendormodify.ModifyVendor(Obj[i]);
                    }
                    else
                    {
                        return Json("錯誤行數:" + (i + 1) + " VENDORID is null");
                      
                    }
                }
                catch (Exception e)
                {
                    return Json("錯誤行數:" + (i + 1) + " Excepton:" + e.Message, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {


            return View();
        }
        public ActionResult WriteTag()
        {


            return View();
        }
        public ActionResult WriteTagGo()//byEASY
        {


            return View();
        }
        public ActionResult TagHandle()
        {


            return View();
        }
        public ActionResult About()
        {


            return View();
        }
        public ActionResult Layout()
        {


            return View();
        }
        //[MyCustomAuthorize(UserGroup = "Data")]
        public ActionResult Data()
        {

            return View();
        }
        public ActionResult T1_PurchasePort()
        {


            return View();
        }
        public ActionResult T2_PurchasePort()
        {


            return View();
        }

        public ActionResult T1_ShipingPort()
        {


            return View();
        }
        public ActionResult T2_ShipingPort()
        {
            return View();
        }
        public ActionResult VendorManagement()//byEASY
        {
            return View();
        }
        public ActionResult TagHistory()
        {

            return View();
        }
        public ActionResult SearchTagHistory()
        {
            return View();
        }
        public ActionResult CustCaption()
        {
            return View();
        }
        public ActionResult GateStatus()
        {
            return View();
        }
        public ActionResult RFAttenuation()
        {
            return View();
        }
        public ActionResult SourceList()
        {
            return View();
        }
    }
}
