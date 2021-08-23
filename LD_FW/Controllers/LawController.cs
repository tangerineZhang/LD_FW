using LDFW.DAL;
using LDFW.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using DAL;
using static LDFW.DAL.FwDataInfo;
using System.Configuration;
using System.Xml;
using LDFW.Common;

using LD.Service;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Text.RegularExpressions;
using WebUtility;

namespace LD_FW.Controllers
{
    public class LawController : Controller
    {
        public static string SSOUrl = ConfigurationManager.AppSettings["SSOUrl"];
        FwDataInfo fwData = new FwDataInfo();
        //
        // GET: /Law/
        #region 单点登录
        public void SSH()
        {
            if (string.IsNullOrEmpty(Request["SysTokenResponse"]))   //主数据的单点登录
            {
                LDFW.Common.ResModel resp = GetSysRequestToken();
                if (resp.StatusCode == "200")
                {
                    string ssoLogInUrl = AuthLoginConfig.AuthSSO_SSOLoginUrl;
                    ssoLogInUrl += "?SysTokenRequest=" + resp.DATA;//通行证
                    Response.Redirect(ssoLogInUrl);
                }
            }
            else
            {
                string userId = GetUserInfoByToken(Request["SysTokenResponse"].ToString());
                if (!string.IsNullOrEmpty(userId))
                {
                    try
                    {
                        HttpContext.Session["userID"] = userId;

                    }
                    catch (Exception ex) { }
                }
            }
        }

        private string GetUserInfoByToken(string sysTokenResponse)
        {
            Auth client = new Auth();
            UserValidationSoapHeader header = new UserValidationSoapHeader();
            header.UserName = AuthLoginConfig.AuthSSO_HeaderUserName;
            header.PassWord = AuthLoginConfig.AuthSSO_HeaderPassword;
            LDFW.Common.ResModel res = JsonConvert.DeserializeObject<LDFW.Common.ResModel>(client.GetUserInfoByToken(sysTokenResponse));

            if (res != null && res.StatusCode == "200")
            {
                AuthUserInfo userInfo = JsonConvert.DeserializeObject<AuthUserInfo>(res.DATA);
                if (userInfo != null)
                {

                    HttpContext.Session["userID"] = userInfo.USERID;

                    return userInfo.USERID;
                }
            }
            return string.Empty;
        }
        public class JsonModel
        {
            public string UserID { get; set; }
        }

        /// <summary>
        /// 主数据的单点登录
        /// </summary>
        private LDFW.Common.ResModel GetSysRequestToken()
        {
            Auth client = new Auth();
            UserValidationSoapHeader header = new UserValidationSoapHeader();
            header.UserName = AuthLoginConfig.AuthSSO_HeaderUserName;
            header.PassWord = AuthLoginConfig.AuthSSO_HeaderPassword;
            string appId = AuthLoginConfig.AuthSSO_AppID;
            string secretKey = AuthLoginConfig.AuthSSO_SecretKey;
            string receiveTokenUrl = AuthLoginConfig.AuthSSO_ReceiveTokenUrl;
            string sysTokenRequest_Json = client.GetSysRequestToken(appId, receiveTokenUrl, secretKey);
            LDFW.Common.ResModel res = JsonConvert.DeserializeObject<LDFW.Common.ResModel>(sysTokenRequest_Json);
            return res;
        }

        //退出
        [HttpPost]
        public string Logout()
        {
            LDFW.Common.ResModel res = GetSysRequestToken();
            if (res.StatusCode.Equals("200"))//判断是否获取通行证（状态码 200-成功 500-失败）
            {
                //清除某个Session
                Session["userID"] = null;
                Session.Remove("userID");
                //请求退出
                string ssoLogInUrl = AuthLoginConfig.WebServiceBaseUrl;
                //  ssoLogInUrl += "?SysTokenRequest=" + resp.DATA;//通行证
                string ssoLogOutUrl = ssoLogInUrl + "AdminMain/Logout?SysTokenRequest=" + res.DATA;//通行证;//请求退出地址

                return ssoLogOutUrl;
            }
            else
            {
                string Msg = res.MSG;//消息 失败时会有错误信息
                return Msg.ToString();
            }
        }
        #endregion
        public ActionResult Lodding()
        {
            return View();
        }
        //初始页
        public ActionResult Index()
        {

            SSH();
            string name = "";
            string lastRole = string.Empty;
            string detname = string.Empty;
            string detname1 = string.Empty;
            string lastRoleon = string.Empty;

            if (Session["UserID"] != null)
            {
                name = Session["UserID"].ToString();
                DataTable dt = fwData.SelectPeopername(name);//查询当前用户名字
                DataTable dtdep = fwData.SelectSYB(name);//查询用户所在事业部
                for (int i = 0; i < dtdep.Rows.Count; i++)
                {
                    detname1 += "'" + dtdep.Rows[i]["OrgUnitName"] + "',";
                    detname += dtdep.Rows[i]["OrgUnitName"] + ",";
                }
                Session["DemName"] = detname.Substring(0, detname.Length - 1);
                Session["DEmName1"] = detname1.Substring(0, detname1.Length - 1);
                string depid = dt.Rows[0]["UserID"].ToString();
                DataTable dtRole = fwData.SelectRole(depid);//查询权限
                if (dtRole.Rows.Count > 0)
                {
                    DataRow[] Rolesname = null;
                    DataRow[] Rolesname1 = null;
                    DataRow[] Rolesname2 = null;
                    DataRow[] Rolesname3 = null;
                    DataRow[] Rolesname4 = null;
                    DataRow[] ht = null;
                    Rolesname = dtRole.Select("ModuleID='Law'");            //系统权限
                    Rolesname1 = dtRole.Select("ModuleID='LawList'");       //功能权限:诉讼列表 
                    ht = dtRole.Select("ModuleID='LawList'");
                    Rolesname2 = dtRole.Select("ModuleID='SelectLaw'");     //功能权限:案件详情流程按钮
                    Rolesname3 = dtRole.Select("ModuleID='OneData'");       //功能权限:个人/公司库
                    Rolesname4 = dtRole.Select("ModuleID='LawExcel'");      //功能权限:报表

                    if (Rolesname != null)
                    {
                        ViewData["Role1"] = "R";
                        if (Rolesname1.Length > 0)
                        {
                            Rolesname1.OrderBy(x => x["F1"]).ToArray();//查询绿都包含的
                            ht.OrderBy(x => x["RoleType"]).ToArray();//查询汇通包含的(全部 1 所属事业部2 无权限 3)
                            lastRole = Rolesname1[0]["F1"].ToString();  //绿都最小的值 全部 1 所属事业部2 无权限 3
                            lastRoleon = ht[0]["RoleType"].ToString();  //汇通最小的值全部 1 所属事业部2 无权限 3
                            for (int i = 0; i < Rolesname1.Length; i++)
                            {
                                Session["Role2"] += Rolesname1[i]["RightsCode"].ToString();//绿都的权限

                            }
                        }
                        else
                        {
                            Session["Role2"] = "0";
                        }

                        if (Rolesname2.Length > 0)
                        {
                            for (int i = 0; i < Rolesname2.Length; i++)
                            {
                                Session["Role3"] += Rolesname2[i]["RightsCode"].ToString();
                            }
                        }
                        else
                        {
                            Session["Role3"] = "0";
                        }

                        if (Rolesname3.Length > 0)
                        {
                            for (int i = 0; i < Rolesname3.Length; i++)
                            {
                                Session["Role4"] += Rolesname3[i]["RightsCode"].ToString();
                            }
                        }
                        else
                        {
                            Session["Role4"] = "0";
                        }

                        if (Rolesname4.Length > 0)
                        {
                            for (int i = 0; i < Rolesname4.Length; i++)
                            {
                                Session["Role5"] += Rolesname4[i]["RightsCode"].ToString();
                            }
                        }
                        else
                        {
                            Session["Role5"] = "0";
                        }

                    }
                    else
                    {
                        return View("EndView");
                    }
                    Session["create"] = dt.Rows[0]["UserName"];        //登录人姓名
                    ViewData["UserName"] = dt.Rows[0]["UserName"];
                }
                else
                {
                    return View("EndView");
                }

                //  ViewData["RoleID"] = lastRole;
            }

            if (Session["Role2"] != null)
            {
                ViewData["Role2"] = Session["Role2"].ToString();
            }
            else
            {
                ViewData["Role2"] = "R";
            }
            if (Session["Role4"] != null)
            {
                ViewData["Role4"] = Session["Role4"].ToString();
            }
            else
            {
                ViewData["Role4"] = "R";
            }
            if (Session["Role5"] != null)
            {
                ViewData["Role5"] = Session["Role5"].ToString();
            }
            else
            {
                ViewData["Role5"] = "R";
            }


            Session["createid"] = name;                         //登录人id

            Session["RoleID"] = lastRole;                       //绿都标识
            Session["RoleIDon"] = lastRoleon;                   //汇通标识


            return View();
        }

        public void Help(string UserId)
        {
            string name = "";
            string lastRole = string.Empty;
            string detname = string.Empty;
            string detname1 = string.Empty;
            string lastRoleon = string.Empty;
            name = UserId;
            DataTable dt = fwData.SelectPeopername(name);//查询当前用户名字
            DataTable dtdep = fwData.SelectSYB(name);//查询事业部
            for (int i = 0; i < dtdep.Rows.Count; i++)
            {
                detname1 += "'" + dtdep.Rows[i]["OrgUnitName"] + "',";
                detname += dtdep.Rows[i]["OrgUnitName"] + ",";
            }
            string depid = dt.Rows[0]["UserID"].ToString();
            DataTable dtRole = fwData.SelectRole(depid);//查询权限
            if (dtRole.Rows.Count > 0)
            {
                DataRow[] Rolesname = null;
                DataRow[] Rolesname1 = null;
                DataRow[] Rolesname2 = null;
                DataRow[] Rolesname3 = null;
                DataRow[] Rolesname4 = null;
                DataRow[] ht = null;
                Rolesname = dtRole.Select("ModuleID='Law'");            //系统权限
                Rolesname1 = dtRole.Select("ModuleID='LawList'");       //功能权限:诉讼列表 
                ht = dtRole.Select("ModuleID='LawList'");
                Rolesname2 = dtRole.Select("ModuleID='SelectLaw'");     //功能权限:案件详情流程按钮
                Rolesname3 = dtRole.Select("ModuleID='OneData'");       //功能权限:个人/公司库
                Rolesname4 = dtRole.Select("ModuleID='LawExcel'");      //功能权限:报表

                if (Rolesname != null)
                {
                    ViewData["Role1"] = "R";
                    if (Rolesname1.Length > 0)
                    {
                        Rolesname1.OrderBy(x => x["F1"]).ToArray();//查询绿都包含的
                        ht.OrderBy(x => x["RoleType"]).ToArray();//查询汇通包含的(全部 1 所属事业部2 无权限 0)
                        lastRole = Rolesname1[0]["F1"].ToString();
                        lastRoleon = ht[0]["RoleType"].ToString();
                        for (int i = 0; i < Rolesname1.Length; i++)
                        {
                            Session["Role2"] += Rolesname1[i]["RightsCode"].ToString();

                        }
                    }
                    else
                    {
                        Session["Role2"] = "0";
                    }

                    if (Rolesname2.Length > 0)
                    {
                        for (int i = 0; i < Rolesname2.Length; i++)
                        {
                            Session["Role3"] += Rolesname2[i]["RightsCode"].ToString();
                        }
                    }
                    else
                    {
                        Session["Role3"] = "0";
                    }

                    if (Rolesname3.Length > 0)
                    {
                        for (int i = 0; i < Rolesname3.Length; i++)
                        {
                            Session["Role4"] += Rolesname3[i]["RightsCode"].ToString();
                        }
                    }
                    else
                    {
                        Session["Role4"] = "0";
                    }

                    if (Rolesname4.Length > 0)
                    {
                        for (int i = 0; i < Rolesname4.Length; i++)
                        {
                            Session["Role5"] += Rolesname4[i]["RightsCode"].ToString();
                        }
                    }
                    else
                    {
                        Session["Role5"] = "0";
                    }

                }

            }

        }

        #region 案件信息

      
        //错误页面
        public ActionResult EndView()
        {

            return View();
        }
        //显示案件方法
        public ActionResult SelectLaw(int page, int limit, string Role)
        {
            string Roleid = Session["RoleID"].ToString();
            string Roleidon = Session["RoleIDon"].ToString();
            string Depmnamne = Session["DemName"].ToString();


            if (Roleid == "1")
            {
                DataTable dtL = fwData.SelectLvdu();
                string det = string.Empty;
                for (int i = 0; i < dtL.Rows.Count; i++)
                {
                    det += dtL.Rows[i]["Name"] + ",";
                }
                Role = det;
            }
            else if (Roleid == "2")
            {
                Role = Depmnamne + ",";
            }
            else
            {
                Role = "";
            }
            if (Roleidon == "1")
            {
                DataTable dtL = fwData.SelectHT();
                string det = string.Empty;
                for (int i = 0; i < dtL.Rows.Count; i++)
                {
                    det += dtL.Rows[i]["Name"] + ",";
                }
                Role += det;
            }
            else if (Roleidon == "2")
            {
                Role += Depmnamne;
            }
            else
            {
                Role += "";
            }
            Role = Role.Substring(0, Role.Length - 1);
            DataTable dt = fwData.SelectlawInfo(page, limit, Role);

            List<LDFW.Model.FW_LawInfo> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LawInfo>>(JsonConvert.SerializeObject(dt));
            int PageCount = (list == null || list.Count == 0) ? 0 : list[0].rowall;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }

        public static string Html2Text(string htmlStr)

        {
            if (String.IsNullOrEmpty(htmlStr))
            {
                return "";

            }
            string regEx_style = "<style[^>]*?>[\\s\\S]*?<\\/style>"; //定义style的正则表达式 

            string regEx_script = "<script[^>]*?>[\\s\\S]*?<\\/script>"; //定义script的正则表达式   

            string regEx_html = "<[^>]+>"; //定义HTML标签的正则表达式   

            htmlStr = Regex.Replace(htmlStr, regEx_style, "");//删除css

            htmlStr = Regex.Replace(htmlStr, regEx_script, "");//删除js

            htmlStr = Regex.Replace(htmlStr, regEx_html, "");//删除html标记

            htmlStr = Regex.Replace(htmlStr, "\\s*|\t|\r|\n", "");//去除tab、空格、空行

            htmlStr = htmlStr.Replace(" ", "");
            return htmlStr.Trim();

        }

      

                //查询案件方法
                [HttpGet]
        public ActionResult SelectLawLook(int page, int limit, string tag1, string tag2, string tag3, string tag4, string tag5, string tag6, string tag7, string tag8, string tag9, string tag10, string tag11, string tag13, string tag14,string tag15,string tag16)
        {
            string d = "";

            if (tag2 != "")
            {
                string[] dmt = tag2.Split(',');
                for (int i = 0; i < dmt.Length; i++)
                {
                    d += "'" + dmt[i] + "',";
                }
            }
            string Where = "";
            if (d != "")
            {
                string dement = d.Substring(0, d.Length - 1);
                Where = "and f.Department IN(" + dement + ")";
            }
            else
            {
                string dement = Dempent();
                Where = "and f.Department IN(" + dement + ")";
            }
            if (tag1 != "" && tag1 != null)
            {
                Where += "and f.TrialState='" + tag1 + "'";
            }
            if (tag3 != "" && tag3 != null)
            {
                Where += "and f.LawType='" + tag3 + "'";
            }
            if (tag4 != "" && tag4 != null)
            {
                Where += "and f.Grade='" + tag4 + "'";
            }
            if (tag5 != "" && tag5 != null)
            {
                Where += "and f.FilingDate>='" + tag5 + "'";
            }
            if (tag6 != "" && tag6 != null)
            {
                Where += "and f.FilingDate<='" + tag6 + "'";
            }
            if (tag7 != "" && tag7 != null)
            {
                Where += "and (f.CaseNo like '%" + tag7 + "%' or f.LawName like  '%" + tag7 + "%')";
            }
            if (tag8 != "" && tag8 != null)
            {
                Where += "and f.LawsuitType like '%" + tag8 + "%'";
            }
            if (tag9 != "" && tag9 != null)
            {
                Where += "and  f.LawFirm like '%" + tag9 + "%'";
            }
            if (tag10 != "" && tag10 != null)
            {
                Where += "and f.Lawyer like '%" + tag10 + "%'";
            }
            if (tag11 != "" && tag11 != null)
            {
                Where += "and f.TrialState like '%" + tag11 + "%'";
            }

            if (tag13 != "" && tag13 != null)
            {
                Where += "and f.ClosingDate>='" + tag13 + "'";

            }
            if (tag14 != "" && tag14 != null)
            {
                Where += "and f.ClosingDate<='" + tag14 + "'";

            }
            if (tag15=="在办")
            {
                Where += "and f.LawStatus <> '结案'";
            }
            else if(tag15 == "结案")
            {
                Where += "and f.LawStatus = '结案'";
            }
            else
            {

            }
            if (tag16!=null && tag16!="")
            {
                string[] s = tag16.Split('|');
                Where += "and f.LawUserNameID = '"+ s[1] + "'";
            }

            //  DataTable dt = fwData.SelectLawInfoList(page, limit, tag1, dement, tag3);
            string TableName = "FW_LawInfo";
            int pageindex = page;
            int PageSize = limit;
            DataTable dt = fwData.GetPaginaList(TableName, pageindex, PageSize, Where, out int totalcount);
            List<LDFW.Model.FW_LawInfo> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LawInfo>>(JsonConvert.SerializeObject(dt));
            //  int PageCount = (list == null || list.Count == 0) ? 0 : list[0].rowall;
            int c = (int)Math.Ceiling((decimal)totalcount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = totalcount.ToString(), data = list }, JsonRequestBehavior.AllowGet);

        }

        //导出案件信息
        [HttpGet]
        public FileContentResult Excel(string data)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(data);

            string str = jo["select"].ToString();
            DataTable dt = HelpData(data);
            string path1 = Request.PhysicalApplicationPath;
            string newpath = Path.Combine(path1, "Excel\\诉讼案件台账.xlsx");//相对路径
            string ReportFileName = Server.MapPath("out10.xlsx");
            FileStream stream = new FileStream(newpath, FileMode.Open, FileAccess.Read);//文件流打开
            IWorkbook workbook = new XSSFWorkbook(stream);//NPOI打开excel
            ISheet sheet = workbook.GetSheetAt(0);//获取sheet1 下标为0
            int num = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dtLitig = fwData.SelectLitigation(dt.Rows[i]["ID"].ToString());  //诉讼保全信息
                DataTable dtSeiz = fwData.SelectLaweyer(dt.Rows[i]["ID"].ToString());    //律师费用
                DataTable dtLega = fwData.SelectLegal(dt.Rows[i]["ID"].ToString());    //诉讼费用
                num++;
                IRow row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(num);
                row.CreateCell(1).SetCellValue(dt.Rows[i]["CaseNo"].ToString());       //案件号   
                row.CreateCell(2).SetCellValue(dt.Rows[i]["Grade"].ToString());       //案件等级   
                row.CreateCell(3).SetCellValue(dt.Rows[i]["LawsuitType"].ToString());     //诉讼类型
                row.CreateCell(4).SetCellValue(dt.Rows[i]["PersonLiable"].ToString());  //责任人
                row.CreateCell(5).SetCellValue(dt.Rows[i]["LawFirm"].ToString());       //承办律所   
                row.CreateCell(6).SetCellValue(dt.Rows[i]["Lawyer"].ToString());     //承办人
                row.CreateCell(7).SetCellValue(dt.Rows[i]["LawUserName"].ToString());     //对接法务

                row.CreateCell(9).SetCellValue(dt.Rows[i]["LawName"].ToString());  //案件名称
                row.CreateCell(10).SetCellValue(dt.Rows[i]["Department"].ToString());       //所属事业部   
                row.CreateCell(11).SetCellValue(dt.Rows[i]["LawsuitType"].ToString());     //案件类型
                row.CreateCell(12).SetCellValue(dt.Rows[i]["Plaintiff"].ToString());  //原告
                row.CreateCell(13).SetCellValue(dt.Rows[i]["Defendant"].ToString());       //被告   
                row.CreateCell(14).SetCellValue(dt.Rows[i]["Describe"].ToString());     //案情简介
                row.CreateCell(15).SetCellValue(dt.Rows[i]["Claims"].ToString());  //诉讼请求

                row.CreateCell(16).SetCellValue(dt.Rows[i]["FilingDate"].ToString());  //立案时间
                row.CreateCell(17).SetCellValue(dt.Rows[i]["ClosingDate"].ToString());       //结案时间   
                if (!string.IsNullOrWhiteSpace(dt.Rows[i]["FilingDate"].ToString()) && !string.IsNullOrWhiteSpace(dt.Rows[i]["ClosingDate"].ToString()))
                {
                    row.CreateCell(18).SetCellValue(GetDuration(Convert.ToDateTime(dt.Rows[i]["FilingDate"]), Convert.ToDateTime(dt.Rows[i]["ClosingDate"])));     //结案周期
                }

                row.CreateCell(19).SetCellValue(dt.Rows[i]["CounselFee"].ToString());  //律师费


                row.CreateCell(20).SetCellValue(dt.Rows[i]["ObjectAction"].ToString());       //诉讼标的   
                row.CreateCell(21).SetCellValue(dt.Rows[i]["AmountInvolved"].ToString());     //涉案本金
                row.CreateCell(22).SetCellValue(dt.Rows[i]["InterestMoney"].ToString());  //利息/违约金


                row.CreateCell(23).SetCellValue(dt.Rows[i]["LegalFee"].ToString());  //诉讼费



                row.CreateCell(24).SetCellValue(dt.Rows[i]["Maintenancefee"].ToString());       //保全费 
                if (dtLitig.Rows.Count > 0)
                {


                    for (int k = 0; k < dtLitig.Rows.Count; k++)
                    {
                        string st = "";
                        if (DateTime.Now < Convert.ToDateTime(dtLitig.Rows[k]["LPDate"]))
                        {

                            st += dtLitig.Rows[k]["PInformation"].ToString();

                        }
                        row.CreateCell(25).SetCellValue(st);     //保全情况
                    }
                    row.CreateCell(26).SetCellValue(dtLitig.Rows[0]["LPDate"].ToString());  //保全到期时间
                }


                row.CreateCell(27).SetCellValue(dt.Rows[i]["Judgment"].ToString());       //实际判决/调解金额（元）   
                row.CreateCell(28).SetCellValue(dt.Rows[i]["Compensation"].ToString());     //实际赔付金额（元））
                row.CreateCell(29).SetCellValue(dt.Rows[i]["Impairments"].ToString());  //减损额（元）
                row.CreateCell(30).SetCellValue(dt.Rows[i]["SecurityMoney"].ToString());       //保函担保费   
                row.CreateCell(31).SetCellValue(dt.Rows[i]["ActualPayment"].ToString());     //已回款额（元）

                row.CreateCell(32).SetCellValue(dt.Rows[i]["ResidueFee"].ToString());  //剩余欠款本金（元）
                row.CreateCell(33).SetCellValue(dt.Rows[i]["CollectionRate"].ToString());       //单案件回款率    
                row.CreateCell(34).SetCellValue(dt.Rows[i]["RiskExposure"].ToString());     //风险敞口


                row.CreateCell(40).SetCellValue(dt.Rows[i]["TrialState"].ToString());  //审理阶段
                row.CreateCell(41).SetCellValue(dt.Rows[i]["LawStatus"].ToString());       //当前状态  
                row.CreateCell(42).SetCellValue(dt.Rows[i]["VentureFenxi"].ToString());  //风险
                row.CreateCell(43).SetCellValue(dt.Rows[i]["Solutions"].ToString());       //整体推荐方案  
                DataTable dts = fwData.SelFullpall(dt.Rows[i]["ID"].ToString());
                string strmas = string.Empty;
                if (dts.Rows.Count>0)
                {
                    
                    for (int m = 0; m < dts.Rows.Count; m++)
                    {
                        strmas += dts.Rows[m]["FollowUpDate"].ToString() + "," + dts.Rows[m]["FollowUpInfo"].ToString();
                    }
                    row.CreateCell(44).SetCellValue(strmas);
                }

                row.CreateCell(48).SetCellValue(dt.Rows[i]["IsPremium"].ToString() == "1" ? "是" : "否");     //是否退费
                row.CreateCell(49).SetCellValue(dt.Rows[i]["IsDeblocking"].ToString() == "1" ? "是" : "否");  //是否解封



            }

            byte[] buffer = new byte[1024 * 5];
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.ToArray();
                ms.Close();
            }
            System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
            Response.Clear();

            return File(buffer, "application/ms-excel", DateTime.Now.ToLongDateString().ToString() + "台账报表.xlsx");

        }

        //获取天数
        protected int GetDuration(DateTime start, DateTime finish)
        {
            return (finish - start).Days;
        }

        [HttpPost]
        public DataTable HelpData(string message)
        {
            string d = "";
            LDFW.Model.FW_LawInfo fwinfo = new LDFW.Model.FW_LawInfo();
            JObject jo = (JObject)JsonConvert.DeserializeObject(message);
            string str = jo["select"].ToString();

            fwinfo.ClosingDate = !string.IsNullOrWhiteSpace(jo["FilingDate"].ToString()) ? Convert.ToDateTime(jo["FilingDate"].ToString()) : (DateTime?)null;
            fwinfo.LawsuitType = jo["LawStatustype"].ToString();
            fwinfo.LawStatus = "";
            fwinfo.Grade = jo["LawGrade"].ToString();
            fwinfo.TrialState = jo["TrialState"].ToString();

            DateTime? dateTime = !string.IsNullOrWhiteSpace(jo["FilingDate1"].ToString()) ? Convert.ToDateTime(jo["FilingDate1"].ToString()) : (DateTime?)null;

            if (str != "")
            {
                string[] dmt = str.Split(',');
                for (int i = 0; i < dmt.Length; i++)
                {
                    d += "'" + dmt[i] + "',";
                }
            }
            string Where = "  f.ISValid = 1";
            if (d != "")
            {
                string dement = d.Substring(0, d.Length - 1);
                Where = "and f.Department IN(" + dement + ")";
            }
            if (fwinfo.LawStatus != "" && fwinfo.LawStatus != null)
            {
                Where = "and f.LawStatus like '%" + fwinfo.LawStatus + "%'";
            }
            if (fwinfo.Grade != "" && fwinfo.Grade != null)
            {
                Where += "and f.Grade='" + fwinfo.Grade + "'";
            }
            if (fwinfo.LawsuitType != "" && fwinfo.LawsuitType != null)
            {
                Where += "and f.LawsuitType like '%" + fwinfo.LawsuitType + "%'";
            }
            if (fwinfo.TrialState != "" && fwinfo.TrialState != null)
            {
                Where += "and f.TrialState like '%" + fwinfo.TrialState + "%'";
            }

            if (fwinfo.ClosingDate.ToString() != "" && fwinfo.ClosingDate != null)
            {
                Where += "and f.ClosingDate>='" + fwinfo.ClosingDate + "'";

            }
            if (dateTime.ToString() != "" && dateTime != null)
            {
                Where += "and f.ClosingDate<='" + jo["FilingDate1"] + "'";

            }
            string TableName = "FW_LawInfo";
            int pageindex = 1;
            int PageSize = 100;
            DataTable dt = fwData.GetPaginaList(TableName, pageindex, PageSize, Where, out int totalcount);

            return dt;
        }

        //查询个人/公司信息
        [HttpGet]
        public ActionResult SelectPersonaLook(int page, int limit, string tag1, string tag2, string tag3)
        {
            DataTable dt = fwData.SelectPersonalInfoList(page, limit, tag1, tag2, tag3);
            List<LDFW.Model.FW_PersonalCompany> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_PersonalCompany>>(JsonConvert.SerializeObject(dt));
            int PageCount = (list == null || list.Count == 0) ? 0 : list[0].rowall;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);

        }

        //案件页面
        public ActionResult LawList()
        {
            ViewData["Role2"] = Session["Role2"].ToString();
            string RoleId = Session["RoleID"].ToString();
            ViewData["RoleID"] = RoleId;
            return View();
        }
        //添加案件页面
        public ActionResult AddLawCase()
        {
            return View();
        }
        //添加案件方法
        [HttpPost]
        public int AddLaw(string message, string fW_Litigation, string fw_LegalCosts, string fW_Lawyer, string fw_LawFiles, string types)
        {
            try
            {
                int rCount = 0;
                string errorInfo = "";
                string CreateID = Session["createid"].ToString();
                string Create = Session["Create"].ToString();
                //案件信息
                LDFW.Model.FW_LawInfo fwinfo = new LDFW.Model.FW_LawInfo();
                JObject jo = (JObject)JsonConvert.DeserializeObject(message);
                fwinfo.LawName = jo["LawName"] == null ? "" : jo["LawName"].ToString();
                fwinfo.Grade = jo["Grade"] == null ? "" : jo["Grade"].ToString();
                fwinfo.LawsuitType = jo["LawsuitType"] == null ? "" : jo["LawsuitType"].ToString();
                fwinfo.LawType = jo["LawType"] == null ? "" : jo["LawType"].ToString();
                fwinfo.Department = jo["Department"] == null ? "" : jo["Department"].ToString();
                fwinfo.Plaintiff = jo["Plaintiff"] == null ? "" : jo["Plaintiff"].ToString();
                fwinfo.Defendant = jo["Defendant"] == null ? "" : jo["Defendant"].ToString();
                fwinfo.TheThird = jo["TheThird"] == null ? "" : jo["TheThird"].ToString();
                fwinfo.Court = jo["Court"] == null ? "" : jo["Court"].ToString();
                fwinfo.CaseNo = jo["CaseNo"] == null ? "" : jo["CaseNo"].ToString();
                fwinfo.LawFirm = jo["LawFirm"] == null ? "" : jo["LawFirm"].ToString();
                fwinfo.Llawyer = jo["Llawyer"] == null ? "" : jo["Llawyer"].ToString();

                if (jo["FilingDate"].ToString() == "")
                {
                    fwinfo.FilingDate = null;
                }
                else
                {
                    fwinfo.FilingDate = Convert.ToDateTime(jo["FilingDate"].ToString());
                }

                if (jo["ClosingDate"].ToString() == "")
                {
                    fwinfo.ClosingDate = null;
                }
                else
                {
                    fwinfo.ClosingDate = Convert.ToDateTime(jo["ClosingDate"].ToString());
                }
                // fwinfo.FilingDate = Convert.ToDateTime(jo["FilingDate"])==null? Convert.ToDateTime(""): Convert.ToDateTime(jo["FilingDate"]);
                //  fwinfo.ClosingDate = Convert.ToDateTime(jo["ClosingDate"]) == null ? Convert.ToDateTime("") : Convert.ToDateTime(jo["ClosingDate"]); 
                fwinfo.LawStatus = jo["LawStatus"] == null ? "" : jo["LawStatus"].ToString();
                fwinfo.Describe = jo["Describe"] == null ? "" : jo["Describe"].ToString();
                fwinfo.Claims = jo["Claims"] == null ? "" : jo["Claims"].ToString();
                //fwinfo.AmountInvolved = Convert.ToDecimal(jo["AmountInvolved"].ToString());
                //fwinfo.AmountueDU = Convert.ToDecimal(jo["AmountueDU"].ToString());







                if (jo["AmountInvolved"].ToString() == null && jo["AmountInvolved"].ToString() == "")
                {
                    fwinfo.AmountInvolved = 0;
                }
                else
                {
                    fwinfo.AmountInvolved = Convert.ToDecimal(jo["AmountInvolved"].ToString());
                }

                if (jo["AmountueDU"].ToString() == null && jo["AmountueDU"].ToString() == "")
                {
                    fwinfo.AmountueDU = 0;
                }
                else
                {
                    fwinfo.AmountueDU = Convert.ToDecimal(jo["AmountueDU"].ToString());
                }


                if (jo["SStopLoss"].ToString() == null && jo["SStopLoss"].ToString() == "")
                {
                    fwinfo.SStopLoss = 0;
                }
                else
                {
                    fwinfo.SStopLoss = Convert.ToDecimal(jo["SStopLoss"].ToString());
                }

                if (jo["AStopLoss"].ToString() == null && jo["AStopLoss"].ToString() == "")
                {
                    fwinfo.AStopLoss = 0;
                }
                else
                {
                    fwinfo.AStopLoss = Convert.ToDecimal(jo["AStopLoss"].ToString());
                }

                //fwinfo.Compensation = Convert.ToDecimal(jo["Compensation"].ToString());
                if (jo["Receivables"].ToString() == null && jo["Receivables"].ToString() == "")
                {
                    fwinfo.AStopLoss = 0;
                }
                else
                {
                    fwinfo.Receivables = Convert.ToDecimal(jo["Receivables"].ToString());
                }

                if (jo["ActualPayment"].ToString() == null && jo["ActualPayment"].ToString() == "")
                {
                    fwinfo.AStopLoss = 0;
                }
                else
                {
                    fwinfo.ActualPayment = Convert.ToDecimal(jo["ActualPayment"].ToString());
                }

                if (jo["Judgment"].ToString() == null && jo["Judgment"].ToString() == "")
                {
                    fwinfo.AStopLoss = 0;
                }
                else
                {
                    fwinfo.Judgment = Convert.ToDecimal(jo["Judgment"].ToString());
                }
                //fwinfo.CollectionRate = Convert.ToDecimal(jo["CollectionRate"].ToString());
                if (jo["RiskExposure"].ToString() == null && jo["RiskExposure"].ToString() == "")
                {
                    fwinfo.AStopLoss = 0;
                }
                else
                {
                    fwinfo.RiskExposure = Convert.ToDecimal(jo["RiskExposure"].ToString());
                }




                ////fwinfo.Compensation = Convert.ToDecimal(jo["Compensation"].ToString());
                //fwinfo.SStopLoss = Convert.ToDecimal(jo["SStopLoss"].ToString());
                //fwinfo.AStopLoss = Convert.ToDecimal(jo["AStopLoss"].ToString());
                //fwinfo.Receivables= Convert.ToDecimal(jo["Receivables"].ToString());
                //fwinfo.ActualPayment = Convert.ToDecimal(jo["ActualPayment"].ToString());
                ////fwinfo.CollectionRate = Convert.ToDecimal(jo["CollectionRate"].ToString());
                //fwinfo.Judgment = Convert.ToDecimal(jo["Judgment"].ToString());
                ////fwinfo.StopLossRate = jo["StopLossRate"].ToString();
                //fwinfo.RiskExposure = Convert.ToDecimal(jo["RiskExposure"].ToString());
                fwinfo.IsAssess = jo["IsAssess"].ToString() == "on" ? "1" : "0";
                fwinfo.Solutions = jo["Solutions"] == null ? "" : jo["Solutions"].ToString();
                fwinfo.PersonLiable = jo["PersonLiable"] == null ? "" : jo["PersonLiable"].ToString();
                fwinfo.PersonFollowUp = jo["PersonFollowUp"] == null ? "" : jo["PersonFollowUp"].ToString();
                fwinfo.FollowUpRecord = jo["FollowUpRecord"] == null ? "" : jo["FollowUpRecord"].ToString();

                if (jo["NextDate"].ToString() == "")
                {
                    fwinfo.NextDate = null;
                }
                else
                {
                    fwinfo.NextDate = Convert.ToDateTime(jo["NextDate"].ToString());
                }
                if (jo["FollowUpDate"].ToString() == "")
                {
                    fwinfo.FollowUpDate = null;
                }
                else
                {
                    fwinfo.FollowUpDate = Convert.ToDateTime(jo["FollowUpDate"].ToString());
                }


                //fwinfo.NextDate = Convert.ToDateTime(jo["NextDate"].ToString())==null?Convert.ToDateTime(null): Convert.ToDateTime(jo["NextDate"].ToString());
                //  fwinfo.FollowUpDate = jo["FollowUpDate"].ToString()==""?null: Convert.ToDateTime(jo["FollowUpDate"].ToString());
                fwinfo.FUStatus = jo["FUStatus"] == null ? "" : jo["FUStatus"].ToString();

                //诉讼保全
                List<LDFW.Model.FW_LitigationPreservation> fW_Litigations = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LitigationPreservation>>(fW_Litigation);
                //查封信息
                //List<LDFW.Model.FW_Seizure> fW_Seizure = JsonConvert.DeserializeObject<List<LDFW.Model.FW_Seizure>>(fw_Seizure);
                //诉讼费用
                List<LDFW.Model.FW_LegalCosts> fW_LegalCost = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LegalCosts>>(fw_LegalCosts);
                //律师费用
                List<LDFW.Model.FW_LawyerCosts> fW_Lawyers = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LawyerCosts>>(fW_Lawyer);
                //附件信息
                List<LDFW.Model.FW_LawFiles> fw_LawFile = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LawFiles>>(fw_LawFiles);

                int i = fwData.TransactionCreateLawAdd(fwinfo, fW_Litigations, fW_LegalCost, fW_Lawyers, fw_LawFile, types, CreateID, Create, ref rCount, ref errorInfo);
                return i;
            }
            catch (Exception ex)
            {
                return 5;

            }

        }

        //上传多文件
        [HttpPost]
        public ActionResult uploadfile()
        {
            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                try
                {
                    //string fileID = Guid.NewGuid().ToString();

                    //得到客户端上传的文件
                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                    int FileSize = file.ContentLength / 1014;
                    string FileFormat = Path.GetExtension(file.FileName).ToLower();
                    // string FileFormat = file.FileName.Substring(0, file.FileName.Count() - Path.GetExtension(file.FileName).ToLower().Length);
                    string Name = file.FileName;
                    //服务器端要保存的路径
                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Filedata/") + Name;// +".txt";
                    file.SaveAs(filePath);
                    string FilePath = "/Filedata/" + file.FileName;
                    //返回结果
                    //List<FIleAdd> fi = new List<FIleAdd>();
                    //fi.Add(new FIleAdd { fileID = fileID, fileFileName = file.FileName, fileurl = fileurl, extName = extName, Wname = Wname });

                    return Json(new { res = "success", file.FileName, FilePath, FileFormat, FileSize });
                }
                catch (Exception ex)
                {
                    return Json(new { res = ex.Message });
                }
            }
            else
            {
                return Json(new { res = "error" });
                //Response.Write("Error1");
            }
        }
        //删除文件
        public int DeleteFile(string ID)
        {
            int i = fwData.FileDelete(ID);
            return i;
        }
        //修改案件页面
        public ActionResult UpdateLaw(string ID)
        {

            DataTable dt = fwData.SelectlawInfoKll(ID);    //案件信息  SelectLawFirm
            DataTable dtLitig = fwData.SelectLitigation(ID);  //诉讼保全信息
            DataTable dtSeiz = fwData.SelectLaweyer(ID);    //律师费用
            DataTable dtLega = fwData.SelectLegal(ID);    //诉讼费用
            DataTable dtFile = fwData.SelectFiles(ID);    //附件信息
            ViewData["dtFiles"] = dtFile;
            ViewData["dtLega"] = dtLega;
            ViewData["dtLitig"] = dtLitig;
            ViewData["dt"] = dt;
            ViewData["dtSeiz"] = dtSeiz;
            Session["LIDs"] = ID;
            return View();
        }

        //修改保全信息
        public string Updatetime(string ID, string time,string type,string userid,string porid)
        {
            try
            {
            string appId = "FWXX";
            string appSecret = "28A7AD47-E569-4935-999A-1018E1B0473A";
            if (type=="BQ")
            {
                    if (time.ToString()=="null")
                    {
                        time = "";
                    }
                int i = fwData.Updatetime(ID, time);
                if (i > 0)
                {
                        WebUtility.ThirdPartyJob third = new ThirdPartyJob();
                        third.Done(appId, appSecret, porid, userid, string.Empty, string.Empty);
                        return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                WebUtility.ThirdPartyJob third = new ThirdPartyJob();
                third.Done(appId, appSecret, porid, userid, string.Empty, string.Empty);
                    return "1";
            }
            }
            catch (Exception  ex)
            {
                return ex.ToString();
                throw;
            }

        }

        //第三方待办页面跟进记录
        public ActionResult ThreeView()
        {
            string id = Request.QueryString["id"];
            string ids = Request.QueryString["IDs"];
            string userid = Request.QueryString["userid"];
            DataTable dt = fwData.SelectlawInfoKll(id);    //案件信息  SelectLawFirm

            DataTable dtLitig = fwData.SelectTreeone(ids);  //诉讼保全信息


            DataTable dtname = fwData.SelectPeopername(userid);//查询当前用户名字
            Session["threeid"] = ids;

            Session["usid"] = userid;
            Session["threeuser"] = dtname.Rows[0]["UserName"];
            //ViewData["Role3"] = Session["Role3"].ToString();
            if (dtLitig.Rows.Count>0)
            {
                ViewData["dtLitig"] = dtLitig.Rows[0]["LPDataEnd"];
            }
            else
            {
                ViewData["dtLitig"] = DateTime.Now.AddDays(2);
            }
           
            ViewData["dt"] = dt;

            return View();
        }

        //案件常规跟进
        public ActionResult Threeall(int page, int limit)
        {
            string ids = Session["threeid"].ToString();
            DataTable dt = fwData.SelectFollowUpRWCG(page, limit, ids);
            List<LDFW.Model.FW_FollowUp> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_FollowUp>>(JsonConvert.SerializeObject(dt));
            int PageCount = (list == null || list.Count == 0) ? 0 : list[0].rowall;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }
        //查询任务跟进信息SelectFollowUpRW
        public ActionResult FollowUpRW(int page, int limit)
        {
            string ids = Session["threeid"].ToString();
            DataTable dt = fwData.SelectFollowUpRW(page, limit, ids);
            List<LDFW.Model.FW_FollowUp> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_FollowUp>>(JsonConvert.SerializeObject(dt));
            int PageCount = (list == null || list.Count == 0) ? 0 : list[0].rowall;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }

        //修改案件方法
        [HttpPost]
        public int UpdLaw(string message, string fW_Litigation, string fw_LawyerCosts, string fw_LegalCosts, string fw_LawFiles, string types, string ID)
        {
            try
            {
                int rCount = 0;
                string errorInfo = "";
                string CreateID = Session["createid"].ToString();
                string Create = Session["Create"].ToString();
                //案件信息
                #region 修改信息
                LDFW.Model.FW_LawInfo fwinfo = new LDFW.Model.FW_LawInfo();
                JObject jo = (JObject)JsonConvert.DeserializeObject(message);
                fwinfo.LawName = jo["LawName"] == null ? "" : jo["LawName"].ToString();
                fwinfo.Grade = jo["Grade"] == null ? "" : jo["Grade"].ToString();

                if (jo["LawUserID"]!=null && jo["LawUserID"].ToString() != "")
                {
                    string[] s1 = jo["LawUserID"].ToString().Split('|');
                    fwinfo.LawUserName = s1[0];
                    fwinfo.LawUserNameID = s1[1];
                }
                else
                {
                    fwinfo.LawUserName = "";
                    fwinfo.LawUserNameID ="";
                }
               

                fwinfo.LawsuitType = jo["LawsuitType"] == null ? "" : jo["LawsuitType"].ToString();
                fwinfo.LawType = jo["LawType"] == null ? "" : jo["LawType"].ToString();
                fwinfo.Department = jo["Department"] == null ? "" : jo["Department"].ToString();
                fwinfo.Plaintiff = jo["Plaintiff"] == null ? "" : jo["Plaintiff"].ToString();
                fwinfo.Defendant = jo["Defendant"] == null ? "" : jo["Defendant"].ToString();
                fwinfo.TheThird = jo["TheThird"] == null ? "" : jo["TheThird"].ToString();
                fwinfo.Court = jo["Court"] == null ? "" : jo["Court"].ToString();
                fwinfo.CaseNo = jo["CaseNo"] == null ? "" : jo["CaseNo"].ToString();
                fwinfo.LawFirm = jo["LawFirm"] == null ? "" : jo["LawFirm"].ToString();
                fwinfo.Llawyer = jo["Llawyer"] == null ? "" : jo["Llawyer"].ToString();
                fwinfo.FilingDate = !string.IsNullOrWhiteSpace(jo["FilingDate"].ToString()) ? Convert.ToDateTime(jo["FilingDate"].ToString()) : (DateTime?)null; // Convert.ToDateTime(jo["FilingDate"]) == null ? Convert.ToDateTime("") : Convert.ToDateTime(jo["FilingDate"]);
                fwinfo.ClosingDate = !string.IsNullOrWhiteSpace(jo["ClosingDate"].ToString()) ? Convert.ToDateTime(jo["ClosingDate"].ToString()) : (DateTime?)null;
                fwinfo.LawStatus = jo["LawStatus"] == null ? "" : jo["LawStatus"].ToString();
                fwinfo.Describe = jo["Describe"] == null ? "" : jo["Describe"].ToString();
                fwinfo.Claims = jo["Claims"] == null ? "" : jo["Claims"].ToString();
                fwinfo.IsPremium = jo["IsPremium"].ToString();
                fwinfo.PremiumDate = !string.IsNullOrWhiteSpace(jo["PremiumDate"].ToString()) ? Convert.ToDateTime(jo["PremiumDate"].ToString()) : (DateTime?)null;
                fwinfo.IsDeblocking = jo["IsDeblocking"].ToString();
                fwinfo.DeblockingDate = !string.IsNullOrWhiteSpace(jo["DeblockingDate"].ToString()) ? Convert.ToDateTime(jo["DeblockingDate"].ToString()) : (DateTime?)null;
                fwinfo.SettleType = jo["SettleType"].ToString();
                fwinfo.ObjectAction = jo["ObjectAction"].ToString();
                fwinfo.InterestMoney = Convert.ToDecimal(jo["InterestMoney"].ToString());
                fwinfo.SecurityMoney = Convert.ToDecimal(jo["SecurityMoney"].ToString());
                fwinfo.SettleResult = jo["SettleResult"].ToString();
                if (jo["AmountInvolved"] == null || jo["AmountInvolved"].ToString() == "")
                {
                    fwinfo.AmountInvolved = 0;
                }
                else
                {
                    fwinfo.AmountInvolved = Convert.ToDecimal(jo["AmountInvolved"].ToString());
                }

                if (jo["AStopLoss"].ToString() == null || jo["AStopLoss"].ToString() == "")
                {
                    fwinfo.AStopLoss = 0;
                }
                else
                {
                    fwinfo.AStopLoss = Convert.ToDecimal(jo["AStopLoss"].ToString());
                }

                //fwinfo.Compensation = Convert.ToDecimal(jo["Compensation"].ToString());

                if (jo["ActualPayment"].ToString() == null || jo["ActualPayment"].ToString() == "")
                {
                    fwinfo.ActualPayment = 0;
                }
                else
                {
                    fwinfo.ActualPayment = Convert.ToDecimal(jo["ActualPayment"].ToString());
                }

                if (jo["Judgment"].ToString() == null || jo["Judgment"].ToString() == "")
                {
                    fwinfo.Judgment = 0;
                }
                else
                {
                    fwinfo.Judgment = Convert.ToDecimal(jo["Judgment"].ToString());
                }

                //单案件回款率
                if (Convert.ToDecimal(jo["ActualPayment"]) == 0)
                {
                    fwinfo.CollectionRate = 0;
                }
                else
                {
                    fwinfo.CollectionRate = Math.Round(Convert.ToDecimal(Convert.ToDecimal(jo["ActualPayment"].ToString()) / Convert.ToDecimal(jo["AmountInvolved"].ToString())), 2);
                }
                //减损额
                if (jo["ObjectAction"]==null || jo["ObjectAction"].ToString()=="")
                {
                    fwinfo.Impairments = 0;
                }
                else
                {
                    fwinfo.Impairments = Math.Round(Convert.ToDecimal(Convert.ToDecimal(jo["ObjectAction"].ToString()) - Convert.ToDecimal(jo["AStopLoss"].ToString())), 2);
                }
            
                //止损率
                if (jo["ObjectAction"] == null || jo["ObjectAction"].ToString() == "")
                {
                    fwinfo.StopLossRate = 0;
                }
                else
                {
                    if (Convert.ToDecimal(jo["ObjectAction"])==0)
                    {
                        fwinfo.StopLossRate = 0;
                    }
                    else
                    {
                        fwinfo.StopLossRate = Convert.ToDecimal(fwinfo.Impairments / Convert.ToDecimal(jo["ObjectAction"].ToString()));
                    }
                    
                }


                //风险敞口
                fwinfo.RiskExposure = Math.Round(Convert.ToDecimal(Convert.ToDecimal(jo["AmountInvolved"].ToString()) - Convert.ToDecimal(jo["ActualPayment"].ToString())), 2);
                //剩余欠款本金
                fwinfo.ResidueFee = Math.Round(Convert.ToDecimal(Convert.ToDecimal(jo["AmountInvolved"].ToString()) - Convert.ToDecimal(jo["ActualPayment"].ToString())), 2);


                //fwinfo.StopLossRate = jo["StopLossRate"].ToString();

                fwinfo.IsAssess = jo["IsAssess"].ToString() == "on" ? "1" : "0";
                fwinfo.Solutions = jo["Solutions"] == null ? "" : jo["Solutions"].ToString();
                fwinfo.PersonLiable = jo["PersonLiable"] == null ? "" : jo["PersonLiable"].ToString();
                //fwinfo.PersonFollowUp = jo["PersonFollowUp"].ToString();
                //fwinfo.FollowUpRecord = jo["FollowUpRecord"].ToString();
                //fwinfo.NextDate = Convert.ToDateTime(jo["NextDate"].ToString());
                //fwinfo.FollowUpDate = Convert.ToDateTime(jo["FollowUpDate"].ToString());
                //fwinfo.FUStatus = jo["FUStatus"].ToString();

                //诉讼保全
                List<LDFW.Model.FW_LitigationPreservation> fW_Litigations = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LitigationPreservation>>(fW_Litigation);
                ////查封信息
                //List<LDFW.Model.FW_Seizure> fW_Seizure = JsonConvert.DeserializeObject<List<LDFW.Model.FW_Seizure>>(fw_Seizure);
                //诉讼费用

                List<LDFW.Model.FW_LegalCosts> fW_LegalCost = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LegalCosts>>(fw_LegalCosts);

                //律师费用
                List<LDFW.Model.FW_LawyerCosts> fW_LawyerCost = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LawyerCosts>>(fw_LawyerCosts);
                //附件信息
                List<LDFW.Model.FW_LawFiles> fw_LawFile = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LawFiles>>(fw_LawFiles);
                #endregion
                DataTable dt = fwData.SelectlawInfoKll(ID);    //案件信息  SelectLawFirm
                DataTable dtLitig = fwData.SelectLitigation(ID);  //诉讼保全信息
                                                                  // DataTable dtSeiz = fwData.SelectSeizure(ID);    //查封信息
                DataTable dtLega = fwData.SelectLegal(ID);    //诉讼费用
                DataTable dtLawyerr = fwData.SelectLaweyer(ID);//律师费用

                DataTable dtFile = fwData.SelectFiles(ID);    //附件信息
                int i = fwData.TransactionCreateLawUpdate(dt, dtLitig, dtLawyerr, dtLega, dtFile, fwinfo, fW_Litigations, fW_LawyerCost, fW_LegalCost, fw_LawFile, ID, types, CreateID, Create, ref rCount, ref errorInfo);
                return i;
            }
            catch (Exception ex)
            {
                return 5;

            }

        }

        //删除行信息
        public int Deletetal(string Id, string Name)
        {
            int i = fwData.Deletetal(Id, Name);
            return i;
        }

        //修改显示诉讼保全信息
        [HttpGet]
        public ActionResult SelectLitPreser(int page, int limit, string ID)
        {
            page = 1;
            limit = 10;
            ID = Session["LIDs"].ToString();
            DataTable dt = fwData.SelectLiPreserva(page, limit, ID);
            List<PreservaHP> list = JsonConvert.DeserializeObject<List<PreservaHP>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            //PreservaHP hP = new PreservaHP();
            //for (int i = 0; i < list.Count; i++)
            //{
            //   hP=new PreservaHP()
            //    {
            //        tempId = list[i].tempId,
            //         IsPreservationID = list[i].IsPreservationID,
            //          PCost = list[i].PCost,
            //      PInformation = list[i].PInformation
            //};
            // }
            //}

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //修改显示查封信息
        [HttpGet]
        public ActionResult SelectSeizureLog(int page, int limit, string ID)
        {
            page = 1;
            limit = 10;
            ID = Session["LIDs"].ToString();
            DataTable dt = fwData.SelectSeizureLog(page, limit, ID);
            List<SeizureHP> list = JsonConvert.DeserializeObject<List<SeizureHP>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;

            //SeizureHP hP = new SeizureHP();
            //for (int i = 0; i < list.Count; i++)
            //{
            //    hP = new SeizureHP()
            //    {
            //        tempId = list[i].tempId,
            //        IsSeizureID = list[i].IsSeizureID,
            //        SCost = list[i].SCost,
            //        SInformation = list[i].SInformation

            //    };
            //}
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //修改显示诉讼费用
        [HttpGet]
        public ActionResult SelectLegalCostsLog(int page, int limit, string ID)
        {
            page = 1;
            limit = 10;
            ID = Session["LIDs"].ToString();
            DataTable dt = fwData.SelectLegalCostsLog(page, limit, ID);
            List<LegalCostsHP> list = JsonConvert.DeserializeObject<List<LegalCostsHP>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            //LegalCostsHP hP = new LegalCostsHP();
            //for (int i = 0; i < list.Count; i++)
            //{
            //    hP = new LegalCostsHP()
            //    {
            //        tempId = list[i].tempId,
            //        LegalCosts = list[i].LegalCosts,
            //        LCPaymentDate = list[i].LCPaymentDate,
            //        IsSettledID = list[i].IsSettledID,
            //        AttorneyFees = list[i].AttorneyFees,
            //        LawyerPaymentDate = list[i].LawyerPaymentDate
            //    };
            //}

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //修改显示律师费用
        [HttpGet]
        public ActionResult SelectLawyerCostsLog(int page, int limit, string ID)
        {
            page = 1;
            limit = 10;
            ID = Session["LIDs"].ToString();
            DataTable dt = fwData.SelectLawyerCostsLog(page, limit, ID);
            List<LawyerCostsHP> list = JsonConvert.DeserializeObject<List<LawyerCostsHP>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            //LegalCostsHP hP = new LegalCostsHP();
            //for (int i = 0; i < list.Count; i++)
            //{
            //    hP = new LegalCostsHP()
            //    {
            //        tempId = list[i].tempId,
            //        LegalCosts = list[i].LegalCosts,
            //        LCPaymentDate = list[i].LCPaymentDate,
            //        IsSettledID = list[i].IsSettledID,
            //        AttorneyFees = list[i].AttorneyFees,
            //        LawyerPaymentDate = list[i].LawyerPaymentDate
            //    };
            //}

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //显示诉讼保全信息
        [HttpGet]
        public ActionResult SelectPreser(int page, int limit, string ID)
        {
            page = 1;
            limit = 10;
            DataTable dt = fwData.SelectLiPreserva(page, limit, ID);
            List<LDFW.Model.FW_LitigationPreservation> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LitigationPreservation>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }
        //显示诉讼保全历史信息
        public ActionResult SelectPreserLog(int page, int limit, string ID)
        {
            page = 1;
            limit = 10;
            DataTable dt = fwData.SelectLiPreservaLog(page, limit, ID);
            List<LDFW.Model.FW_LitigationPreservation> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LitigationPreservation>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }
        //显示律师费用
        [HttpGet]
        public ActionResult SelectLawyer(int page, int limit, string ID)
        {
            page = 1;
            limit = 10;
            DataTable dt = fwData.SelectLawyerCostsLog(page, limit, ID);
            List<LDFW.Model.FW_LawyerCosts> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LawyerCosts>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }

        //显示律师费用历史信息
        [HttpGet]
        public ActionResult SelectLawyerLoginfo(int page, int limit, string ID)
        {
            page = 1;
            limit = 10;
            DataTable dt = fwData.SelectLawerCossLoginfo(page, limit, ID);
            List<LDFW.Model.FW_LawyerCosts> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LawyerCosts>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }
        //显示诉讼费用
        [HttpGet]
        public ActionResult SelectLegalCosts(int page, int limit, string ID)
        {
            page = 1;
            limit = 10;
            //ID = Session["LIDs"].ToString();
            DataTable dt = fwData.SelectLegalCostsLog(page, limit, ID);
            List<LDFW.Model.FW_LegalCosts> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LegalCosts>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }
        //显示诉讼费用历史信息SelectLegalCostsLogInfo
        [HttpGet]
        public ActionResult SelectLegalCostsInfo(int page, int limit, string ID)
        {
            page = 1;
            limit = 10;
            //ID = Session["LIDs"].ToString();
            DataTable dt = fwData.SelectLegalCostsLogInfo(page, limit, ID);
            List<LDFW.Model.FW_LegalCosts> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LegalCosts>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }


        //第三方待办
        public ActionResult SelectThree(string ID)
        {
            int page = 1;
            int limit = 10;
            DataTable dt = fwData.SelectTreeone(ID);
            List<LDFW.Model.FW_LitigationPreservation> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_LitigationPreservation>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 绑定下拉框搜索框
        //绑定人员下拉框
        [HttpPost]
        public ActionResult SelectplainAll(string keyword)
        {
            System.Data.DataTable dt = fwData.SelectPlaintiff(keyword);
            List<Plaintiff> list = JsonConvert.DeserializeObject<List<Plaintiff>>(JsonConvert.SerializeObject(dt));
            return Json(new LayUis { code = "0", msg = "", data = list });
        }

        //绑定被告下拉框
        [HttpPost]
        public ActionResult SelectDefendantAll(string keyword)
        {
            System.Data.DataTable dt = fwData.SelectDefendant(keyword);
            List<Plaintiff> list = JsonConvert.DeserializeObject<List<Plaintiff>>(JsonConvert.SerializeObject(dt));
            return Json(new LayUis { code = "0", msg = "", data = list });
        }
        //绑定第三人下拉框
        [HttpPost]
        public ActionResult SelectTheThirdAll(string keyword)
        {
            System.Data.DataTable dt = fwData.SelectTheThird(keyword);
            List<Plaintiff> list = JsonConvert.DeserializeObject<List<Plaintiff>>(JsonConvert.SerializeObject(dt));
            return Json(new LayUis { code = "0", msg = "", data = list });
        }

        //绑定律所下拉框 SelectLlawyer
        [HttpPost]
        public ActionResult SelectLawFirmAll(string keyword)
        {
            System.Data.DataTable dt = fwData.SelectLawFirm(keyword);
            List<Plaintiff> list = JsonConvert.DeserializeObject<List<Plaintiff>>(JsonConvert.SerializeObject(dt));
            return Json(new LayUis { code = "0", msg = "", data = list });
        }


        //绑定律师下拉框 SelectLlawyer
        [HttpPost]
        public ActionResult SelectLlawyerAll(string keyword)
        {
            System.Data.DataTable dt = fwData.SelectLlawyer(keyword);
            List<Plaintiff> list = JsonConvert.DeserializeObject<List<Plaintiff>>(JsonConvert.SerializeObject(dt));
            return Json(new LayUis { code = "0", msg = "", data = list });
        }
        //绑定事业部下拉框
        [HttpGet]
        public ActionResult BangDempart(string Role)
        {
            Role = Dempent();
            System.Data.DataTable dt = fwData.SelectDempant(Role);
            List<DempentList> list = JsonConvert.DeserializeObject<List<DempentList>>(JsonConvert.SerializeObject(dt));
            return Json(new LayUis { code = "0", msg = "", data = list }, JsonRequestBehavior.AllowGet);
        }


        //查询用户事业部权限
        public string Dempent()
        {
            string Role = "";
            string Roleid = Session["RoleID"].ToString();
            string Roleidon = Session["RoleIDon"].ToString();
            string Depmnamne = Session["DemName1"].ToString();

            if (Roleid == "1")
            {
                DataTable dtL = fwData.SelectLvdu();
                string det = string.Empty;
                for (int i = 0; i < dtL.Rows.Count; i++)
                {
                    det += "'" + dtL.Rows[i]["Name"] + "',";
                }
                Role += det;
            }
            else if (Roleid == "2")
            {
                Role = Depmnamne + ",";
            }
            else
            {
                Role += "";
            }
            if (Roleidon == "1")
            {
                DataTable dtL = fwData.SelectHT();
                string det = string.Empty;
                for (int i = 0; i < dtL.Rows.Count; i++)
                {
                    det += "'" + dtL.Rows[i]["Name"] + "',";
                }
                Role += det;
            }
            else if (Roleidon == "2")
            {
                Role += Depmnamne + ",";
            }
            else
            {
                Role += "";
            }
            if (Role!="")
            {
                Role = Role.Substring(0, Role.Length - 1);
            }
            return Role;
           
        }


        //绑定案件类型下拉框
        [HttpGet]
        public ActionResult BangLawType()
        {
            string key = "LawType";
            System.Data.DataTable dt = fwData.SelectLawType(key);
            List<LDFW.Model.FW_Dictionary> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_Dictionary>>(JsonConvert.SerializeObject(dt));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //绑定案件等级下拉框
        [HttpGet]
        public ActionResult BangGrade()
        {
            System.Data.DataTable dt = fwData.SelectLawType("Grade");
            List<LDFW.Model.FW_Dictionary> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_Dictionary>>(JsonConvert.SerializeObject(dt));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //绑定诉讼类型下拉框
        [HttpGet]
        public ActionResult BangsuitType()
        {
            System.Data.DataTable dt = fwData.SelectLawType("LawsuitType");
            List<LDFW.Model.FW_Dictionary> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_Dictionary>>(JsonConvert.SerializeObject(dt));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //绑定结案类型下拉SelectSettleType
        [HttpGet]
        public ActionResult BandSettleType()
        {
            System.Data.DataTable dt = fwData.SelectLawType("SettleType");
            List<LDFW.Model.FW_Dictionary> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_Dictionary>>(JsonConvert.SerializeObject(dt));
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        //绑定当前状态下拉框
        [HttpGet]
        public ActionResult BangLawStatus()
        {
            System.Data.DataTable dt = fwData.SelectLawType("LawStatus");
            List<LDFW.Model.FW_Dictionary> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_Dictionary>>(JsonConvert.SerializeObject(dt));
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //绑定个人/公司类型
        [HttpGet]
        public ActionResult BangPersonaStatus()
        {
            System.Data.DataTable dt = fwData.SelectPersonalStatus();
            List<LDFW.Model.FW_Dictionary> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_Dictionary>>(JsonConvert.SerializeObject(dt));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //绑定附件类型下拉框 
        [HttpGet]
        public ActionResult BangFileType()
        {
            System.Data.DataTable dt = fwData.SelectFileType();
            List<LDFW.Model.FW_Dictionary> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_Dictionary>>(JsonConvert.SerializeObject(dt));
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除/查看界面
        //删除案件
        [HttpPost]
        public int DeletePeoperLaw(string ID)
        {
            int i = fwData.DeletePeoperLaw(ID);
            return i;
        }

        //显示案件详情页面
        public ActionResult LawInfo(string ID)
        {

            DataTable dt = fwData.SelectlawInfoKll(ID);    //案件信息  SelectLawFirm

            DataTable dtLitig = fwData.SelectLitigation(ID);  //诉讼保全信息
            DataTable dtSeiz = fwData.SelectSeizure(ID);    //查封信息
            DataTable dtLega = fwData.SelectLegal(ID);    //律师费用
            DataTable dtFile = fwData.SelectFiles(ID);    //诉讼费用

            ViewData["Role3"] = Session["Role3"].ToString();
            ViewData["dtFiles"] = dtFile;
            ViewData["dtLega"] = dtLega;
            ViewData["dtLitig"] = dtLitig;
            ViewData["dt"] = dt;
            ViewData["dtSeiz"] = dtSeiz;
            Session["IDs"] = ID;

            return View();
        }
        //显示案件日志
        public ActionResult LawInfoLog(string ID)
        {
            DataTable dt = fwData.SelectlawInfolog(ID);    //案件信息  SelectLawFirm
            if (dt.Rows.Count > 0)
            {
                string IDs = dt.Rows[0]["ID"].ToString();

                DataTable dtLitig = fwData.SelectLitigationlog(IDs);  //诉讼保全信息
                DataTable dtSeiz = fwData.SelectSeizurelog(IDs);    //查封信息
                DataTable dtLega = fwData.SelectLegallog(IDs);    //律师费用
                ViewData["dtLega"] = dtLega;
                ViewData["dtLitig"] = dtLitig;
                ViewData["dt"] = dt;
                ViewData["dtSeiz"] = dtSeiz;
                Session["IDs"] = ID;
            }
            return View();
        }
        #endregion

        #region 案件结案
        //结案界面
        public ActionResult AddL(string ID)
        {
            Session["IDend"] = ID;
            ViewData["Username"] = Session["create"].ToString();
            ViewData["Bpmurl"] = GetBPMUrl(Session["CreateID"].ToString(), 1, ID, "FW02");
            return View();
        }


        public string GetUrltow(string boid)
        {
            return GetBPMUrl(Session["CreateID"].ToString(), 1, boid, "FW02");
        }


        [HttpPost]
        public string LawEnd(string followUp)
        {
            try
            {
                string ID = Session["IDend"].ToString();
                int rCount = 0;
                string IDs = "";
                string errorInfo = "";
                string CreateID = Session["createid"].ToString();
                string Create = Session["Create"].ToString();
                string url = "";
                //案件信息
                LDFW.Model.FW_LawInfo fwllow = new LDFW.Model.FW_LawInfo();
                JObject jo = (JObject)JsonConvert.DeserializeObject(followUp);
                fwllow.ClosingDate = Convert.ToDateTime(jo["ClosingDate"].ToString());
                fwllow.SettleType = jo["SettleType"].ToString();
                fwllow.SettleResult = jo["SettleResult"].ToString();
                fwllow.IsPremium= jo["IsPremium"].ToString();
                fwllow.IsDeblocking= jo["IsDeblocking"].ToString();
                fwllow.ID = Guid.NewGuid();


                fwllow.ModifyDate = Convert.ToDateTime(jo["NextDate"]) == null ? Convert.ToDateTime("") : Convert.ToDateTime(jo["NextDate"]);
                //     fwllow.PersonLiable = jo["PersonLiable"].ToString();
                //    fwllow.Solutions = jo["Solutions"].ToString();
                int i = fwData.TransactionCreateLawEnd(fwllow, ID, CreateID, Create, ref rCount, ref errorInfo);

                if (i > 0)
                {


                    BPMHelp bPM = new BPMHelp();
                    MyResponse Response = new MyResponse();
                    XmlDocument xmlflow = new XmlDocument();
                    XmlDeclaration xmldecl;
                    xmldecl = xmlflow.CreateXmlDeclaration("1.0", "UTF-8", null);
                    xmlflow.AppendChild(xmldecl);
                    var DATA = xmlflow.CreateElement("DATA");
                    xmlflow.AppendChild(DATA);
                    System.Data.DataTable dt = fwData.SelectlawInfoK(ID, fwllow.ID.ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "TopOrgID", "00");// --绿都地产:00 --汇通能源: 01

                    bPM.AutoCreateElement(xmlflow, DATA, "LawName", dt.Rows[0]["LawName"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "CaseNo", dt.Rows[0]["CaseNo"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "IsPremium", jo["IsPremium"].ToString() == "0" ? "否" : "是");
                    bPM.AutoCreateElement(xmlflow, DATA, "ID", fwllow.ID.ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "PremiumDate", !string.IsNullOrWhiteSpace(dt.Rows[0]["PremiumDate"].ToString()) ? dt.Rows[0]["PremiumDate"].ToString() : "");
                    bPM.AutoCreateElement(xmlflow, DATA, "IsDeblocking", jo["IsDeblocking"].ToString() == "0" ? "否" : "是");
                    bPM.AutoCreateElement(xmlflow, DATA, "DeblockingDate", !string.IsNullOrWhiteSpace(dt.Rows[0]["DeblockingDate"].ToString()) ? dt.Rows[0]["DeblockingDate"].ToString() : "");
                    bPM.AutoCreateElement(xmlflow, DATA, "SettleType", jo["SettleType"].ToString().ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "SettleResult", jo["SettleResult"].ToString().ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "ClosingDate", !string.IsNullOrWhiteSpace(jo["ClosingDate"].ToString()) ? jo["ClosingDate"].ToString().ToString() : "");


                    string xmlFile = bPM.ConvertXmlToString(xmlflow);
                    string result = "";
                    string workflowType = "FW";
                    var bsid = "FW02";
                    string reqeestId = ReqeestId(fwllow.ID.ToString());
                    BS_Service bS_Service = new BS_Service();
                    result = bS_Service.WriteBusinessDataToBPM("FW", bsid, fwllow.ID.ToString(), xmlFile, reqeestId, Session["CreateID"].ToString());
                    var obj = JsonConvert.DeserializeObject<BPMResultModel>(result);
                    url = GetBPMUrl(Session["CreateID"].ToString(), 1, fwllow.ID.ToString(), bsid);
                    if (obj.msg != "发起成功")
                    {
                        i = -200;
                    }

                }


                return url;
            }
            catch (Exception ex)
            {
                return "";
                throw;

            }
        }

        #endregion

        #region 跟进记录
        //显示跟进记录方法
        public ActionResult SelectFollowUp(int page, int limit)
        {
            string ids = Session["IDs"].ToString();
            DataTable dt = fwData.SelectFollowUp(page, limit, ids);
            List<LDFW.Model.FW_FollowUp> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_FollowUp>>(JsonConvert.SerializeObject(dt));
            int PageCount = (list == null || list.Count == 0) ? 0 : list[0].rowall;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }

        //跟进记录页面
        public ActionResult AddCaseProgress(string ID)
        {
            Session["ID"] = ID;

            ViewData["Username"] = Session["create"] != null ? Session["create"] : Session["threeuser"];


            return View();
        }
        //添加跟进记录方法
        [HttpPost]

        public int AddFollowUp(string followUp)
        {
            try
            {
                string ID = Session["ID"] != null ? Session["ID"].ToString() : Session["threeid"].ToString();
                int rCount = 0;
                string errorInfo = "";
                string CreateID = Session["createid"] != null ? Session["createid"].ToString() : Session["usid"].ToString();
                string Create = Session["Create"] != null ? Session["Create"].ToString() : Session["threeuser"].ToString();
                //案件信息
                LDFW.Model.FW_FollowUp fwllow = new LDFW.Model.FW_FollowUp();
                JObject jo = (JObject)JsonConvert.DeserializeObject(followUp);
                fwllow.FollowUpDate = Convert.ToDateTime(jo["FollowUpDate"].ToString());
                fwllow.PersonFollowUp = jo["PersonFollowUp"].ToString();
                fwllow.FollowUpInfo = jo["FollowUpInfo"].ToString();
                fwllow.FUStatus = jo["FUStatus"].ToString();

                fwllow.LawType = jo["LawType"].ToString();
                fwllow.TrialState = jo["TrialState"].ToString();
                fwllow.NextMassage = jo["NextMassage"].ToString();


                fwllow.NextDate = Convert.ToDateTime(jo["NextDate"]) == null ? Convert.ToDateTime("") : Convert.ToDateTime(jo["NextDate"]);
                //     fwllow.PersonLiable = jo["PersonLiable"].ToString();
                //    fwllow.Solutions = jo["Solutions"].ToString();
                int i = fwData.TransactionCreateFollowUp(fwllow, ID, CreateID, Create, ref rCount, ref errorInfo);
                return i;
            }
            catch (Exception ex)
            {
                return 0;
                throw;

            }

        }
        #endregion

        #region 任务跟进块
        public ActionResult AddForm(string ID)
        {

            Session["IDf"] = ID;

            //   ViewData["Bpmurl"] = GetBPMUrl(Session["CreateID"].ToString(), 1, BOID, "FW01");
            ViewData["Username"] = Session["create"] != null ? Session["create"] : Session["threeuser"];
            return View();
        }

        public string GetUrl(string boid)
        {
            string userid = Session["CreateID"] != null ? Session["CreateID"].ToString() : Session["usid"].ToString();
            return GetBPMUrl(userid, 1, boid, "FW01");
        }



        public string AddTask(string followUp)
        {
            try
            {
                string ID = Session["IDf"] != null ? Session["IDf"].ToString() : Session["threeid"].ToString();
                int rCount = 0;
                string errorInfo = "";
                string CreateID = Session["createid"] != null ? Session["createid"].ToString() : Session["usid"].ToString();
                string Create = Session["Create"] != null ? Session["Create"].ToString() : Session["threeuser"].ToString();

                string url = "";
                //案件信息
                LDFW.Model.FW_FollowUp fwllow = new LDFW.Model.FW_FollowUp();
                JObject jo = (JObject)JsonConvert.DeserializeObject(followUp);
                fwllow.TaskTime = Convert.ToDateTime(jo["TaskTime"].ToString());
                fwllow.FollowUpDate = DateTime.Now;
                fwllow.PersonFollowUp = Create;
                fwllow.PersonFollowUpID = CreateID;
                fwllow.LawType = "任务事项跟进";
                fwllow.ID = Guid.NewGuid().ToString();

                string[] s1 = jo["TaskPerson"].ToString().Split('|');
                fwllow.TaskPerson = s1[0];
                fwllow.TaskPersonID = s1[1];
                fwllow.TaskEndTime = (DateTime?)null;

                string[] s2 = jo["YewuPerson"].ToString().Split('|');
                fwllow.YewuPerson = s2[0];
                fwllow.YewuPersonID = s2[1];

                fwllow.TaskAsk = jo["TaskAsk"].ToString();

                fwllow.TaskFwwdback = "";

                int i = fwData.TransactionCreateTask(fwllow, ID, CreateID, Create, ref rCount, ref errorInfo);
                if (i > 0)
                {
                    BPMHelp bPM = new BPMHelp();
                    MyResponse Response = new MyResponse();
                    XmlDocument xmlflow = new XmlDocument();
                    XmlDeclaration xmldecl;
                    xmldecl = xmlflow.CreateXmlDeclaration("1.0", "UTF-8", null);
                    xmlflow.AppendChild(xmldecl);
                    var DATA = xmlflow.CreateElement("DATA");
                    xmlflow.AppendChild(DATA);
                    System.Data.DataTable dt = fwData.SelectlawInfoK(ID, fwllow.ID);
                    bPM.AutoCreateElement(xmlflow, DATA, "BPM_Title","关于"+ dt.Rows[0]["LawName"].ToString()+"的任务跟进流程");
                    bPM.AutoCreateElement(xmlflow, DATA, "Bpmuserid", dt.Rows[0]["YewuPersonID"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "TopOrgID", "00");// --绿都地产:00 --汇通能源: 01
                    bPM.AutoCreateElement(xmlflow, DATA, "Department", dt.Rows[0]["Department"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "LawsuitType", dt.Rows[0]["LawsuitType"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "LawType", dt.Rows[0]["LawType"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "LawName", dt.Rows[0]["LawName"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "CaseNo", dt.Rows[0]["CaseNo"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "Plaintiff", dt.Rows[0]["Plaintiff"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "Defendant", dt.Rows[0]["Defendant"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "ID", fwllow.ID);
                    bPM.AutoCreateElement(xmlflow, DATA, "TaskPerson", dt.Rows[0]["TaskPerson"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "TaskPersonID", dt.Rows[0]["TaskPersonID"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "YewuPerson", dt.Rows[0]["YewuPerson"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "YewuPersonID", dt.Rows[0]["YewuPersonID"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "TaskTime", dt.Rows[0]["TaskTime"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "TaskEndTime", !string.IsNullOrWhiteSpace(dt.Rows[0]["TaskEndTime"].ToString()) ? dt.Rows[0]["TaskEndTime"].ToString() : "");
                    bPM.AutoCreateElement(xmlflow, DATA, "TaskAsk", dt.Rows[0]["TaskAsk"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "TaskFwwdback", dt.Rows[0]["TaskFwwdback"].ToString());
                    bPM.AutoCreateElement(xmlflow, DATA, "Describe", dt.Rows[0]["Describe"].ToString());
                    string xmlFile = bPM.ConvertXmlToString(xmlflow);
                    string result = "";
                    string workflowType = "FW";
                    var bsid = "FW01";
                    string reqeestId = ReqeestId(fwllow.ID);
                    BS_Service bS_Service = new BS_Service();
                    result = bS_Service.WriteBusinessDataToBPM("FW", bsid, fwllow.ID, xmlFile, reqeestId, CreateID);
                    var obj = JsonConvert.DeserializeObject<BPMResultModel>(result);

                    url = GetBPMUrl(CreateID, 1, fwllow.ID, bsid);
                    if (obj.msg != "发起成功")
                    {
                        i = -200;
                    }


                }
                return url;
            }
            catch (Exception ex)
            {
                return "0";
                throw;

            }
        }


        #endregion

        /// <summary>
        /// 获取流程链接
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="formtype"></param>
        /// <param name="boid"></param>
        /// <returns></returns>
        public string GetBPMUrl(string userid, int formtype, string boid, string btids)
        {
            try
            {
                System.Data.DataTable dt = new PublicDao().GetCallBackRequestId(boid);
                string loginkey = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(string.Format("{0}BPM", userid), "MD5").ToLower();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ProcessUrl"].ToString().Length > 0)
                        return dt.Rows[0]["ProcessUrl"].ToString() + "&UserID=" + userid + "&LoginKey=" + loginkey;
                    else
                    {
                        string urlstr = System.Configuration.ConfigurationManager.AppSettings["BPMApplyRecord"];
                        string workflowType = "FW";
                        string btid = btids;
                        return string.Format(urlstr, btid, boid, userid, loginkey);
                    }
                }
                else
                {
                    string urlstr = "http://newoa.lvdu-dc.com/Workflow/MTStart2.aspx?BSID=FW&BTID={0}&BOID={1}&UserID={2}&LoginKey={3}";
                    string workflowType = "FW";
                    string btid = btids;

                    string url = string.Format(urlstr, btid, boid, userid, loginkey);
                    return url;
                }
            }
            catch (Exception ex)
            {
                return "GetBPMUrl错误" + ex.Message;
            }
        }

        public string ReqeestId(string mainID, bool deleteOA = false)
        {
            System.Data.DataTable dt = new PublicDao().GetCallBackRequestId(mainID);
            string requestId = "";

            if (dt.Rows.Count > 0)
            {
                requestId = dt.Rows[0]["RequestId"].ToString();

            }
            return requestId;
        }

        public ActionResult ExcelData()
        {
            ViewData["LawExcel"] = Session["Role5"].ToString();
            return View();
        }


        #region 个人/公司库 块
        //显示个人/公司库
        public ActionResult CompanyPersonalView()
        {
            return View();
        }


        //添加个人/公司库页面
        public ActionResult AddCompanyPersonal()
        {
            ViewData["OneAdd"] = Session["Role4"].ToString();
            return View();
        }
        [HttpGet]
        public ActionResult SelectPersonalCompany(int page, int limit)
        {

            DataTable dt = fwData.SelectPersonalCompany(page, limit);
            List<LDFW.Model.FW_PersonalCompany> list = JsonConvert.DeserializeObject<List<LDFW.Model.FW_PersonalCompany>>(JsonConvert.SerializeObject(dt));
            int PageCount = list.Count;
            int c = (int)Math.Ceiling((decimal)PageCount / limit);
            ViewBag.parper = (page <= 1) ? 1 : page - 1;
            ViewBag.pagenext = page >= c ? c : page + 1;
            ViewBag.pahelast = c;
            return Json(new LayUi { code = "0", msg = "", count = PageCount.ToString(), data = list }, JsonRequestBehavior.AllowGet);
        }


        //添加个人/公司库方法
        [HttpPost]
        public int AddCompany(string Company, string Telephone, string Phone, string Contacts, string Address, string typeid)
        {
            try
            {
                string s = typeid.Substring(0, typeid.Length - 1);
                int rCount = 0;
                string errorInfo = "";
                FwDataInfo fwData = new FwDataInfo();
                string CreateID = Session["createid"].ToString();
                string Create = Session["Create"].ToString();
                int i = fwData.TransactionCreateCompan(Company, Telephone, Phone, Contacts, Address, s, Create, ref rCount, ref errorInfo);
                return i;
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion



    }

    public class LayUi
    {
        public string code { get; set; }
        public string msg { get; set; }
        public string count { get; set; }
        public object data { get; set; }
    }
    public class BPMResultModel
    {
        public string code { get; set; }
        public string msg { get; set; }
        public string pcStartUrl { get; set; }
    }

    public class DempentList
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class MyResponse
    {
        public bool Success { set; get; }
        public string Message { set; get; }
    }


    public class PreservaHP
    {

        public string tempId { get; set; }
        public string Applicant { get; set; }
        public string Respondent { get; set; }
        public string LPCourt { get; set; }
        public string PCost { get; set; }
        public DateTime LPDate { get; set; }
        public string PInformation { get; set; }
        public string LPType { get; set; }
        public int LAY_TABLE_INDEX { get; set; }
    }

    public class SeizureHP
    {

        public string tempId { get; set; }
        public int IsSeizureID { get; set; }
        public string SCost { get; set; }
        public string SInformation { get; set; }
    }

    public class LegalCostsHP
    {
        public string tempId { get; set; }
        public string LCType { get; set; }
        public DateTime LCPaymentDate { get; set; }
        public string LegalCosts { get; set; }
    }

    public class LawyerCostsHP
    {
        public string tempId { get; set; }
        public string LawyerType { get; set; }
        public DateTime LawyerPaymentDate { get; set; }
        public string AttorneyFees { get; set; }
    }

    public class Plaintiff
    {
        public string value { get; set; }
        public string name { get; set; }
        public bool selected { get; set; } = false;
        public bool disabled { get; set; } = false;
    }
    public class LayUis
    {
        public string code { get; set; }
        public string msg { get; set; }

        public object data { get; set; }
    }

    public class Companyhelp
    {
        //public string ID { get; set; }

        public string Company { get; set; }

        public string Telephone { get; set; }

        public string Phone { get; set; }

        public string Contacts { get; set; }

        public string Address { get; set; }

        public string UserName { get; set; }

        //public int? ISValid { get; set; }
        public string like11 { get; set; }
        //public string like12 { get; set; }
        //public string like13 { get; set; }
        //public string like14 { get; set; }
        //public string like15 { get; set; }
        //public string like16 { get; set; }


        //public DateTime? CreateDate { get; set; }
        public string CreateID { get; set; }
        public string Create { get; set; }
        public string TypeID { get; set; }
        public string TypeName { get; set; }


    }
}