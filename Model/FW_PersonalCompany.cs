using System;
namespace LDFW.Model
{
    /// <summary>
    /// FW_PersonalCompany:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class FW_PersonalCompany
    {
        public FW_PersonalCompany()
        { }
        #region Model
        private string _TypeName;
        private int _rowall;
        private int _rowid;
        private Guid _id;
        private string _company;
        private string _telephone;
        private string _phone;
        private string _contacts;
        private string _address;
        private string _username;
        private int? _isvalid = 1;
        private DateTime? _createdate = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// 
        public string TypeName
        {
            set { _TypeName = value; }
            get { return _TypeName; }
        }
        public int rowall
        {
            set { _rowall = value; }
            get { return _rowall; }
        }
        public int rowid
        {
            set { _rowid = value; }
            get { return _rowid; }
        }
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Company
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Contacts
        {
            set { _contacts = value; }
            get { return _contacts; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ISValid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model

    }
}

