using Common;
using LDFW.DAL;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using WebUtility;

namespace ThierdMassage
{
    public class Program
    {
        static void Main(string[] args)
        {
            FW_LitigationMass();
            FollowUpTypeRW();
           
            Console.ReadKey();
        }


        public static void FW_LitigationMass()
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
                string time;
                var remdnum="";
                DataTable dt = FW_LitigationHelp();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ClosingDateend"] == null || dt.Rows[i]["ClosingDateend"].ToString() == "")
                    {

                        if (dt.Rows[i]["LPDataEnd"] != null && dt.Rows[i]["LPDataEnd"].ToString() != "")
                        {
                            if (DateTime.Now.Day >= Convert.ToInt32(Convert.ToDateTime(dt.Rows[i]["LPDataEnd"]).Day - 45))
                            {
                                if (dt.Rows[i]["ModifyDate"] != null && dt.Rows[i]["ModifyDate"].ToString() != "")
                                {
                                    if (DateTime.Now >= Convert.ToDateTime(dt.Rows[i]["ModifyDate"]))
                                    {
                                         time = System.DateTime.Now.ToString("yyMMdd");
                                         remdnum = GenerateRandomCode(6);
                                        procInstId = "FWXX" + time + remdnum; 
                                        topic = "诉讼保全信息待办——" + dt.Rows[i]["LawName"].ToString();
                                        originator = "admin";
                                        originatorName = "管理员";
                                        startDate = DateTime.Now.ToString();
                                        jobUserId = dt.Rows[i]["CreatorID"].ToString();
                                        jobStepName = "待办";
                                        webUrl = "http://47.100.116.153:86/Law/ThreeView?id=" + dt.Rows[i]["CreatorID"] + "&type=BQ&ids=" + dt.Rows[i]["ID"] + "&procid=" + procInstId;
                                        third.Todo(appId, appSecret, procInstId, topic, originator, originatorName, startDate, jobUserId, jobStepName, webUrl, string.Empty);
                                        SystemLog.Info("诉讼保全信息待办发起成功!待办信息是:" + topic + "|" + jobUserId + "|" + startDate + "|" + webUrl);
                                        break;
                                    }
                                }
                                 time = System.DateTime.Now.ToString("yyMMdd");
                                 remdnum = GenerateRandomCode(6);
                                procInstId = "FWXX" + time + remdnum;
                                topic = "诉讼保全信息待办——" + dt.Rows[i]["LawName"].ToString();
                                originator = "admin";
                                originatorName = "管理员";
                                startDate = DateTime.Now.ToString();
                                jobUserId = dt.Rows[i]["CreatorID"].ToString();
                                jobStepName = "待办";
                                webUrl = "http://47.100.116.153:86/Law/ThreeView?id=" + dt.Rows[i]["CreatorID"] + "&type=BQ&ids=" + dt.Rows[i]["ID"] + "&procid=" + procInstId;
                                third.Todo(appId, appSecret, procInstId, topic, originator, originatorName, startDate, jobUserId, jobStepName, webUrl, string.Empty);
                                SystemLog.Info("诉讼保全信息待办发起成功!待办信息是:" + topic + "|" + jobUserId + "|" + startDate + "|" + webUrl);
                                Console.WriteLine(dt.Rows[i]["CreatorID"].ToString() + "诉讼保全信息待办发起成功");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SystemLog.Info("诉讼保全信息待办发送失败!失败原因:" + ex.ToString());
                throw;
            }


            //ThirdPartyJob third = new ThirdPartyJob();


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



        public static void FollowUpTypeRW()
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
                string time;
                string remdnum;
                DataTable dt = FW_FollowUpRW();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ClosingDateend"] == null || dt.Rows[i]["ClosingDateend"].ToString() == "")
                    {
                        if (dt.Rows[i]["TaskTime"] != null && dt.Rows[i]["TaskTime"].ToString() != "")
                        {
                            if (DateTime.Now.Day >= Convert.ToInt32(Convert.ToDateTime(dt.Rows[i]["TaskTime"]).Day - 3))
                            {
                                if (dt.Rows[i]["TaskPersonID"] != null && dt.Rows[i]["TaskPersonID"].ToString() != "")
                                {
                                    time = System.DateTime.Now.ToString("yyMMdd");
                                    remdnum = GenerateRandomCode(6);
                                    procInstId = "FWXX" + time + remdnum;
                                    topic = "任务事项跟进待办,任务负责人——" + dt.Rows[i]["LawName"].ToString();
                                    originator = "admin";
                                    originatorName = "管理员";
                                    startDate = DateTime.Now.ToString();
                                    jobUserId = dt.Rows[i]["TaskPersonID"].ToString();
                                    jobStepName = "待办";
                                    webUrl = "http://47.100.116.153:86/Law/ThreeView?id=" + dt.Rows[i]["CreatorID"] + "&type=RW&ids=" + dt.Rows[i]["ID"] + "&procid=" + procInstId;
                                    third.Todo(appId, appSecret, procInstId, topic, originator, originatorName, startDate, jobUserId, jobStepName, webUrl, string.Empty);
                                    SystemLog.Info("任务事项跟进待办,任务负责人发起成功!待办信息是:" + topic + "|" + jobUserId + "|" + startDate + "|" + webUrl);
                                }
                                if (dt.Rows[i]["YewuPersonID"] != null && dt.Rows[i]["YewuPersonID"].ToString() != "")
                                {
                                    time = System.DateTime.Now.ToString("yyMMdd");
                                    remdnum = GenerateRandomCode(6);
                                    procInstId = "FWXX" + time + remdnum;
                                    topic = "任务事项跟进待办,业务负责人——" + dt.Rows[i]["LawName"].ToString();
                                    originator = "admin";
                                    originatorName = "管理员";
                                    startDate = DateTime.Now.ToString();
                                    jobUserId = dt.Rows[i]["YewuPersonID"].ToString();
                                    jobStepName = "待办";
                                    webUrl = "http://47.100.116.153:86/Law/ThreeView?id=" + dt.Rows[i]["CreatorID"] + "&type=RW&ids=" + dt.Rows[i]["ID"] + "&procid=" + procInstId;
                                    third.Todo(appId, appSecret, procInstId, topic, originator, originatorName, startDate, jobUserId, jobStepName, webUrl, string.Empty);
                                    SystemLog.Info("任务事项跟进待办,业务负责人发起成功!待办信息是:" + topic + "|" + jobUserId + "|" + startDate + "|" + webUrl);
                                }

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SystemLog.Info("任务事项跟进待办发送失败!失败原因:" + ex.ToString());
                throw;
            }


            //ThirdPartyJob third = new ThirdPartyJob();


        }

        //查询任务信息
        public static DataTable FW_LitigationHelp()
        {
            string str = String.Format(@"  SELECT  p.ID,p.LID,i.LawName,p.Applicant,p.Respondent,p.LPDataEnd,p.LPCourt,p.LPDate,p.PCost,p.PInformation,(CASE when i.ClosingDate='1900-01-01 00:00:00.000' THEN NULL ELSE i.ClosingDate END) AS ClosingDateend,
  p.ModifyDate,p.SortID,p.ISValid,p.CreateDate,p.CreatorID FROM	FW_LitigationPreservation p LEFT JOIN FW_LawInfo i ON p.LID=i.ID ");
            return DbHelperSQL160.ExecSqlDateTable(str);
        }





        //查询案件任务跟进情况
        public static DataTable FW_FollowUpRW()
        {
            string str = string.Format(@"  select ( case f.NextDate when  '1900-01-01 00:00:00.000' then null else f.NextDate end) as NextDate,
  (case f.FollowUpdate when  '1900-01-01 00:00:00.000' then null else f.FollowUpdate end ) as FollowUpdate ,
(CASE when c.ClosingDate='1900-01-01 00:00:00.000' THEN NULL ELSE c.ClosingDate END) AS ClosingDateend,f.SortID,f.TaskPerson,f.TaskPersonID,f.YewuPerson,f.YewuPersonID,f.TaskTime,f.ID,f.LogInfoID,f.LawType,f.LID,f.PersonFollowUp,f.FollowUpInfo,f.FUStatus,f.PersonLiable,f.Solutions,f.ISValid,f.CreateDate,c.ID AS CID 
  ,f.CreatorID,f.Creator,c.LawName  from FW_FollowUp f  LEFT JOIN dbo.FW_LawInfo c ON f.LID=c.ID 
   where  f.ISValid=1 AND f.SortID =0  AND f.LawType='任务事项跟进' 
    ");
            return DbHelperSQL160.ExecSqlDateTable(str);
        }
    }


}
