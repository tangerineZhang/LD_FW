using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace LDFW.DAL
{
	/// <summary>
	/// 数据访问类:FW_LawInfoModifyLog
	/// </summary>
	public partial class FW_LawInfoModifyLog
	{
		public FW_LawInfoModifyLog()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(LDFW.Model.FW_LawInfoModifyLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into FW_LawInfoModifyLog(");
			strSql.Append("ID,LID,LawName,GradeID,Grade,LawsuitTypeID,LawsuitType,LawTypeID,LawType,DepartmentID,Department,PlaintiffID,Plaintiff,DefendantID,Defendant,TheThirdID,TheThird,CourtID,Court,CaseNo,LawFirmID,LawFirm,FilingDate,ClosingDate,LawStatusID,LawStatus,Describe,Claims,AmountInvolved,AmountueDU,Compensation,SStopLoss,AStopLoss,StopLossRate,RiskExposure,IsAssessID,IsAssess,Solutions,PersonLiableID,PersonLiable,PersonFollowUpID,PersonFollowUp,FollowUpRecord,ModifyDate,SortID,ISValid,CreatorID,Creator,CreateDate)");
			strSql.Append(" values (");
			strSql.Append("@ID,@LID,@LawName,@GradeID,@Grade,@LawsuitTypeID,@LawsuitType,@LawTypeID,@LawType,@DepartmentID,@Department,@PlaintiffID,@Plaintiff,@DefendantID,@Defendant,@TheThirdID,@TheThird,@CourtID,@Court,@CaseNo,@LawFirmID,@LawFirm,@FilingDate,@ClosingDate,@LawStatusID,@LawStatus,@Describe,@Claims,@AmountInvolved,@AmountueDU,@Compensation,@SStopLoss,@AStopLoss,@StopLossRate,@RiskExposure,@IsAssessID,@IsAssess,@Solutions,@PersonLiableID,@PersonLiable,@PersonFollowUpID,@PersonFollowUp,@FollowUpRecord,@ModifyDate,@SortID,@ISValid,@CreatorID,@Creator,@CreateDate)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LID", SqlDbType.VarChar,100),
					new SqlParameter("@LawName", SqlDbType.VarChar,100),
					new SqlParameter("@GradeID", SqlDbType.Int,4),
					new SqlParameter("@Grade", SqlDbType.VarChar,30),
					new SqlParameter("@LawsuitTypeID", SqlDbType.Int,4),
					new SqlParameter("@LawsuitType", SqlDbType.VarChar,30),
					new SqlParameter("@LawTypeID", SqlDbType.Int,4),
					new SqlParameter("@LawType", SqlDbType.VarChar,30),
					new SqlParameter("@DepartmentID", SqlDbType.VarChar,100),
					new SqlParameter("@Department", SqlDbType.VarChar,30),
					new SqlParameter("@PlaintiffID", SqlDbType.VarChar,100),
					new SqlParameter("@Plaintiff", SqlDbType.VarChar,30),
					new SqlParameter("@DefendantID", SqlDbType.VarChar,100),
					new SqlParameter("@Defendant", SqlDbType.VarChar,30),
					new SqlParameter("@TheThirdID", SqlDbType.VarChar,100),
					new SqlParameter("@TheThird", SqlDbType.VarChar,30),
					new SqlParameter("@CourtID", SqlDbType.VarChar,100),
					new SqlParameter("@Court", SqlDbType.VarChar,30),
					new SqlParameter("@CaseNo", SqlDbType.VarChar,100),
					new SqlParameter("@LawFirmID", SqlDbType.VarChar,100),
					new SqlParameter("@LawFirm", SqlDbType.VarChar,30),
					new SqlParameter("@FilingDate", SqlDbType.DateTime),
					new SqlParameter("@ClosingDate", SqlDbType.DateTime),
					new SqlParameter("@LawStatusID", SqlDbType.Int,4),
					new SqlParameter("@LawStatus", SqlDbType.VarChar,30),
					new SqlParameter("@Describe", SqlDbType.VarChar,-1),
					new SqlParameter("@Claims", SqlDbType.VarChar,-1),
					new SqlParameter("@AmountInvolved", SqlDbType.Decimal,9),
					new SqlParameter("@AmountueDU", SqlDbType.Decimal,9),
					new SqlParameter("@Compensation", SqlDbType.Decimal,9),
					new SqlParameter("@SStopLoss", SqlDbType.Decimal,9),
					new SqlParameter("@AStopLoss", SqlDbType.Decimal,9),
					new SqlParameter("@StopLossRate", SqlDbType.VarChar,10),
					new SqlParameter("@RiskExposure", SqlDbType.Decimal,9),
					new SqlParameter("@IsAssessID", SqlDbType.Int,4),
					new SqlParameter("@IsAssess", SqlDbType.VarChar,5),
					new SqlParameter("@Solutions", SqlDbType.VarChar,-1),
					new SqlParameter("@PersonLiableID", SqlDbType.VarChar,100),
					new SqlParameter("@PersonLiable", SqlDbType.VarChar,10),
					new SqlParameter("@PersonFollowUpID", SqlDbType.VarChar,100),
					new SqlParameter("@PersonFollowUp", SqlDbType.VarChar,10),
					new SqlParameter("@FollowUpRecord", SqlDbType.VarChar,-1),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@SortID", SqlDbType.Int,4),
					new SqlParameter("@ISValid", SqlDbType.Int,4),
					new SqlParameter("@CreatorID", SqlDbType.VarChar,100),
					new SqlParameter("@Creator", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.LID;
			parameters[2].Value = model.LawName;
			parameters[3].Value = model.GradeID;
			parameters[4].Value = model.Grade;
			parameters[5].Value = model.LawsuitTypeID;
			parameters[6].Value = model.LawsuitType;
			parameters[7].Value = model.LawTypeID;
			parameters[8].Value = model.LawType;
			parameters[9].Value = model.DepartmentID;
			parameters[10].Value = model.Department;
			parameters[11].Value = model.PlaintiffID;
			parameters[12].Value = model.Plaintiff;
			parameters[13].Value = model.DefendantID;
			parameters[14].Value = model.Defendant;
			parameters[15].Value = model.TheThirdID;
			parameters[16].Value = model.TheThird;
			parameters[17].Value = model.CourtID;
			parameters[18].Value = model.Court;
			parameters[19].Value = model.CaseNo;
			parameters[20].Value = model.LawFirmID;
			parameters[21].Value = model.LawFirm;
			parameters[22].Value = model.FilingDate;
			parameters[23].Value = model.ClosingDate;
			parameters[24].Value = model.LawStatusID;
			parameters[25].Value = model.LawStatus;
			parameters[26].Value = model.Describe;
			parameters[27].Value = model.Claims;
			parameters[28].Value = model.AmountInvolved;
			parameters[29].Value = model.AmountueDU;
			parameters[30].Value = model.Compensation;
			parameters[31].Value = model.SStopLoss;
			parameters[32].Value = model.AStopLoss;
			parameters[33].Value = model.StopLossRate;
			parameters[34].Value = model.RiskExposure;
			parameters[35].Value = model.IsAssessID;
			parameters[36].Value = model.IsAssess;
			parameters[37].Value = model.Solutions;
			parameters[38].Value = model.PersonLiableID;
			parameters[39].Value = model.PersonLiable;
			parameters[40].Value = model.PersonFollowUpID;
			parameters[41].Value = model.PersonFollowUp;
			parameters[42].Value = model.FollowUpRecord;
			parameters[43].Value = model.ModifyDate;
			parameters[44].Value = model.SortID;
			parameters[45].Value = model.ISValid;
			parameters[46].Value = model.CreatorID;
			parameters[47].Value = model.Creator;
			parameters[48].Value = model.CreateDate;

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
		public bool Update(LDFW.Model.FW_LawInfoModifyLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update FW_LawInfoModifyLog set ");
			strSql.Append("ID=@ID,");
			strSql.Append("LID=@LID,");
			strSql.Append("LawName=@LawName,");
			strSql.Append("GradeID=@GradeID,");
			strSql.Append("Grade=@Grade,");
			strSql.Append("LawsuitTypeID=@LawsuitTypeID,");
			strSql.Append("LawsuitType=@LawsuitType,");
			strSql.Append("LawTypeID=@LawTypeID,");
			strSql.Append("LawType=@LawType,");
			strSql.Append("DepartmentID=@DepartmentID,");
			strSql.Append("Department=@Department,");
			strSql.Append("PlaintiffID=@PlaintiffID,");
			strSql.Append("Plaintiff=@Plaintiff,");
			strSql.Append("DefendantID=@DefendantID,");
			strSql.Append("Defendant=@Defendant,");
			strSql.Append("TheThirdID=@TheThirdID,");
			strSql.Append("TheThird=@TheThird,");
			strSql.Append("CourtID=@CourtID,");
			strSql.Append("Court=@Court,");
			strSql.Append("CaseNo=@CaseNo,");
			strSql.Append("LawFirmID=@LawFirmID,");
			strSql.Append("LawFirm=@LawFirm,");
			strSql.Append("FilingDate=@FilingDate,");
			strSql.Append("ClosingDate=@ClosingDate,");
			strSql.Append("LawStatusID=@LawStatusID,");
			strSql.Append("LawStatus=@LawStatus,");
			strSql.Append("Describe=@Describe,");
			strSql.Append("Claims=@Claims,");
			strSql.Append("AmountInvolved=@AmountInvolved,");
			strSql.Append("AmountueDU=@AmountueDU,");
			strSql.Append("Compensation=@Compensation,");
			strSql.Append("SStopLoss=@SStopLoss,");
			strSql.Append("AStopLoss=@AStopLoss,");
			strSql.Append("StopLossRate=@StopLossRate,");
			strSql.Append("RiskExposure=@RiskExposure,");
			strSql.Append("IsAssessID=@IsAssessID,");
			strSql.Append("IsAssess=@IsAssess,");
			strSql.Append("Solutions=@Solutions,");
			strSql.Append("PersonLiableID=@PersonLiableID,");
			strSql.Append("PersonLiable=@PersonLiable,");
			strSql.Append("PersonFollowUpID=@PersonFollowUpID,");
			strSql.Append("PersonFollowUp=@PersonFollowUp,");
			strSql.Append("FollowUpRecord=@FollowUpRecord,");
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
					new SqlParameter("@LawName", SqlDbType.VarChar,100),
					new SqlParameter("@GradeID", SqlDbType.Int,4),
					new SqlParameter("@Grade", SqlDbType.VarChar,30),
					new SqlParameter("@LawsuitTypeID", SqlDbType.Int,4),
					new SqlParameter("@LawsuitType", SqlDbType.VarChar,30),
					new SqlParameter("@LawTypeID", SqlDbType.Int,4),
					new SqlParameter("@LawType", SqlDbType.VarChar,30),
					new SqlParameter("@DepartmentID", SqlDbType.VarChar,100),
					new SqlParameter("@Department", SqlDbType.VarChar,30),
					new SqlParameter("@PlaintiffID", SqlDbType.VarChar,100),
					new SqlParameter("@Plaintiff", SqlDbType.VarChar,30),
					new SqlParameter("@DefendantID", SqlDbType.VarChar,100),
					new SqlParameter("@Defendant", SqlDbType.VarChar,30),
					new SqlParameter("@TheThirdID", SqlDbType.VarChar,100),
					new SqlParameter("@TheThird", SqlDbType.VarChar,30),
					new SqlParameter("@CourtID", SqlDbType.VarChar,100),
					new SqlParameter("@Court", SqlDbType.VarChar,30),
					new SqlParameter("@CaseNo", SqlDbType.VarChar,100),
					new SqlParameter("@LawFirmID", SqlDbType.VarChar,100),
					new SqlParameter("@LawFirm", SqlDbType.VarChar,30),
					new SqlParameter("@FilingDate", SqlDbType.DateTime),
					new SqlParameter("@ClosingDate", SqlDbType.DateTime),
					new SqlParameter("@LawStatusID", SqlDbType.Int,4),
					new SqlParameter("@LawStatus", SqlDbType.VarChar,30),
					new SqlParameter("@Describe", SqlDbType.VarChar,-1),
					new SqlParameter("@Claims", SqlDbType.VarChar,-1),
					new SqlParameter("@AmountInvolved", SqlDbType.Decimal,9),
					new SqlParameter("@AmountueDU", SqlDbType.Decimal,9),
					new SqlParameter("@Compensation", SqlDbType.Decimal,9),
					new SqlParameter("@SStopLoss", SqlDbType.Decimal,9),
					new SqlParameter("@AStopLoss", SqlDbType.Decimal,9),
					new SqlParameter("@StopLossRate", SqlDbType.VarChar,10),
					new SqlParameter("@RiskExposure", SqlDbType.Decimal,9),
					new SqlParameter("@IsAssessID", SqlDbType.Int,4),
					new SqlParameter("@IsAssess", SqlDbType.VarChar,5),
					new SqlParameter("@Solutions", SqlDbType.VarChar,-1),
					new SqlParameter("@PersonLiableID", SqlDbType.VarChar,100),
					new SqlParameter("@PersonLiable", SqlDbType.VarChar,10),
					new SqlParameter("@PersonFollowUpID", SqlDbType.VarChar,100),
					new SqlParameter("@PersonFollowUp", SqlDbType.VarChar,10),
					new SqlParameter("@FollowUpRecord", SqlDbType.VarChar,-1),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@SortID", SqlDbType.Int,4),
					new SqlParameter("@ISValid", SqlDbType.Int,4),
					new SqlParameter("@CreatorID", SqlDbType.VarChar,100),
					new SqlParameter("@Creator", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.LID;
			parameters[2].Value = model.LawName;
			parameters[3].Value = model.GradeID;
			parameters[4].Value = model.Grade;
			parameters[5].Value = model.LawsuitTypeID;
			parameters[6].Value = model.LawsuitType;
			parameters[7].Value = model.LawTypeID;
			parameters[8].Value = model.LawType;
			parameters[9].Value = model.DepartmentID;
			parameters[10].Value = model.Department;
			parameters[11].Value = model.PlaintiffID;
			parameters[12].Value = model.Plaintiff;
			parameters[13].Value = model.DefendantID;
			parameters[14].Value = model.Defendant;
			parameters[15].Value = model.TheThirdID;
			parameters[16].Value = model.TheThird;
			parameters[17].Value = model.CourtID;
			parameters[18].Value = model.Court;
			parameters[19].Value = model.CaseNo;
			parameters[20].Value = model.LawFirmID;
			parameters[21].Value = model.LawFirm;
			parameters[22].Value = model.FilingDate;
			parameters[23].Value = model.ClosingDate;
			parameters[24].Value = model.LawStatusID;
			parameters[25].Value = model.LawStatus;
			parameters[26].Value = model.Describe;
			parameters[27].Value = model.Claims;
			parameters[28].Value = model.AmountInvolved;
			parameters[29].Value = model.AmountueDU;
			parameters[30].Value = model.Compensation;
			parameters[31].Value = model.SStopLoss;
			parameters[32].Value = model.AStopLoss;
			parameters[33].Value = model.StopLossRate;
			parameters[34].Value = model.RiskExposure;
			parameters[35].Value = model.IsAssessID;
			parameters[36].Value = model.IsAssess;
			parameters[37].Value = model.Solutions;
			parameters[38].Value = model.PersonLiableID;
			parameters[39].Value = model.PersonLiable;
			parameters[40].Value = model.PersonFollowUpID;
			parameters[41].Value = model.PersonFollowUp;
			parameters[42].Value = model.FollowUpRecord;
			parameters[43].Value = model.ModifyDate;
			parameters[44].Value = model.SortID;
			parameters[45].Value = model.ISValid;
			parameters[46].Value = model.CreatorID;
			parameters[47].Value = model.Creator;
			parameters[48].Value = model.CreateDate;

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
			strSql.Append("delete from FW_LawInfoModifyLog ");
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
		public LDFW.Model.FW_LawInfoModifyLog GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,LID,LawName,GradeID,Grade,LawsuitTypeID,LawsuitType,LawTypeID,LawType,DepartmentID,Department,PlaintiffID,Plaintiff,DefendantID,Defendant,TheThirdID,TheThird,CourtID,Court,CaseNo,LawFirmID,LawFirm,FilingDate,ClosingDate,LawStatusID,LawStatus,Describe,Claims,AmountInvolved,AmountueDU,Compensation,SStopLoss,AStopLoss,StopLossRate,RiskExposure,IsAssessID,IsAssess,Solutions,PersonLiableID,PersonLiable,PersonFollowUpID,PersonFollowUp,FollowUpRecord,ModifyDate,SortID,ISValid,CreatorID,Creator,CreateDate from FW_LawInfoModifyLog ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			LDFW.Model.FW_LawInfoModifyLog model=new LDFW.Model.FW_LawInfoModifyLog();
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
		public LDFW.Model.FW_LawInfoModifyLog DataRowToModel(DataRow row)
		{
			LDFW.Model.FW_LawInfoModifyLog model=new LDFW.Model.FW_LawInfoModifyLog();
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
				if(row["LawName"]!=null)
				{
					model.LawName=row["LawName"].ToString();
				}
				if(row["GradeID"]!=null && row["GradeID"].ToString()!="")
				{
					model.GradeID=int.Parse(row["GradeID"].ToString());
				}
				if(row["Grade"]!=null)
				{
					model.Grade=row["Grade"].ToString();
				}
				if(row["LawsuitTypeID"]!=null && row["LawsuitTypeID"].ToString()!="")
				{
					model.LawsuitTypeID=int.Parse(row["LawsuitTypeID"].ToString());
				}
				if(row["LawsuitType"]!=null)
				{
					model.LawsuitType=row["LawsuitType"].ToString();
				}
				if(row["LawTypeID"]!=null && row["LawTypeID"].ToString()!="")
				{
					model.LawTypeID=int.Parse(row["LawTypeID"].ToString());
				}
				if(row["LawType"]!=null)
				{
					model.LawType=row["LawType"].ToString();
				}
				if(row["DepartmentID"]!=null)
				{
					model.DepartmentID=row["DepartmentID"].ToString();
				}
				if(row["Department"]!=null)
				{
					model.Department=row["Department"].ToString();
				}
				if(row["PlaintiffID"]!=null)
				{
					model.PlaintiffID=row["PlaintiffID"].ToString();
				}
				if(row["Plaintiff"]!=null)
				{
					model.Plaintiff=row["Plaintiff"].ToString();
				}
				if(row["DefendantID"]!=null)
				{
					model.DefendantID=row["DefendantID"].ToString();
				}
				if(row["Defendant"]!=null)
				{
					model.Defendant=row["Defendant"].ToString();
				}
				if(row["TheThirdID"]!=null)
				{
					model.TheThirdID=row["TheThirdID"].ToString();
				}
				if(row["TheThird"]!=null)
				{
					model.TheThird=row["TheThird"].ToString();
				}
				if(row["CourtID"]!=null)
				{
					model.CourtID=row["CourtID"].ToString();
				}
				if(row["Court"]!=null)
				{
					model.Court=row["Court"].ToString();
				}
				if(row["CaseNo"]!=null)
				{
					model.CaseNo=row["CaseNo"].ToString();
				}
				if(row["LawFirmID"]!=null)
				{
					model.LawFirmID=row["LawFirmID"].ToString();
				}
				if(row["LawFirm"]!=null)
				{
					model.LawFirm=row["LawFirm"].ToString();
				}
				if(row["FilingDate"]!=null && row["FilingDate"].ToString()!="")
				{
					model.FilingDate=DateTime.Parse(row["FilingDate"].ToString());
				}
				if(row["ClosingDate"]!=null && row["ClosingDate"].ToString()!="")
				{
					model.ClosingDate=DateTime.Parse(row["ClosingDate"].ToString());
				}
				if(row["LawStatusID"]!=null && row["LawStatusID"].ToString()!="")
				{
					model.LawStatusID=int.Parse(row["LawStatusID"].ToString());
				}
				if(row["LawStatus"]!=null)
				{
					model.LawStatus=row["LawStatus"].ToString();
				}
				if(row["Describe"]!=null)
				{
					model.Describe=row["Describe"].ToString();
				}
				if(row["Claims"]!=null)
				{
					model.Claims=row["Claims"].ToString();
				}
				if(row["AmountInvolved"]!=null && row["AmountInvolved"].ToString()!="")
				{
					model.AmountInvolved=decimal.Parse(row["AmountInvolved"].ToString());
				}
				if(row["AmountueDU"]!=null && row["AmountueDU"].ToString()!="")
				{
					model.AmountueDU=decimal.Parse(row["AmountueDU"].ToString());
				}
				if(row["Compensation"]!=null && row["Compensation"].ToString()!="")
				{
					model.Compensation=decimal.Parse(row["Compensation"].ToString());
				}
				if(row["SStopLoss"]!=null && row["SStopLoss"].ToString()!="")
				{
					model.SStopLoss=decimal.Parse(row["SStopLoss"].ToString());
				}
				if(row["AStopLoss"]!=null && row["AStopLoss"].ToString()!="")
				{
					model.AStopLoss=decimal.Parse(row["AStopLoss"].ToString());
				}
				if(row["StopLossRate"]!=null)
				{
					model.StopLossRate=row["StopLossRate"].ToString();
				}
				if(row["RiskExposure"]!=null && row["RiskExposure"].ToString()!="")
				{
					model.RiskExposure=decimal.Parse(row["RiskExposure"].ToString());
				}
				if(row["IsAssessID"]!=null && row["IsAssessID"].ToString()!="")
				{
					model.IsAssessID=int.Parse(row["IsAssessID"].ToString());
				}
				if(row["IsAssess"]!=null)
				{
					model.IsAssess=row["IsAssess"].ToString();
				}
				if(row["Solutions"]!=null)
				{
					model.Solutions=row["Solutions"].ToString();
				}
				if(row["PersonLiableID"]!=null)
				{
					model.PersonLiableID=row["PersonLiableID"].ToString();
				}
				if(row["PersonLiable"]!=null)
				{
					model.PersonLiable=row["PersonLiable"].ToString();
				}
				if(row["PersonFollowUpID"]!=null)
				{
					model.PersonFollowUpID=row["PersonFollowUpID"].ToString();
				}
				if(row["PersonFollowUp"]!=null)
				{
					model.PersonFollowUp=row["PersonFollowUp"].ToString();
				}
				if(row["FollowUpRecord"]!=null)
				{
					model.FollowUpRecord=row["FollowUpRecord"].ToString();
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
			strSql.Append("select ID,LID,LawName,GradeID,Grade,LawsuitTypeID,LawsuitType,LawTypeID,LawType,DepartmentID,Department,PlaintiffID,Plaintiff,DefendantID,Defendant,TheThirdID,TheThird,CourtID,Court,CaseNo,LawFirmID,LawFirm,FilingDate,ClosingDate,LawStatusID,LawStatus,Describe,Claims,AmountInvolved,AmountueDU,Compensation,SStopLoss,AStopLoss,StopLossRate,RiskExposure,IsAssessID,IsAssess,Solutions,PersonLiableID,PersonLiable,PersonFollowUpID,PersonFollowUp,FollowUpRecord,ModifyDate,SortID,ISValid,CreatorID,Creator,CreateDate ");
			strSql.Append(" FROM FW_LawInfoModifyLog ");
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
			strSql.Append(" ID,LID,LawName,GradeID,Grade,LawsuitTypeID,LawsuitType,LawTypeID,LawType,DepartmentID,Department,PlaintiffID,Plaintiff,DefendantID,Defendant,TheThirdID,TheThird,CourtID,Court,CaseNo,LawFirmID,LawFirm,FilingDate,ClosingDate,LawStatusID,LawStatus,Describe,Claims,AmountInvolved,AmountueDU,Compensation,SStopLoss,AStopLoss,StopLossRate,RiskExposure,IsAssessID,IsAssess,Solutions,PersonLiableID,PersonLiable,PersonFollowUpID,PersonFollowUp,FollowUpRecord,ModifyDate,SortID,ISValid,CreatorID,Creator,CreateDate ");
			strSql.Append(" FROM FW_LawInfoModifyLog ");
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
			strSql.Append("select count(1) FROM FW_LawInfoModifyLog ");
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
			strSql.Append(")AS Row, T.*  from FW_LawInfoModifyLog T ");
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
			parameters[0].Value = "FW_LawInfoModifyLog";
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

