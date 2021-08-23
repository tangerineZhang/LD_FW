using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_FwApi.Models
{
 
    public partial class Fw_LawInfo
    {

        public string ID
        {
            get; set;
        }

        public decimal? Receivables
        {
            get; set;
         
        }
        public decimal? ActualPayment
        {
            get; set;
        }
        public decimal? CollectionRate
        {
            get; set;
        }
        public decimal? Judgment
        {
            get; set;
        }


        public DateTime? FollowUpDate
        {
            get; set;
        }
        public DateTime? NextDate
        {
            get; set;
        }
        public string LlawyerID
        {
            get; set;
        }
        public string FUStatus
        {
            get; set;
        }

        public string Llawyer
        {
            get; set;
        }

        public int Bpmstastr
        {
            get; set;
        }
 




        /// <summary>
        /// 
        /// </summary>
        public string LawName
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public int? GradeID
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string Grade
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public int? LawsuitTypeID
    {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string LawsuitType
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public int? LawTypeID
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string LawType
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string DepartmentID
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string Department
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string PlaintiffID
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string Plaintiff
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string DefendantID
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string Defendant
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string TheThirdID
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string TheThird
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string CourtID
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string Court
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string CaseNo
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string LawFirmID
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string LawFirm
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FilingDate
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ClosingDate
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public int? LawStatusID
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string LawStatus
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string Describe
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public string Claims
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public decimal? AmountInvolved
        {
        get; set;
    }
        /// <summary>
        /// 
        /// </summary>
        public decimal? AmountueDU
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Compensation
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SStopLoss
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? AStopLoss
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string StopLossRate
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? RiskExposure
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsAssessID
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsAssess
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Solutions
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string PersonLiableID
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string PersonLiable
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string PersonFollowUpID
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string PersonFollowUp
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string FollowUpRecord
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifyDate
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SortID
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ISValid
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorID
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Creator
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            get; set;
        }


        /// <summary>
        /// 风险分析
        /// </summary>
        public string VentureFenxi
        {
            get; set;
        }
        /// <summary>
        /// 结果预测
        /// </summary>
        public string Prediction
        {
            get; set;
        }
        /// <summary>
        /// 结案类型id
        /// </summary>
        public string SettleTypeID
        {
            get; set;
        }
        /// <summary>
        /// 结案类型
        /// </summary>
        public string SettleType
        {
            get; set;
        }

        /// <summary>
        /// 审理阶段
        /// </summary>
        public string TrialState
        {
            get; set;
        }

        /// <summary>
        /// 律师费
        /// </summary>
        public decimal CounselFee { get; set; }
        /// <summary>
        /// 诉讼费
        /// </summary>
        public decimal LegalFee { get; set; }
        /// <summary>
        /// 剩余欠款本金
        /// </summary>
        public decimal ResidueFee { get; set; }
        /// <summary>
        /// 减损额
        /// </summary>
        public decimal Impairments { get; set; }

        /// <summary>
        /// 是否退费
        /// </summary>
        public string IsPremium { get; set; }

        /// <summary>
        /// 退费时间
        /// </summary>
        public DateTime? PremiumDate { get; set; }

        /// <summary>
        /// 是否解封
        /// </summary>
        public string IsDeblocking { get; set; }

        /// <summary>
        /// 解封时间
        /// </summary>
        public DateTime? DeblockingDate { get; set; }


        /// <summary>
        /// 结案处理结果
        /// </summary>
        public string SettleResult { get; set; }

        /// <summary>
        /// 利息/违约金
        /// </summary>
        public decimal InterestMoney { get; set; }

        /// <summary>
        /// 保函担保费
        /// </summary>
        public decimal SecurityMoney { get; set; }

        /// <summary>
        /// 诉讼标的
        /// </summary>
        public string ObjectAction { get; set; }

        /// <summary>
        /// 保全费
        /// </summary>
        public decimal Maintenancefee { get; set; }
   
        /// <summary>
        /// 审批步骤
        /// </summary>
        public int ApproveStatus { get; set; }
    }
}