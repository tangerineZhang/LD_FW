using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

//using BlueThink.Comm;
//using DotNet.Utilities;
using System.Net;
//using System.Runtime.Serialization.Json;
using Maticsoft.DBUtility;

namespace LDFW.DAL
{
    /// <summary>
    /// 数据访问类:LotInfo
    /// </summary>
    public  class PublicDao
    {
        public  string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection89"].ConnectionString;
        public PublicDao()
        { }
        #region  BasicMethod


        #endregion  BasicMethod
        #region  ExtensionMethod




        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns>返回毫秒数</returns>
        public long GetTime()
        {
            return (DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000;
        }

        ////根据openid，access token获得用户信息
        //public WXUser Get_UserInfo(string REFRESH_TOKEN, string OPENID, ref string getjson)
        //{
        //    //string Str = GetJson("https://api.weixin.qq.com/sns/userinfo?access_token=" + REFRESH_TOKEN + "&openid=" + OPENID);公众号接口
        //    string Str = GetJson("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=" + REFRESH_TOKEN + "&code=" + OPENID);
        //    getjson = Str;
        //    //div1.InnerText += " <Get_UserInfo> " + Str + " <> ";

        //    WXUser OAuthUser_Model = JsonHelper.ParseFromJson<WXUser>(Str);
        //    return OAuthUser_Model;
        //}

        //访问微信url并返回微信信息
        protected string GetJson(string url)
        {
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            wc.Encoding = System.Text.Encoding.UTF8;
            string returnText = wc.DownloadString(url);

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
                return returnText;
            }
            return returnText;
        }

        public class WXUser
        {
            public WXUser()
            { }
            #region 数据库字段
            private string _UserId;
            private string _DeviceId;

            #endregion

            #region 字段属性
            /// <summary>
            /// 用户的唯一标识
            /// </summary>
            public string UserId
            {
                set { _UserId = value; }
                get { return _UserId; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string DeviceId
            {
                set { _DeviceId = value; }
                get { return _DeviceId; }
            }

            #endregion
        }

        #endregion  ExtensionMethod



        public bool TransactionAddEmployee(string Company, string Department, string UserName,string UserID, string IsDeclare,
            string Reason, string VisitCompany, string Address, string VisitDate, string VisitTime,
            string NumVisitors, string SupplierName, ref string ID, ref int rCount, ref string errorInfo)
        {
            StringBuilder strSql = new StringBuilder();

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;

            try
            {

                strSql = new StringBuilder();
                strSql.Append("select NEWID() ");
                comm.CommandText = strSql.ToString();
                ID = comm.ExecuteScalar().ToString();
                
                strSql = new StringBuilder();
                strSql.Append("insert into [LDXX].[dbo].[YQ_EmployeeDeclaration] (ID,Company,Department,UserName,UserID,IsDeclare,Reason,VisitCompany,Address,VisitDate,");
                strSql.Append("BegVisitTime,EndVisitTime,VisitTime,NumVisitors,SupplierName,SortID,CreatorID,Creator) ");
                strSql.Append("Values('" + ID + "','" + Company + "','" + Department + "','" + UserName + "','" + UserID + "','" + IsDeclare + "','" + Reason + "','" + VisitCompany + "','" + Address + "','" + VisitDate + "',");
                strSql.Append("'','','" + VisitTime + "'," + NumVisitors + ",'" + SupplierName + "',null,'','')");

                comm.CommandText = strSql.ToString();
                //comm.Parameters.Clear();
                //comm.Parameters.AddRange(parametersDM);
                rCount += comm.ExecuteNonQuery();

                tran.Commit();
                conn.Close();
                return true;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = strSql.ToString();
                conn.Close();
                return false;
            }
            finally
            {
                //conn.Close();
            }
        }

  


        public DataSet GetUserInfo(string usercode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CASE WHEN ISNULL(u.F3,'')<>'' THEN u.F3 ELSE u.UserCode END as 工号, ");
            strSql.Append("CASE WHEN ISNULL(substring(substring(o.F3,charindex('/',o.F3)+1,len(o.F3)-charindex('/',o.F3)),0,charindex('/',substring(o.F3,charindex('/',o.F3)+1,len(o.F3)-charindex('/',o.F3)))),'')<>'' THEN ");
            strSql.Append("substring(substring(o.F3,charindex('/',o.F3)+1,len(o.F3)-charindex('/',o.F3)),0,charindex('/',substring(o.F3,charindex('/',o.F3)+1,len(o.F3)-charindex('/',o.F3)))) ");
            strSql.Append("else substring(o.F3,charindex('/',o.F3)+1,len(o.F3)) ");
            strSql.Append("END as 公司,o.F3 as 组织全路径,");
            strSql.Append("CASE WHEN o.OrgUnitLever>4 THEN substring(substring(substring(o.F3,charindex('/',o.F3)+1,len(o.F3)),charindex('/',substring(o.F3,charindex('/',o.F3)+1,len(o.F3)))+1,len(o.F3)),0,charindex('/',substring(substring(o.F3,charindex('/',o.F3)+1,len(o.F3)),charindex('/',substring(o.F3,charindex('/',o.F3)+1,len(o.F3)))+1,len(o.F3)))) ");
            strSql.Append("ELSE CASE WHEN o.OrgUnitLever=4 THEN ISNULL(o.F2,o.OrgUnitName) ELSE NULL END END as 中心,o.OrgUnitName as 部门 , ");
            strSql.Append("p.PositionName as 岗位,u.RankName as 职级编号, ");
            strSql.Append("u.UserName as 姓名,u.UserLoginID as 账号,u.GenderText as 性别,u.MobilePhone as 手机,u.Email as 邮箱,CONVERT(VARCHAR(10), u.BirthDay, 120)  as 生日 ");
            strSql.Append("from dbo.MDM_User u ");
            strSql.Append("inner join dbo.MDM_User_Position_Link up on up.UserGUID=u.UserID and up.IsMainPosition=1 and up.Status=1 ");
            strSql.Append("inner join dbo.MDM_PostOrganization_Link po on po.PositionGuid=up.PositionGUID and po.Status=1 ");
            strSql.Append("inner join dbo.MDM_OrganizationUnit o on o.OrgUnitGUID = po.OrgUnitGUID and o.Status=1 ");
            strSql.Append("left join dbo.MDM_OrganizationUnit q on q.OrgUnitGUID = po.OrgUnitGUID and o.Status=1 and q.CompanyType=102 ");
            strSql.Append("left join dbo.MDM_Position as p on p.PositionGUID=po.PositionGuid ");
            strSql.Append("where u.Status=1 ");
            strSql.Append("and u.usertypename = '内部员工' ");
            strSql.Append("and u.UserLoginID = '" + usercode + "' ");
            strSql.Append("order by substring(substring(o.F3,charindex('/',o.F3)+1,len(o.F3)-charindex('/',o.F3)),0,charindex('/',substring(o.F3,charindex('/',o.F3)+1,len(o.F3)-charindex('/',o.F3)))),o.OrgUnitName ");
            return DbHelperSQL160.Query(strSql.ToString());
        }



        public  DataTable GetProcessUrl(string boid)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"SELECT top 1
		                            *
		                            FROM dbo.CallbackLog WHERE MainId='{0}'", boid);
            return DbHelperSQL160.Query(sqlStr.ToString()).Tables[0];
           
        }

        public  bool EmptyUrl(string strboid)
        {
            bool result = true;
            var url = GetProcessUrl(strboid);
            try
            {
                if (url.Rows.Count > 0)
                {
                    var strSql = @"update CallbackLog set ProcessUrl=null where MainId = '" + strboid + "'";
                    DbHelperSQL160.ExecuteSql(strSql);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public  int Insert(string codes, string mainId, int i, string url, DateTime date,string btid)
        {
            var k = 0;
            var callback = GetProcessUrl(mainId);
            if (callback.Rows.Count > 0)
            {
                var id = callback.Rows[0]["ID"].ToString();
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendFormat(@"  update dbo.CallbackLog set RequestId = '{0}',MainId ='{1}',StatusType = '{2}',ProcessUrl = '{3}',CreateDate = '{4}'
                                  where ID = '{5}' ", codes, mainId, i, url, date, id);
                k = DbHelperSQL160.ExecuteSql(sqlStr.ToString());
            }
            else
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendFormat(@"  INSERT INTO dbo.CallbackLog
                                                ( ID ,
                                                  RequestId ,
                                                  MainId ,
                                                  StatusType ,
												  ProcessUrl,
												  CreateDate,
                                                  F1
                                                )VALUES  ( '{0}','{1}','{2}','{3}','{4}','{5},'{6}')", Guid.NewGuid(), codes, mainId, i, url, date,btid);
                k = DbHelperSQL160.ExecuteSql(sqlStr.ToString());

            }
            return k;
        }


        public  DataTable GetCallBackRequestId(string mainId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"SELECT top 1
		                            *
		                            FROM dbo.CallbackLog WHERE MainId='{0}'", mainId);
            DataSet ds = DbHelperSQL160.Query(sqlStr.ToString());
            return ds.Tables[0];
        }

    }


    ///// <summary>
    ///// 将Json格式数据转化成对象
    ///// </summary>
    //public class JsonHelper
    //{
    //    /// <summary>  
    //    /// 生成Json格式  
    //    /// </summary>  
    //    /// <typeparam name="T"></typeparam>  
    //    /// <param name="obj"></param>  
    //    /// <returns></returns>  
    //    public static string GetJson<T>(T obj)
    //    {
    //        DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
    //        using (MemoryStream stream = new MemoryStream())
    //        {
    //            json.WriteObject(stream, obj);
    //            string szJson = Encoding.UTF8.GetString(stream.ToArray()); return szJson;
    //        }
    //    }
    //    /// <summary>  
    //    /// 获取Json的Model  
    //    /// </summary>  
    //    /// <typeparam name="T"></typeparam>  
    //    /// <param name="szJson"></param>  
    //    /// <returns></returns>  
    //    public static T ParseFromJson<T>(string szJson)
    //    {
    //        T obj = Activator.CreateInstance<T>();
    //        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
    //        {
    //            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
    //            return (T)serializer.ReadObject(ms);
    //        }
    //    }
    //}






}

public class T//:IEquatable<Person>
{
    public string _sql = null;
    public string _sql2 = null;
    public string _inputdate = null;
    public string _bankaccountno = null;
    public string _cName = null;
    public string _cAreaname = null;

    public T() { }

    public T(string sql, string inputdate, string bankaccountno, string cName, string cAreaname, string sql2)
    {
        this._sql = sql;
        this._sql2 = sql2;
        this._inputdate = inputdate;
        this._bankaccountno = bankaccountno;
        this._cName = cName;
        this._cAreaname = cAreaname;
    }
}


