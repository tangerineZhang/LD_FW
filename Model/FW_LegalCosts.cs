using System;
namespace LDFW.Model
{
	/// <summary>
	/// FW_LegalCosts:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FW_LegalCosts
	{
		public FW_LegalCosts()
		{}
        #region Model
        public string tempId { get; set; }
        public int rowid { get; set; }
        private string _lCType;
        private Guid _id;
		private string _lid;
		private string _legalcosts;
		private DateTime? _lcpaymentdate;
		private int? _issettledid;
		private string _issettled;
		private string _attorneyfees;
		private DateTime? _lawyerpaymentdate;
		private DateTime? _modifydate;
		private int? _sortid;
		private int? _isvalid=1;
		private string _creatorid;
		private string _creator;
		private DateTime? _createdate= DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string LCType
        {
            set { _lCType = value; }
            get { return _lCType; }
        }
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
		public string LegalCosts
		{
			set{ _legalcosts=value;}
			get{return _legalcosts;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LCPaymentDate
		{
			set{ _lcpaymentdate=value;}
			get{return _lcpaymentdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsSettledID
		{
			set{ _issettledid=value;}
			get{return _issettledid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsSettled
		{
			set{ _issettled=value;}
			get{return _issettled;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AttorneyFees
		{
			set{ _attorneyfees=value;}
			get{return _attorneyfees;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LawyerPaymentDate
		{
			set{ _lawyerpaymentdate=value;}
			get{return _lawyerpaymentdate;}
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

