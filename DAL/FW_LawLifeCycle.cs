using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace LDFW.DAL
{
	/// <summary>
	/// 数据访问类:FW_LawLifeCycle
	/// </summary>
	public partial class FW_LawLifeCycle
	{
		public FW_LawLifeCycle()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(LDFW.Model.FW_LawLifeCycle model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into FW_LawLifeCycle(");
			strSql.Append("ID,LID,SLUserID,SLUserName,OperationDate,SLObject,SLOperationType,SLSystem,SortID,ISValid,Massage,CreateDate)");
			strSql.Append(" values (");
			strSql.Append("@ID,@LID,@SLUserID,@SLUserName,@OperationDate,@SLObject,@SLOperationType,@SLSystem,@SortID,@ISValid,@Massage,@CreateDate)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LID", SqlDbType.VarChar,100),
					new SqlParameter("@SLUserID", SqlDbType.VarChar,100),
					new SqlParameter("@SLUserName", SqlDbType.VarChar,50),
					new SqlParameter("@OperationDate", SqlDbType.DateTime),
					new SqlParameter("@SLObject", SqlDbType.VarChar,100),
					new SqlParameter("@SLOperationType", SqlDbType.VarChar,100),
					new SqlParameter("@SLSystem", SqlDbType.VarChar,100),
					new SqlParameter("@SortID", SqlDbType.Int,4),
					new SqlParameter("@ISValid", SqlDbType.Int,4),
					new SqlParameter("@Massage", SqlDbType.VarChar,500),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.LID;
			parameters[2].Value = model.SLUserID;
			parameters[3].Value = model.SLUserName;
			parameters[4].Value = model.OperationDate;
			parameters[5].Value = model.SLObject;
			parameters[6].Value = model.SLOperationType;
			parameters[7].Value = model.SLSystem;
			parameters[8].Value = model.SortID;
			parameters[9].Value = model.ISValid;
			parameters[10].Value = model.Massage;
			parameters[11].Value = model.CreateDate;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LDFW.Model.FW_LawLifeCycle model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update FW_LawLifeCycle set ");
			strSql.Append("ID=@ID,");
			strSql.Append("LID=@LID,");
			strSql.Append("SLUserID=@SLUserID,");
			strSql.Append("SLUserName=@SLUserName,");
			strSql.Append("OperationDate=@OperationDate,");
			strSql.Append("SLObject=@SLObject,");
			strSql.Append("SLOperationType=@SLOperationType,");
			strSql.Append("SLSystem=@SLSystem,");
			strSql.Append("SortID=@SortID,");
			strSql.Append("ISValid=@ISValid,");
			strSql.Append("Massage=@Massage,");
			strSql.Append("CreateDate=@CreateDate");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LID", SqlDbType.VarChar,100),
					new SqlParameter("@SLUserID", SqlDbType.VarChar,100),
					new SqlParameter("@SLUserName", SqlDbType.VarChar,50),
					new SqlParameter("@OperationDate", SqlDbType.DateTime),
					new SqlParameter("@SLObject", SqlDbType.VarChar,100),
					new SqlParameter("@SLOperationType", SqlDbType.VarChar,100),
					new SqlParameter("@SLSystem", SqlDbType.VarChar,100),
					new SqlParameter("@SortID", SqlDbType.Int,4),
					new SqlParameter("@ISValid", SqlDbType.Int,4),
					new SqlParameter("@Massage", SqlDbType.VarChar,500),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.LID;
			parameters[2].Value = model.SLUserID;
			parameters[3].Value = model.SLUserName;
			parameters[4].Value = model.OperationDate;
			parameters[5].Value = model.SLObject;
			parameters[6].Value = model.SLOperationType;
			parameters[7].Value = model.SLSystem;
			parameters[8].Value = model.SortID;
			parameters[9].Value = model.ISValid;
			parameters[10].Value = model.Massage;
			parameters[11].Value = model.CreateDate;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from FW_LawLifeCycle ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LDFW.Model.FW_LawLifeCycle GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,LID,SLUserID,SLUserName,OperationDate,SLObject,SLOperationType,SLSystem,SortID,ISValid,Massage,CreateDate from FW_LawLifeCycle ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			LDFW.Model.FW_LawLifeCycle model=new LDFW.Model.FW_LawLifeCycle();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LDFW.Model.FW_LawLifeCycle DataRowToModel(DataRow row)
		{
			LDFW.Model.FW_LawLifeCycle model=new LDFW.Model.FW_LawLifeCycle();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID= new Guid(row["ID"].ToString());
				}
				if(row["LID"]!=null)
				{
					model.LID=row["LID"].ToString();
				}
				if(row["SLUserID"]!=null)
				{
					model.SLUserID=row["SLUserID"].ToString();
				}
				if(row["SLUserName"]!=null)
				{
					model.SLUserName=row["SLUserName"].ToString();
				}
				if(row["OperationDate"]!=null && row["OperationDate"].ToString()!="")
				{
					model.OperationDate=DateTime.Parse(row["OperationDate"].ToString());
				}
				if(row["SLObject"]!=null)
				{
					model.SLObject=row["SLObject"].ToString();
				}
				if(row["SLOperationType"]!=null)
				{
					model.SLOperationType=row["SLOperationType"].ToString();
				}
				if(row["SLSystem"]!=null)
				{
					model.SLSystem=row["SLSystem"].ToString();
				}
				if(row["SortID"]!=null && row["SortID"].ToString()!="")
				{
					model.SortID=int.Parse(row["SortID"].ToString());
				}
				if(row["ISValid"]!=null && row["ISValid"].ToString()!="")
				{
					model.ISValid=int.Parse(row["ISValid"].ToString());
				}
				if(row["Massage"]!=null)
				{
					model.Massage=row["Massage"].ToString();
				}
				if(row["CreateDate"]!=null && row["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(row["CreateDate"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,LID,SLUserID,SLUserName,OperationDate,SLObject,SLOperationType,SLSystem,SortID,ISValid,Massage,CreateDate ");
			strSql.Append(" FROM FW_LawLifeCycle ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,LID,SLUserID,SLUserName,OperationDate,SLObject,SLOperationType,SLSystem,SortID,ISValid,Massage,CreateDate ");
			strSql.Append(" FROM FW_LawLifeCycle ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM FW_LawLifeCycle ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T. desc");
			}
			strSql.Append(")AS Row, T.*  from FW_LawLifeCycle T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "FW_LawLifeCycle";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

