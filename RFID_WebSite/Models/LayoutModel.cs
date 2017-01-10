using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace RFID_WebSite.Models
{
    public class LayoutModel
    {
        public List<Structure.layoutObj> GetLayout(string Name)
        {
            try
            {
                List<Structure.layoutObj> layoutObject = new List<Structure.layoutObj>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select objectID, objectHeight, objectWidth, objectTop, objectLeft, objectCaption, objectZIndex, objectType
                                     from rf_layout where layoutName='{0}'";
                sqlString = string.Format(sqlString, Name);
                DataTable result = dbObj.SelectSQL(sqlString);

                foreach (DataRow eachRow in result.Rows)
                {
                    Structure.layoutObj eachObj = new Structure.layoutObj();

                    eachObj.id = eachRow["objectID"].ToString();
                    eachObj.height = eachRow["objectHeight"].ToString();
                    eachObj.width = eachRow["objectWidth"].ToString();
                    eachObj.top = eachRow["objectTop"].ToString();
                    eachObj.left = eachRow["objectLeft"].ToString();
                    eachObj.caption = eachRow["objectCaption"].ToString();
                    eachObj.zIndex = eachRow["objectZIndex"].ToString();
                    eachObj.type = eachRow["objectType"].ToString();
                    layoutObject.Add(eachObj);
                }

                return layoutObject;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void StoreLayout(string Name, List<Structure.layoutObj> LayoutObject) 
        {
            try
            {
                OracleDB dbObj = new OracleDB("RFID_DB");
                string deleteSql = @"delete from rf_layout where layoutName = '{0}'";
                deleteSql = string.Format(deleteSql, Name);
                dbObj.ExcuteNoQuery(deleteSql);
                foreach (Structure.layoutObj eachObj in LayoutObject)
                {
                    string insertSql = @"insert into rf_layout (layoutName, objectID, objectHeight, objectWidth, objectTop, objectLeft, objectCaption, objectZIndex, objectType)
                                         values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
                    insertSql = string.Format(insertSql, Name, eachObj.id, eachObj.height, eachObj.width, eachObj.top, eachObj.left, eachObj.caption, eachObj.zIndex, eachObj.type);
                    dbObj.ExcuteNoQuery(insertSql);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }
    }
}