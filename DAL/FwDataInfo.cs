using LDFW.Common;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace LDFW.DAL
{
    public class FwDataInfo
    {
        //创建个人/公司库
        public int TransactionCreateCompan(string Company, string Telephone, string Phone, string Contacts, string Address, string s, string Creator, ref int rCount, ref string errorInfo)
        {

            StringBuilder strSql = new StringBuilder();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection89"].ConnectionString);
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;
            try
            {

                string id = Guid.NewGuid().ToString();
                strSql = new StringBuilder();

                strSql.Append($"insert into FW_PersonalCompany (ID,Company,Telephone,Phone,Contacts,Address,UserName) values('{id}','{Company}','{Telephone}','{Phone}','{Contacts}','{Address}','{Creator}')");

                //rCount += DbHelperSQL.ExecuteSql(strSql.ToString());
                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();

                string TypeName = string.Empty;

                string[] TypeID = s.Split(',');
                for (int i = 0; i < TypeID.Length; i++)
                {
                    if (TypeID[i] == "1")
                    {
                        TypeName = "原告";
                    }
                    if (TypeID[i] == "2")
                    {
                        TypeName = "被告";
                    }
                    if (TypeID[i] == "3")
                    {
                        TypeName = "第3人";
                    }
                    if (TypeID[i] == "4")
                    {
                        TypeName = "律所";
                    }
                    if (TypeID[i] == "5")
                    {
                        TypeName = "律师";
                    }
                    if (TypeID[i] == "6")
                    {
                        TypeName = "其他";
                    }


                    strSql = new StringBuilder();

                    strSql.Append($"insert into FW_PCType (PCID,TypeID,TypeName,UserName) values('{id}','{TypeID[i]}','{TypeName}','{Creator}')");

                    //rCount += DbHelperSQL.ExecuteSql(strSql.ToString());
                    comm.CommandText = strSql.ToString();
                    rCount += comm.ExecuteNonQuery();

                }
                //#endregion
                tran.Commit();
                conn.Close();
                return 1;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = strSql.ToString();
                conn.Close();
                return 0;
            }


        }

        //添加案件
        public int TransactionCreateLawAdd(LDFW.Model.FW_LawInfo fwinfo, List<LDFW.Model.FW_LitigationPreservation> fW_Litigation, List<LDFW.Model.FW_LegalCosts> fW_Legal, List<LDFW.Model.FW_LawyerCosts> fW_Lawyers, List<LDFW.Model.FW_LawFiles> fW_file, string types, string CreateID, string Create, ref int rCount, ref string errorInfo)
        {

            StringBuilder strSql = new StringBuilder();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection89"].ConnectionString);
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;
            try
            {
                if (fwinfo.ActualPayment == 0 && fwinfo.Receivables == 0)
                {
                    fwinfo.CollectionRate = 0;
                }
                else
                {
                    fwinfo.CollectionRate = Math.Round((decimal)fwinfo.ActualPayment / (decimal)fwinfo.Receivables * 100, 2);
                }
                if (fwinfo.AStopLoss == 0 && fwinfo.SStopLoss == 0)
                {
                    fwinfo.StopLossRate = 0;
                }
                else
                {
                    fwinfo.StopLossRate = Convert.ToDecimal(Math.Round((decimal)fwinfo.AStopLoss / (decimal)fwinfo.SStopLoss * 100, 2));
                }
                //添加案件
                string LID = Guid.NewGuid().ToString();

                strSql = new StringBuilder();
                strSql.Append($"insert into FW_LawInfo(");
                strSql.Append($"ID,LawName,Grade,LawsuitType,LawType,Department,Plaintiff,Defendant,");
                strSql.Append($"TheThird,Court,CaseNo,LawFirm,Lawyer,FilingDate,ClosingDate,LawStatus,Describe,Claims,AmountInvolved,Judgment,Receivables,ActualPayment,CollectionRate,AmountueDU,");
                strSql.Append($"SStopLoss,AStopLoss,StopLossRate,RiskExposure,IsAssess,Solutions,PersonLiable,PersonFollowUp,FollowUpRecord,");
                strSql.Append($"CreatorID,Creator,ApproveStatus) ");
                strSql.Append($"values (");
                strSql.Append($"'{LID}','{fwinfo.LawName}','{fwinfo.Grade}','{fwinfo.LawsuitType}','{fwinfo.LawType}','{fwinfo.Department}','{fwinfo.Plaintiff}','{fwinfo.Defendant}',");
                strSql.Append($"'{fwinfo.TheThird}','{fwinfo.Court}','{fwinfo.CaseNo}','{fwinfo.LawFirm}','{fwinfo.Llawyer}','{fwinfo.FilingDate}','{fwinfo.ClosingDate}','{fwinfo.LawStatus}','{fwinfo.Describe}','{fwinfo.Claims}',{fwinfo.AmountInvolved},{fwinfo.Judgment},{fwinfo.Receivables},{fwinfo.ActualPayment},{fwinfo.CollectionRate},{fwinfo.AmountueDU},");
                strSql.Append($"{fwinfo.SStopLoss},{fwinfo.AStopLoss},'{fwinfo.StopLossRate}',{fwinfo.RiskExposure},'{fwinfo.IsAssess}','{fwinfo.Solutions}','{fwinfo.PersonLiable}','{fwinfo.PersonFollowUp}','{fwinfo.FollowUpRecord}',");
                strSql.Append($"'{CreateID}','{Create}',1) ");
                //strSql.Append($"insert into FW_PersonalCompany (ID,Company,Telephone,Phone,Contacts,Address,UserName) values('{id}','{Company}','{Telephone}','{Phone}','{Contacts}','{Address}','{Creator}')");
                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();


                //添加跟进记录信息
                strSql = new StringBuilder();
                strSql.Append($"insert into FW_FollowUp(");
                strSql.Append($"LID,FollowUpDate,LawType,PersonFollowUp,FollowUpInfo,NextDate,FUStatus,PersonLiable,Solutions,");
                strSql.Append($"CreatorID,Creator) ");
                strSql.Append($"values (");
                strSql.Append($"'{LID}','{fwinfo.FollowUpDate}','案件常规跟进','{fwinfo.PersonFollowUp}','{fwinfo.FollowUpRecord}','{fwinfo.NextDate}','{fwinfo.FUStatus}','{fwinfo.PersonLiable}','{fwinfo.Solutions}',");
                strSql.Append($"'{CreateID}','{Create}') ");
                //strSql.Append($"insert into FW_PersonalCompany (ID,Company,Telephone,Phone,Contacts,Address,UserName) values('{id}','{Company}','{Telephone}','{Phone}','{Contacts}','{Address}','{Creator}')");
                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();

                //添加诉讼保全信息
                for (int i = 0; i < fW_Litigation.Count; i++)
                {

                    if (fW_Litigation[i].PCost != null && fW_Litigation[i].LPDate != null)
                    {
                        decimal d;
                        if (fW_Litigation[i].PCost != null)
                        {
                            if (!decimal.TryParse(fW_Litigation[i].PCost.ToString(), out d))
                            {
                                return 2;
                            }
                        }

                        strSql = new StringBuilder();
                        strSql.Append($"insert into FW_LitigationPreservation(");
                        strSql.Append($"LID,Applicant,Respondent,LPCourt,LPDate,PCost,PInformation,CreatorID,Creator)");
                        strSql.Append($" values (");
                        strSql.Append($"'{LID}','{fW_Litigation[i].Applicant}','{fW_Litigation[i].Respondent}','{fW_Litigation[i].LPCourt}','{fW_Litigation[i].LPDate}',{fW_Litigation[i].PCost},'{fW_Litigation[i].PInformation}','{CreateID}','{Create}')");
                        comm.CommandText = strSql.ToString();
                        rCount += comm.ExecuteNonQuery();
                    }
                }

                //添加诉讼费用
                for (int i = 0; i < fW_Legal.Count; i++)
                {
                    if (fW_Legal[i].LegalCosts != null && fW_Legal[i].LCPaymentDate != null)
                    {

                        decimal d;
                        if (fW_Legal[i].LegalCosts != null)
                        {
                            if (!decimal.TryParse(fW_Legal[i].LegalCosts.ToString(), out d))
                            {
                                return 3;
                            }
                        }

                        strSql = new StringBuilder();
                        strSql.Append($"insert into FW_LegalCosts(");
                        strSql.Append($"LID,LCType,LegalCosts,LCPaymentDate,CreatorID,Creator)");
                        strSql.Append($" values (");
                        strSql.Append($"'{LID}','{fW_Legal[i].LCType}',{fW_Legal[i].LegalCosts},'{fW_Legal[i].LCPaymentDate}','{CreateID}','{Create}')");
                        comm.CommandText = strSql.ToString();
                        rCount += comm.ExecuteNonQuery();
                    }
                }
                //添加律师费用

                for (int i = 0; i < fW_Lawyers.Count; i++)
                {
                    if (fW_Lawyers[i].AttorneyFees != null && fW_Lawyers[i].LawyerPaymentDate != null)
                    {
                        decimal d;
                        if (fW_Lawyers[i].AttorneyFees != null)
                        {
                            if (!decimal.TryParse(fW_Lawyers[i].AttorneyFees.ToString(), out d))
                            {
                                return 4;
                            }
                        }

                        strSql = new StringBuilder();
                        strSql.Append($"insert into FW_LawyerCosts(");
                        strSql.Append($"LID,LawyerType,AttorneyFees,LawyerPaymentDate,CreatorID,Creator)");
                        strSql.Append($" values (");
                        strSql.Append($"'{LID}','{fW_Lawyers[i].LawyerType}',{fW_Lawyers[i].AttorneyFees},'{fW_Lawyers[i].LawyerPaymentDate}','{CreateID}','{Create}')");
                        comm.CommandText = strSql.ToString();
                        rCount += comm.ExecuteNonQuery();
                    }
                }
                if (fW_file.Count > 0)
                {
                    if (types != "0")
                    {
                        string types1 = types.Substring(2);
                        types = types1.Replace("\"", "");
                        var fieltype = types.Split(',');
                        //添加附件信息
                        for (int i = 0; i < fW_file.Count; i++)
                        {
                            if (fieltype[i] != "")
                            {
                                strSql = new StringBuilder();
                                strSql.Append($"insert into FW_LawFiles(");
                                strSql.Append($"LID,FileName,FilePath,FileFormat,FileSize,FileType,CreatorID,Creator)");
                                strSql.Append($" values (");
                                strSql.Append($"'{LID}','{fW_file[i].FileName}','{fW_file[i].FilePath}','{fW_file[i].FileFormat}',{fW_file[i].FileSize},'{fieltype[i]}','{CreateID}','{Create}')");
                                comm.CommandText = strSql.ToString();
                                rCount += comm.ExecuteNonQuery();
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                tran.Commit();
                conn.Close();
                return 1;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = strSql.ToString();
                conn.Close();
                return 5;
            }
            finally
            {
                conn.Close();
            }

        }

        //修改案件
        public int TransactionCreateLawUpdate(DataTable dt, DataTable dtLitig, DataTable dtLawyerr, DataTable dtLega, DataTable dtFile, LDFW.Model.FW_LawInfo fwinfo, List<LDFW.Model.FW_LitigationPreservation> fW_Litigation, List<LDFW.Model.FW_LawyerCosts> fW_LawyerCost, List<LDFW.Model.FW_LegalCosts> fW_Legal, List<LDFW.Model.FW_LawFiles> fW_file, string ID, string types, string CreateID, string Create, ref int rCount, ref string errorInfo)
        {

            StringBuilder strSql = new StringBuilder();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection89"].ConnectionString);
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;
            try
            {
                #region 添加日志
                //添加案件日志
                string RID = Guid.NewGuid().ToString();
                strSql = new StringBuilder();
                strSql.Append($"insert into FW_LawInfoModifyLog( ");
                strSql.Append($"ID,LID,LawName,Grade,LawsuitType,LawType,Department,Plaintiff,Defendant,");
                strSql.Append($"TheThird,Court,CaseNo,LawFirm,Lawyer,FilingDate,ClosingDate,LawStatus,Describe,Claims,AmountInvolved,Judgment,ActualPayment,CollectionRate,");
                strSql.Append($"AStopLoss,StopLossRate,RiskExposure,IsAssess,Solutions,PersonLiable,");
                strSql.Append($"CreatorID,Creator,VentureFenxi,Prediction,SettleType,TrialState,CounselFee,LegalFee,ResidueFee,Impairments,IsPremium,PremiumDate,IsDeblocking,DeblockingDate,SettleResult, ");
                strSql.Append($"InterestMoney,SecurityMoney,ObjectAction,Maintenancefee,LawUserName,LawUserNameID ) ");
                strSql.Append($"values (");                                             
                strSql.Append($"'{RID}','{ID}','{dt.Rows[0]["LawName"]}','{dt.Rows[0]["Grade"]}','{dt.Rows[0]["LawsuitType"]}','{dt.Rows[0]["LawType"]}','{dt.Rows[0]["Department"]}','{dt.Rows[0]["Plaintiff"]}','{dt.Rows[0]["Defendant"]}',");
                strSql.Append($"'{dt.Rows[0]["TheThird"]}','{dt.Rows[0]["Court"]}','{dt.Rows[0]["CaseNo"]}','{dt.Rows[0]["LawFirm"]}','{dt.Rows[0]["Lawyer"]}','{dt.Rows[0]["FilingDate"]}','{dt.Rows[0]["ClosingDate"]}','{dt.Rows[0]["LawStatus"]}','{dt.Rows[0]["Describe"]}','{dt.Rows[0]["Claims"]}',{dt.Rows[0]["AmountInvolved"]},{dt.Rows[0]["Judgment"]},{dt.Rows[0]["ActualPayment"]},{dt.Rows[0]["CollectionRate"]},");
                strSql.Append($"{dt.Rows[0]["AStopLoss"]},'{dt.Rows[0]["StopLossRate"]}',{dt.Rows[0]["RiskExposure"]},'{dt.Rows[0]["IsAssess"]}','{dt.Rows[0]["Solutions"]}','{dt.Rows[0]["PersonLiable"]}',");
                strSql.Append($"'{CreateID}','{Create}','{dt.Rows[0]["VentureFenxi"]}','{dt.Rows[0]["Prediction"]}','{dt.Rows[0]["SettleType"]}','{dt.Rows[0]["TrialState"]}',{dt.Rows[0]["CounselFee"]},{dt.Rows[0]["LegalFee"]},{dt.Rows[0]["ResidueFee"]},{dt.Rows[0]["Impairments"]},'{dt.Rows[0]["IsPremium"]}','{dt.Rows[0]["PremiumDate"]}','{dt.Rows[0]["IsDeblocking"]}','{dt.Rows[0]["DeblockingDate"]}','{dt.Rows[0]["SettleResult"]}',{dt.Rows[0]["InterestMoney"]},{dt.Rows[0]["SecurityMoney"]},'{dt.Rows[0]["ObjectAction"]}',{dt.Rows[0]["Maintenancefee"]},'{dt.Rows[0]["LawUserName"]}','{dt.Rows[0]["LawUserNameID"]}') ");
                //strSql.Append($"insert into FW_PersonalCompany (ID,Company,Telephone,Phone,Contacts,Address,UserName) values('{id}','{Company}','{Telephone}','{Phone}','{Contacts}','{Address}','{Creator}')");
                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();

                strSql = new StringBuilder();
                strSql.Append($"insert into FW_FollowUp(");
                strSql.Append($"LID,LogInfoID,FollowUpDate,LawType,PersonFollowUp,FUStatus,");
                strSql.Append($"CreatorID,Creator) ");
                strSql.Append($"values (");
                strSql.Append($"'{ID}','{RID}','{DateTime.Now}','信息更改跟进','{Create}','{dt.Rows[0]["LawStatus"]}',");
                strSql.Append($"'{CreateID}','{Create}') ");
                //rCount += DbHelperSQL.ExecuteSql(strSql.ToString());
                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();


                //添加诉讼保全信息日志
                for (int i = 0; i < dtLitig.Rows.Count; i++)
                {
                    if (dtLitig.Rows[0]["LPType"]==null)
                    {
                        dtLitig.Rows[0]["LPType"] = "";
                    }
                    if (dtLitig.Rows[0]["LPDataEnd"] == null)
                    {
                        dtLitig.Rows[0]["LPDataEnd"] = "";
                    }
                    strSql = new StringBuilder();
                    strSql.Append($"insert into FW_LitigationPreservationLog(");
                    strSql.Append($"LID,Applicant,Respondent,LPCourt,LPDate,PCost,PInformation,LPType,LPDataEnd,CreatorID,Creator)");
                    strSql.Append($" values (");
                    strSql.Append($"'{RID}','{dtLitig.Rows[i]["Applicant"]}','{dtLitig.Rows[i]["Respondent"]}','{dtLitig.Rows[i]["LPCourt"]}','{dtLitig.Rows[i]["LPDate"]}',{dtLitig.Rows[i]["PCost"]},'{dtLitig.Rows[i]["PInformation"]}','{dtLitig.Rows[i]["LPType"]}','{dtLitig.Rows[i]["LPDataEnd"]}','{CreateID}','{Create}')");
                    comm.CommandText = strSql.ToString();
                    rCount += comm.ExecuteNonQuery();
                }

                //添加律师费用日志
                for (int i = 0; i < dtLawyerr.Rows.Count; i++)
                {
                    strSql = new StringBuilder();
                    strSql.Append($"insert into FW_LawyerCostsModifyLog(");
                    strSql.Append($"LID,LawyerType,AttorneyFees,LawyerPaymentDate,CreatorID,Creator)");
                    strSql.Append($" values (");
                    strSql.Append($"'{RID}','{dtLawyerr.Rows[i]["LawyerType"]}','{dtLawyerr.Rows[i]["AttorneyFees"]}','{dtLawyerr.Rows[i]["LawyerPaymentDate"]}','{CreateID}','{Create}')");
                    comm.CommandText = strSql.ToString();
                    rCount += comm.ExecuteNonQuery();
                }
                //添加诉讼费用日志
                for (int i = 0; i < dtLega.Rows.Count; i++)
                {
                    strSql = new StringBuilder();
                    strSql.Append("insert into FW_LegalCostsModifyLog(");
                    strSql.Append("LID,LCType,LegalCosts,LCPaymentDate,CreatorID,Creator)");
                    strSql.Append(" values (");
                    strSql.Append($"'{RID}','{dtLega.Rows[i]["LCType"]}',{dtLega.Rows[i]["LegalCosts"]},'{dtLega.Rows[i]["LCPaymentDate"]}','{CreateID}','{Create}')");
                    comm.CommandText = strSql.ToString();
                    rCount += comm.ExecuteNonQuery();
                }



                if (fW_file.Count > 0)
                {
                    if (types != "0")
                    {
                        string types1 = types.Substring(2);
                        types = types1.Replace("\"", "");
                        var fieltype = types.Split(',');
                        //添加附件信息日志
                        for (int i = 0; i < fW_file.Count; i++)
                        {
                            if (fieltype[i] != "")
                            {


                                strSql = new StringBuilder();
                                strSql.Append($"insert into FW_LawFiles(");
                                strSql.Append($"LID,FileName,FilePath,FileFormat,FileSize,FileType,CreatorID,Creator)");
                                strSql.Append($" values (");
                                strSql.Append($"'{ID}','{fW_file[i].FileName}','{fW_file[i].FilePath}','{fW_file[i].FileFormat}',{fW_file[i].FileSize},'{fieltype[i]}','{CreateID}','{Create}')");
                                comm.CommandText = strSql.ToString();
                                rCount += comm.ExecuteNonQuery();
                            }
                            else
                            {
                                return 0;

                            }
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                #endregion




                #region 删除原表数据
                for (int i = 0; i < fW_Litigation.Count; i++)
                {
                    strSql = new StringBuilder();
                    strSql.Append($"delete from  FW_LitigationPreservation where LID='{ID}' ");
                    comm.CommandText = strSql.ToString();
                    rCount += comm.ExecuteNonQuery();

                }
                for (int i = 0; i < fW_LawyerCost.Count; i++)
                {
                    strSql = new StringBuilder();
                    strSql.Append($"delete from  FW_LawyerCosts where LID='{ID}' ");
                    comm.CommandText = strSql.ToString();
                    rCount += comm.ExecuteNonQuery();

                }

                for (int i = 0; i < fW_Legal.Count; i++)
                {
                    strSql = new StringBuilder();
                    strSql.Append($"delete from  FW_LegalCosts where LID='{ID}' ");
                    comm.CommandText = strSql.ToString();
                    rCount += comm.ExecuteNonQuery();

                }
                #endregion

                #region 增加新数据        
                for (int i = 0; i < fW_Litigation.Count; i++)
                {
                    if (fW_Litigation[i].LPDate==null)
                    {
                        fW_Litigation[i].LPDate = DateTime.Now;
                    }
                    if (fW_Litigation[i].PCost != null && fW_Litigation[i].LPDate != null)
                    {
                        fwinfo.Maintenancefee += Convert.ToDecimal(fW_Litigation[i].PCost);
                        fwinfo.RiskExposure = fwinfo.RiskExposure - Convert.ToDecimal(fW_Litigation[i].PCost);
                        if (fW_Litigation[i].IsPreservationID == 1)
                        {
                            fW_Litigation[i].IsPreservation = "是";
                        }
                        else
                        {
                            fW_Litigation[i].IsPreservation = "否";
                        }
                        decimal d;
                        if (!decimal.TryParse(fW_Litigation[i].PCost.ToString(), out d))
                        {
                            return 2;
                        }
                       else if (fW_Litigation[i].LPType.ToString()== "银行存款")
                        {
                            fW_Litigation[i].LPDataEnd = Convert.ToDateTime(fW_Litigation[i].LPDate).AddYears(1);
                        }
                      else  if (fW_Litigation[i].LPType.ToString() == "动产")
                        {
                            fW_Litigation[i].LPDataEnd = Convert.ToDateTime(fW_Litigation[i].LPDate).AddYears(2);
                        }
                       else if (fW_Litigation[i].LPType.ToString() == "其他财产权")
                        {
                            fW_Litigation[i].LPDataEnd = Convert.ToDateTime(fW_Litigation[i].LPDate).AddYears(3);
                        }
                        else
                        {
                            fW_Litigation[i].LPType = "";
                            fW_Litigation[i].LPDataEnd = Convert.ToDateTime(fW_Litigation[i].LPDate).AddYears(3);
                        }

                        strSql = new StringBuilder();
                        strSql.Append($"insert into FW_LitigationPreservation(");
                        strSql.Append($"LID,Applicant,Respondent,LPCourt,LPDate,PCost,PInformation,LPDataEnd,LPType,CreatorID,Creator)");
                        strSql.Append($" values (");
                        strSql.Append($"'{ID}','{fW_Litigation[i].Applicant}','{fW_Litigation[i].Respondent}','{fW_Litigation[i].LPCourt}','{fW_Litigation[i].LPDate}',{fW_Litigation[i].PCost},'{fW_Litigation[i].PInformation}','{fW_Litigation[i].LPDataEnd}','{fW_Litigation[i].LPType}','{CreateID}','{Create}')");
                        comm.CommandText = strSql.ToString();
                        rCount += comm.ExecuteNonQuery();
                    }
                }

                //添加律师费用
                for (int i = 0; i < fW_LawyerCost.Count; i++)
                {
                    if (fW_LawyerCost[i].LawyerPaymentDate==null)
                    {
                        fW_LawyerCost[i].LawyerPaymentDate = DateTime.Now;
                    } 
                    if (fW_LawyerCost[i].AttorneyFees != null && fW_LawyerCost[i].LawyerPaymentDate != null)
                    {
                        fwinfo.CounselFee += Convert.ToDecimal(fW_LawyerCost[i].AttorneyFees);
                        decimal d;
                        if (!decimal.TryParse(fW_LawyerCost[i].AttorneyFees.ToString(), out d))
                        {

                            return 4;
                        }
                        strSql = new StringBuilder();
                        strSql.Append($"insert into FW_LawyerCosts(");
                        strSql.Append($"LID,LawyerType,AttorneyFees,LawyerPaymentDate,CreatorID,Creator)");
                        strSql.Append($" values (");
                        strSql.Append($"'{ID}','{fW_LawyerCost[i].LawyerType}','{fW_LawyerCost[i].AttorneyFees}','{fW_LawyerCost[i].LawyerPaymentDate}','{CreateID}','{Create}')");
                        comm.CommandText = strSql.ToString();
                        rCount += comm.ExecuteNonQuery();
                    }
                }
                //添加诉讼费用
                for (int i = 0; i < fW_Legal.Count; i++)
                {
                    if (fW_Legal[i].LCPaymentDate==null)
                    {
                        fW_Legal[i].LCPaymentDate = DateTime.Now;
                    }
                    if (fW_Legal[i].LegalCosts != null && fW_Legal[i].LCPaymentDate != null)
                    {
                        fwinfo.LegalFee += Convert.ToDecimal(fW_Legal[i].LegalCosts);
                        decimal d;
                        if (!decimal.TryParse(fW_Legal[i].LegalCosts.ToString(), out d))
                        {

                            return 3;
                        }
                        strSql = new StringBuilder();
                        strSql.Append($"insert into FW_LegalCosts(");
                        strSql.Append($"LID,LCType,LegalCosts,LCPaymentDate,CreatorID,Creator)");
                        strSql.Append($" values (");
                        strSql.Append($"'{ID}','{fW_Legal[i].LCType}',{fW_Legal[i].LegalCosts},'{fW_Legal[i].LCPaymentDate}','{CreateID}','{Create}')");
                        comm.CommandText = strSql.ToString();
                        rCount += comm.ExecuteNonQuery();
                    }
                }
                #endregion


                DateTime time = DateTime.Now;
                //修改案件信息
                strSql = new StringBuilder();
                strSql.Append($"update  FW_LawInfo set ");
                strSql.Append($"LawName='{fwinfo.LawName}',Grade='{fwinfo.Grade}',LawsuitType='{fwinfo.LawsuitType}',LawType='{fwinfo.LawType}',Department='{fwinfo.Department}',Plaintiff='{fwinfo.Plaintiff}',Defendant='{fwinfo.Defendant}',");
                strSql.Append($"TheThird='{fwinfo.TheThird}',Court='{fwinfo.Court}',CaseNo='{fwinfo.CaseNo}',LawFirm='{fwinfo.LawFirm}',Lawyer='{fwinfo.Llawyer}',FilingDate='{fwinfo.FilingDate}',ClosingDate='{fwinfo.ClosingDate}',LawStatus='{fwinfo.LawStatus}',Describe='{fwinfo.Describe}',Claims='{fwinfo.Claims}',AmountInvolved={fwinfo.AmountInvolved} ,Judgment={fwinfo.Judgment},ActualPayment={fwinfo.ActualPayment},CollectionRate={fwinfo.CollectionRate},");
                strSql.Append($"AStopLoss={fwinfo.AStopLoss},StopLossRate='{fwinfo.StopLossRate}',RiskExposure={fwinfo.RiskExposure},IsAssess='{fwinfo.IsAssess}',Solutions='{fwinfo.Solutions}',PersonLiable='{fwinfo.PersonLiable}',ModifyDate='{time}',");
                strSql.Append($"CreatorID='{CreateID}',Creator='{Create}',IsPremium='{fwinfo.IsPremium}',PremiumDate='{fwinfo.PremiumDate}',IsDeblocking='{fwinfo.IsDeblocking}',DeblockingDate='{fwinfo.DeblockingDate}',ObjectAction='{fwinfo.ObjectAction}',CounselFee={fwinfo.CounselFee},LegalFee={fwinfo.LegalFee},InterestMoney={fwinfo.InterestMoney},Maintenancefee={fwinfo.Maintenancefee},SecurityMoney={fwinfo.SecurityMoney},Impairments={fwinfo.Impairments},SettleResult='{fwinfo.SettleResult}',LawUserName='{fwinfo.LawUserName}',LawUserNameID='{fwinfo.LawUserNameID}' where ID='{ID}' ");

                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();

                tran.Commit();
                conn.Close();
                return 1;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = strSql.ToString();
                conn.Close();
                return 5;
            }
            finally
            {
                conn.Close();
            }


        }

        //删除案件
        public int DeletePeoperLaw(string ID)
        {
            string str = $"update  FW_LawInfo set ISValid=0 where ID='{ID}'";
            int dt = DbHelperSQL160.ExecuteSql(str);
            return dt;
        }


        public int Deletetal(string Id, string Name)
        {
            string str = $"update  {Name} set ISValid=0 where ID='{Id}'";
            int dt = DbHelperSQL160.ExecuteSql(str);
            return dt;
        }

        public bool UpdateAYZ_SealInfo(string sql)
        {
            return DbHelperSQL160.ExecuteSql(sql) > 0;
        }

        public DataTable Selectid(string sql)
        {
            return DbHelperSQL160.ExecSqlDateTable(sql);
        }

        //修改保全信息时间
        public int Updatetime(string ID,string time)
        {
            string str = $"update  FW_LitigationPreservation set ModifyDate='{time}' where ID='{ID}'";
            int dt = DbHelperSQL160.ExecuteSql(str);
            return dt;
        }

        //案件结案
        public int TransactionCreateLawEnd(LDFW.Model.FW_LawInfo fwllow, string ID, string CreatorID, string Creator, ref int rCount, ref string errorInfo)
        {
            StringBuilder strSql = new StringBuilder();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection89"].ConnectionString);
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;
            try
            {
                DateTime time = DateTime.Now;
                strSql = new StringBuilder();
                var smas = "时间:" + DateTime.Now.ToShortDateString() + "   状态:案件结案流程发起" + "---------";
                //string uid = Guid.NewGuid().ToString();
                //IDs = uid;
                //  strSql.Append($"insert into FW_FollowUp (LID,FollowUpDate,PersonFollowUp,FollowUpInfo,NextDate,FUStatus,Creator) values('{ID}','{fwllow.FollowUpDate}','{fwllow.PersonFollowUp}','{fwllow.FollowUpInfo}','{fwllow.NextDate}','{fwllow.FUStatus}','{Creator}')");
                strSql.Append($"insert into FW_FollowUp(");
                strSql.Append($"ID,LID,FollowUpDate,FUStatus,LawType,PersonFollowUp,FollowUpInfo,");
                strSql.Append($"CreatorID,Creator) ");
                strSql.Append($"values (");
                strSql.Append($"'{fwllow.ID}','{ID}','{time}','流程发起','案件结案跟进','{Creator}','{smas}',");
                strSql.Append($"'{CreatorID}','{Creator}') ");
                //rCount += DbHelperSQL.ExecuteSql(strSql.ToString());
                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();
                tran.Commit();
                conn.Close();
                return 1;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = strSql.ToString();
                conn.Close();
                return 0;
            }

        }


        //添加跟进记录
        public int TransactionCreateFollowUp(LDFW.Model.FW_FollowUp fwllow, string ID, string CreatorID, string Creator, ref int rCount, ref string errorInfo)
        {

            StringBuilder strSql = new StringBuilder();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection89"].ConnectionString);
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;
            try
            {


                strSql = new StringBuilder();

                //  strSql.Append($"insert into FW_FollowUp (LID,FollowUpDate,PersonFollowUp,FollowUpInfo,NextDate,FUStatus,Creator) values('{ID}','{fwllow.FollowUpDate}','{fwllow.PersonFollowUp}','{fwllow.FollowUpInfo}','{fwllow.NextDate}','{fwllow.FUStatus}','{Creator}')");
                strSql.Append($"insert into FW_FollowUp(");
                strSql.Append($"LID,FollowUpDate,LawType,PersonFollowUp,FollowUpInfo,NextDate,FUStatus,TrialState,NextMassage,");
                strSql.Append($"CreatorID,Creator) ");
                strSql.Append($"values (");
                strSql.Append($"'{ID}','{fwllow.FollowUpDate}','{fwllow.LawType}','{fwllow.PersonFollowUp}','{fwllow.FollowUpInfo}','{fwllow.NextDate}','{fwllow.TrialState+"-"+fwllow.FUStatus}',");
                strSql.Append($"'{fwllow.TrialState}','{fwllow.NextMassage}','{CreatorID}','{Creator}') ");
                //rCount += DbHelperSQL.ExecuteSql(strSql.ToString());
                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();

                DateTime time = DateTime.Now;
                //修改案件信息
                strSql = new StringBuilder();
                strSql.Append($"update  FW_LawInfo set ");
                strSql.Append($"ModifyDate='{time}',LawStatus='{fwllow.FUStatus}',TrialState='{fwllow.TrialState}' where ID='{ID}' ");

                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();

                tran.Commit();
                conn.Close();
                return 1;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = strSql.ToString();
                conn.Close();
                return 0;
            }


        }

        //添加任务跟进记录
        public int TransactionCreateTask(LDFW.Model.FW_FollowUp fwllow, string ID, string CreatorID, string Creator, ref int rCount, ref string errorInfo)
        {
            StringBuilder strSql = new StringBuilder();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection89"].ConnectionString);
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;
            try
            {


                strSql = new StringBuilder();
              //  string guID = Guid.NewGuid().ToString();
                //IDs = fwllow.ID;
                //  strSql.Append($"insert into FW_FollowUp (LID,FollowUpDate,PersonFollowUp,FollowUpInfo,NextDate,FUStatus,Creator) values('{ID}','{fwllow.FollowUpDate}','{fwllow.PersonFollowUp}','{fwllow.FollowUpInfo}','{fwllow.NextDate}','{fwllow.FUStatus}','{Creator}')");
                strSql.Append($"insert into FW_FollowUp(");
                strSql.Append($"ID,LID,FollowUpDate,LawType,PersonFollowUp,PersonFollowUpID,TaskPerson,TaskPersonID,YewuPerson,YewuPersonID,TaskTime,TaskEndTime,TaskAsk,TaskFwwdback,");
                strSql.Append($"CreatorID,Creator,NextDate,FUStatus,FollowUpInfo) ");
                strSql.Append($"values (");
                strSql.Append($"'{fwllow.ID}','{ID}','{fwllow.FollowUpDate}','{fwllow.LawType}','{fwllow.PersonFollowUp}','{fwllow.PersonFollowUpID}','{fwllow.TaskPerson}','{fwllow.TaskPersonID}','{fwllow.YewuPerson}',");
                strSql.Append($"'{fwllow.YewuPersonID}','{fwllow.TaskTime}','{fwllow.TaskEndTime}','{fwllow.TaskAsk}','{fwllow.TaskFwwdback}','{CreatorID}','{Creator}','{fwllow.TaskTime}','{"流程发起"}','{"时间:"+DateTime.Now.ToShortDateString()+" 状态:任务跟进流程发起"}') ");
                //rCount += DbHelperSQL.ExecuteSql(strSql.ToString());
                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();

                tran.Commit();
                conn.Close();
                return 1;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = strSql.ToString();
                conn.Close();
                return 0;
            }
        }



        //显示跟进记录
        public DataTable SelectFollowUp(int page, int limit, string IDs)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@ids", IDs);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectFollowUp", pairs);
            return dt;
        }

        //显示任务跟进记录[FW_SelectFollowUpTypeRW]
        public DataTable SelectFollowUpRW(int page, int limit, string IDs)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@ids", IDs);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectFollowUpTypeRW", pairs);
            return dt;
        }

        //显示案件常规跟进[FW_SelectFollowUpTypeCG]
        public DataTable SelectFollowUpRWCG(int page, int limit, string IDs)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@ids", IDs);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectFollowUpTypeCG", pairs);
            return dt;
        }

        //查询案件信息
        public DataTable SelectLawInfoList(int page, int limit, string tag1, string tag2, string tag3)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@name1", tag1);
            pairs.Add("@name2", tag2);
            pairs.Add("@name3", tag3);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectLawInfoList", pairs);
            return dt;
        }


        /// <summary>
        /// sql分页
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="PageSize">每页几条</param>
        /// <param name="Where">sql条件</param>
        /// <param name="totalcount">总条数</param>
        /// <returns></returns>

        public DataTable GetPaginaList(string TableName, int pageIndex, int PageSize, string Where, out int totalcount)
        {
            string sql = string.Empty;
            if (TableName== "FW_LawInfo")
            {
                 sql = string.Format(@"SELECT ROW_NUMBER() over (order by ID ) as rowid,  CASE WHEN f.ClosingDate <>'1900-01-01 00:00:00.000' THEN  (DATEDIFF(D,CAST(f.FilingDate AS DATETIME) ,  f.ClosingDate ))
   ELSE  (DATEDIFF(D,CAST(f.FilingDate AS DATETIME),getdate()))  END  as times,* FROM {0} f where 1=1 and ISValid = 1", TableName);
            }
            else
            {
                 sql = string.Format(@"SELECT ROW_NUMBER() over (order by ID ) as rowid,* FROM {0} f where 1=1 and ISValid = 1", TableName);
            }
          

            if (!string.IsNullOrEmpty(Where))
            {
                if (Where.StartsWith("and"))
                    sql += Where;
                else
                    sql += string.Format(" and {0}", Where);
            }
            string conditionSQL = string.Format("select * from ({0}) a where a.rowid between {1} and {2}", sql, (pageIndex - 1) * PageSize, pageIndex * PageSize);

            string totalSQL = @"select 
            count(1) as totalnum
            from ( " + sql + " ) as sourcedata ";

            var totaldata = DbHelperSQL160.ExecSqlDateTable(totalSQL);
            int totalnum = 0;

            foreach (DataRow row in totaldata.Rows)
            {
                totalnum = int.Parse(row["totalnum"].ToString());
                break;
            }
            totalcount = totalnum;
            return DbHelperSQL160.ExecSqlDateTable(conditionSQL);
        }



        //查询个人/公司
        public DataTable SelectPersonalInfoList(int page, int limit, string tag1, string tag2, string tag3)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@name1", tag1);
            pairs.Add("@name2", tag2);
            pairs.Add("@name3", tag3);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectPersonalList", pairs);
            return dt;
        }

        //查询人名字
        public DataTable SelectPeopername(string ID)
        {
            string str = $"select  u.UserID, u.UserLoginID ,case when u.UserNickName is not null and u.UserNickName <> '' then u.UserNickName else u.UserName end as UserName";
            str += $" from dbo.MDM_User u";
            str += $" where u.Status = 1 and u.UserLoginID='{ID}' ";
            DataTable dt = DbHelperSQL.ExecSqlDateTable(str);
            return dt;
        }

        //显示诉讼保全信息
        public DataTable SelectLiPreserva(int page, int limit, string ID)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@RoleId", ID);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectLitiPreser", pairs);
            return dt;
        }
        //诉讼保全信息历史数据
        public DataTable SelectLiPreservaLog(int page, int limit, string ID)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@RoleId", ID);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectLitiPreserLog", pairs);
            return dt;
        }

        //显示最新一条的诉讼保全信息
        public DataTable SelectTreeone( string ID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"select ID as tempId ,* from FW_LitigationPreservation where ISValid=1 AND SortID=0 and ID='{0}'  ORDER BY LPDate DESC ", ID);
            DataSet ds = DbHelperSQL160.Query(sqlStr.ToString());
            return ds.Tables[0];
        }

        //查询案件的跟进记录
        public DataTable SelFullpall(string ID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"select * from FW_FollowUp where ISValid=1 AND SortID=0 and LID='{0}' and LawType='案件常规跟进'", ID);
            DataSet ds = DbHelperSQL160.Query(sqlStr.ToString());
            return ds.Tables[0];
        }

        //显示查封信息
        public DataTable SelectSeizureLog(int page, int limit, string ID)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@RoleId", ID);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectSeizureLog", pairs);
            return dt;
        }
        //显示诉讼费用
        public DataTable SelectLegalCostsLog(int page, int limit, string ID)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@RoleId", ID);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectLegalCostsLog", pairs);
            return dt;
        }
        //显示诉讼费用历史信息FW_SelectLegalCostsLogInfo
        public DataTable SelectLegalCostsLogInfo(int page, int limit, string ID)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@RoleId", ID);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectLegalCostsLogInfo", pairs);
            return dt;
        }
        //显示律师费用
        public DataTable SelectLawyerCostsLog(int page, int limit, string ID)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@RoleId", ID);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectLawyerCostsLog", pairs);
            return dt;
        }

        //显示律师费用历史信息
        public DataTable SelectLawerCossLoginfo(int page, int limit, string ID)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@RoleId", ID);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectLawyerCostsLoginfo", pairs);
            return dt;
          
    }

        //显示个人/公司库
        public DataTable SelectPersonalCompany(int page, int limit)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectPersonalCompany", pairs);
            return dt;
        }

        //权限——按用户账号查询事业部
        public DataTable SelectSYB(string nameid)
        {
            //string str = string.Format(" SELECT UserID,UserNo,Organization,OrgUnitGUID,Company,DepartmentZ,Department,DepartmentX,UserName,UserCode,UserPostName ,PositionCode,PositionName ,UserLevelName ,PositionLevel ,EMAIL ,Phone,IdentityCard ,Status FROM View_LD_User where UserCode = '{0}'", nameid);
            string strd = string.Format(@"with cte ( OrgUnitGUID,UpperOrgUnitGUID,OrgUnitName,OrgUnitLever)
AS (
SELECT o.OrgUnitGUID,po.ParentOrgUnitGuid,o.OrgUnitName,o.OrgUnitLever FROM dbo.MDM_OrganizationUnit o
INNER JOIN dbo.MDM_Organization_Link po ON po.OrgUnitGuid = o.OrgUnitGUID
 WHERE o.OrgUnitGUID IN(
SELECT o.OrgUnitGUID from dbo.MDM_OrganizationUnit o
 inner join dbo.MDM_PostOrganization_Link po on po.OrgUnitGuid=o.OrgUnitGUID
 inner join dbo.MDM_Position as b on b.PositionGUID=po.PositionGuid
 inner join dbo.MDM_User_Position_Link c on b.PositionGUID=c.PositionGUID 
 inner join dbo.MDM_User d on c.UserGUID=d.UserID
   WHERE d.UserLoginID='{0}' AND po.Status=1 AND o.Status=1 AND c.Status=1
   ) 
  UNION ALL
 SELECT o.OrgUnitGUID,po.ParentOrgUnitGuid,o.OrgUnitName,o.OrgUnitLever FROM dbo.MDM_OrganizationUnit o INNER JOIN dbo.MDM_Organization_Link po
 ON po.OrgUnitGuid=o.OrgUnitGUID INNER JOIN cte ON cte.UpperOrgUnitGUID=o.OrgUnitGUID
 )
 select DISTINCT * from cte WHERE cte.OrgUnitLever=3", nameid);

            DataTable dt = DbHelperSQL.ExecSqlDateTable(strd);
            return dt;
        }
        //权限--查询当前用户的权限
        public DataTable SelectRole(string UserId)
        {
            string str = string.Format(@"select r.RoleID,r.RoleType,r.RoleName,r.Description,r.Status,r.F1,ur.UserRoleID,ur.UserID,ur.RoleID,s.RoleRightsID,s.RightsCode,s.ModuleID from dbo.SYS_Role r" +
                " left join dbo.SYS_UserRole ur on r.RoleID = ur.RoleID" +
                " LEFT JOIN SYS_RoleRights s ON s.RoleID = r.RoleID WHERE  UserID = '{0}'  AND s.ModuleID IN('Law', 'LawList', 'SelectLaw','OneData','LawExcel')  and r.RoleID in ( SELECT RoleID FROM	SYS_Role WHERE RoleName LIKE '%诉讼系统%')", UserId);
            DataTable dt = DbHelperSQL.ExecSqlDateTable(str);
            return dt;
        }
        //权限-诉讼系统设计到的权限


        //删除文件
        public int FileDelete(string ID)
        {
            string str = $"update  FW_LawFiles  set ISValid = 0 where cast(ID as varchar(36)) = '{ID}'";
            int i = DbHelperSQL160.ExecSqlResult(str);
            return i;
        }
        #region 绑定下拉框搜索框
        //显示诉讼列表
        public DataTable SelectlawInfo(int page, int limit, string RoleId)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("@pageindex", page);
            pairs.Add("@pagesize", limit);
            pairs.Add("@RoleId", RoleId);
            DataTable dt = DbHelperSQL160.ExecSqlGetDataTable("FW_SelectLawInfo", pairs);
            return dt;
        }

        /// <summary>
        /// 查询流程号
        /// </summary>
        /// <param name="mainId"></param>
        /// <returns></returns>
        public DataTable GetCallBackRequestId(string mainId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"SELECT top 1
		                            *
		                            FROM dbo.CallbackLog WHERE MainId='{0}'", mainId);
            DataSet ds = DbHelperSQL160.Query(sqlStr.ToString());
            return ds.Tables[0];
        }

        //显示某条诉讼详细信息(id)
        public DataTable SelectlawInfoK(string ID, string IDs)
        {
            string str = $"select ( case f.ClosingDate when  '1900-01-01 00:00:00.000' then null else f.ClosingDate end) as ClosingDate1, * from [dbo].[FW_LawInfo] f left JOIN dbo.FW_FollowUp u  ON f.ID=u.LID  where cast(f.ID as varchar(36))='{ID}' and u.ID='{IDs}' and f.ISValid=1";
            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }

        //显示某条诉讼详细信息(id)
        public DataTable SelectlawInfoKll(string ID)
        {
            string str = $"select ( case f.ClosingDate when  '1900-01-01 00:00:00.000' then null else f.ClosingDate end) as ClosingDate1, * from [dbo].[FW_LawInfo] f   where cast(f.ID as varchar(36))='{ID}' and  f.ISValid=1";
            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }

        //显示某条诉讼详细信息历史记录(id)
        public DataTable SelectlawInfolog(string ID)
        {
            string str = $"select ( case f.ClosingDate when  '1900-01-01 00:00:00.000' then null else f.ClosingDate end) as ClosingDate1, * from [dbo].[FW_LawInfoModifyLog] f where cast(f.ID as varchar(36))='{ID}' and f.ISValid=1";
            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }

        //显示诉讼保全信息(id)
        public DataTable SelectLitigation(string ID)
        {
            string str = $"select * from FW_LitigationPreservation f where f.LID='{ID}' and f.ISValid=1 order by CreateDate desc";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }
        //显示诉讼保全信息日志(id)
        public DataTable SelectLitigationlog(string ID)
        {
            string str = $"select * from FW_LitigationPreservationLog f where f.LID='{ID}' and f.ISValid=1";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }

        //显示查封信息
        public DataTable SelectSeizure(string ID)
        {
            string str = $"select * from FW_Seizure f where f.LID='{ID}' and f.ISValid=1";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }
        //显示查封信息日志
        public DataTable SelectSeizurelog(string ID)
        {
            string str = $"select * from FW_SeizureModifyLog f where f.LID='{ID}' and f.ISValid=1";
            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }

        //显示诉讼费用(id)
        public DataTable SelectLegal(string ID)
        {
            string str = $"select * from FW_LegalCosts f where f.LID='{ID}' and f.ISValid=1";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }

        //显示诉讼费用日志(id)
        public DataTable SelectLegallog(string ID)
        {
            string str = $"select * from FW_LawyerCostsModifyLog f where f.LID='{ID}' and f.ISValid=1";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }


        //显示律师费用(id)
        public DataTable SelectLaweyer(string ID)
        {
            string str = $"select * from FW_LawyerCosts f where f.LID='{ID}' and f.ISValid=1";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }
        //显示附件(id)
        public DataTable SelectFiles(string ID)
        {
            string str = $"select * from FW_LawFiles f where f.LID='{ID}' and f.ISValid=1";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }

        //查询绿都事业部
        public DataTable SelectLvdu()
        {
            string str = $"select * from FW_Dictionary f where f.TypeName='Department' and TableID='L'  and f.ISValid=1";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }

        //查询汇通事业部
        public DataTable SelectHT()
        {
            string str = $"select * from FW_Dictionary f where f.TypeName='Department' and TableID='H'  and f.ISValid=1";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;

        }


        //查询所有人员
        public DataTable SelectPlaintiff(string keyname)
        {
            string str = string.Format("select top(100) u.UserID, (u.UserName  +'|'+u.UserLoginID) as value,(u.UserName +' - '+ u.UserLoginID)as name from MDM_User u where u.UserName like  '%{0}%'or u.UserLoginID like '%{0}%' ", keyname);
            DataTable dt = DbHelperSQL.ExecSqlDateTable(str);
            return dt;
        }

        //查询所有被告
        public DataTable SelectDefendant(string keyname)
        {
            string str = $"(select (case when p.Company ='' then p.Contacts  when p.Contacts='' then p.Company   else (p.Company + '-' +p.Contacts) end )  as value, ( case when p.Company ='' then p.Contacts  when p.Contacts='' then p.Company   else (p.Company + '-' +p.Contacts) end )  as name from [dbo].[FW_PersonalCompany] p left join [dbo].[FW_PCType] c on p.ID=c.PCID where c.TypeID=2 and (p.Contacts like  '%{keyname}%' or p.Company like '%{keyname}%' ) and p.ISValid=1)";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }
        //查询所有第三人
        public DataTable SelectTheThird(string keyname)
        {
            string str = $"(select (case when p.Company ='' then p.Contacts  when p.Contacts='' then p.Company   else (p.Company + '-' +p.Contacts) end )  as value, ( case when p.Company ='' then p.Contacts  when p.Contacts='' then p.Company   else (p.Company + '-' +p.Contacts) end )  as name from [dbo].[FW_PersonalCompany] p left join [dbo].[FW_PCType] c on p.ID=c.PCID where c.TypeID=3 and (p.Contacts like  '%{keyname}%' or p.Company like '%{keyname}%'  ) and p.ISValid=1)";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }

        //查询所有律所
        public DataTable SelectLawFirm(string keyname)
        {
            // string str = $"(select (case when p.Company ='' then p.Contacts  when p.Contacts='' then p.Company   else (p.Company + '-' +p.Contacts) end )  as value, ( case when p.Company ='' then p.Contacts  when p.Contacts='' then p.Company   else (p.Company + '-' +p.Contacts) end )  as name from [dbo].[FW_PersonalCompany] p left join [dbo].[FW_PCType] c on p.ID=c.PCID where c.TypeID=4 and (p.Contacts like  '%{keyname}%' or p.Company like '%{keyname}%'))";
            string str = $"select p.Company as value,p.Company as name from [dbo].[FW_PersonalCompany] p left join [dbo].[FW_PCType] c on p.ID=c.PCID  where c.TypeID=4 and p.Contacts like  '%{keyname}%' and p.ISValid=1";
            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }
        //查询所有律师
        public DataTable SelectLlawyer(string keyname)
        {
            string str = $"(select (case when p.Company ='' then p.Contacts  when p.Contacts='' then p.Company   else (p.Company + '-' +p.Contacts) end )  as value, ( case when p.Company ='' then p.Contacts  when p.Contacts='' then p.Company   else (p.Company + '-' +p.Contacts) end )  as name from [dbo].[FW_PersonalCompany] p left join [dbo].[FW_PCType] c on p.ID=c.PCID where c.TypeID=5 and (p.Contacts like  '%{keyname}%' or p.Company like '%{keyname}%' ) and p.ISValid=1 )";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }
        //查询所有事业部
        public DataTable SelectDempant(string roleid)
        {
            string str = $"select Name as name,No as value from FW_Dictionary f where f.TypeName='Department' and f.Name in ({roleid}) and f.ISValid=1 order by f.SortID  ";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }
        //查询所有案件类型
        public DataTable SelectLawType(string key)
        {
            string str = $"select * from FW_Dictionary f where f.TypeName='{key}' and f.ISValid=1";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }
        ////查询所有案件等级
        //public DataTable SelectGrade()
        //{
        //    string str = $"select * from FW_Dictionary f where f.TypeName='Grade' and f.ISValid=1";

        //    DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
        //    return dt;
        //}

        ////查询所有诉讼类型
        //public DataTable SelectsuitType()
        //{
        //    string str = $"select * from FW_Dictionary f where f.TypeName='LawsuitType' and f.ISValid=1";

        //    DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
        //    return dt;
        //}
        ////查询所有结案类型
        //public DataTable SelectSettleType()
        //{
        //    string str = $"select * from FW_Dictionary f where f.TypeName='SettleType' and f.ISValid=1";

        //    DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
        //    return dt;
        //}

        ////查询所有当前状态
        //public DataTable SelectLawStatus()
        //{
        //    string str = $"select * from FW_Dictionary f where f.TypeName='LawStatus' and f.ISValid=1";

        //    DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
        //    return dt;
        //}
        //查询个人/公司状态
        public DataTable SelectPersonalStatus()
        {
            string str = $"SELECT TypeName  FROM FW_PCType group by  TypeName";

            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }

        //查询所有附件类型
        public DataTable SelectFileType()
        {
            string str = $"select * from FW_Dictionary f where f.TypeName='FileType' and f.ISValid=1";


            DataTable dt = DbHelperSQL160.ExecSqlDateTable(str);
            return dt;
        }
        #endregion





        #region 将Json格式数据转化成对象


        /// <summary>
        /// 将Json格式数据转化成对象
        /// </summary>
        public class JsonHelper
        {
            /// <summary>  
            /// 生成Json格式  
            /// </summary>  
            /// <typeparam name="T"></typeparam>  
            /// <param name="obj"></param>  
            /// <returns></returns>  
            public static string GetJson<T>(T obj)
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    json.WriteObject(stream, obj);
                    string szJson = Encoding.UTF8.GetString(stream.ToArray()); return szJson;
                }
            }
            /// <summary>  
            /// 获取Json的Model  
            /// </summary>  
            /// <typeparam name="T"></typeparam>  
            /// <param name="szJson"></param>  
            /// <returns></returns>  
            public static T ParseFromJson<T>(string szJson)
            {
                T obj = Activator.CreateInstance<T>();
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                    return (T)serializer.ReadObject(ms);
                }
            }
        }
        #endregion

        #region 接口用到的方法

        public int Insert(string TableName, object entity)
        {

            System.Reflection.PropertyInfo[] properties = entity.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);



            string sql = (string.Format("INSERT INTO {0}(" + ExtModelHelp.GetPropertiesColumns(entity) + ") VALUES (" + ExtModelHelp.GetPropertiesValus(entity) + ");", TableName));
            return DbHelperSQL160.ExecSqlResult(sql);
        }

        public int UpdateFoll(string str)
        {
            return DbHelperSQL160.ExecSqlResult(str);
        }

        public int Update(string TableName, object entity)
        {
            System.Reflection.PropertyInfo[] properties = entity.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            string key = properties[0].Name;
            object keyVale = properties[0].GetValue(entity, null);
            string sql = (string.Format("update {0} set " + ExtModelHelp.GetPropertiesNameValus(entity) + " where {1} = '{2}'", TableName, key, keyVale));
            return DbHelperSQL160.ExecSqlResult(sql);
        }

        #endregion





    }
}
