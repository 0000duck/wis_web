using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RFID_WebSite.Models
{
    public class CylinderModels
    {
        public class Cylinder
        {
            public string FAB = "";
            public string AREA = "";
            public string TAGID = "";
            public string POSITION = "";
            public string UPDATETIME = "";
            public string CHECKTIMES = "";
            public string GASTYPE = "";
            public string NEWPOSITION = "";
            public string STATUS = "";
        }

        public List<Cylinder> GetCylinders()
        {
            List<Cylinder> result = new List<Cylinder>();

            OracleDB dbObj = new OracleDB("RFID_DB");

            string sqlString = @"select * from rf_gas_status t";
         

            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {
                Cylinder each = new Cylinder();

                each.AREA = eachRow["AREA"].ToString();
                each.CHECKTIMES = eachRow["CHECKTIMES"].ToString();
                each.FAB = eachRow["FAB"].ToString();
                each.GASTYPE = eachRow["GASTYPE"].ToString();
                each.NEWPOSITION = eachRow["NEWPOSITION"].ToString();
                each.POSITION = eachRow["POSITION"].ToString();
                each.STATUS = eachRow["STATUS"].ToString();
                each.TAGID = eachRow["TAGID"].ToString();
                each.UPDATETIME = eachRow["UPDATETIME"].ToString();

                result.Add(each);
            }

            return result;
        }
    }
}