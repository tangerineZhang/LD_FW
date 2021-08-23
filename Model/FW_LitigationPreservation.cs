using System;
namespace LDFW.Model
{
	/// <summary>
	/// FW_LitigationPreservation:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FW_LitigationPreservation
	{
		public FW_LitigationPreservation()
		{
            
    }
        #region Model
   
        public string tempId { get; set; }
        private int _rowall;
        public int rowid { get; set; }
        private Guid _id;
		private string _lid;
        private string _applicant;
        private string _respondent;
        private string _lPCourt;
        private DateTime? _lPDate;
        private int? _ispreservationid;
		private string _ispreservation;
		private string _pcost;
		private string _pinformation;
		private DateTime? _modifydate;
		private int? _sortid;
		private int? _isvalid=1;
		private string _creatorid;
		private string _creator;
		private DateTime? _createdate= DateTime.Now;
        private DateTime? _lPDataEnd;
        private string _lPType;
        /// <summary>
        /// _rowall
        /// </summary>
        /// 

            public string LPType
        {

            set { _lPType = value; }
            get { return _lPType; }
        }

        public DateTime? LPDataEnd
        {
            set { _lPDataEnd = value; }
            get { return _lPDataEnd; }
        }

        public DateTime? LPDate
        {
            set { _lPDate = value; }
            get { return _lPDate; }
        }

        public string Applicant
        {
            set { _applicant = value; }
            get { return _applicant; }
        }

        public string Respondent
        {
            set { _respondent = value; }
            get { return _respondent; }
        }
        public string LPCourt
        {
            set { _lPCourt = value; }
            get { return _lPCourt; }
        }
        public int rowall
        {
            set { _rowall = value; }
            get { return _rowall; }
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
		public int? IsPreservationID
		{
			set{ _ispreservationid=value;}
			get{return _ispreservationid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsPreservation
		{
			set{ _ispreservation=value;}
			get{return _ispreservation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PCost
		{
			set{ _pcost=value;}
			get{return _pcost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PInformation
		{
			set{ _pinformation=value;}
			get{return _pinformation;}
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

