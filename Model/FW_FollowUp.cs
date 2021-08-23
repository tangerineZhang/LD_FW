using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LDFW.Model
{
   public class FW_FollowUp
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
        public string CID { get; set; }
        public string RequestId { get; set; }
        public string ProcessUrl { get; set; }
    }
}
