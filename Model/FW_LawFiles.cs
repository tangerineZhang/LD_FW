using System;
namespace LDFW.Model
{
	/// <summary>
	/// FW_LawFiles:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FW_LawFiles
	{
		public FW_LawFiles()
		{}
		#region Model
		private Guid _id;
		private string _lid;
		private string _furid;
		private string _filename;
		private string _filepath;
		private string _fileformat;
		private decimal? _filesize;
		private int? _filetypeid;
		private string _filetype;
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
		public string FURID
		{
			set{ _furid=value;}
			get{return _furid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileName
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FilePath
		{
			set{ _filepath=value;}
			get{return _filepath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileFormat
		{
			set{ _fileformat=value;}
			get{return _fileformat;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? FileSize
		{
			set{ _filesize=value;}
			get{return _filesize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FileTypeID
		{
			set{ _filetypeid=value;}
			get{return _filetypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileType
		{
			set{ _filetype=value;}
			get{return _filetype;}
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

