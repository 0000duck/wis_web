using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace RFID_WebSite.Models
{
    public class BCModels
    {
        public class BCInfo
        {
            public string Fab = "";
            public string BCName = "";
            public string BCIP = "";
        }

        public class RecipeBody
        {
            public string BCName = "";
            public string SubeqID = "";
            public string EqName = "";
            public string RecipeSplit = "";
            public string ItemName = "";
            public string ItemFormat = "";
            public string ItemOffset = "";
            public string ItemLength = "";
            public string ItemRate = "";
            public string ItemEffLen = "";
            public string ItemSigned = "";
            public string ItemUnit = "";
        }

        public class RecipeBodyParseData
        {
            public string Name = "";
            public string Value = "";
            public string Unit = "";
        }

        public class BCNode
        {
            public int NodeNo = 0;
            public string EQName = "";
        }
        
        public class PPID
        {
            public string Name = "";
            public List<Recipe> RecipeTable = new List<Recipe>();
        }

        public class Recipe
        {
            public int NodeNo = 0;
            public int RecipeNo = 0;
        }

        public class EQP
        {
            public string EQPName = "";
            public string Description = "";
            public int NodeNo = 0;
            public string RecipeSplit = "";
            
        }

        public class RecipePaserSetting
        {
            public string ItemName = "";
            public int ItemOffset = 0;
            public int ItemLength = 0;
            public double ItemRate =0;
            public int ItemEfflen = 0;
            public int ItemSigned =0;
            public string ItemUnit = "";
            public string ItemFormat = "";

        }

        public class ED03
        {
            public string ReportType = "";
            public string DataItem = "";
            public string Index = "";
        }

        public void CreateBCCmd(BCInfo BC,Recipe req){
            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST =" + BC.BCIP + ")(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME =ORCL)));User Id=innolux;Password=innoluxabc123;");
            string BCNo = "";
            string FabType = "";
            string BCLineNo = "";
            string CMD = req.NodeNo.ToString("00") + req.RecipeNo.ToString("0000")+"0";
            string UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string sqlString = "select * from main_bc_line t where t.hostlineid = '" + BC.BCName+ "'";
            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {
                BCNo = eachRow["BCNo"].ToString();
                FabType = eachRow["FabType"].ToString();
                BCLineNo = eachRow["BCLineNo"].ToString();

            }

            if (BCNo.Equals("") || FabType.Equals("") || BCLineNo.Equals(""))
            {
                throw new Exception("Get main_bc_line fail");
            }

            sqlString = @"Insert Into TrxBCGetCmd
                                  (BCNo,
                                   BCLineNo,
                                   FabType,
                                   CmdKey,
                                   NodeNo,
                                   EQPortNo,
                                   CmdData,
                                   CmdType,
                                   OPName,
                                   UpdateTime,
                                   UpdateOP,
                                   SP1,
                                   SP2)
                                Values
                                  ({0},
                                   {1},
                                   {2},
                                   'RECIPE_BODY_QUERY',
                                   {3},
                                   0,
                                   '{4}',
                                   'INSERT',
                                   'WebInterface',
                                   '{5}',
                                   'CIM',
                                   '',
                                   '')";
            sqlString = string.Format(sqlString, BCNo, BCLineNo, FabType, req.NodeNo, CMD, UpdateTime);

            dbObj.ExcuteNoQuery(sqlString);
        }

        public ED03 GetED03(BCInfo BC, EQP eqp)
        {
            ED03 result = new ED03();

            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST =" + BC.BCIP + ")(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME =ORCL)));User Id=innolux;Password=innoluxabc123;");

            string sqlString = @"select node.hostsubeqid,
                                       evt.funckey,
                                       substr(to_char(substr(plc.outputdata,
                                                             to_number(evt.startadr, 'xxxxxxx') * 4 + 1,
                                                             evt.datalen * 4)),
                                              4,
                                              1) reportType,
                                       substr(to_char(substr(plc.outputdata,
                                                             to_number(evt.startadr, 'xxxxxxx') * 4 + 1,
                                                             evt.datalen * 4)),
                                              evt.datalen * 4 - 3,
                                              4) evtindex,
                                       node.recipesplit,
                                       substr(to_char(substr(plc.outputdata,
                                                             to_number(evt.startadr, 'xxxxxxx') * 4 + 1,
                                                             evt.datalen * 4)),
                                              17,
                                              800) itemdata
                                  from main_bc_line t, io_event evt, main_bc_node node, plcdata plc
                                 where t.hostlineid = '" + BC.BCName+ @"'
                                   and node.hostsubeqid = '" + eqp.EQPName+ @"'
                                   and t.bcno = evt.bcno
                                   and t.bclineno = evt.bclineno
                                   and t.fabtype = evt.fabtype
                                   and t.bcno = node.bcno
                                   and t.bclineno = node.bclineno
                                   and t.fabtype = node.fabtype
                                   and t.bcno = plc.bcno
                                   and t.bclineno = plc.bclineno
                                   and t.fabtype = plc.fabtype
                                   and plc.devicetype = '2'
                                   and evt.nodeno = node.nodeno
                                   and evt.funckey = 'ED03'
                                   and evt.subfunckey = '01'";

            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {
                result.ReportType = eachRow["reportType"].ToString();
                result.DataItem = eachRow["itemdata"].ToString();
                result.Index = eachRow["evtindex"].ToString();
                
            }


            return result;
        }

        public List<RecipeBodyParseData> GetRecipeBodyParseData(BCInfo BC, EQP eqp, ED03 ed03)
        {
            List<RecipeBodyParseData> result = new List<RecipeBodyParseData>();
            
            foreach (RecipePaserSetting eachSetting in GetRecipeBodySetting(BC, eqp))
            {
                string itemHexString = ed03.DataItem.Substring(eachSetting.ItemOffset * 4 - 4, eachSetting.ItemLength * 4);
                RecipeBodyParseData eachResult = new RecipeBodyParseData();
                switch (eachSetting.ItemFormat)
                {
                    case "A":
                        string tmpAsciiString = "";
                        string reverseAsciiString = "";
                        for (int i = 0; i < itemHexString.Length; i += 4)
                        {
                            string tmpAscii = itemHexString.Substring(i,4);
                            for (int j = 0; j < tmpAscii.Length; j += 2)
                            {
                                tmpAsciiString = tmpAscii.Substring(j, 2) + tmpAsciiString;
                            }
                            reverseAsciiString = reverseAsciiString + tmpAsciiString;
                        }

                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < reverseAsciiString.Length; i += 2)
                        {
                            string hs = reverseAsciiString.Substring(i, 2);
                            sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
                        }
                        eachResult.Name = eachSetting.ItemName;
                        eachResult.Value = sb.ToString().Substring(0, eachSetting.ItemEfflen);
                        eachResult.Unit = eachSetting.ItemUnit;
                        result.Add(eachResult);

                        break;
                    case "N":
                    case "D":
                        string reverseItemString = "";
                        
                        for (int i = 0; i < itemHexString.Length; i += 4)
                        {
                            reverseItemString = itemHexString.Substring(i, 4) + reverseItemString;
                        }

                        double dbValue = 0;
                        
                        if (eachSetting.ItemSigned.Equals("1"))
                        {
                            dbValue = Convert.ToDouble(int.Parse(reverseItemString, System.Globalization.NumberStyles.HexNumber));
                        }
                        else
                        {
                            dbValue = Convert.ToDouble(uint.Parse(reverseItemString, System.Globalization.NumberStyles.HexNumber));
                        }

                        
                        dbValue = dbValue * eachSetting.ItemRate;
                        
                        eachResult.Name = eachSetting.ItemName;
                        eachResult.Value = dbValue.ToString("0.000000");
                        eachResult.Unit = eachSetting.ItemUnit;
                        result.Add(eachResult);

                            break;
                    case "H":
                        eachResult.Name = eachSetting.ItemName;
                        eachResult.Value = itemHexString;
                        eachResult.Unit = eachSetting.ItemUnit;
                        result.Add(eachResult);
                            break;
                    case "B":
                        eachResult.Name = eachSetting.ItemName;
                        eachResult.Value = Convert.ToString(Convert.ToInt32(itemHexString, 16), 2).PadLeft(itemHexString.Length*4, '0');
                        eachResult.Unit = eachSetting.ItemUnit;
                        result.Add(eachResult);
                            break;
                }

            }

            return result;
        }

        public List<RecipePaserSetting> GetRecipeBodySetting(BCInfo BC, EQP eqp)
        {
            List<RecipePaserSetting> result = new List<RecipePaserSetting>();

            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST =" + BC.BCIP + ")(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME =ORCL)));User Id=innolux;Password=innoluxabc123;");

            string sqlString = @"select * from setrecipebodyparameter t where t.recipesplit = '"+eqp.RecipeSplit+"'";

            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {
                RecipePaserSetting each = new RecipePaserSetting();

                each.ItemName = eachRow["ItemName"].ToString();
                each.ItemOffset = int.Parse(eachRow["ItemOffset"].ToString());
                each.ItemLength = int.Parse(eachRow["ItemLength"].ToString());
                each.ItemRate = double.Parse(eachRow["ItemRate"].ToString());

                if (!int.TryParse(eachRow["ItemEfflen"].ToString(), out each.ItemEfflen))
                {
                    each.ItemEfflen = 999;
                }

                if (!int.TryParse(eachRow["ItemSigned"].ToString(), out each.ItemSigned))
                {
                    each.ItemSigned = 1;
                }
                
                each.ItemUnit = eachRow["ItemUnit"].ToString();
                each.ItemFormat = eachRow["ItemFormat"].ToString();
                result.Add(each);
            }

            return result;
        }

        public List<EQP> GetEQP(BCInfo BC)
        {
            List<EQP> result = new List<EQP>();

            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST =" + BC.BCIP + ")(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME =ORCL)));User Id=innolux;Password=innoluxabc123;");

            string sqlString = @"select node.* from main_bc_node node ,main_bc_line t
                                     where t.hostlineid = '"+BC.BCName+@"'
                                     and t.bcno = node.bcno
                                     and t.fabtype = node.fabtype
                                     and t.bclineno = node.bclineno";

            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {
                EQP each = new EQP();

                each.EQPName = eachRow["hostsubeqid"].ToString();
                each.Description = eachRow["eqname"].ToString();
                each.NodeNo = int.Parse(eachRow["nodeno"].ToString());
                each.RecipeSplit = eachRow["recipesplit"].ToString();


                result.Add(each);
            }

            return result;
        }

        public List<PPID> GetPPID(BCInfo BC, string PPID)
        {
            List<PPID> result = new List<PPID>();

            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST =" + BC.BCIP + ")(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME =ORCL)));User Id=innolux;Password=innoluxabc123;");

            string sqlString = @"select pp.*
                                  from setppid pp, main_bc_line t
                                 where t.hostlineid = '" + BC.BCName + @"'
                                   and t.bcno = pp.bcno
                                   and t.fabtype = pp.fabtype
                                   and t.bclineno = pp.bclineno";
            if (!PPID.Equals(""))
            {
                sqlString += " and pp.ppid = '"+PPID+"'";
            }

            sqlString += " order by pp.ppid";

            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {
                PPID each = new PPID();

                each.Name = eachRow["PPID"].ToString();
                for (int i = 1; i <= 32; i++)
                {
                    Recipe rep = new Recipe();
                    rep.NodeNo = i;
                    rep.RecipeNo = int.Parse(eachRow["EQ" + i + "Recipe"].ToString());
                    each.RecipeTable.Add(rep);
                }
                


                result.Add(each);
            }

            return result;
        }

        public List<BCInfo> GetBCInfo()
        {
            List<BCInfo> result = new List<BCInfo>();

            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION=(ADDRESS_LIST =      (ADDRESS = (PROTOCOL = TCP)(HOST = 172.20.9.1)(PORT = 1521))      (ADDRESS = (PROTOCOL = TCP)(HOST = 172.20.9.2)(PORT = 1521))      (LOAD_BALANCE = yes)    )    (CONNECT_DATA =      (SERVER = DEDICATED)      (SERVICE_NAME = t2pcel)    ));User Id=L5CEL;Password=L5CEL;");

            string sqlString = "select * from dems_bcip t order by bc_name";

            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {
                BCInfo each = new BCInfo();

                each.Fab = eachRow["Fab"].ToString();
                each.BCName = eachRow["BC_Name"].ToString();
                each.BCIP = eachRow["IP"].ToString();
               

                result.Add(each);
            }

            return result;
        }

        public List<RecipeBody> GetRecipeBody(string BCName,string BCIP)
        {
            List<RecipeBody> result = new List<RecipeBody>();

            OracleDB dbObj = new OracleDB("Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST =" + BCIP + ")(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME =ORCL)));User Id=innolux;Password=innoluxabc123;");

            string sqlString = @"select t.hostlineid1 bcname,
                                   t.hostsubeqid subeqid,
                                   t.eqname,
                                   t2.recipesplit,
                                   t2.itemname,
                                   t2.itemformat,
                                   t2.itemoffset,
                                   t2.itemlength,
                                   t2.itemrate,
                                   t2.itemefflen,
                                   t2.itemsigned,
                                   t2.itemunit
                              from main_bc_node t, main_bc_line t1, setrecipebodyparameter t2
                             where t1.hostlineid = '"+BCName+@"'
                               and t.bcno = t1.bcno
                               and t.bclineno = t1.bclineno
                               and t.fabtype = t1.fabtype
                               and t.recipesplit = t2.recipesplit(+)
                            union
                            select 
                            '' as bcname,
                                   '' as ubeqid,
                                   '' as eqname,
                                   t2.recipesplit,
                                   t2.itemname,
                                   t2.itemformat,
                                   t2.itemoffset,
                                   t2.itemlength,
                                   t2.itemrate,
                                   t2.itemefflen,
                                   t2.itemsigned,
                                   t2.itemunit
                              from setrecipebodyparameter t2
                             where t2.recipesplit not in(select distinct t.recipesplit
                              from main_bc_node t, main_bc_line t1, setrecipebodyparameter t2
                             where t1.hostlineid = '" + BCName + @"'
                               and t.bcno = t1.bcno
                               and t.bclineno = t1.bclineno
                               and t.fabtype = t1.fabtype
                               and t.recipesplit is not null)";

            DataTable Dt = dbObj.SelectSQL(sqlString);

            foreach (DataRow eachRow in Dt.Rows)
            {
                RecipeBody each = new RecipeBody();

                each.BCName = eachRow["BCName"].ToString();
                each.EqName = eachRow["EqName"].ToString();
                each.RecipeSplit = eachRow["RecipeSplit"].ToString();
                each.ItemEffLen = eachRow["ItemEffLen"].ToString();
                each.ItemFormat = eachRow["ItemFormat"].ToString();
                each.ItemLength = eachRow["ItemLength"].ToString();
                each.ItemName = eachRow["ItemName"].ToString();
                each.ItemOffset = eachRow["ItemOffset"].ToString();
                each.ItemRate = eachRow["ItemRate"].ToString();
                each.ItemSigned = eachRow["ItemSigned"].ToString();
                each.ItemUnit = eachRow["ItemUnit"].ToString();
                each.SubeqID = eachRow["SubeqID"].ToString();

                result.Add(each);
            }

            return result;
        }

    }
}