
using LDFW.Model;
using Maticsoft.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace DAL
{
    public class SSOHelpeer
    {
        public static string SSOUrl = ConfigurationManager.AppSettings["SSOUrl"];
        /// <summary>
        /// 检验通行证
        /// </summary>
        public ResModel CheckAuth()
        {
            //正式环境
            //LDFW.DAL.AuthService1.Auth client = new LDFW.DAL.AuthService1.Auth();
            //LDFW.DAL.AuthService1.UserValidationSoapHeader header = new LDFW.DAL.AuthService1.UserValidationSoapHeader();

            //初始化WebService客户端调用类
            LDFW.DAL.AuthServiec.Auth client = new LDFW.DAL.AuthServiec.Auth();
            ////初始化请求头SoapHeader
            LDFW.DAL.AuthServiec.UserValidationSoapHeader header = new LDFW.DAL.AuthServiec.UserValidationSoapHeader();
            //SoapHeader 参数
            header.UserName = "zldc";
            header.PassWord = "zldc.com";
            //请求参数
            string APPID = "24";//应用程序标识
            string SecretKey = "fEqNCco3Yq9h5ZUglD3CZJT4lBs=";//应用程序密钥
                                                              //string ReceiveTokenUrl = "http://localhost:39398/WBGX/index1";
            string ReceiveTokenUrl = ConfigurationManager.AppSettings["CurrentUrl"];//系统接受信息地址
            //返回的通行证格式为json字符串，解析为json
            string SysTokenRequest_Json = client.GetSysRequestToken(APPID, ReceiveTokenUrl, SecretKey);
            ResModel res = JsonConvert.DeserializeObject<ResModel>(SysTokenRequest_Json);
            return res;
        }


        /// <summary>
        /// 跳转到SSO认证中心
        /// </summary>
        public void GoToSSO()
        {
            ResModel res = CheckAuth();
            if (res.StatusCode.Equals("200"))//判断是否获取通行证（状态码 200-成功 500-失败）
            {
                //认证中心
                //string SSOUrl = "http://zsjtest.lvdu-dc.com:83/";
                string ssoLoginUrl = SSOUrl + "AdminMain/GetUserToken?SysTokenRequest=" + res.DATA;//通行证
                HttpContext.Current.Response.Redirect(ssoLoginUrl);

            }
            else
            {
                string Msg = res.MSG;//消息 失败时会有错误信息
            }
        }



        public static string RemoveUrlParam(string url, string param)
        {
            var lowerUrl = url.ToLower();
            var lowerParam = param.ToLower();
            if (lowerUrl.IndexOf("&" + lowerParam) > 0)
            {
                var beginUrl = url.Substring(0, lowerUrl.IndexOf("&" + lowerParam));
                var endUrl = url.Substring(lowerUrl.IndexOf("&" + lowerParam) + 1, url.Length - lowerUrl.IndexOf("&" + lowerParam) - 1);
                if (endUrl.IndexOf("&") > 0)
                    endUrl = endUrl.Substring(endUrl.IndexOf("&"), endUrl.Length - endUrl.IndexOf("&"));
                else
                    endUrl = "";
                return beginUrl + endUrl;
            }
            if (lowerUrl.IndexOf("?" + lowerParam) > 0)
            {
                var beginUrl = url.Substring(0, lowerUrl.IndexOf("?" + lowerParam));
                var endUrl = url.Substring(lowerUrl.IndexOf("?" + lowerParam) + 1, url.Length - lowerUrl.IndexOf("?" + lowerParam) - 1);
                if (endUrl.IndexOf("&") > 0)
                    endUrl = "?" + endUrl.Substring(endUrl.IndexOf("&") + 1, endUrl.Length - endUrl.IndexOf("&") - 1);
                else
                    endUrl = "";
                return beginUrl + endUrl;
            }
            return url;
        }





    }
}
