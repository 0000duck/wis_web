using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RFID_WebSite.Models
{
    public class Dt2List
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
<<<<<<< HEAD
                    if (pro.Name.ToUpper() == column.ColumnName.ToUpper())
=======
                    if (pro.Name == column.ColumnName)
>>>>>>> origin/新增手動邦定解除綁定頁面
                    {

                        if (dr[column.ColumnName] != DBNull.Value)
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);

                        }
                        break;
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}