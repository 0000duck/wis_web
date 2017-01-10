using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace RFID_WebSite.Models
{
    public class TagModels
    {

        public List<Structure.TagHis> GeTagHistory(string area, string gateID, string fab, string antType, string hideTagType, string start, string end)
        {
            try
            {
                List<Structure.TagHis> TagHistory = new List<Structure.TagHis>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"
                                    select t.*,to_char(unix_ts_to_date( t.discovertime ),'yyyy-mm-dd hh24:mi:ss') ct_discovertime ,to_char(unix_ts_to_date( t.renewtime ),'yyyy-mm-dd hh24:mi:ss') ct_renewtime
                                      from rf_taghistory t
                                     where 1=1";
                if (area != null)
                {
                    if (!area.Equals(""))
                    {
                        sqlString += " and t.area = '" + area + "' ";
                    }
                }
                if (fab != null)
                {
                    if (!fab.Equals(""))
                    {
                        sqlString += " and t.fab = '" + fab + "'";
                    }
                }
                if (gateID != null)
                {
                    if (!gateID.Equals(""))
                    {
                        sqlString += " and t.gate = '" + gateID + "'";
                    }
                }
                if (antType != null)
                {
                    if (!antType.Equals(""))
                    {
                        sqlString += " and t.anttype = '" + antType + "'";
                    }
                }
                if (hideTagType != null)
                {
                    if (!hideTagType.Equals(""))
                    {
                        sqlString += " and t.type <> '" + hideTagType + "'";
                    }
                }
                //if (start != null && end != null)
                //{
                sqlString += " and t.timestamp between '" + start + "' and '" + end + "'";
                sqlString += " order by t.timestamp desc";
                //}
                //else
                //{
                //    sqlString += " and t.timestamp between '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm") + "' and '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'";
                //}
                //sqlString += " and t.timestamp > '" + DateTime.Now.AddDays(-0.25).ToString("yyyy-MM-dd HH:mm") + "'";
                //sqlString += " and t.type not in('FD','PD')";
                //sqlString += " order by t.discovertime desc ) where rownum < 500";

                DataTable result = dbObj.SelectSQL(sqlString);


                foreach (DataRow eachRow in result.Rows)
                {
                    Structure.TagHis each = new Structure.TagHis();

                    each.ip = eachRow["ip"].ToString();
                    each.timeStamp = eachRow["timeStamp"].ToString();
                    each.tagID = eachRow["tagID"].ToString();
                    each.count = eachRow["count"].ToString();
                    each.type = eachRow["type"].ToString();
                    each.rssi = eachRow["rssi"].ToString();
                    each.rawData = eachRow["rawData"].ToString();
                    each.antType = eachRow["antType"].ToString();
                    each.discoverTime = eachRow["discoverTime"].ToString();
                    each.reNewTime = eachRow["reNewTime"].ToString();
                    each.ct_discoverTime = eachRow["ct_discoverTime"].ToString();
                    each.ct_reNewTime = eachRow["ct_reNewTime"].ToString();

                    TagHistory.Add(each);

                }


                return TagHistory;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Structure.TagHis> SearchTagHistory(string area, string gateID, string fab, string antType, string tagID, string start, string end)
        {
            try
            {
                List<Structure.TagHis> TagHistory = new List<Structure.TagHis>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"
                                    select t.*,to_char(unix_ts_to_date( t.discovertime ),'yyyy-mm-dd hh24:mi:ss') ct_discovertime ,to_char(unix_ts_to_date( t.renewtime ),'yyyy-mm-dd hh24:mi:ss') ct_renewtime
                                      from rf_taghistory t
                                     where 1=1";
                if (area != null)
                {
                    if (!area.Equals(""))
                    {
                        sqlString += " and t.area = '" + area + "' ";
                    }
                }
                if (fab != null)
                {
                    if (!fab.Equals(""))
                    {
                        sqlString += " and t.fab = '" + fab + "'";
                    }
                }
                if (gateID != null)
                {
                    if (!gateID.Equals(""))
                    {
                        sqlString += " and t.gate = '" + gateID + "'";
                    }
                }
                if (antType != null)
                {
                    if (!antType.Equals(""))
                    {
                        sqlString += " and t.anttype = '" + antType + "'";
                    }
                }

                sqlString += " and t.tagid = '" + tagID + "'";
                    
            
                //if (start != null && end != null)
                //{
                sqlString += " and t.timestamp between '" + start + "' and '" + end + "'";
                sqlString += " order by t.timestamp desc";
                //}
                //else
                //{
                //    sqlString += " and t.timestamp between '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm") + "' and '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'";
                //}
               // sqlString += " and t.timestamp > '" + DateTime.Now.AddDays(-0.25).ToString("yyyy-MM-dd HH:mm") + "'";
                //sqlString += " and t.type not in('FD','PD')";
                //sqlString += " order by t.discovertime desc ) where rownum < 500";

                DataTable result = dbObj.SelectSQL(sqlString);


                foreach (DataRow eachRow in result.Rows)
                {
                    Structure.TagHis each = new Structure.TagHis();

                    each.ip = eachRow["ip"].ToString();
                    each.timeStamp = eachRow["timeStamp"].ToString();
                    each.tagID = eachRow["tagID"].ToString();
                    each.count = eachRow["count"].ToString();
                    each.type = eachRow["type"].ToString();
                    each.rssi = eachRow["rssi"].ToString();
                    each.rawData = eachRow["rawData"].ToString();
                    each.antType = eachRow["antType"].ToString();
                    each.discoverTime = eachRow["discoverTime"].ToString();
                    each.reNewTime = eachRow["reNewTime"].ToString();
                    each.ct_discoverTime = eachRow["ct_discoverTime"].ToString();
                    each.ct_reNewTime = eachRow["ct_reNewTime"].ToString();

                    TagHistory.Add(each);

                }


                return TagHistory;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}