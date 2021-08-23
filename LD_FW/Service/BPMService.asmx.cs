using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;
using Common;
using DAL;

using LDFW.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LD.Service
{
    /// <summary>
    /// BPMService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class BPMService : System.Web.Services.WebService
    {
        FwDataInfo Fw = new FwDataInfo();


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string ApproveClose(string strBTID, string strBOID, int iProcInstID, string eProcessInstanceResult, string strComment, DateTime dtTime)
        {
            bool success = false;
            string errmsg = string.Empty;
            CreateResultModel result = new CreateResultModel();
            BPMResultEntity entity = new BPMResultEntity
            {
                strBTID = strBTID,
                strBOID = strBOID,
                iProcInstID = iProcInstID,
                eProcessInstanceResult = eProcessInstanceResult,
                strComment = strComment,
                dtTime = dtTime,
            };
            var model = JsonConvert.SerializeObject(entity);
            if (model != null)
            {
                int ApproveStatus = 4;
                if (eProcessInstanceResult == "0" || eProcessInstanceResult == "2")
                {
                    ApproveStatus = 1;
                    var strSql = @"update CallbackLog set ProcessUrl=null where MainId = '" + strBOID + "'";
                    Fw.UpdateAYZ_SealInfo(strSql);
                    var smas1 = "时间:" + DateTime.Now.ToLongDateString() + "   状态:流程作废" + "---------";
                    string sqler1 = $"UPDATE FW_FollowUp set FUStatus='{"流程作废"}',FollowUpInfo=CONCAT('{smas1}',FollowUpInfo) where ID='{strBOID}'";
                    success = Fw.UpdateAYZ_SealInfo(sqler1);

                }
                else
                {
                    string s = $"select LID from FW_FollowUp where ID='{strBOID}'";
                    DataTable dt = Fw.Selectid(s);
                    if (dt.Rows.Count > 0)
                    {
                        string sql = string.Format("UPDATE  [dbo].[FW_LawInfo] SET ApproveStatus={0}  WHERE ID='{1}'", ApproveStatus, dt.Rows[0]["LID"]);
                        success = Fw.UpdateAYZ_SealInfo(sql);
                    }

                    var smas = "时间:" + DateTime.Now.ToLongDateString() + "   状态:流程通过" + "---------";
                    string sqler = $"UPDATE FW_FollowUp set FUStatus='{"流程通过"}',FollowUpInfo=CONCAT('{smas}',FollowUpInfo) where ID='{strBOID}'";
                    success = Fw.UpdateAYZ_SealInfo(sqler);

                    if (strBTID == "FW02")
                    {
                        if (strComment != null)
                        {


                            var res = JsonConvert.DeserializeObject<JObject>(strComment);
                            if (res != null)
                            {
                                if (res["IsDeblocking"].ToString()=="是")
                                {
                                    res["IsDeblocking"] = "1";
                                }
                                else
                                {
                                    res["IsDeblocking"] = "0";
                                }
                                if (res["IsPremium"].ToString()=="是")
                                {
                                    res["IsPremium"] = "1";
                                }
                                else
                                {
                                    res["IsPremium"] = "0";
                                }
                            
                                if (dt.Rows.Count > 0)
                                {
                                    string sql11 = string.Format("UPDATE  [dbo].[FW_LawInfo] SET IsPremium='{0}',PremiumDate='{3}',IsDeblocking='{5}',DeblockingDate='{6}',ModifyDate='{1}',SettleResult='{4}',SettleType='{7}',ClosingDate='{8}',LawStatus='{9}'  WHERE ID='{2}'", res["IsPremium"], DateTime.Now, dt.Rows[0]["LID"], res["PremiumDate"], res["SettleResult"], res["IsDeblocking"], res["DeblockingDate"], res["SettleType"], res["ClosingDate"],"结案");
                                    success = Fw.UpdateAYZ_SealInfo(sql11);
                                }
                            }
                        }
                    }
                    if (strBTID == "FW01")
                    {
                        if (strComment != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                string sqlers = $"UPDATE FW_FollowUp set TaskFwwdback='{strComment}',SortID=1 where ID='{strBOID}'";
                                success = Fw.UpdateAYZ_SealInfo(sqlers);
                            }
                        }

                    }
                }

                if (success)
                {
                    result.success = "成功";
                }
                else
                {
                    result.error = "失败";
                }
            }

            if (result != null)
            {

            }
            return XmlSerialization<CreateResultModel>(result);

        }

        [WebMethod]
        public string Audit(string strBTID, string strBOID, int iProcInstID, string strStepName, string strApproverId, string eAction, string strComment, System.DateTime dtTime)
        {
            bool success = false;
            string errmsg = string.Empty;
            CreateResultModel result = new CreateResultModel();
            BPMResultEntity entity = new BPMResultEntity
            {
                strBTID = strBTID,
                strBOID = strBOID,
                strStepName = strStepName,
                iProcInstID = iProcInstID,
                strApproverId = strApproverId,
                eAction = eAction,
                strComment = strComment,
                dtTime = dtTime
            };

            var model = JsonConvert.SerializeObject(entity);
            SystemLog.Info(model.ToJson().ToString());
            if (model != null)
            {
                int ApproveStatus = 3;
                if (eAction == "4" || eAction == "2")
                {
                    ApproveStatus = 1;
                    var strSql = @"update CallbackLog set ProcessUrl=null where MainId = '" + strBOID + "'";
                    Fw.UpdateAYZ_SealInfo(strSql);
                }
                string sql = string.Format("UPDATE  [dbo].[FW_LawInfo] SET ApproveStatus={0}  WHERE ID='{1}'", ApproveStatus, strBOID);
                success = Fw.UpdateAYZ_SealInfo(sql);
                var smas = "时间:" + DateTime.Now.ToLongDateString() + "   状态:流程驳回" + "---------";
                string sqler = $"UPDATE FW_FollowUp set FUStatus='{"流程驳回"}',FollowUpInfo=CONCAT('{smas}',FollowUpInfo) where ID='{strBOID}'";
                success = Fw.UpdateAYZ_SealInfo(sqler);
                if (success)
                {
                    result.success = "成功";
                }
                else
                {
                    result.error = "失败";
                }
            }
            return XmlSerialization<CreateResultModel>(result);
        }

        [WebMethod]
        public string CreateResult(string strBTID, string strBOID, string bSuccess, int iProcInstID, string procURL, string strMessage)
        {
            try
            {

                bool success = false;
                string errmsg = string.Empty;
                CreateResultModel result = new CreateResultModel();
                BPMResultEntity entity = new BPMResultEntity
                {
                    strBTID = strBTID,
                    strBOID = strBOID,
                    bSuccess = bSuccess,
                    iProcInstID = iProcInstID,
                    procURL = procURL,
                    strMessage = strMessage
                };
                var model = JsonConvert.SerializeObject(entity);
                SystemLog.Info(model.ToJson().ToString());
                if (model != null)
                {
                    if (bSuccess.Trim() == "1")
                    {
                        success = new PublicDao().Insert(iProcInstID.ToString(), strBOID, 1, procURL, DateTime.Now, strBTID) > 0;
                    }
                    if (bSuccess.Trim() == "4")
                    {
                        int ApproveStatus = 1;
                        var strSql = @"update CallbackLog set ProcessUrl=null where MainId = '" + strBOID + "'";
                        Fw.UpdateAYZ_SealInfo(strSql);
                        string sql = string.Format("UPDATE  [dbo].[FW_LawInfo] SET ApproveStatus={0}  WHERE ID='{1}'", ApproveStatus, strBOID);
                        success = Fw.UpdateAYZ_SealInfo(sql);

                        var smas = "时间:" + DateTime.Now.ToLongDateString() + "   状态:驳回后流程发起" + "---------";
                        string sqler = $"UPDATE FW_FollowUp set FUStatus='{"流程发起"}',FollowUpInfo=CONCAT('{smas}',FollowUpInfo) where ID='{strBOID}'";
                        success = Fw.UpdateAYZ_SealInfo(sqler);
                    }
                    if (success)
                    {
                        result.success = "成功";
                    }

                    else
                    {
                        result.error = "失败";
                    }
                }

                return XmlSerialization<CreateResultModel>(result);
            }
            catch (Exception ex)
            {
                return ex.Message;

            }


        }
        [WebMethod]
        public string PassResult(string strBTID, string strBOID, string bSuccess, int iProcInstID, string procURL, string strMessage)
        {
            try
            {
                bool success = false;
                string errmsg = string.Empty;
                CreateResultModel result = new CreateResultModel();
                if (strBTID == "FW01")
                {
                    string sql = string.Format($"UPDATE  [dbo].[FW_LawInfo] SET LawStatus='{"结案"}'  WHERE ID='{strBOID}'");
                    success = Fw.UpdateAYZ_SealInfo(sql);
                    if (success)
                    {
                        result.success = "成功";
                    }

                    else
                    {
                        result.error = "失败";
                    }
                }
                if (strBTID == "FW02")
                {
                    string sql = string.Format($"UPDATE  [dbo].[FW_LawInfo] SET LawStatus='{"结案"}'  WHERE ID='{strBOID}'");
                    success = Fw.UpdateAYZ_SealInfo(sql);
                    if (success)
                    {
                        result.success = "成功";
                    }

                    else
                    {
                        result.error = "失败";
                    }
                }
                return XmlSerialization<CreateResultModel>(result);

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        /// <summary>
        /// 实体类转换成xml
        /// </summary>
        /// <typeparam name="T">实体类数据</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string XmlSerialization<T>(T obj)
        {
            XmlWriterSettings xmlsetting = null;
            MemoryStream ms = null;
            XmlWriter xw = null;
            XmlSerializerNamespaces xmlns = null;
            XmlSerializer xml = null;
            string xmlstring = string.Empty;
            try
            {
                xmlsetting = new XmlWriterSettings();
                xmlsetting.Encoding = Encoding.UTF8;

                ms = new MemoryStream();
                xw = XmlWriter.Create(ms, xmlsetting);

                Type type = obj.GetType();

                xmlns = new XmlSerializerNamespaces();
                xmlns.Add("", "");

                xml = new XmlSerializer(type);
                xml.Serialize(xw, obj, xmlns);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xmlstring = Encoding.UTF8.GetString(ms.ToArray());
                xw.Close();
                ms.Close();
            }
            return xmlstring;
        }
    }

    public class BPMResultEntity
    {
        /// <summary>
        /// 业务单据ID
        /// </summary>
        public string strBTID { get; set; }
        /// <summary>
        /// 业务系统在接口传入的业务对象ID
        /// </summary>
        public string strBOID { get; set; }
        /// <summary>
        /// 表示创建流程实例是否成功
        /// </summary>
        public string bSuccess { get; set; }
        /// <summary>
        /// 该业务对象对应的创建的流程实例ID
        /// </summary>
        public int iProcInstID { get; set; }
        /// <summary>
        /// 返回的BPM流程URL
        /// </summary>
        public string procURL { get; set; }
        /// <summary>
        /// BPM接口提供的信息反馈
        /// </summary>
        public string strMessage { get; set; }
        /// <summary>
        /// 审批时的步骤名称
        /// </summary>
        public string strStepName { get; set; }
        /// <summary>
        /// 表示审批者用户ID
        /// </summary>
        public string strApproverId { get; set; }
        /// <summary>
        /// 1.  审批同意  2.  退回发起人
        /// </summary>
        public string eAction { get; set; }
        /// <summary>
        /// 用户的审批意见备注
        /// </summary>
        public string strComment { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime dtTime { get; set; }
        /// <summary>
        /// 流程审批结果
        /// </summary>
        public string eProcessInstanceResult { get; set; }
    }

    [XmlRoot("DATA")]
    public class CreateResultModel
    {
        public string success { get; set; }
        public string error { get; set; }
    }
}
