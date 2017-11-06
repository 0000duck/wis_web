using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

namespace RFID_WebSite.Models
{
    public class ContainerModels
    {
        public string PortBinding(Structure.Gate carInfo){
            using(var client = newHttpClient())  
            {  
                client.BaseAddress = newUri("http://localhost:55587/");  
                client.DefaultRequestHeaders.Accept.Clear();  
                client.DefaultRequestHeaders.Accept.Add(newMediaTypeWithQualityHeaderValue("application/json"));  

                var department = newDepartment() { DepartmentName = "Test Department" };  
                HttpResponseMessage response = awaitclient.PostAsJsonAsync("api/Department", department);  

                if (response.IsSuccessStatusCode)  
                {  
                // Get the URI of the created resource.  
                UrireturnUrl = response.Headers.Location;  
                Console.WriteLine(returnUrl);  
                }  
            }  
        }

        public Structure.RF_TagMapping GetTagMappingByRealID(string RealID)
        {
            OracleDB dbObj = new OracleDB("RFID_DB");

            string sqlString = @"select * from rf_tagmapping t where t.real_id = '" + RealID + "'";

            DataTable result = dbObj.SelectSQL(sqlString);
            List < Structure.RF_TagMapping> TagMappingList = Dt2List.ConvertDataTable<Structure.RF_TagMapping>(result);
            if (TagMappingList.Count != 0)
            {
                return TagMappingList[0];
            }
            else
            {
                return new Structure.RF_TagMapping();
            }
        }

        public Structure.RF_TagMapping GetTagMappingByTagID(string TagID)
        {
            OracleDB dbObj = new OracleDB("RFID_DB");

            string sqlString = @"select * from rf_tagmapping t where t.tag_id = '" + TagID + "'";

            DataTable result = dbObj.SelectSQL(sqlString);
            List<Structure.RF_TagMapping> TagMappingList = Dt2List.ConvertDataTable<Structure.RF_TagMapping>(result);
            if (TagMappingList.Count != 0)
            {
                return TagMappingList[0];
            }
            else
            {
                return new Structure.RF_TagMapping();
            }
        }

        public void AddTagMapping(string RealID)
        {
            
            OracleDB dbObj = new OracleDB("RFID_DB");
            string sqlString = @"insert into rf_tagmapping t
                                  (t.tag_id, t.real_id)
                                values
                                  ('INXCAR' || LPAD(emp_sequence.NEXTVAL, 5, '0'), '" + RealID + "')";
            dbObj.ExcuteNoQuery(sqlString);
           
        }

        public void DeleteTagMapping(string TagID)
        {

            OracleDB dbObj = new OracleDB("RFID_DB");
            string sqlString = @"delete rf_tagmapping t where t.tag_id='"+TagID+"'";
            dbObj.ExcuteNoQuery(sqlString);

        }

        public Structure.areaCount GetAreaCount()
        {
            Structure.areaCount areaCountObject = new Structure.areaCount();
            OracleDB dbObj = new OracleDB("RFID_DB");

            //            string sqlString = @"select t.fab,
            //                                   t.area,
            //                                   sum(case
            //                                         when t.car_type = 'Container' then
            //                                          1
            //                                         else
            //                                          0
            //                                       end) container,
            //                                   sum(case
            //                                         when t.car_type <> 'Container' then
            //                                          1
            //                                         else
            //                                          0
            //                                       end) Guest
            //                              from rf_containerinfo t
            //                             group by t.fab, t.area";
            string sqlString = @"select t.fab,
                                   t.area,
                                   sum(case
                                         when t.car_type = 'Container' then
                                          1
                                         else
                                          0
                                       end) container,
                                   sum(case
                                         when t.car_type = 'Truck' then
                                          1
                                         else
                                          0
                                       end) Truck,
                                    sum(case
                                         when t.car_type = 'Guest' then
                                          1
                                         else
                                          0
                                       end) Guest
                              from rf_containerinfo t where t.endtime is null 
                             group by t.fab, t.area";//byEASY
            //Container:貨櫃，Truck:(大小)貨車，Guest:訪客 20160514 byEASY
            DataTable result = dbObj.SelectSQL(sqlString);

            if (result.Rows.Count > 0)
            {
                Structure.carType total = new Structure.carType();
                foreach (DataRow eachRow in result.Rows)
                {
                    string fab = eachRow["fab"].ToString();
                    string area = eachRow["area"].ToString();
                    switch (fab)
                    {
                        case "T1":
                            switch (area)
                            {
                                case "Receive":
                                    Structure.carType eachArea = new Structure.carType();
                                    eachArea.Container = int.Parse(eachRow["container"].ToString());
                                    eachArea.Guest = int.Parse(eachRow["Guest"].ToString());
                                    eachArea.Truck = int.Parse(eachRow["Truck"].ToString());//byEASY
                                    areaCountObject.T1_Receive = eachArea;
                                    total.Container += eachArea.Container;
                                    total.Guest += eachArea.Guest;
                                    total.Truck += eachArea.Truck;//byEASY
                                    break;
                                case "Delivery":
                                    Structure.carType eachArea1 = new Structure.carType();
                                    eachArea1.Container = int.Parse(eachRow["container"].ToString());
                                    eachArea1.Guest = int.Parse(eachRow["Guest"].ToString());
                                    eachArea1.Truck = int.Parse(eachRow["Truck"].ToString());//byEASY
                                    areaCountObject.T1_Delivery = eachArea1;
                                    total.Container += eachArea1.Container;
                                    total.Guest += eachArea1.Guest;
                                    total.Truck += eachArea1.Truck;//byEASY
                                    break;
                                case "SB":
                                    Structure.carType eachArea2 = new Structure.carType();
                                    eachArea2.Container = int.Parse(eachRow["container"].ToString());
                                    eachArea2.Guest = int.Parse(eachRow["Guest"].ToString());
                                    eachArea2.Truck = int.Parse(eachRow["Truck"].ToString());//byEASY
                                    areaCountObject.T1_SB = eachArea2;
                                    total.Container += eachArea2.Container;
                                    total.Guest += eachArea2.Guest;
                                    total.Truck += eachArea2.Truck;//byEASY
                                    break;
                                case "WH":
                                    Structure.carType eachArea3 = new Structure.carType();
                                    eachArea3.Container = int.Parse(eachRow["container"].ToString());
                                    eachArea3.Guest = int.Parse(eachRow["Guest"].ToString());
                                    eachArea3.Truck = int.Parse(eachRow["Truck"].ToString());//byEASY
                                    areaCountObject.T1_WH = eachArea3;
                                    total.Container += eachArea3.Container;
                                    total.Guest += eachArea3.Guest;
                                    total.Truck += eachArea3.Truck;//byEASY
                                    break;
                                case "Parking"://byEASY
                                    Structure.carType eachArea4 = new Structure.carType();
                                    eachArea4.Container = int.Parse(eachRow["container"].ToString());
                                    eachArea4.Guest = int.Parse(eachRow["Guest"].ToString());
                                    eachArea4.Truck = int.Parse(eachRow["Truck"].ToString());//byEASY
                                    areaCountObject.T1_Parking = eachArea4;
                                    total.Container += eachArea4.Container;
                                    total.Guest += eachArea4.Guest;
                                    total.Truck += eachArea4.Truck;//byEASY
                                    break;

                            }
                            break;
                        case "T2":
                            switch (area)
                            {
                                case "Receive":
                                    Structure.carType eachArea4 = new Structure.carType();
                                    eachArea4.Container = int.Parse(eachRow["container"].ToString());
                                    eachArea4.Guest = int.Parse(eachRow["Guest"].ToString());
                                    eachArea4.Truck = int.Parse(eachRow["Truck"].ToString());//byEASY
                                    areaCountObject.T2_Receive = eachArea4;
                                    total.Container += eachArea4.Container;
                                    total.Guest += eachArea4.Guest;
                                    total.Truck += eachArea4.Truck;//byEASY
                                    break;
                                case "Delivery":
                                    Structure.carType eachArea5 = new Structure.carType();
                                    eachArea5.Container = int.Parse(eachRow["container"].ToString());
                                    eachArea5.Guest = int.Parse(eachRow["Guest"].ToString());
                                    eachArea5.Truck = int.Parse(eachRow["Truck"].ToString());//byEASY
                                    areaCountObject.T2_Delivery = eachArea5;
                                    total.Container += eachArea5.Container;
                                    total.Guest += eachArea5.Guest;
                                    total.Truck += eachArea5.Truck;//byEASY
                                    break;
                                case "CP":
                                    Structure.carType eachArea6 = new Structure.carType();
                                    eachArea6.Container = int.Parse(eachRow["container"].ToString());
                                    eachArea6.Guest = int.Parse(eachRow["Guest"].ToString());
                                    eachArea6.Truck = int.Parse(eachRow["Truck"].ToString());//byEASY
                                    areaCountObject.T2_Center = eachArea6;
                                    total.Container += eachArea6.Container;
                                    total.Guest += eachArea6.Guest;
                                    total.Truck += eachArea6.Truck;//byEASY
                                    break;
                                case "SB":
                                    Structure.carType eachArea7 = new Structure.carType();
                                    eachArea7.Container = int.Parse(eachRow["container"].ToString());
                                    eachArea7.Guest = int.Parse(eachRow["Guest"].ToString());
                                    eachArea7.Truck = int.Parse(eachRow["Truck"].ToString());//byEASY
                                    areaCountObject.T2_SB = eachArea7;
                                    total.Container += eachArea7.Container;
                                    total.Guest += eachArea7.Guest;
                                    total.Truck += eachArea7.Truck;//byEASY
                                    break;
                                case "WH":
                                    Structure.carType eachArea8 = new Structure.carType();
                                    eachArea8.Container = int.Parse(eachRow["container"].ToString());
                                    eachArea8.Guest = int.Parse(eachRow["Guest"].ToString());
                                    eachArea8.Truck = int.Parse(eachRow["Truck"].ToString());//byEASY
                                    areaCountObject.T2_WH = eachArea8;
                                    total.Container += eachArea8.Container;
                                    total.Guest += eachArea8.Guest;
                                    total.Truck += eachArea8.Truck;//byEASY
                                    break;

                            }
                            break;
                    }

                }
                areaCountObject.Total = total;
            }

            return areaCountObject;
        }

        //byEASY
        public DataTable getCarDetail(String DivPoint)
        {
            try
            {

                String Fab = DivPoint.Substring(5, 2);//ex:DivPoint=pointT2WH
                String area = DivPoint.Substring(7, DivPoint.Length - 7);

                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select t.starttime, t.car_id, t.container_id, t.vendor_name, t.car_type
                                from rf_containerinfo t
                                where t.fab='{0}' and t.area='{1}' and t.endtime is null and t.car_type in ('Container','Guest','Truck')
                                order by t.starttime";
                sqlString = String.Format(sqlString, Fab, area);
                DataTable result = dbObj.SelectSQL(sqlString);
                return result;
            }
            catch (Exception e)
            { throw e; }
            ;
        }

        public Structure.containerInfo GetContainerInfo(string containerID, string carID)
        {

            try
            {
                Structure.containerInfo containerObject = new Structure.containerInfo();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select t.container_id,
                                           t.container_type,
                                           t.vendor_name,
                                           t.drivername,
                                           t.driverphone,
                                           t.reason
                                      from rf_containerinfo t
                                     where 1 = 1";
                if (containerID != null)
                {
                    sqlString += " and t.container_id = '" + containerID + "' ";
                }
                if (carID != null)
                {
                    sqlString += " and t.car_id = '" + carID + "' ";
                }
                sqlString += "";

                DataTable result = dbObj.SelectSQL(sqlString);

                if (result.Rows.Count > 0)
                {
                    foreach (DataRow eachRow in result.Rows)
                    {


                        containerObject.ID = eachRow["container_id"].ToString();
                        containerObject.type = eachRow["container_type"].ToString();
                        containerObject.vendor = eachRow["vendor_name"].ToString();
                        containerObject.driverName = eachRow["drivername"].ToString();
                        containerObject.driverPhone = eachRow["driverphone"].ToString();
                        containerObject.reason = eachRow["reason"].ToString();

                        break;
                    }
                }


                return containerObject;
            }
            catch (Exception e)
            {
                throw e;
            }



        }

        public void AddInfo(Structure.containerInfo data)
        {
            try
            {

                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"delete from wmsuser.rf_containerinfo t where t.container_id = '{0}'";
                sqlString = string.Format(sqlString, data.ID);
                dbObj.ExcuteNoQuery(sqlString);

                sqlString = @"insert into wmsuser.rf_containerinfo t
                                      (t.container_id,
                                       t.container_type,
                                       t.container_status,
                                       t.vendor_name,
                                       t.starttime,
                                       t.endtime,
                                       t.car_id,
                                       t.fab,
                                       t.area,
                                       t.gate,
                                       t.car_type,
                                       t.impno,
                                       t.drivername,
                                       t.driverphone,
                                       t.reason,
                                       t.vendorid,             
                                       t.vendorcount,
                                       t.source
                                        )
                                    VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')";
                sqlString = string.Format(sqlString, data.ID, data.type, data.status, data.vendor, data.start, data.end, data.carID, data.fab, data.area, data.gate, data.carType, data.impNo, data.driverName, data.driverPhone, data.reason, data.VENDORID, data.VENDORCOUNT,data.source);
                dbObj.ExcuteNoQuery(sqlString);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void updateInfo(Structure.containerInfo data)
        {
            try
            {
              
                OracleDB dbObj = new OracleDB("RFID_DB");
                //string sqlString = @"delete from wmsuser.rf_containerinfo t where t.container_id = '{0}'";
                
                string sqlString = @"delete from wmsuser.rf_containerinfo t where t.container_id = '{0}'";
                sqlString = string.Format(sqlString, data.ID);
                dbObj.ExcuteNoQuery(sqlString);
                    
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddHistory(Structure.containerHis data)
        {
            try
            {

                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"INSERT INTO wmsuser.rf_movehistory t
                                      (t.container_id,
                                       t.fab,
                                       t.area,
                                       t.gate,
                                       t.car_id,
                                       t.container_type,
                                       t.container_status,
                                       t.vendor_name,
                                       t.car_type,
                                       t.drivername,
                                       t.driverphone,
                                       t.reason,
                                       t.timestamp,
                                       t.source)
                                    VALUES
                                      ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}','{10}','{11}','{12}','{13}')";
                sqlString = string.Format(sqlString, data.container_id, data.fab, data.area, data.gate, data.car_id, data.container_type, data.container_status, data.vendor_name, data.car_type, data.driverName, data.driverPhone, data.reason, data.timestamp,data.source);
                dbObj.ExcuteNoQuery(sqlString);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GetShipTo(string id)
        {
            string shipTo = "";
            try
            {
                
                OracleDB dbObj = new OracleDB("RFID_DB");
                //string sqlString = @"delete from wmsuser.rf_containerinfo t where t.container_id = '{0}'";

                string sqlString = @"select ship_to from jn_t2_wms.v_t2_wms_container_status@DW2T2WMS
                                        where container_no = '{0}' or truck_no = '{0}'
                                        union
                                        select ship_to from jn_t1_wms.v_t1_wms_container_status@DW2T1WMS
                                        where container_no = '{0}' or truck_no = '{0}'";
                sqlString = string.Format(sqlString, id);
                DataTable result = dbObj.SelectSQL(sqlString);
                if (result.Rows.Count > 0)
                {
                    foreach (DataRow eachRow in result.Rows)
                    {
                        shipTo = eachRow["ship_to"].ToString();
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return shipTo;
        }


        public Structure.containerInfo GetContainer(string id)
        {
            try
            {
                Structure.containerInfo containerObject = new Structure.containerInfo();
                OracleDB dbObj = new OracleDB("LIS_DB");
                string sqlString = @"select t.IMPNR, t.ESTYP, t.CONNR, t.CRTYP from ods_lis.zie_vw_connr_rtn_wms t where t.CONNR = '{0}' and t.SRDAT is not null";
                sqlString = string.Format(sqlString, id);
                DataTable result = dbObj.SelectSQL(sqlString);

                if (result.Rows.Count > 0)
                {
                    foreach (DataRow eachRow in result.Rows)
                    {


                        containerObject.impNo = eachRow["IMPNR"].ToString();
                        containerObject.fab = eachRow["ESTYP"].ToString();
                        containerObject.ID = eachRow["CONNR"].ToString();
                        containerObject.type = eachRow["CRTYP"].ToString();
                        containerObject.status = "NonEmpty";

                        break;
                    }
                }
                else
                {
                    containerObject.impNo = "";
                    containerObject.fab = "";
                    containerObject.ID = id;
                    containerObject.type = "";
                    containerObject.status = "Empty";
                }

                return containerObject;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string> GetContainerList()
        {
            try
            {
                List<string> ContainerList = new List<string>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select distinct t.container_id from wmsuser.rf_movehistory t";

                DataTable result = dbObj.SelectSQL(sqlString);


                foreach (DataRow eachRow in result.Rows)
                {


                    ContainerList.Add(eachRow["container_id"].ToString());



                }


                return ContainerList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string> GetCarIDList()
        {
            try
            {
                List<string> ContainerList = new List<string>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select distinct t.car_id from rf_movehistory t";

                DataTable result = dbObj.SelectSQL(sqlString);

                foreach (DataRow eachRow in result.Rows)
                {
                    ContainerList.Add(eachRow["car_id"].ToString());
                }

                return ContainerList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Structure.driverInfo GetDriverInfoByCarID(string carID)
        {
            try
            {
                Structure.driverInfo driver = new Structure.driverInfo();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select *
                                      from (select t.vendor_name, t.drivername, t.driverphone
                                              from rf_movehistory t
                                             where t.car_id = '{0}'
                                             order by t.timestamp desc)
                                     where rownum = 1";
                sqlString = string.Format(sqlString, carID);

                DataTable result = dbObj.SelectSQL(sqlString);

                foreach (DataRow eachRow in result.Rows)
                {
                    driver.driverName = eachRow["drivername"].ToString();
                    driver.driverPhone = eachRow["driverphone"].ToString();
                    driver.vendorName = eachRow["vendor_name"].ToString();

                    break;
                }

                return driver;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string> GetDriverNameList()
        {
            try
            {
                List<string> ContainerList = new List<string>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select distinct t.drivername from rf_movehistory t";

                DataTable result = dbObj.SelectSQL(sqlString);

                foreach (DataRow eachRow in result.Rows)
                {
                    ContainerList.Add(eachRow["drivername"].ToString());
                }

                return ContainerList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Structure.driverInfo GetDriverInfo(string name)
        {
            try
            {
                Structure.driverInfo driver = new Structure.driverInfo();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select *
                                      from (select t.vendor_name, t.car_id, t.driverphone
                                              from rf_movehistory t
                                             where t.drivername = '{0}'
                                             order by t.timestamp desc)
                                     where rownum = 1";
                sqlString = string.Format(sqlString, name);

                DataTable result = dbObj.SelectSQL(sqlString);

                foreach (DataRow eachRow in result.Rows)
                {
                    driver.carID = eachRow["car_id"].ToString();
                    driver.driverPhone = eachRow["driverphone"].ToString();
                    driver.vendorName = eachRow["vendor_name"].ToString();

                    break;
                }

                return driver;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Structure.containerHis> GetContainerHistory(string containerID, string carID, string fab, string start, string end)
        {
            try
            {
                List<Structure.containerHis> ContainerHistory = new List<Structure.containerHis>();
                OracleDB dbObj = new OracleDB("RFID_DB");
                string sqlString = @"select t.container_id,
                                               t.fab,
                                               t.area,
                                               t.gate,
                                               t.car_id,
                                               t.container_type,
                                               t.container_status,
                                               t.vendor_name,
                                               t.car_type,
                                               t.driverName,
                                               t.driverPhone,
                                               t.reason,
                                               t.timestamp,
                                               t.source
                                          from wmsuser.rf_movehistory t
                                         where 1 = 1";
                if (containerID != null)
                {
                    if (!containerID.Equals(""))
                    {
                        sqlString += " and t.container_id = '" + containerID + "' ";
                    }
                }
                if (fab != null)
                {
                    if (!fab.Equals(""))
                    {
                        sqlString += " and t.fab = '" + fab + "'";
                    }
                }
                if (carID != null)
                {
                    if (!carID.Equals(""))
                    {
                        sqlString += " and t.car_id = '" + carID + "'";
                    }
                }
                if (start != null && end != null)
                {
                    sqlString += " and t.timestamp between '" + start + "' and '" + end + "'";
                }
                else
                {
                    sqlString += " and t.timestamp between '" + DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm") + "' and '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'";
                }
                sqlString += " order by t.timestamp desc,t.container_id";

                DataTable result = dbObj.SelectSQL(sqlString);


                foreach (DataRow eachRow in result.Rows)
                {
                    Structure.containerHis each = new Structure.containerHis();

                    each.container_id = eachRow["container_id"].ToString();
                    each.fab = eachRow["fab"].ToString();
                    each.area = eachRow["area"].ToString();
                    each.gate = eachRow["gate"].ToString();
                    each.car_id = eachRow["car_id"].ToString();
                    each.container_type = eachRow["container_type"].ToString();
                    each.container_status = eachRow["container_status"].ToString();
                    each.vendor_name = eachRow["vendor_name"].ToString();
                    each.car_type = eachRow["car_type"].ToString();
                    each.driverName = eachRow["driverName"].ToString();
                    each.driverPhone = eachRow["driverPhone"].ToString();
                    each.reason = eachRow["reason"].ToString();
                    each.timestamp = eachRow["timestamp"].ToString();
                    each.source = eachRow["source"].ToString();
                    ContainerHistory.Add(each);

                }


                return ContainerHistory;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}