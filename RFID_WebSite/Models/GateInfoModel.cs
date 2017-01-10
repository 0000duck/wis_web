using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace RFID_WebSite.Models
{
    public class GateInfoModel
    {
        public List<Structure.RF_CONTAINERINFO> GetGateInfo(string FAB, string AREA)
        {
            try
            {
                List<Structure.RF_CONTAINERINFO> RF_CONTAINERINFOobj = new List<Structure.RF_CONTAINERINFO>();
                   OracleDB dbObj = new OracleDB("RFID_DB");
                   string sqlString = @"select t.*, t.rowid from rf_containerinfo t  where t.fab='{0}' and t.area='{1}'";
                sqlString = string.Format(sqlString, FAB,AREA);
                DataTable result = dbObj.SelectSQL(sqlString);

                for (int i = 0; i < 6; i++)
                {
                    Structure.RF_CONTAINERINFO eachObj = new Structure.RF_CONTAINERINFO();
                    eachObj.GATE = (i + 1).ToString();
                    eachObj.FAB = "";
                    eachObj.AREA = "";
                    eachObj.CAR_TYPE = "";
                    eachObj.CONTAINER_ID = "";
                    eachObj.CONTAINER_TYPE = "";
                    eachObj.CONTAINER_STATUS = "";
                    eachObj.VENDOR_NAME = "";
                    eachObj.STARTTIME = "";
                    eachObj.ENDTIME = "";
                    eachObj.CAR_ID = "";
                    RF_CONTAINERINFOobj.Add(eachObj);
                }

                    foreach (DataRow eachRow in result.Rows)
                    {
                        Structure.RF_CONTAINERINFO eachObj = new Structure.RF_CONTAINERINFO();

                        int gateNo = 0;

                        eachObj.FAB = eachRow["FAB"].ToString();
                        eachObj.AREA = eachRow["AREA"].ToString();
                        eachObj.GATE = eachRow["GATE"].ToString();
                        gateNo = int.Parse(eachObj.GATE);
                        eachObj.CAR_TYPE = eachRow["CAR_TYPE"].ToString();
                        eachObj.CONTAINER_ID = eachRow["CONTAINER_ID"].ToString();
                        eachObj.CONTAINER_TYPE = eachRow["CONTAINER_TYPE"].ToString();
                        eachObj.CONTAINER_STATUS = eachRow["CONTAINER_STATUS"].ToString();
                        eachObj.VENDOR_NAME = eachRow["VENDOR_NAME"].ToString();
                        eachObj.STARTTIME = eachRow["STARTTIME"].ToString();
                        eachObj.ENDTIME = eachRow["ENDTIME"].ToString();
                        eachObj.CAR_ID = eachRow["CAR_ID"].ToString();
                        if (gateNo != 0 && gateNo <= 6)
                        {
                            RF_CONTAINERINFOobj[gateNo - 1] = eachObj;
                        }
                        
                    }
                return RF_CONTAINERINFOobj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Structure.CaptionInfo> GetCaptionList(string FAB, string AREA)
        {
            try
            {
                List<Structure.CaptionInfo> CaptionList = new List<Structure.CaptionInfo>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select t.fab,t.area,t.gate,t.readerip, t1.caption, t1.active  from rf_antmapping t, rf_custcaption t1 where t.type = 'CT' and t.antnumber < 5 and t.fab = t1.fab(+) and t.area = t1.area(+) and t.gate = t1.gate(+)";

                if (FAB != null)
                {
                    if (!FAB.Equals(""))
                    {
                        sqlString += " and t.fab = '" + FAB + "' ";
                    }
                }

                if (AREA != null)
                {
                    if (!AREA.Equals(""))
                    {
                        sqlString += " and t.area = '" + AREA + "' ";
                    }
                }
                sqlString += " order by t.fab,t.area,t.gate";

                DataTable result = dbObj.SelectSQL(sqlString);               

                foreach (DataRow eachRow in result.Rows)
                {
                    Structure.CaptionInfo eachObj = new Structure.CaptionInfo();

                    eachObj.Fab = eachRow["FAB"].ToString();
                    eachObj.Area = eachRow["AREA"].ToString();
                    eachObj.Gate = eachRow["GATE"].ToString();                
                    eachObj.CustCaptionStr = eachRow["Caption"].ToString();
                    if (eachRow["Active"].ToString().Equals("T"))
                    {
                        eachObj.Active = "T";
                    }
                    else
                    {
                        eachObj.Active = "F";
                    }
                    eachObj.ReaderIP = eachRow["readerip"].ToString();
                    eachObj.Modify = false;

                    CaptionList.Add(eachObj);
                }
                return CaptionList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}