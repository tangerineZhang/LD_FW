using System;
namespace LDFW.Model
{
	/// <summary>
	/// FW_LawInfoModifyLog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FW_LawInfoModifyLog
	{
		public FW_LawInfoModifyLog()
		{}
		#region Model
		private Guid _id;
		private string _lid;
		private string _lawname;
		private int? _gradeid;
		private string _grade;
		private int? _lawsuittypeid;
		private string _lawsuittype;
		private int? _lawtypeid;
		private string _lawtype;
		private string _departmentid;
		private string _department;
		private string _plaintiffid;
		private string _plaintiff;
		private string _defendantid;
		private string _defendant;
		private string _thethirdid;
		private string _thethird;
		private string _courtid;
		private string _court;
		private string _caseno;
		private string _lawfirmid;
		private string _lawfirm;
		private DateTime? _filingdate;
		private DateTime? _closingdate;
		private int? _lawstatusid;
		private string _lawstatus;
		private string _describe;
		private string _claims;
		private decimal? _amountinvolved;
		private decimal? _amountuedu;
		private decimal? _compensation;
		private decimal? _sstoploss;
		private decimal? _astoploss;
		private string _stoplossrate;
		private decimal? _riskexposure;
		private int? _isassessid;
		private string _isassess;
		private string _solutions;
		private string _personliableid;
		private string _personliable;
		private string _personfollowupid;
		private string _personfollowup;
		private string _followuprecord;
		private DateTime? _modifydate;
		private int? _sortid;
		private int? _isvalid=1;
		private string _creatorid;
		private string _creator;
		private DateTime? _createdate= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public Guid ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LID
		{
			set{ _lid=value;}
			get{return _lid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LawName
		{
			set{ _lawname=value;}
			get{return _lawname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? GradeID
		{
			set{ _gradeid=value;}
			get{return _gradeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Grade
		{
			set{ _grade=value;}
			get{return _grade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LawsuitTypeID
		{
			set{ _lawsuittypeid=value;}
			get{return _lawsuittypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LawsuitType
		{
			set{ _lawsuittype=value;}
			get{return _lawsuittype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LawTypeID
		{
			set{ _lawtypeid=value;}
			get{return _lawtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LawType
		{
			set{ _lawtype=value;}
			get{return _lawtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DepartmentID
		{
			set{ _departmentid=value;}
			get{return _departmentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Department
		{
			set{ _department=value;}
			get{return _department;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PlaintiffID
		{
			set{ _plaintiffid=value;}
			get{return _plaintiffid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Plaintiff
		{
			set{ _plaintiff=value;}
			get{return _plaintiff;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DefendantID
		{
			set{ _defendantid=value;}
			get{return _defendantid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Defendant
		{
			set{ _defendant=value;}
			get{return _defendant;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TheThirdID
		{
			set{ _thethirdid=value;}
			get{return _thethirdid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TheThird
		{
			set{ _thethird=value;}
			get{return _thethird;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CourtID
		{
			set{ _courtid=value;}
			get{return _courtid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Court
		{
			set{ _court=value;}
			get{return _court;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CaseNo
		{
			set{ _caseno=value;}
			get{return _caseno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LawFirmID
		{
			set{ _lawfirmid=value;}
			get{return _lawfirmid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LawFirm
		{
			set{ _lawfirm=value;}
			get{return _lawfirm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FilingDate
		{
			set{ _filingdate=value;}
			get{return _filingdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ClosingDate
		{
			set{ _closingdate=value;}
			get{return _closingdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LawStatusID
		{
			set{ _lawstatusid=value;}
			get{return _lawstatusid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LawStatus
		{
			set{ _lawstatus=value;}
			get{return _lawstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Claims
		{
			set{ _claims=value;}
			get{return _claims;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? AmountInvolved
		{
			set{ _amountinvolved=value;}
			get{return _amountinvolved;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? AmountueDU
		{
			set{ _amountuedu=value;}
			get{return _amountuedu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Compensation
		{
			set{ _compensation=value;}
			get{return _compensation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SStopLoss
		{
			set{ _sstoploss=value;}
			get{return _sstoploss;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? AStopLoss
		{
			set{ _astoploss=value;}
			get{return _astoploss;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StopLossRate
		{
			set{ _stoplossrate=value;}
			get{return _stoplossrate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? RiskExposure
		{
			set{ _riskexposure=value;}
			get{return _riskexposure;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsAssessID
		{
			set{ _isassessid=value;}
			get{return _isassessid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsAssess
		{
			set{ _isassess=value;}
			get{return _isassess;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Solutions
		{
			set{ _solutions=value;}
			get{return _solutions;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PersonLiableID
		{
			set{ _personliableid=value;}
			get{return _personliableid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PersonLiable
		{
			set{ _personliable=value;}
			get{return _personliable;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PersonFollowUpID
		{
			set{ _personfollowupid=value;}
			get{return _personfollowupid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PersonFollowUp
		{
			set{ _personfollowup=value;}
			get{return _personfollowup;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FollowUpRecord
		{
			set{ _followuprecord=value;}
			get{return _followuprecord;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ModifyDate
		{
			set{ _modifydate=value;}
			get{return _modifydate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SortID
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ISValid
		{
			set{ _isvalid=value;}
			get{return _isvalid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreatorID
		{
			set{ _creatorid=value;}
			get{return _creatorid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Creator
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		#endregion Model

	}
}

