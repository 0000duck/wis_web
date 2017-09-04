using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace RFID_WebSite.Models
{
    public class GateInfoModel
    {
        public Structure.RF_ANTCURRENT GetEachGate(string FAB, string AREA, string GATE)
        {
            try
            {
                Structure.RF_ANTCURRENT result = new Structure.RF_ANTCURRENT();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select t.fab,
                                       t.area,
                                       t.gate,
                                       t.container_id,
                                       t.cartype,
                                       t1.readerip,
                                       t.updatetime,
                                       t.manualmode
                                  from rf_antcurrent t, rf_antmapping t1
                                 where t.fab = t1.fab
                                   and t.area = t1.area
                                   and t.gate = t1.gate
                                   and t.fab = '{0}'
                                   and t.area = '{1}'
                                   and t.gate = '{2}'
                                   and t1.type = 'CT'";
                sqlString = String.Format(sqlString,FAB,AREA,GATE);
                DataTable rt = dbObj.SelectSQL(sqlString);

                foreach (DataRow eachRow in rt.Rows)
                {
                    result.FAB = eachRow["FAB"].ToString();
                    result.AREA = eachRow["AREA"].ToString();
                    result.GATE = eachRow["GATE"].ToString();
                    result.CONTAINER_ID = eachRow["CONTAINER_ID"].ToString();
                    result.CAR_TYPE = eachRow["cartype"].ToString();

                    result.READER_IP = eachRow["readerip"].ToString();
                    result.UPDATETIME = eachRow["UPDATETIME"].ToString();
                    result.MANUALMODE = Boolean.Parse(eachRow["MANUALMODE"].ToString());
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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

        public List<Structure.RF_ANTCURRENT> GetGateStatus()
        {
            try
            {
                List<Structure.RF_ANTCURRENT> GateStatusList = new List<Structure.RF_ANTCURRENT>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select t.fab,t.area,t.gate,t1.container_id,t1.cartype,t.readerip,t1.updatetime,t1.manualmode
                                      from rf_antmapping t, rf_antcurrent t1
                                     where t.type = 'CT'
                                       and t.antnumber < 5
                                       and t.fab = t1.fab(+)
                                       and t.area = t1.area(+)
                                       and t.gate = t1.gate(+)
                                       order by t.fab,t.area,t.gate";
                DataTable result = dbObj.SelectSQL(sqlString);

                foreach (DataRow eachRow in result.Rows)
                {
                    Structure.RF_ANTCURRENT eachObj = new Structure.RF_ANTCURRENT();

                    eachObj.FAB = eachRow["FAB"].ToString();
                    eachObj.AREA = eachRow["AREA"].ToString();
                    eachObj.GATE = eachRow["GATE"].ToString();
                    eachObj.CONTAINER_ID = eachRow["CONTAINER_ID"].ToString();
                    eachObj.CAR_TYPE = eachRow["cartype"].ToString();

                    eachObj.READER_IP = eachRow["readerip"].ToString();
                    eachObj.UPDATETIME = eachRow["UPDATETIME"].ToString();
                    eachObj.MANUALMODE = Boolean.Parse(eachRow["MANUALMODE"].ToString());
                    GateStatusList.Add(eachObj);
                }
                return GateStatusList;
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

        public List<Structure.AntSetting> GetAntSetting(string Fab,string Area )
        {
            try
            {
                List<Structure.AntSetting> AntSettingList = new List<Structure.AntSetting>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select t.fab, t.area, t.gate, t.type, t.readerip, t.antnumber,t1.rfattenuation,to_char(t1.updatetime,'yyyy/mm/dd hh24:mi:ss')updatetime
                                      from rf_antmapping t, rf_rfattenuation t1
                                     where t.readerip = t1.readerip(+)
                                     and t.antnumber = t1.antnumber(+)
                                     and t.antnumber <=3
                                     and t.fab = '" + Fab+@"'
                                     and t.area = '"+Area+@"'
                                     order by t.readerip,t.antnumber";
                DataTable result = dbObj.SelectSQL(sqlString);

                foreach (DataRow eachRow in result.Rows)
                {
                    Structure.AntSetting eachObj = new Structure.AntSetting();

                    eachObj.Fab = eachRow["FAB"].ToString();
                    eachObj.Area = eachRow["AREA"].ToString();
                    eachObj.Gate = eachRow["GATE"].ToString();
                    eachObj.Type = eachRow["Type"].ToString();
                    eachObj.Value = eachRow["rfattenuation"].ToString();
                    eachObj.AntNumber = eachRow["antnumber"].ToString();
                    eachObj.ReaderIP = eachRow["readerip"].ToString();
                    eachObj.UpdateTime = eachRow["updatetime"].ToString();
                    AntSettingList.Add(eachObj);
                }
                return AntSettingList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string> GetAreaList(string Fab)
        {
            try
            {
                List<string> AreaList = new List<string>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select distinct t.area from rf_antmapping t where t.fab = '" + Fab + "'";
                DataTable result = dbObj.SelectSQL(sqlString);

                foreach (DataRow eachRow in result.Rows)
                {
                    

                    AreaList.Add(eachRow["AREA"].ToString());
                }
                return AreaList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateAntSetting(Structure.AntSetting each)
        {
            try
            {
                List<string> AreaList = new List<string>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select * from rf_rfattenuation t where t.readerip = '" + each.ReaderIP + "' and t.antnumber ='" + each.AntNumber+ "'";
                DataTable result = dbObj.SelectSQL(sqlString);

                if (result.Rows.Count > 0)
                {
                    sqlString = "update rf_rfattenuation t set t.rfattenuation='" + each.Value + "',t.updatetime=sysdate where t.readerip = '" + each.ReaderIP + "' and t.antnumber ='" + each.AntNumber + "'";
                    dbObj.ExcuteNoQuery(sqlString);
                }
                else
                {
                    sqlString = "insert into rf_rfattenuation t (t.readerip,t.antnumber,t.rfattenuation,t.updatetime) values('" + each.ReaderIP + "','" + each.AntNumber + "','" + each.Value + "',sysdate)";
                    dbObj.ExcuteNoQuery(sqlString);
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}