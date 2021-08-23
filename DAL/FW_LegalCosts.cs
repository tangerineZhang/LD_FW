using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace LDFW.DAL
{
	/// <summary>
	/// 数据访问类:FW_LegalCosts
	/// </summary>
	public partial class FW_LegalCosts
	{
		public FW_LegalCosts()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(LDFW.Model.FW_LegalCosts model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into FW_LegalCosts(");
			strSql.Append("ID,LID,LegalCosts,LCPaymentDate,IsSettledID,IsSettled,AttorneyFees,LawyerPaymentDate,ModifyDate,SortID,ISValid,CreatorID,Creator,CreateDate)");
			strSql.Append(" values (");
			strSql.Append("@ID,@LID,@LegalCosts,@LCPaymentDate,@IsSettledID,@IsSettled,@AttorneyFees,@LawyerPaymentDate,@ModifyDate,@SortID,@ISValid,@CreatorID,@Creator,@CreateDate)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LID", SqlDbType.VarChar,100),
					new SqlParameter("@LegalCosts", SqlDbType.Decimal,9),
					new SqlParameter("@LCPaymentDate", SqlDbType.DateTime),
					new SqlParameter("@IsSettledID", SqlDbType.Int,4),
					new SqlParameter("@IsSettled", SqlDbType.VarChar,5),
					new SqlParameter("@AttorneyFees", SqlDbType.Decimal,9),
					new SqlParameter("@LawyerPaymentDate", SqlDbType.DateTime),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@SortID", SqlDbType.Int,4),
					new SqlParameter("@ISValid", SqlDbType.Int,4),
					new SqlParameter("@CreatorID", SqlDbType.VarChar,100),
					new SqlParameter("@Creator", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.LID;
			parameters[2].Value = model.LegalCosts;
			parameters[3].Value = model.LCPaymentDate;
			parameters[4].Value = model.IsSettledID;
			parameters[5].Value = model.IsSettled;
			parameters[6].Value = model.AttorneyFees;
			parameters[7].Value = model.LawyerPaymentDate;
			parameters[8].Value = model.ModifyDate;
			parameters[9].Value = model.SortID;
			parameters[10].Value = model.ISValid;
			parameters[11].Value = model.CreatorID;
			parameters[12].Value = model.Creator;
			parameters[13].Value = model.CreateDate;

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
		public bool Update(LDFW.Model.FW_LegalCosts model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update FW_LegalCosts set ");
			strSql.Append("ID=@ID,");
			strSql.Append("LID=@LID,");
			strSql.Append("LegalCosts=@LegalCosts,");
			strSql.Append("LCPaymentDate=@LCPaymentDate,");
			strSql.Append("IsSettledID=@IsSettledID,");
			strSql.Append("IsSettled=@IsSettled,");
			strSql.Append("AttorneyFees=@AttorneyFees,");
			strSql.Append("LawyerPaymentDate=@LawyerPaymentDate,");
			strSql.Append("ModifyDate=@ModifyDate,");
			strSql.Append("SortID=@SortID,");
			strSql.Append("ISValid=@ISValid,");
			strSql.Append("CreatorID=@CreatorID,");
			strSql.Append("Creator=@Creator,");
			strSql.Append("CreateDate=@CreateDate");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LID", SqlDbType.VarChar,100),
					new SqlParameter("@LegalCosts", SqlDbType.Decimal,9),
					new SqlParameter("@LCPaymentDate", SqlDbType.DateTime),
					new SqlParameter("@IsSettledID", SqlDbType.Int,4),
					new SqlParameter("@IsSettled", SqlDbType.VarChar,5),
					new SqlParameter("@AttorneyFees", SqlDbType.Decimal,9),
					new SqlParameter("@LawyerPaymentDate", SqlDbType.DateTime),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@SortID", SqlDbType.Int,4),
					new SqlParameter("@ISValid", SqlDbType.Int,4),
					new SqlParameter("@CreatorID", SqlDbType.VarChar,100),
					new SqlParameter("@Creator", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.LID;
			parameters[2].Value = model.LegalCosts;
			parameters[3].Value = model.LCPaymentDate;
			parameters[4].Value = model.IsSettledID;
			parameters[5].Value = model.IsSettled;
			parameters[6].Value = model.AttorneyFees;
			parameters[7].Value = model.LawyerPaymentDate;
			parameters[8].Value = model.ModifyDate;
			parameters[9].Value = model.SortID;
			parameters[10].Value = model.ISValid;
			parameters[11].Value = model.CreatorID;
			parameters[12].Value = model.Creator;
			parameters[13].Value = model.CreateDate;

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
			strSql.Append("delete from FW_LegalCosts ");
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
		public LDFW.Model.FW_LegalCosts GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,LID,LegalCosts,LCPaymentDate,IsSettledID,IsSettled,AttorneyFees,LawyerPaymentDate,ModifyDate,SortID,ISValid,CreatorID,Creator,CreateDate from FW_LegalCosts ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			LDFW.Model.FW_LegalCosts model=new LDFW.Model.FW_LegalCosts();
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
		public LDFW.Model.FW_LegalCosts DataRowToModel(DataRow row)
		{
			LDFW.Model.FW_LegalCosts model=new LDFW.Model.FW_LegalCosts();
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
				//if(row["LegalCosts"]!=null && row["LegalCosts"].ToString()!="")
				//{
				//	model.LegalCosts=decimal.Parse(row["LegalCosts"].ToString());
				//}
				if(row["LCPaymentDate"]!=null && row["LCPaymentDate"].ToString()!="")
				{
					model.LCPaymentDate=DateTime.Parse(row["LCPaymentDate"].ToString());
				}
				if(row["IsSettledID"]!=null && row["IsSettledID"].ToString()!="")
				{
					model.IsSettledID=int.Parse(row["IsSettledID"].ToString());
				}
				if(row["IsSettled"]!=null)
				{
					model.IsSettled=row["IsSettled"].ToString();
				}
				//if(row["AttorneyFees"]!=null && row["AttorneyFees"].ToString()!="")
				//{
				//	model.AttorneyFees=decimal.Parse(row["AttorneyFees"].ToString());
				//}
				if(row["LawyerPaymentDate"]!=null && row["LawyerPaymentDate"].ToString()!="")
				{
					model.LawyerPaymentDate=DateTime.Parse(row["LawyerPaymentDate"].ToString());
				}
				if(row["ModifyDate"]!=null && row["ModifyDate"].ToString()!="")
				{
					model.ModifyDate=DateTime.Parse(row["ModifyDate"].ToString());
				}
				if(row["SortID"]!=null && row["SortID"].ToString()!="")
				{
					model.SortID=int.Parse(row["SortID"].ToString());
				}
				if(row["ISValid"]!=null && row["ISValid"].ToString()!="")
				{
					model.ISValid=int.Parse(row["ISValid"].ToString());
				}
				if(row["CreatorID"]!=null)
				{
					model.CreatorID=row["CreatorID"].ToString();
				}
				if(row["Creator"]!=null)
				{
					model.Creator=row["Creator"].ToString();
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
			strSql.Append("select ID,LID,LegalCosts,LCPaymentDate,IsSettledID,IsSettled,AttorneyFees,LawyerPaymentDate,ModifyDate,SortID,ISValid,CreatorID,Creator,CreateDate ");
			strSql.Append(" FROM FW_LegalCosts ");
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
			strSql.Append(" ID,LID,LegalCosts,LCPaymentDate,IsSettledID,IsSettled,AttorneyFees,LawyerPaymentDate,ModifyDate,SortID,ISValid,CreatorID,Creator,CreateDate ");
			strSql.Append(" FROM FW_LegalCosts ");
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
			strSql.Append("select count(1) FROM FW_LegalCosts ");
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
			strSql.Append(")AS Row, T.*  from FW_LegalCosts T ");
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
			parameters[0].Value = "FW_LegalCosts";
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

