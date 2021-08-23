//using LDFW.DAL;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;


//namespace LawApi.Controllers
//{
//    public class FwLawController : Controller
//    {
//        FwDataInfo fwData = new FwDataInfo();
//        // GET: FwLaw
//        [HttpGet]
//        public ActionResult Index(FW_Dictionary dictionary )
//        {

//            DataTable dt = fwData.SelectLawType();
//            List<FW_Dictionary> fW_s=JsonConvert.DeserializeObject<List<FW_Dictionary>>(JsonConvert.SerializeObject(dt));
//            var data = new
//            {
//                errcode = 0,
//                data = fW_s,
//                errmsg = "ok",
//            };


//            return Content(data.ToJson());
//        }
//    }
//}