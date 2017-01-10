using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace RFID_WebSite
{
    public class OracleDB
    {
        string db = "";
        public OracleDB(string db)
        {
            this.db = db;
        }

        public void ExcuteNoQuery(string sqlString)
        {
            
            try
            {
                OracleConnection conn = new OracleConnection();
                DataTable result = new DataTable();


                conn.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[this.db].ConnectionString;
                conn.Open();



                OracleCommand cmd = new OracleCommand(sqlString, conn);
                cmd.CommandType = CommandType.Text;

                int returnCode = cmd.ExecuteNonQuery(); // C#
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            


        }

        public DataTable SelectSQL(string sqlString)
        {
            OracleConnection conn = new OracleConnection();
            DataTable result = new DataTable();
            try
            {



                conn.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[this.db].ConnectionString;
                conn.Open();



                OracleCommand cmd = new OracleCommand(sqlString, conn);
                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader(); // C#
                result.Load(dr);






                dr.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();

            }
            return result;
        }

    }
}