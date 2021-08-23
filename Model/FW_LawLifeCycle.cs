using System;
namespace LDFW.Model
{
	/// <summary>
	/// FW_LawLifeCycle:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FW_LawLifeCycle
	{
		public FW_LawLifeCycle()
		{}
		#region Model
		private Guid _id;
		private string _lid;
		private string _sluserid;
		private string _slusername;
		private DateTime? _operationdate;
		private string _slobject;
		private string _sloperationtype;
		private string _slsystem;
		private int? _sortid;
		private int? _isvalid=1;
		private string _massage;
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
		public string SLUserID
		{
			set{ _sluserid=value;}
			get{return _sluserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SLUserName
		{
			set{ _slusername=value;}
			get{return _slusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? OperationDate
		{
			set{ _operationdate=value;}
			get{return _operationdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SLObject
		{
			set{ _slobject=value;}
			get{return _slobject;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SLOperationType
		{
			set{ _sloperationtype=value;}
			get{return _sloperationtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SLSystem
		{
			set{ _slsystem=value;}
			get{return _slsystem;}
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
		public string Massage
		{
			set{ _massage=value;}
			get{return _massage;}
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

