using System;
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

        public JsonResult GetTagMappingByTagID(string tagID)
        {
            try
            {
                ResponseContent<Structure.RF_TagMapping> result = new ResponseContent<Structure.RF_TagMapping>();
                RFID_WebSite.Models.ContainerModels ModelsData = new RFID_WebSite.Models.ContainerModels();
                Structure.RF_TagMapping objs = ModelsData.GetTagMappingByTagID(tagID);
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
                ResponseContent<string> result = new ResponseContent<string>();
                RFID_WebSite.Models.GateInfoModel AntSettingData = new GateInfoModel();
                AntSettingData.UpdateAntSetting(infoObj);
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

        public JsonResult GetAreaList(string Fab)
        {
            try
            {
                ResponseContent<List<string>> result = new ResponseContent<List<string>>();
                RFID_WebSite.Models.GateInfoModel AntSettingData = new GateInfoModel();
                List<string> objs = AntSettingData.GetAreaList(Fab);

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

        public JsonResult GetAntSetting(string Fab, string Area)
        {
            try
            {
                ResponseContent<List<Structure.AntSetting>> result = new ResponseContent<List<Structure.AntSetting>>();
                RFID_WebSite.Models.GateInfoModel AntSettingData = new GateInfoModel();
                List<Structure.AntSetting> objs = AntSettingData.GetAntSetting(Fab, Area);

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

        public JsonResult GetAreaCount()
        {
            try
            {
                ResponseContent<Structure.areaCount> result = new ResponseContent<Structure.areaCount>();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.areaCount objs = containerData.GetAreaCount();

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

        //byEASY
        public JsonResult GetCarDetail(String DivPoint)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                RFID_WebSite.Models.ContainerModels cardata = new ContainerModels();
                DataTable carDT = cardata.getCarDetail(DivPoint);
                string objs = JsonConvert.SerializeObject(carDT, Formatting.Indented);
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

        public JsonResult GetShipTo(string carID)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                string shipTo = containerData.GetShipTo(carID);

                result.Status = "200";
                result.Message = shipTo;
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

        public JsonResult GetContainerInfo(string containerID, string carID)
        {
            try
            {
                ResponseContent<Structure.containerInfo> result = new ResponseContent<Structure.containerInfo>();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.containerInfo objs = containerData.GetContainerInfo(containerID, carID);

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

        public JsonResult GetHostName()
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                hostNameStr = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (hostNameStr == null)
                {
                    hostNameStr = HttpContext.Request.UserHostName;
                }
                // hostNameStr = HttpContext.Request.Headers["REMOTE_ADDR"];
                string hostName = DetermineCompName(hostNameStr);
                //System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                result.Status = "200";
                result.Message = hostName;
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

        public JsonResult AddContainerInfo(string jsonObj)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();

                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.containerInfo infoObj = JsonConvert.DeserializeObject<Structure.containerInfo>(jsonObj);
                infoObj.start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (infoObj.ID.Equals(""))
                {
                    infoObj.ID = infoObj.carID;
                }
                containerData.AddInfo(infoObj);
                result.Status = "200";
                result.Message = "OK";
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
        //byEASY
        public JsonResult updateContainerInfo(String jsonObj)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                Structure.containerInfo infoObj = JsonConvert.DeserializeObject<Structure.containerInfo>(jsonObj);
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                infoObj.end = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Structure.RF_TagMapping tag = containerData.GetTagMappingByRealID(infoObj.ID);

                containerData.DeleteTagMapping(tag.Tag_ID);
                containerData.updateInfo(infoObj);
                result.Status = "200";
                result.Message = "OK";
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

        public JsonResult getSourceList()
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                RFID_WebSite.Models.VendorListModel ModelData = new VendorListModel();
                DataTable DT = ModelData.GetSource();
                string objs = JsonConvert.SerializeObject(DT, Formatting.Indented);

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

        public JsonResult DeleteSource(string input)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                RFID_WebSite.Models.VendorListModel ModelData = new VendorListModel();

                string objs = ModelData.DeleteSource(JsonConvert.DeserializeObject<Structure.Source>(input));

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

        public JsonResult UpdateSource(string input)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                RFID_WebSite.Models.VendorListModel ModelData = new VendorListModel();

                string objs = ModelData.ModifySource(JsonConvert.DeserializeObject<Structure.Source>(input));

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

        //byEASY
        public JsonResult getVendorList()
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                RFID_WebSite.Models.VendorListModel ModelData = new VendorListModel();
                DataTable DT = ModelData.getVendorlistModel();
                string objs = JsonConvert.SerializeObject(DT, Formatting.Indented);

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
        //byEASY
        public JsonResult getVendorOnly(String key)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                RFID_WebSite.Models.VendorListModel ModelData = new VendorListModel();
                DataTable DT = ModelData.getVendorOnlyModel(key);
                string objs = JsonConvert.SerializeObject(DT, Formatting.Indented);

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

        public JsonResult AddContainerHistory(string jsonObj)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.containerHis hisObj = JsonConvert.DeserializeObject<Structure.containerHis>(jsonObj);
                hisObj.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (hisObj.container_id.Equals(""))
                {
                    hisObj.container_id = hisObj.car_id;
                }
                containerData.AddHistory(hisObj);
                result.Status = "200";
                result.Message = "OK";
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

        public JsonResult GetContainerHistory(string containerID, string carID, string fab, string start, string end)
        {
            try
            {
                ResponseContent<List<Structure.containerHis>> result = new ResponseContent<List<Structure.containerHis>>();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                List<Structure.containerHis> objs = containerData.GetContainerHistory(containerID, carID, fab, start, end);

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

        public JsonResult GetTagHistory(string area, string gateID, string fab, string antType, string hideTagType, string start, string end)
        {
            try
            {
                ResponseContent<List<Structure.TagHis>> result = new ResponseContent<List<Structure.TagHis>>();
                RFID_WebSite.Models.TagModels TagModelsData = new TagModels();
                List<Structure.TagHis> objs = TagModelsData.GeTagHistory(area, gateID, fab, antType, hideTagType, start, end);

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

        public JsonResult GetSearchTagHistory(string area, string gateID, string fab, string antType, string tagID, string start, string end)
        {
            try
            {
                ResponseContent<List<Structure.TagHis>> result = new ResponseContent<List<Structure.TagHis>>();
                RFID_WebSite.Models.TagModels TagModelsData = new TagModels();
                List<Structure.TagHis> objs = TagModelsData.SearchTagHistory(area, gateID, fab, antType, tagID, start, end);

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

        public JsonResult GetContainerList()
        {
            try
            {
                ResponseContent<List<string>> result = new ResponseContent<List<string>>();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                List<string> objs = containerData.GetContainerList();

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

        public JsonResult GetDriverNameList()
        {
            try
            {
                ResponseContent<List<string>> result = new ResponseContent<List<string>>();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                List<string> objs = containerData.GetDriverNameList();

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

        public JsonResult GetDriverInfo(string name)
        {
            try
            {
                ResponseContent<Structure.driverInfo> result = new ResponseContent<Structure.driverInfo>();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.driverInfo objs = containerData.GetDriverInfo(name);

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

        public JsonResult GetCarIDList()
        {
            try
            {
                ResponseContent<List<string>> result = new ResponseContent<List<string>>();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                List<string> objs = containerData.GetCarIDList();

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

        public JsonResult GetDriverInfoByCarID(string carID)
        {
            try
            {
                ResponseContent<Structure.driverInfo> result = new ResponseContent<Structure.driverInfo>();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.driverInfo objs = containerData.GetDriverInfoByCarID(carID);

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

        public JsonResult GetContainer(string id)
        {
            try
            {
                ResponseContent<Structure.containerInfo> result = new ResponseContent<Structure.containerInfo>();
                RFID_WebSite.Models.ContainerModels containerData = new ContainerModels();
                Structure.containerInfo objs = containerData.GetContainer(id);

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

        public JsonResult GetLayout(string Name)
        {
            try
            {
                ResponseContent<List<Structure.layoutObj>> result = new ResponseContent<List<Structure.layoutObj>>();
                RFID_WebSite.Models.LayoutModel layoutData = new LayoutModel();
                List<Structure.layoutObj> objs = new List<Structure.layoutObj>();
                objs = layoutData.GetLayout(Name);
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

        public JsonResult SaveLayout(string Name, string listObject)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                List<Structure.layoutObj> objs = new List<Structure.layoutObj>();
                RFID_WebSite.Models.LayoutModel layoutData = new LayoutModel();

                objs = JsonConvert.DeserializeObject<List<Structure.layoutObj>>(listObject);

                layoutData.StoreLayout(Name, objs);
                result.Status = "200";
                result.Message = "OK";
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

        public JsonResult GetCustCaption(string FAB, string AREA)
        {
            try
            {
                ResponseContent<List<Structure.CaptionInfo>> result = new ResponseContent<List<Structure.CaptionInfo>>();
                RFID_WebSite.Models.GateInfoModel GateData = new GateInfoModel();
                List<Structure.CaptionInfo> objs = GateData.GetCaptionList(FAB, AREA);

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

        public JsonResult GetGateData(string FAB, string AREA, string GATE)
        {
            try
            {
                ResponseContent<Structure.RF_ANTCURRENT> result = new ResponseContent<Structure.RF_ANTCURRENT>();
                RFID_WebSite.Models.GateInfoModel GateData = new GateInfoModel();
                Structure.RF_ANTCURRENT objs = GateData.GetEachGate(FAB, AREA, GATE);

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

        public JsonResult GetGateStatus()
        {
            try
            {
                ResponseContent<List<Structure.RF_ANTCURRENT>> result = new ResponseContent<List<Structure.RF_ANTCURRENT>>();
                RFID_WebSite.Models.GateInfoModel GateData = new GateInfoModel();
                List<Structure.RF_ANTCURRENT> objs = GateData.GetGateStatus();

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

        // byEASY
        public JsonResult GateInfo(string FAB, string AREA)
        {
            try
            {
                ResponseContent<List<Structure.RF_CONTAINERINFO>> result = new ResponseContent<List<Structure.RF_CONTAINERINFO>>();
                RFID_WebSite.Models.GateInfoModel GateData = new GateInfoModel();
                List<Structure.RF_CONTAINERINFO> objs = GateData.GetGateInfo(FAB, AREA);

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
        //byEASY
        public JsonResult getVendorDetail(string guestid)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
                RFID_WebSite.Models.VendorListModel vendorlist = new VendorListModel();
                DataTable vendorData = vendorlist.getVendorData(guestid);
                string objs = JsonConvert.SerializeObject(vendorData, Formatting.Indented);

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
        //byEASY
        public JsonResult DeleteVendorData(string jsonObj)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();

                RFID_WebSite.Models.VendorListModel vendordelete = new VendorListModel();
                Structure.vendorManagement Obj = JsonConvert.DeserializeObject<Structure.vendorManagement>(jsonObj);
                Obj.UPDATETIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                vendordelete.DeleteVendor(Obj);
                result.Status = "200";
                result.Message = "OK";
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
        //byEASY
        public JsonResult ModifyVendorData(string jsonObj)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();

                RFID_WebSite.Models.VendorListModel vendormodify = new VendorListModel();
                Structure.vendorManagement Obj = JsonConvert.DeserializeObject<Structure.vendorManagement>(jsonObj);
                Obj.UPDATETIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                vendormodify.ModifyVendor(Obj);
                result.Status = "200";
                result.Message = "OK";
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

        public JsonResult ImportVendorData(string jsonObj)
        {
            try
            {
                ResponseContent<string> result = new ResponseContent<string>();
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
                result.Status = "200";
                result.Message = "OK";
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
