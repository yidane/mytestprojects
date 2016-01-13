using System;
using System.Net;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplicationTest
{
    [TestClass]
    public class DownloadLogTest
    {
        private const string Url = "http://hdwy.bjhd.gov.cn/redApp20160112/redLog20160107.php?page={0}";

        [TestMethod]
        public void DownloadLog()
        {
            for (int index = 101; index <= 103; index++)
            {
                var html = GetHtml(index);
                WriteLog(index, html);
            }


            Assert.IsTrue(true);
        }


        private string GetHtml(int id)
        {
            var oWebClient = new WebClient();
            oWebClient.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
            byte[] data = oWebClient.DownloadData(string.Format(Url, id));
            var html = Encoding.GetEncoding("gb2312").GetString(data);
            return html;
        }

        private void WriteLog(int id, string html)
        {
            var fileName = string.Format("{0}\\html\\{1}.txt", AppDomain.CurrentDomain.BaseDirectory, id);
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(html);
            }
        }


        [TestMethod]
        public void SaveAsExcel()
        {
            var beginIndex = 0;
            var fileName = string.Format("{0}\\html\\{1}.txt", AppDomain.CurrentDomain.BaseDirectory, beginIndex);
            StringBuilder stringBuilder = new StringBuilder();
            while (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    var html = reader.ReadToEnd();
                    var dataList = html.Replace("<hr>", "@").Split('@');
                    for (int index = 1; index < dataList.Length; index++)
                    {
                        var dataItem = dataList[index].Replace(">", ",");
                        if (dataItem.Length > 10)
                            stringBuilder.Append(dataItem + Environment.NewLine);
                    }
                }

                beginIndex++;
                fileName = string.Format("{0}\\html\\{1}.txt", AppDomain.CurrentDomain.BaseDirectory, beginIndex);
            }

            var excelName = string.Format("{0}\\html\\result.csv", AppDomain.CurrentDomain.BaseDirectory);
            using (StreamWriter writer = new StreamWriter(excelName))
            {
                writer.Write(stringBuilder.ToString(), Encoding.GetEncoding("utf-8"));
            }
        }
    }
}
