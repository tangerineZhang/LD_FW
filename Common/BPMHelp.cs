using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace LDFW.Common
{
   public class BPMHelp
    {


        /// <summary>
        /// 添加主表Main信息
        /// </summary>
        /// <param name="xmlflow">主节点</param>
        /// <param name="parentEmt">子节点</param>
        /// <param name="emtName">字段名</param>
        /// <param name="emtText">字段值</param>
        public void AutoCreateElement(XmlDocument xmlflow, XmlElement parentEmt, string emtName, string emtText, bool needEscape = true)
        {
            var elemt = xmlflow.CreateElement(emtName);
            if (string.IsNullOrEmpty(emtText))
            {
                elemt.InnerXml = "";
            }
            else
            {
                if (needEscape)
                {
                    elemt.InnerXml = System.Security.SecurityElement.Escape(emtText);
                }
                else
                {
                    elemt.InnerXml = emtText.Replace("&", "&amp;");
                }
            }
            parentEmt.AppendChild(elemt);
        }

        

        /// <summary>
        /// xml转换成string类型
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public string ConvertXmlToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            StreamReader sr = null;
            string xmlString = string.Empty;
            try
            {
                stream = new MemoryStream();
                writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
                writer.Formatting = System.Xml.Formatting.Indented;
                xmlDoc.Save(writer);
                sr = new StreamReader(stream, System.Text.Encoding.UTF8);
                stream.Position = 0;
                xmlString = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sr.Close();
                stream.Close();
            }
            return xmlString;
        }
    }
}
