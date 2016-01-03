using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Common
{
    public class PathHelper
    {
        /// <summary>
        /// 获取文件的全路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFullPathName(string fileName)
        {
            string strFileName = GetRunningFolder();

            strFileName += fileName;
            strFileName = strFileName.Replace(@"\\", @"\");
            return strFileName;
        }
        private static string runningFolder = null;
        public static string GetRunningFolder()
        {
            if (!string.IsNullOrEmpty(runningFolder))
                return runningFolder;
            else
            {
                runningFolder = System.AppDomain.CurrentDomain.BaseDirectory;
                if (!runningFolder.EndsWith(@"\"))
                    runningFolder = runningFolder + @"\";
                return runningFolder;
            }
        }
    }
}
