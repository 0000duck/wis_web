using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFID_WebSite.Models
{
    public class Structure
    {
        public class ResponseObj
        {
            public string status { get; set; }
            public object detail { get; set; }
        }

        public class layoutObj
        {
            public string id { get; set; }
            public string height { get; set; }
            public string width { get; set; }
            public string top { get; set; }
            public string left { get; set; }
            public string caption { get; set; }
            public string type { get; set; }
            public string zIndex { get; set; }
        }

        public class containerInfo
        {
            public string impNo;
            public string fab;
            public string ID;
            public string type;
            public string status;
            public string vendor;
            public string start;
            public string end;
            public string carID;
            public string area;
            public string gate;
            public string carType;
            public string driverName;
            public string driverPhone;
            public string reason;
            public string VENDORID;  //訪客身分證
            public string VENDORCOUNT; //訪客人數
        }

        public class TagHis
        {
            public string ip;
            public string timeStamp;
            public string tagID;
            public string count;
            public string type;
            public string rssi;
            public string rawData;
            public string antType;
            public string discoverTime;
            public string reNewTime;
            public string ct_discoverTime;
            public string ct_reNewTime;
        }
        
        public class containerHis
        {
            public string container_id;
            public string fab;
            public string area;
            public string gate;
            public string car_id;
            public string container_type;
            public string container_status;
            public string vendor_name;
            public string car_type;
            public string timestamp;
            public string driverName;
            public string driverPhone;
            public string reason;
        }

        public class driverInfo
        {
            public string carID;
            public string vendorName;
            public string driverPhone;
            public string driverName;
        }

        public class carType
        {
            public int Container;
            public int Guest;       //訪客
            public int Truck;    //byEASY
        }

        //byEASY
        public class CarDetail
        {
            public String starttime;
            public String car_id;
            public String container_id;
            public String vendor_name;
            public String car_type;
        }
        
        public class carDetailset
        {
            public CarDetail carSet;
        
        }

        public class vendorManagement
        {
            public String VENDORID;
            public String VENDORNAME;
            public String DRIVERNAME;
            public String DRIVERPHONE;
            public String CARID;
            public String UPDATETIME;
        }

        public class areaCount
        {

            public carType T1_WH;
            public carType T1_SB;
            public carType T1_Receive;
            public carType T1_Delivery;
            public carType T1_Parking;//byEASY add T1Parking
            public carType T2_WH;
            public carType T2_SB;
            public carType T2_Receive;
            public carType T2_Delivery;
            public carType T2_Center;
            public carType Total;
            public areaCount()
            {
                T1_WH = new carType();
                T1_WH.Container = 0;
                T1_WH.Guest=0;
                T1_WH.Truck = 0;
                T1_SB = new carType();
                T1_SB.Container = 0;
                T1_SB.Guest = 0;
                T1_SB.Truck = 0;
                T1_Receive = new carType();
                T1_Receive.Container = 0;
                T1_Receive.Guest = 0;
                T1_Receive.Truck = 0;
                T1_Delivery = new carType();
                T1_Delivery.Container = 0;
                T1_Delivery.Guest = 0;
                T1_Delivery.Truck = 0;
                T1_Parking = new carType();
                T1_Parking.Container = 0;
                T1_Parking.Guest = 0;
                T1_Parking.Truck = 0;
                T2_WH = new carType();
                T2_WH.Container = 0;
                T2_WH.Guest = 0;
                T2_WH.Truck = 0;
                T2_SB = new carType();
                T2_SB.Container = 0;
                T2_SB.Guest = 0;
                T2_SB.Truck = 0;
                T2_Receive = new carType();
                T2_Receive.Container = 0;
                T2_Receive.Guest = 0;
                T2_Receive.Truck = 0;
                T2_Delivery = new carType();
                T2_Delivery.Container = 0;
                T2_Delivery.Guest = 0;
                T2_Delivery.Truck = 0;
                T2_Center = new carType();
                T2_Center.Container = 0;
                T2_Center.Guest = 0;
                T2_Center.Truck = 0;
                Total = new carType();
                Total.Container = 0;
                Total.Guest = 0;
                Total.Truck = 0;              
            }
            
        }

        public class CaptionInfo
        {
            public string Fab { get; set; }            
            public string Area { get; set; }
            public string Gate { get; set; }
            public string ReaderIP { get; set; }
            public string CustCaptionStr { get; set; }
            public string Active { get; set; }
            public bool Modify { get; set; }
        }

        //byEASY
        public class RF_ANTCURRENT
        {
            public string FAB { get; set; }              //T1/T2            
            public string AREA { get; set; }             //出貨/收貨碼頭
            public string GATE { get; set; }             //碼頭門號       
            public string CONTAINER_ID { get; set; }     //貨櫃號碼     
            public string CAR_TYPE { get; set; }
            public string READER_IP { get; set; }
            public string SIGNAL_LIGHT { get; set; }     //三色燈狀態   
            public string MARQUEE { get; set; }          //字幕機狀態   
            public string UPDATETIME { get; set; }
        }
        //byEASY
        public class RF_CONTAINERINFO
        {
            public string FAB { get; set; }               //T1/T2
            public string AREA { get; set; }              //Delivery,Receive
            public string GATE { get; set; }              //碼頭門號 
            public string CAR_TYPE { get; set; }          //BigTruck,Truck,Container
            public string CONTAINER_ID { get; set; }       
            public string CONTAINER_TYPE { get; set; }    //nonEmpty,Emppty
            public string CONTAINER_STATUS { get; set; }    
            public string VENDOR_NAME { get; set; }
            public string STARTTIME { get; set; }
            public string ENDTIME { get; set; }
            public string CAR_ID { get; set; }
  

        }
    }
}