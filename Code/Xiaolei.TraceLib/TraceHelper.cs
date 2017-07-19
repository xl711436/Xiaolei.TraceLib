using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Xiaolei.TraceLib
{
    /// <summary>输出调试信息到debugview和log4net日志中
    /// </summary>
    public class TraceHelper
    {
        #region 静态变量

        private static Dictionary<LogType, ILog> EnumLogDic;

        private static readonly int IsTrace;

        private static readonly int IsLog;

        #endregion

        #region 静态购置函数

        static TraceHelper()
        {
            EnumLogDic = new Dictionary<LogType, ILog>();
            string tempString = ConfigurationManager.AppSettings["LogName"];

            if (tempString != null)
            {
                List<string> logNameList = tempString.Split(',').ToList();

                foreach (LogType item in Enum.GetValues(typeof(LogType)))
                {
                    string curEnumString = AttributeHelper.GetEnumDescribeString<LogType>(item);

                    if (logNameList.Contains(curEnumString))
                    {
                        ILog tempLog = LogManager.GetLogger(curEnumString);
                        EnumLogDic.Add(item, tempLog);
                    }
                }
            }

            IsTrace = Convert.ToInt32(ConfigurationManager.AppSettings["IsTrace"]);
            IsLog = Convert.ToInt32(ConfigurationManager.AppSettings["IsLog"]);
        }

        #endregion

        #region 静态方法
        /// <summary>使用默认的ILog，输出日志
        /// </summary>
        /// <param name="I_TraceInfo"></param>
        public static void TraceInfo(string I_TraceInfo)
        {
            TraceInfo(LogType.Default, I_TraceInfo);
        }

        public static void TraceInfo(LogType I_LogType, string I_TraceInfo)
        {
            if (EnumLogDic.ContainsKey(I_LogType))
            {
                ILog curLogName = EnumLogDic[I_LogType];
                TraceHelper.TraceInfo(curLogName, I_TraceInfo);
            }
        }

        public static void TraceInfo(ILog I_CurLog, string I_TraceInfo)
        {
            if (I_CurLog != null)
            {
                if (IsTrace == 1)
                {
                    System.Diagnostics.Trace.WriteLine(I_TraceInfo);
                }

                if (IsLog == 1 && I_CurLog != null)
                {
                    I_CurLog.Debug(I_TraceInfo);
                }
            }
        }

        #endregion
    }


    public enum LogType
    {
        [Describe("DefaultLogger")]
        Default,

        [Describe("TestLogger")]
        Test,
    }



    public static class AttributeHelper
    {
        public static string GetEnumDescribeString<T>(T I_EnumValue)
        {
            string R_Result = string.Empty;

            FieldInfo tempFieldInfo = typeof(T).GetField(I_EnumValue.ToString());
            object[] tempObjArray = tempFieldInfo.GetCustomAttributes(typeof(DescribeAttribute), false);
            DescribeAttribute curAttribute = (DescribeAttribute)tempObjArray[0];

            R_Result = curAttribute.Describe;

            return R_Result;
        }
    }

    public class DescribeAttribute : Attribute
    {
        public DescribeAttribute(string I_Describe)
        {
            Describe = I_Describe;
        }
        public string Describe { get; set; }
    }

}
