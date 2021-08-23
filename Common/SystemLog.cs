using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Common
{
    public class SystemLog
    {
        private static object lockObject = new object();
        public static void Info(string msg)
        {
            try
            {
                lock (lockObject)
                {
                    string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SystemLogs\" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                    FileInfo info = new FileInfo(fileName);
                    if (!info.Directory.Exists)
                    {
                        info.Directory.Create();
                    }
                    if (!File.Exists(fileName))
                    {
                        File.Create(fileName).Close();
                    }
                    FileInfo info2 = new FileInfo(fileName);
                    using (StreamWriter writer = info2.AppendText())
                    {
                        writer.WriteLine(string.Format("{0}:{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
                        writer.Flush();
                        writer.Close();
                    }
                    info2 = null;
                }
                Console.WriteLine(string.Format("{0}:{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void Error(string msg, Exception ex)
        {
            try
            {
                lock (lockObject)
                {
                    string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SystemErrors\" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                    FileInfo info = new FileInfo(fileName);
                    if (!info.Directory.Exists)
                    {
                        info.Directory.Create();
                    }
                    if (!File.Exists(fileName))
                    {
                        File.Create(fileName).Close();
                    }
                    FileInfo info2 = new FileInfo(fileName);
                    using (StreamWriter writer = info2.AppendText())
                    {
                        writer.WriteLine(string.Format("{0}:{1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg, ex.Message));
                        writer.Flush();
                        writer.Close();
                    }
                    info2 = null;
                }
                Console.WriteLine(string.Format("{0}:{1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg, ex.Message));
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
