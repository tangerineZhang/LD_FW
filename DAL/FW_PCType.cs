﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace LDFW.DAL
{
	/// <summary>
	/// 数据访问类:FW_PCType
	/// </summary>
	public partial class FW_PCType
	{
		public FW_PCType()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(LDFW.Model.FW_PCType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into FW_PCType(");
			strSql.Append("ID,PCID,TypeID,TypeName,UserName,ISValid,CreateDate)");
			strSql.Append(" values (");
			strSql.Append("@ID,@PCID,@TypeID,@TypeName,@UserName,@ISValid,@CreateDate)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@PCID", SqlDbType.Char,10),
					new SqlParameter("@TypeID", SqlDbType.VarChar,100),
					new SqlParameter("@TypeName", SqlDbType.VarChar,20),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@ISValid", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.PCID;
			parameters[2].Value = model.TypeID;
			parameters[3].Value = model.TypeName;
			parameters[4].Value = model.UserName;
			parameters[5].Value = model.ISValid;
			parameters[6].Value = model.CreateDate;

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
		public bool Update(LDFW.Model.FW_PCType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update FW_PCType set ");
			strSql.Append("ID=@ID,");
			strSql.Append("PCID=@PCID,");
			strSql.Append("TypeID=@TypeID,");
			strSql.Append("TypeName=@TypeName,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("ISValid=@ISValid,");
			strSql.Append("CreateDate=@CreateDate");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@PCID", SqlDbType.Char,10),
					new SqlParameter("@TypeID", SqlDbType.VarChar,100),
					new SqlParameter("@TypeName", SqlDbType.VarChar,20),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@ISValid", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.PCID;
			parameters[2].Value = model.TypeID;
			parameters[3].Value = model.TypeName;
			parameters[4].Value = model.UserName;
			parameters[5].Value = model.ISValid;
			parameters[6].Value = model.CreateDate;

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
			strSql.Append("delete from FW_PCType ");
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
		public LDFW.Model.FW_PCType GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,PCID,TypeID,TypeName,UserName,ISValid,CreateDate from FW_PCType ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			LDFW.Model.FW_PCType model=new LDFW.Model.FW_PCType();
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
		public LDFW.Model.FW_PCType DataRowToModel(DataRow row)
		{
			LDFW.Model.FW_PCType model=new LDFW.Model.FW_PCType();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID= new Guid(row["ID"].ToString());
				}
				if(row["PCID"]!=null)
				{
					model.PCID=row["PCID"].ToString();
				}
				if(row["TypeID"]!=null)
				{
					model.TypeID=row["TypeID"].ToString();
				}
				if(row["TypeName"]!=null)
				{
					model.TypeName=row["TypeName"].ToString();
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["ISValid"]!=null && row["ISValid"].ToString()!="")
				{
					model.ISValid=int.Parse(row["ISValid"].ToString());
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
			strSql.Append("select ID,PCID,TypeID,TypeName,UserName,ISValid,CreateDate ");
			strSql.Append(" FROM FW_PCType ");
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
			strSql.Append(" ID,PCID,TypeID,TypeName,UserName,ISValid,CreateDate ");
			strSql.Append(" FROM FW_PCType ");
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
			strSql.Append("select count(1) FROM FW_PCType ");
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
			strSql.Append(")AS Row, T.*  from FW_PCType T ");
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
			parameters[0].Value = "FW_PCType";
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

