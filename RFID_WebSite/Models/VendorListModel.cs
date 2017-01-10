using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

namespace RFID_WebSite.Models
{
    public class VendorListModel
    {
        public DataTable getVendorData(String GuestId){
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select t.* from rf_vendormanagement t where t.vendorid='{0}'";
                sqlString = String.Format(sqlString,GuestId.ToUpper());
                DataTable result = dbObj.SelectSQL(sqlString);
                return result;
        
        
        }
        public DataTable getVendorlistModel()
        {
            OracleDB dbObj = new OracleDB("RFID_DB");
            string sqlString = @"select * from rf_vendormanagement t";
            //sqlString = String.Format(sqlString, "");
            DataTable result = dbObj.SelectSQL(sqlString);
            return result;


        }
        public DataTable getVendorOnlyModel(String key)
        {
            OracleDB dbObj = new OracleDB("RFID_DB");
            string sqlString = @"select * from rf_vendormanagement t where t.vendorid='{0}' or t.vendorname='{0}' or t.drivername='{0}' or t.driverphone='{0}' ";
            sqlString = String.Format(sqlString, key, key, key, key);
            DataTable result = dbObj.SelectSQL(sqlString);
            return result;

        }

        public String DeleteVendor(Structure.vendorManagement data) 
        {
            try 
            {
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"
                                    delete from rf_vendormanagement t
                                    where t.vendorid='{0}' ";
                sqlString = String.Format(sqlString, data.VENDORID);
                 dbObj.ExcuteNoQuery(sqlString);
                return "OK";
            
            }catch(Exception e)
            {
                return "NG";
            }
        
        }

        public void ModifyVendor(Structure.vendorManagement data)
        {
            try
            {
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"delete from rf_vendormanagement t
                                    where t.vendorid='{0}' ";
                sqlString = string.Format(sqlString, data.VENDORID);
                dbObj.ExcuteNoQuery(sqlString);

                sqlString = @"insert into rf_vendormanagement t (t.vendorid,t.vendorname,t.drivername,t.driverphone,t.updatetime,t.carid) values('{0}','{1}','{2}','{3}','{4}','{5}')";
                sqlString = string.Format(sqlString, data.VENDORID,data.VENDORNAME,data.DRIVERNAME,data.DRIVERPHONE,data.UPDATETIME,data.CARID);
                dbObj.ExcuteNoQuery(sqlString);

                

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        } 
    }
}