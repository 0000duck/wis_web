using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RFID_WebSite.Models
{
    public class SPCYModels
    {
        public class Config
        {
            public string Prod_ID = "";
            public string Step_ID = "";
            public string Parameter = "";
            public string Upper_Spec = "";
            public string Lower_Spec = "";
            public string Hold_EQ = "";
            public string Hold_Recipe = "";
        }

        public class ErrorLog
        {
            public string EqpID = "";
            public string ChartID = "";
            public string SampleID = "";
            public string SampleDate = "";
            public string Parameter = "";
            public string MeanValue = "";
            public string MaxValue = "";
            public string MinValue = "";
            public string Process_Step = "";
            public string Process_EQ = "";
            public string Recipe_ID = "";
            public string Lot_Type = "";
            public string Prod_ID = "";
            public string GLASS = "";
            public string Error_Msg = "";
            public string TimeStamp = "";
        }

        public List<string> GetErrorLogProd()
        {
            List<string> result = new List<string>();

            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION =    (ADDRESS_LIST =      (ADDRESS = (PROTOCOL = TCP)(HOST = 172.20.8.48)(PORT = 1521))    )    (CONNECT_DATA =      (SERVER = DEDICATED)      (SERVICE_NAME = T2PSPC)      (INSTANCE_NAME = T2PSPC)    )  );User Id=mes_user;Password=mes_user123;");

            string sqlString = @"select distinct t.Prod_ID from spcy_errorlog t";



            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {


                result.Add(eachRow["Prod_ID"].ToString());

            }


            return result;
        }

        public List<string> GetConfigProd()
        {
            List<string> result = new List<string>();

            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION =    (ADDRESS_LIST =      (ADDRESS = (PROTOCOL = TCP)(HOST = 172.20.8.48)(PORT = 1521))    )    (CONNECT_DATA =      (SERVER = DEDICATED)      (SERVICE_NAME = T2PSPC)      (INSTANCE_NAME = T2PSPC)    )  );User Id=mes_user;Password=mes_user123;");

            string sqlString = @"select distinct t.Prod_ID from spcy_config t";



            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {


                result.Add(eachRow["Prod_ID"].ToString());

            }


            return result;
        }

        public List<string> GetConfigStep()
        {
            List<string> result = new List<string>();

            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION =    (ADDRESS_LIST =      (ADDRESS = (PROTOCOL = TCP)(HOST = 172.20.8.48)(PORT = 1521))    )    (CONNECT_DATA =      (SERVER = DEDICATED)      (SERVICE_NAME = T2PSPC)      (INSTANCE_NAME = T2PSPC)    )  );User Id=mes_user;Password=mes_user123;");

            string sqlString = @"select distinct t.step_id from spcy_config t";

            

            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {


                result.Add(eachRow["step_id"].ToString());

            }


            return result;
        }

        public List<ErrorLog> GetErrorLog(string BeginTime,string EndTime,  string Prod_ID)
        {
            List<ErrorLog> result = new List<ErrorLog>();

            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION =    (ADDRESS_LIST =      (ADDRESS = (PROTOCOL = TCP)(HOST = 172.20.8.48)(PORT = 1521))    )    (CONNECT_DATA =      (SERVER = DEDICATED)      (SERVICE_NAME = T2PSPC)      (INSTANCE_NAME = T2PSPC)    )  );User Id=mes_user;Password=mes_user123;");

            string sqlString = @"select t.eqpid,t.chartid,t.sampleid,t.sampledate,t.parameter,t.meanvalue,t.maxvalue,t.minvalue,t.process_step,t.process_eq,t.recipe_id,t.lot_type,t.prod_id,t.glass,t.error_msg,to_char(t.timestamp,'yyyy-mm-dd hh24:mi:ss') TimeStamp from spcy_errorlog t where t.timestamp between to_date('" + BeginTime + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + EndTime + "','yyyy-mm-dd hh24:mi:ss') ";
            
            if (!Prod_ID.Equals(""))
            {
                sqlString += " and t.Prod_ID = '" + Prod_ID + "'";
            }
            
            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {
                ErrorLog eachLog = new ErrorLog();
                eachLog.EqpID = eachRow["EqpID"].ToString();
                eachLog.ChartID = eachRow["ChartID"].ToString();
                eachLog.SampleID = eachRow["SampleID"].ToString();
                eachLog.SampleDate = eachRow["SampleDate"].ToString();
                eachLog.Parameter = eachRow["Parameter"].ToString();
                eachLog.MeanValue = eachRow["MeanValue"].ToString();
                eachLog.MaxValue = eachRow["MaxValue"].ToString();
                eachLog.MinValue = eachRow["MinValue"].ToString();
                eachLog.Process_Step = eachRow["Process_Step"].ToString();
                eachLog.Process_EQ = eachRow["Process_EQ"].ToString();
                eachLog.Recipe_ID = eachRow["Recipe_ID"].ToString();
                eachLog.Lot_Type = eachRow["Lot_Type"].ToString();
                eachLog.Prod_ID = eachRow["Prod_ID"].ToString();
                eachLog.GLASS = eachRow["GLASS"].ToString();
                eachLog.Error_Msg = eachRow["Error_Msg"].ToString();
                eachLog.TimeStamp = eachRow["TimeStamp"].ToString();


                result.Add(eachLog);

            }


            return result;
        }

        public List<Config> GetConfig(string Step, string Prod_ID, string Parameter)
        {
            List<Config> result = new List<Config>();

            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION =    (ADDRESS_LIST =      (ADDRESS = (PROTOCOL = TCP)(HOST = 172.20.8.48)(PORT = 1521))    )    (CONNECT_DATA =      (SERVER = DEDICATED)      (SERVICE_NAME = T2PSPC)      (INSTANCE_NAME = T2PSPC)    )  );User Id=mes_user;Password=mes_user123;");

            string sqlString = @"select * from SPCY_Config t where 1=1 ";
            if (!Step.Equals(""))
            {
                sqlString += " and t.Step_ID='" + Step + "'";
            }
            if (!Prod_ID.Equals(""))
            {
                sqlString += " and t.Prod_ID = '" + Prod_ID + "'";
            }
            if (!Parameter.Equals(""))
            {
                sqlString += " and t.Parameter='" + Parameter + "'";
            }
            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {
                Config eachCfg = new Config();
                eachCfg.Step_ID = eachRow["Step_ID"].ToString();
                eachCfg.Prod_ID = eachRow["Prod_ID"].ToString();
                eachCfg.Parameter = eachRow["Parameter"].ToString();
                eachCfg.Lower_Spec = eachRow["Lower_Spec"].ToString();
                eachCfg.Upper_Spec = eachRow["Upper_Spec"].ToString();
                eachCfg.Hold_EQ = eachRow["Hold_EQ"].ToString();
                eachCfg.Hold_Recipe = eachRow["Hold_Recipe"].ToString();
                result.Add(eachCfg);

            }


            return result;
        }
    }
}