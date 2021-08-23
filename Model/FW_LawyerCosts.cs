using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LDFW.Model
{
   public class FW_LawyerCosts
    {
        public string tempId { get; set; }
        public int rowid { get; set; }
        public string ID { get; set; }
        public string LID { get; set; }
        public string LawyerType { get; set; }
        public string AttorneyFees { get; set; }
        public DateTime? LawyerPaymentDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string SortID { get; set; }
        public string ISValid { get; set; }
        public string CreatorID { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }

    }
}
