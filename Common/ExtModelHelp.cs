using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LDFW.Common
{
   public class ExtModelHelp
    {
        /// <summary>
        /// 获取列名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetPropertiesColumns<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }
            int i = 0;
            foreach (System.Reflection.PropertyInfo item in properties)
            {
              
                    string name = item.Name;
                    object value = item.GetValue(t, null);
                    if (value != null)
                    {
                        tStr += string.Format("{0},", name);
                    }
                
                i++;
            }
            return tStr.TrimEnd(',');
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetPropertiesValus<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }
            int i = 0;
            foreach (System.Reflection.PropertyInfo item in properties)
            {
           
                    object value = item.GetValue(t, null);
                    if (value != null)
                    {
                        tStr += string.Format("'{0}',", value);
                    }
            
                i++;
            }
            return tStr.TrimEnd(',');
        }

        public static string GetPropertiesNameValus<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (value != null)
                {
                    tStr += string.Format("{0}='{1}',", name, value);
                }
            }
            return tStr.TrimEnd(',');
        }

    }
}
