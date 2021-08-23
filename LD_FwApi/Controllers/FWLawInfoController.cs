using LDFW.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LD_FwApi.Controllers
{
    public class FWLawInfoController : Controller
    {
        // GET: FWLawInfo
        [HttpGet]
        public ActionResult Index()
        {
            FwDataInfo fW_Law = new FwDataInfo();
            string key = "Grade";
            DataTable dt = fW_Law.SelectLawType(key);
            List<LDFW.Model.FW_Dictionary> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_Dictionary>>(JsonConvert.SerializeObject(dt));

            var data = new
            {
                errcode = 0,
                data = list,
                errmsg = "ok"
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        public ActionResult AddLaw(Root info)
        {
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            string errmsg = string.Empty;
            string sql = string.Empty;
            try
            {
                FwDataInfo fW = new FwDataInfo();

                if (info != null)
                {
                    if (info.Creator!=null && info.Creator != "")
                    {
                        DataTable dt = fW.SelectPeopername(info.Creator);
                        if (dt.Rows.Count>0)
                        {
                            info.Creator = dt.Rows[0]["UserName"].ToString();
                        }
                       
                    }
                    if (info.Solutions == null)
                    {
                        info.Solutions = "";
                    }
                    if (info.VentureFenxi == null)
                    {
                        info.VentureFenxi = "";
                    }
                    if (info.CaseNo==null)
                    {
                        info.CaseNo = "";
                    }
                    if (info.FilingDate==null || info.FilingDate.ToString()== "NaN-0NaN-0NaN")
                    {
                        info.FilingDate = "";
                    }
                    if (info.Prediction == null)
                    {
                        info.Prediction = "";
                    }
                    if (info.TheThird==null)
                    {
                        info.TheThird = "";
                    }
                    if (info.Describe!="")
                    {
                        info.Describe = NoHTML(info.Describe);
                    }
                    if (info.Claims!="")
                    {
                        info.Claims= NoHTML(info.Claims);
                    }
                    if (info.Solutions != "")
                    {
                        info.Solutions = NoHTML(info.Solutions);
                    }
                    if (info.VentureFenxi != "")
                    {
                        info.VentureFenxi = NoHTML(info.VentureFenxi);
                    }
                    if (info.Prediction != "")
                    {
                        info.Prediction = NoHTML(info.Prediction);
                    }
                    if (info.LawsuitType=="主诉")
                    {
                        info.AmountInvolved = info.ObjectAction;                      
                    }
                    else
                    {
                        info.AmountInvolved = "0";
                    }
                    string GuID = Guid.NewGuid().ToString();
                    LD_FwApi.Models.Fw_LawInfo fW_Law = new LD_FwApi.Models.Fw_LawInfo()
                    {
                        ID = GuID,
                        Creator = info.Creator,
                        CreateDate = DateTime.Now,
                        Department = info.Department,
                        ObjectAction = info.ObjectAction,

                        LawsuitType = info.LawsuitType,
                        LawName = info.LawName,
                        CaseNo = info.CaseNo,
                        Grade = info.Grade,
                        AmountInvolved=Convert.ToDecimal(info.AmountInvolved),
                        LawType = info.LawType,

                        FilingDate = !string.IsNullOrWhiteSpace(info.FilingDate.ToString()) ? Convert.ToDateTime(info.FilingDate.ToString()) : (DateTime?)null,
                        Plaintiff = info.Plaintiff != null ? info.Plaintiff : "",
                        Defendant = info.Defendant != null ? info.Defendant : "",
                        TheThird = info.TheThird != null ? info.TheThird : "",
                        Describe = info.Describe != null ? info.Describe : "",
                        Claims = info.Claims != null ? info.Claims : "",
                        Solutions = info.Solutions != null ? info.Solutions : "",
                        VentureFenxi = info.VentureFenxi != null ? info.VentureFenxi : "",
                        Prediction = info.Prediction != null ? info.Prediction : "",
                        ApproveStatus = 1

                    };


                    int i = fW.Insert("FW_LawInfo", fW_Law);

                    LD_FwApi.Models.FollowUp foll = new LD_FwApi.Models.FollowUp()
                    {
                        ID = Guid.NewGuid().ToString(),
                        LID = GuID,
                        Creator = info.Creator,
                        CreateDate = DateTime.Now,
                        FollowUpDate = DateTime.Now,
                        PersonFollowUp = info.Creator,
                        LawType = "案件申请跟进"

                    };
                    int k = fW.Insert("FW_FollowUp", foll);

                    if (i > 0)
                    {
                        var data = new
                        {
                            errcode = 0,

                            errmsg = "ok"
                        };
                        return Content(data.ToJson());
                    }
                    else
                    {
                        errmsg = "insert入库出错!";
                        dicResult.Add("errmsg", errmsg);
                        dicResult.Add("errcode", "-1");
                        return Content(dicResult.ToJson());
                    }



                }
                else
                {
                    errmsg = "数据源为空!";
                    dicResult.Add("errmsg", errmsg);
                    dicResult.Add("errcode", "-1");
                    return Content(dicResult.ToJson());
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.ToString();
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "-1");
                return Content(dicResult.ToJson());
                throw;
            }
        }

        /// <summary>
        /// 任务跟进接口
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TaskAdd(Fllow info)
        {
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            string errmsg = string.Empty;
            try
            {
                FwDataInfo fW = new FwDataInfo();

                if (info != null)
                {
                    LD_FwApi.Models.FollowUp fW_Law = new LD_FwApi.Models.FollowUp()
                    {
                        ID = Guid.NewGuid().ToString(),
                        LID = info.LID,
                        Creator = info.Creator,
                        CreateDate = !string.IsNullOrWhiteSpace(info.CreateDate.ToString()) ? Convert.ToDateTime(info.CreateDate.ToString()) : (DateTime?)null,
                        FollowUpDate = !string.IsNullOrWhiteSpace(info.FollowUpDate.ToString()) ? Convert.ToDateTime(info.FollowUpDate.ToString()) : (DateTime?)null,
                        PersonFollowUp = info.PersonFollowUp,
                        PersonFollowUpID = info.PersonFollowUpID,
                        LawType = "任务事项跟进",
                        TaskFwwdback = info.TaskFwwdback,
                        FollowUpInfo = info.FollowUpInfo,
                        TaskEndTime = info.TaskEndTime
                    };
                    int i = fW.Insert("FW_FollowUp", fW_Law);
                    if (i > 0)
                    {
                        var data = new
                        {
                            errcode = 0,

                            errmsg = "ok"
                        };
                        return Content(data.ToJson());
                    }
                    else
                    {
                        errmsg = "insert入库出错!";
                        dicResult.Add("errmsg", errmsg);
                        dicResult.Add("errcode", "-1");
                        return Content(dicResult.ToJson());
                    }
                }
                else
                {
                    errmsg = "数据源为空!";
                    dicResult.Add("errmsg", errmsg);
                    dicResult.Add("errcode", "-1");
                    return Content(dicResult.ToJson());
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.ToString();
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "-1");
                return Content(dicResult.ToJson());
                throw;
            }
        }


        /// <summary>
        /// 案件结案
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EndLaw(Root info)
        {
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            string errmsg = string.Empty;
            string sql = string.Empty;
            try
            {
                FwDataInfo fW = new FwDataInfo();

                if (info != null)
                {
                    // string GuID = Guid.NewGuid().ToString();
                    End fW_Law = new End()
                    {
                        ID = info.ID,

                        ClosingDate = !string.IsNullOrWhiteSpace(info.ClosingDate.ToString()) ? Convert.ToDateTime(info.ClosingDate.ToString()) : (DateTime?)null,
                        ModifyDate = !string.IsNullOrWhiteSpace(info.ModifyDate.ToString()) ? Convert.ToDateTime(info.ModifyDate.ToString()) : (DateTime?)null,
                        SettleType = info.SettleType,
                        SettleResult = info.SettleResult
                    };

                    int i = fW.Update("FW_LawInfo", fW_Law);

                    LD_FwApi.Models.FollowUp foll = new LD_FwApi.Models.FollowUp()
                    {
                        ID = Guid.NewGuid().ToString(),
                        LID = info.ID,
                        Creator = info.Creator,
                        CreateDate = DateTime.Now,
                        FollowUpDate = DateTime.Now,
                        PersonFollowUp = info.Creator,
                        FollowUpInfo = "案件结案流程审批结束",
                        FUStatus = "结案",
                        LawType = "案件结案跟进"

                    };
                    int k = fW.Insert("FW_FollowUp", foll);

                    if (i > 0)
                    {
                        var data = new
                        {
                            errcode = 0,

                            errmsg = "ok"
                        };
                        return Content(data.ToJson());
                    }
                    else
                    {
                        errmsg = "insert入库出错!";
                        dicResult.Add("errmsg", errmsg);
                        dicResult.Add("errcode", "-1");
                        return Content(dicResult.ToJson());
                    }



                }
                else
                {
                    errmsg = "数据源为空!";
                    dicResult.Add("errmsg", errmsg);
                    dicResult.Add("errcode", "-1");
                    return Content(dicResult.ToJson());
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.ToString();
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "-1");
                return Content(dicResult.ToJson());
                throw;
            }
        }
        /// <summary>
        /// 用印接口 发起/驳回/作废
        /// </summary>
        /// <param name="id"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SealCreate(Root info, string type)
        {
            FwDataInfo fW = new FwDataInfo();
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            string errmsg = string.Empty;
            string sql = string.Empty;
            try
            {
                if (info != null && !string.IsNullOrEmpty(type))
                {
                    switch (type)
                    {
                        case "create":
                            dicResult = Cretatseal(info, type);
                            break;
                        case "update":
                            dicResult = AuitSeal(info, type);
                            break;

                        case "outopen":
                            dicResult = Outseal(info, type);
                            break;
                        case "tongguo":
                            dicResult = Tongguo(info, type);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    errmsg = "有必填项参数为空!";
                    dicResult.Add("errmsg", errmsg);
                    dicResult.Add("errcode", "-1");
                }
                return Content(dicResult.ToJson());

            }
            catch (Exception ex)
            {
                errmsg = ex.ToString();
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "-1");
                return Content(dicResult.ToJson());
                throw;
            }
        }


        public Dictionary<string, object> Cretatseal(Root info, string type)
        {
            FwDataInfo fW = new FwDataInfo();
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            string errmsg = string.Empty;
            string sql = string.Empty;
            LD_FwApi.Models.FollowUp foll = new LD_FwApi.Models.FollowUp()
            {
                ID = info.ID,
                LID = info.FID,
                Creator = info.Creator,
                CreateDate = DateTime.Now,
                FollowUpDate = DateTime.Now,
                PersonFollowUp = info.Creator,
                FollowUpInfo = "----时间:" + DateTime.Now.ToShortDateString() + "状态:法务用印跟进流程发起,流程单号" + info.ID + "----",
                FUStatus = "流程发起",
                LawType = "法务用印跟进"

            };
            int k = fW.Insert("FW_FollowUp", foll);

            if (k > 0)
            {
                errmsg = "操作成功!";
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "0");
                return dicResult;
            }
            else
            {
                errmsg = "insert入库出错!";
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "-1");
                return dicResult;
            };
        }

        public Dictionary<string, object> AuitSeal(Root info, string type)
        {
            FwDataInfo fW = new FwDataInfo();
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            string errmsg = string.Empty;
            string sql = string.Empty;
            LD_FwApi.Models.FollowUp foll = new LD_FwApi.Models.FollowUp()
            {
                ID = info.ID,
                LID = info.FID,
                Creator = info.Creator,
                CreateDate = DateTime.Now,
                FollowUpDate = DateTime.Now,
                PersonFollowUp = info.Creator,
                FollowUpInfo = "---时间:" + DateTime.Now.ToShortDateString() + "状态:法务用印流程驳回---",
                FUStatus = "流程驳回",
                LawType = "法务用印跟进"

            };
            sql = $"UPDATE FW_FollowUp set FUStatus='{"流程驳回"}',FollowUpInfo=CONCAT('{foll.FollowUpInfo}',FollowUpInfo) where ID='{foll.ID}'";
            int k = fW.UpdateFoll(sql);

            if (k > 0)
            {
                errmsg = "操作成功!";
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "0");
                return dicResult;
            }
            else
            {
                errmsg = "insert入库出错!";
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "-1");
                return dicResult;
            };
        }

        public Dictionary<string, object> Outseal(Root info, string type)
        {
            FwDataInfo fW = new FwDataInfo();
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            string errmsg = string.Empty;
            string sql = string.Empty;
            LD_FwApi.Models.FollowUp foll = new LD_FwApi.Models.FollowUp()
            {
                ID = info.ID,
                LID = info.FID,
                Creator = info.Creator,
                CreateDate = DateTime.Now,
                FollowUpDate = DateTime.Now,
                PersonFollowUp = info.Creator,
                FollowUpInfo = "---时间:" + DateTime.Now.ToShortDateString() + "状态:法务用印流程作废---",
                FUStatus = "流程作废",
                LawType = "法务用印跟进"
            };
            sql = $"UPDATE FW_FollowUp set FUStatus='{"流程作废"}',FollowUpInfo=CONCAT('{foll.FollowUpInfo}',FollowUpInfo) where ID='{foll.ID}'";
            int k = fW.UpdateFoll(sql);

            if (k > 0)
            {
                errmsg = "操作成功!";
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "0");
                return dicResult;
            }
            else
            {
                errmsg = "insert入库出错!";
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "-1");
                return dicResult;
            };
        }

        public Dictionary<string, object> Tongguo(Root info, string type)
        {
            FwDataInfo fW = new FwDataInfo();
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            string errmsg = string.Empty;
            string sql = string.Empty;
            LD_FwApi.Models.FollowUp foll = new LD_FwApi.Models.FollowUp()
            {
                ID = info.ID,
                LID = info.FID,
                Creator = info.Creator,
                CreateDate = DateTime.Now,
                FollowUpDate = DateTime.Now,
                PersonFollowUp = info.Creator,
                FollowUpInfo = "---时间:" + DateTime.Now.ToShortDateString() + "状态:法务用印流程通过---",
                FUStatus = "审批完成",
                LawType = "法务用印跟进"
            };
            sql = $"UPDATE FW_FollowUp set FUStatus='{"流程通过"}',FollowUpInfo=CONCAT('{foll.FollowUpInfo}',FollowUpInfo) where ID='{foll.ID}'";
            int k = fW.UpdateFoll(sql);

            if (k > 0)
            {
                errmsg = "操作成功!";
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "0");
                return dicResult;
            }
            else
            {
                errmsg = "insert入库出错!";
                dicResult.Add("errmsg", errmsg);
                dicResult.Add("errcode", "-1");
                return dicResult;
            };
        }

        public static string NoHTML(string Htmlstring)
        {
            //删除脚本
           

            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
            RegexOptions.IgnoreCase);
            //删除HTML 
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"–>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!–.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = Regex.Replace(Htmlstring, @"/\\S*\<\/span\>/g", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"<[^>]*>", "", RegexOptions.IgnoreCase);
            return Htmlstring;
        }


        public class Root
        {
            /// <summary>
            /// ID
            /// </summary>
            public string ID { get; set; }

            /// <summary>
            /// 结案日期
            /// </summary>
            public DateTime ClosingDate { get; set; }

            public DateTime ModifyDate { get; set; }

            /// <summary>
            /// 结案类型
            /// </summary>
            public string SettleType { get; set; }

            /// <summary>
            /// 结案处理结果
            /// </summary>
            public string SettleResult { get; set; }
            /// <summary>
            /// 发起人
            /// </summary>
            public string Creator { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CreateDate { get; set; }
            /// <summary>
            /// 事业部
            /// </summary>
            public string Department { get; set; }
            /// <summary>
            /// 诉讼标的金额
            /// </summary>
            public string ObjectAction { get; set; }
            /// <summary>
            /// 涉案本金
            /// </summary>
            public string AmountInvolved { get; set; }
            /// <summary>
            /// 诉讼类型
            /// </summary>
            public string LawsuitType { get; set; }
            /// <summary>
            /// 诉讼类型id
            /// </summary>
            public string LawsuitTypeID { get; set; }
            /// <summary>
            /// 案件名称
            /// </summary>
            public string LawName { get; set; }
            /// <summary>
            /// 案号
            /// </summary>
            public string CaseNo { get; set; }
            /// <summary>
            /// 案件等级
            /// </summary>
            public string Grade { get; set; }
            /// <summary>
            /// 案件等级id
            /// </summary>
            public string GradeID { get; set; }
            /// <summary>
            /// 诉讼类型
            /// </summary>
            public string LawType { get; set; }
            /// <summary>
            /// 诉讼类型id
            /// </summary>
            public string LawTypeID { get; set; }
            /// <summary>
            /// 立案时间
            /// </summary>
            public string FilingDate { get; set; }
            /// <summary>
            /// 原告
            /// </summary>
            public string Plaintiff { get; set; }
            /// <summary>
            /// 被告
            /// </summary>
            public string Defendant { get; set; }
            /// <summary>
            /// 第三人
            /// </summary>
            public string TheThird { get; set; }
            /// <summary>
            /// 案情简介
            /// </summary>
            public string Describe { get; set; }
            /// <summary>
            /// 诉讼期望
            /// </summary>
            public string Claims { get; set; }
            /// <summary>
            /// 解决方案
            /// </summary>
            public string Solutions { get; set; }
            /// <summary>
            /// 风险分析
            /// </summary>
            public string VentureFenxi { get; set; }
            /// <summary>
            /// 结果预测
            /// </summary>
            public string Prediction { get; set; }

            /// <summary>
            /// 案件跟进id
            /// </summary>
            public string FID { get; set; }

            /// <summary>
            /// 流程单号
            /// </summary>
            public string PID { get; set; }
        }


        public class Fllow
        {
            public int rowall { get; set; }
            public int rowid { get; set; }
            public string ID { get; set; }
            public string LID { get; set; }
            public DateTime? FollowUpDate { get; set; }
            public string PersonFollowUpID { get; set; }
            public string PersonFollowUp { get; set; }
            public string FollowUpInfo { get; set; }
            public DateTime? NextDate { get; set; }
            public string FUStatusID { get; set; }
            public string FUStatus { get; set; }
            public DateTime? ModifyDate { get; set; }
            public string CreatorID { get; set; }
            public string LogInfoID { get; set; }
            public string LawType { get; set; }
            public DateTime? CreateDate { get; set; }
            public string Creator { get; set; }
            public int? SortID { get; set; }
            public int? ISValid { get; set; }
            public string Solutions { get; set; }
            public string PersonLiable { get; set; }
            public string NextMassage { get; set; }
            public string TrialState { get; set; }
            public string TaskPerson { get; set; }
            public string TaskPersonID { get; set; }

            public string YewuPerson { get; set; }
            public string YewuPersonID { get; set; }
            public DateTime? TaskTime { get; set; }
            public DateTime? TaskEndTime { get; set; }
            public string TaskAsk { get; set; }
            public string TaskFwwdback { get; set; }
        }

        public class End
        {
            public string ID
            {
                get; set;
            }

            public DateTime? ClosingDate { get; set; }

            public DateTime? ModifyDate { get; set; }

            public string SettleType { get; set; }
            public string SettleResult { get; set; }

        }
    }
}