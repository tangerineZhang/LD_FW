using System;
namespace LDFW.Model
{
	/// <summary>
	/// FW_PCType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FW_PCType
	{
		public FW_PCType()
		{}
		#region Model
		private Guid _id;
		private string _pcid;
		private string _typeid;
		private string _typename;
		private string _username;
		private int? _isvalid=1;
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
		public string PCID
		{
			set{ _pcid=value;}
			get{return _pcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
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
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		#endregion Model

	}
}

