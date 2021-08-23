using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace LDFW.DAL
{
	/// <summary>
	/// 数据访问类:FW_LawFiles
	/// </summary>
	public partial class FW_LawFiles
	{
		public FW_LawFiles()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(LDFW.Model.FW_LawFiles model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into FW_LawFiles(");
			strSql.Append("ID,LID,FURID,FileName,FilePath,FileFormat,FileSize,FileTypeID,FileType,SortID,ISValid,CreatorID,Creator,CreateDate)");
			strSql.Append(" values (");
			strSql.Append("@ID,@LID,@FURID,@FileName,@FilePath,@FileFormat,@FileSize,@FileTypeID,@FileType,@SortID,@ISValid,@CreatorID,@Creator,@CreateDate)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LID", SqlDbType.VarChar,100),
					new SqlParameter("@FURID", SqlDbType.VarChar,100),
					new SqlParameter("@FileName", SqlDbType.VarChar,100),
					new SqlParameter("@FilePath", SqlDbType.VarChar,500),
					new SqlParameter("@FileFormat", SqlDbType.VarChar,12),
					new SqlParameter("@FileSize", SqlDbType.Decimal,9),
					new SqlParameter("@FileTypeID", SqlDbType.Int,4),
					new SqlParameter("@FileType", SqlDbType.VarChar,30),
					new SqlParameter("@SortID", SqlDbType.Int,4),
					new SqlParameter("@ISValid", SqlDbType.Int,4),
					new SqlParameter("@CreatorID", SqlDbType.VarChar,100),
					new SqlParameter("@Creator", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.LID;
			parameters[2].Value = model.FURID;
			parameters[3].Value = model.FileName;
			parameters[4].Value = model.FilePath;
			parameters[5].Value = model.FileFormat;
			parameters[6].Value = model.FileSize;
			parameters[7].Value = model.FileTypeID;
			parameters[8].Value = model.FileType;
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
		public bool Update(LDFW.Model.FW_LawFiles model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update FW_LawFiles set ");
			strSql.Append("ID=@ID,");
			strSql.Append("LID=@LID,");
			strSql.Append("FURID=@FURID,");
			strSql.Append("FileName=@FileName,");
			strSql.Append("FilePath=@FilePath,");
			strSql.Append("FileFormat=@FileFormat,");
			strSql.Append("FileSize=@FileSize,");
			strSql.Append("FileTypeID=@FileTypeID,");
			strSql.Append("FileType=@FileType,");
			strSql.Append("SortID=@SortID,");
			strSql.Append("ISValid=@ISValid,");
			strSql.Append("CreatorID=@CreatorID,");
			strSql.Append("Creator=@Creator,");
			strSql.Append("CreateDate=@CreateDate");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LID", SqlDbType.VarChar,100),
					new SqlParameter("@FURID", SqlDbType.VarChar,100),
					new SqlParameter("@FileName", SqlDbType.VarChar,100),
					new SqlParameter("@FilePath", SqlDbType.VarChar,500),
					new SqlParameter("@FileFormat", SqlDbType.VarChar,12),
					new SqlParameter("@FileSize", SqlDbType.Decimal,9),
					new SqlParameter("@FileTypeID", SqlDbType.Int,4),
					new SqlParameter("@FileType", SqlDbType.VarChar,30),
					new SqlParameter("@SortID", SqlDbType.Int,4),
					new SqlParameter("@ISValid", SqlDbType.Int,4),
					new SqlParameter("@CreatorID", SqlDbType.VarChar,100),
					new SqlParameter("@Creator", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.LID;
			parameters[2].Value = model.FURID;
			parameters[3].Value = model.FileName;
			parameters[4].Value = model.FilePath;
			parameters[5].Value = model.FileFormat;
			parameters[6].Value = model.FileSize;
			parameters[7].Value = model.FileTypeID;
			parameters[8].Value = model.FileType;
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
			strSql.Append("delete from FW_LawFiles ");
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
		public LDFW.Model.FW_LawFiles GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,LID,FURID,FileName,FilePath,FileFormat,FileSize,FileTypeID,FileType,SortID,ISValid,CreatorID,Creator,CreateDate from FW_LawFiles ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			LDFW.Model.FW_LawFiles model=new LDFW.Model.FW_LawFiles();
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
		public LDFW.Model.FW_LawFiles DataRowToModel(DataRow row)
		{
			LDFW.Model.FW_LawFiles model=new LDFW.Model.FW_LawFiles();
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
				if(row["FURID"]!=null)
				{
					model.FURID=row["FURID"].ToString();
				}
				if(row["FileName"]!=null)
				{
					model.FileName=row["FileName"].ToString();
				}
				if(row["FilePath"]!=null)
				{
					model.FilePath=row["FilePath"].ToString();
				}
				if(row["FileFormat"]!=null)
				{
					model.FileFormat=row["FileFormat"].ToString();
				}
				if(row["FileSize"]!=null && row["FileSize"].ToString()!="")
				{
					model.FileSize=decimal.Parse(row["FileSize"].ToString());
				}
				if(row["FileTypeID"]!=null && row["FileTypeID"].ToString()!="")
				{
					model.FileTypeID=int.Parse(row["FileTypeID"].ToString());
				}
				if(row["FileType"]!=null)
				{
					model.FileType=row["FileType"].ToString();
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
			strSql.Append("select ID,LID,FURID,FileName,FilePath,FileFormat,FileSize,FileTypeID,FileType,SortID,ISValid,CreatorID,Creator,CreateDate ");
			strSql.Append(" FROM FW_LawFiles ");
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
			strSql.Append(" ID,LID,FURID,FileName,FilePath,FileFormat,FileSize,FileTypeID,FileType,SortID,ISValid,CreatorID,Creator,CreateDate ");
			strSql.Append(" FROM FW_LawFiles ");
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
			strSql.Append("select count(1) FROM FW_LawFiles ");
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
			strSql.Append(")AS Row, T.*  from FW_LawFiles T ");
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
			parameters[0].Value = "FW_LawFiles";
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

