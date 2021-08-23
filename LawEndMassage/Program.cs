using Common;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebUtility;

namespace LawEndMassage
{
   public class Program
    {
        static void Main(string[] args)
        {
            FollowUpTypeCG();
        }

        public static void FollowUpTypeCG()
        {
            string appId = "FWXX";
            string appSecret = "28A7AD47-E569-4935-999A-1018E1B0473A";
            WebUtility.ThirdPartyJob third = new ThirdPartyJob();
            try
            {
                string timedate = DateTime.Now.ToLongDateString().ToString();
                string procInstId = "";
                string topic = "";
                string originator = "";
                string originatorName = "";
                string startDate = "";
                string jobUserId = "";
                string jobStepName = "";
                string webUrl = "";
                DataTable dt = FW_FollowUpCG();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ClosingDateend"] == null && dt.Rows[i]["ClosingDateend"].ToString() == "")
                    {


                        if (dt.Rows[i]["NextDate"] != null && dt.Rows[i]["NextDate"].ToString() != "")
                        {
                            if (DateTime.Now.Day <= Convert.ToInt32(Convert.ToDateTime(dt.Rows[i]["NextDate"]).Day))
                            {
                                string time = System.DateTime.Now.ToString("yyMMdd");
                                var remdnum = GenerateRandomCode(6);
                                //var Code = "ZC" + time + remdnum;
                                procInstId = "FWXX" + time + remdnum;
                                topic = "案件常规跟进待办——" + dt.Rows[i]["LawName"].ToString();
                                originator = "admin";
                                originatorName = "管理员";
                                startDate = DateTime.Now.ToString();
                                jobUserId = dt.Rows[i]["CreatorID"].ToString();
                                jobStepName = "待办";
                                webUrl = "http://47.100.116.153:86/Law/ThreeView?id=" + dt.Rows[i]["CreatorID"] + "&type=CG&ids=" + dt.Rows[i]["ID"] + "&procid=" + procInstId;
                                third.Todo(appId, appSecret, procInstId, topic, originator, originatorName, startDate, jobUserId, jobStepName, webUrl, string.Empty);
                                SystemLog.Info("案件常规跟进待办发起成功!待办信息是:" + topic + "|" + jobUserId + "|" + startDate + "|" + webUrl);
                                Console.WriteLine(dt.Rows[i]["CreatorID"].ToString()+ "案件常规跟进待办发起成功");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SystemLog.Info("案件常规跟进待办发送失败!失败原因:" + ex.ToString());
                throw;
            }
        }

        public static string GenerateRandomCode(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }

        //查询案件常规跟进情况
        public static DataTable FW_FollowUpCG()
        {
            string str = string.Format(@"  select ( case f.NextDate when  '1900-01-01 00:00:00.000' then null else f.NextDate end) as NextDate,
  (case f.FollowUpdate when  '1900-01-01 00:00:00.000' then null else f.FollowUpdate end ) as FollowUpdate ,
(CASE when c.ClosingDate='1900-01-01 00:00:00.000' THEN NULL ELSE c.ClosingDate END) AS ClosingDateend,f.SortID,f.ID,f.LogInfoID,f.LawType,f.LID,f.PersonFollowUp,f.FollowUpInfo,f.FUStatus,f.PersonLiable,f.Solutions,f.ISValid,f.CreateDate,c.ID AS CID 
  ,f.CreatorID,f.Creator,c.LawName  from FW_FollowUp f  LEFT JOIN dbo.FW_LawInfo c ON f.LID=c.ID 
   where  f.ISValid=1 AND f.SortID =0  AND f.LawType='案件常规跟进'
    ");
            return DbHelperSQL160.ExecSqlDateTable(str);
        }
    }
}
