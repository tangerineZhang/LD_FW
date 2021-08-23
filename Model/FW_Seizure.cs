using System;
namespace LDFW.Model
{
	/// <summary>
	/// FW_Seizure:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FW_Seizure
	{
		public FW_Seizure()
		{}
		#region Model
		private Guid _id;
		private string _lid;
		private int? _isseizureid;
		private string _isseizure;
		private decimal? _scost;
		private string _sinformation;
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
		public int? IsSeizureID
		{
			set{ _isseizureid=value;}
			get{return _isseizureid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsSeizure
		{
			set{ _isseizure=value;}
			get{return _isseizure;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SCost
		{
			set{ _scost=value;}
			get{return _scost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SInformation
		{
			set{ _sinformation=value;}
			get{return _sinformation;}
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

