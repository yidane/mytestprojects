using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Common;

namespace WeixinPF.Shared
{
    public class BusEntry
    {
        public IBus MyBus { get; private set; }
        private string _endpointName;
        private const int SLEEP_TIME = 10000; //单位毫秒

        public BusEntry(string endpointName)
        {
            if (string.IsNullOrEmpty(endpointName))
            {
                return;
            }

            this._endpointName = endpointName;
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName(endpointName);
            busConfiguration.PurgeOnStartup(true);
            busConfiguration.ApplyCommonConfiguration();

            MyBus = Bus.Create(busConfiguration).Start();
        }

        /// <summary>
        /// 在同步程序中模拟异步方式发送消息到目的地
        /// </summary>
        /// <typeparam name="TSource">发送消息类型</typeparam>
        /// <typeparam name="TResult">回复消息类型</typeparam>
        /// <param name="destinationEndpointName">目的地终端节点的名称（地址）</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ResultInfo Send<TResult>(string destinationEndpointName, dynamic message) where TResult : class
        {
            TResult result = null;
            IAsyncResult asynResult = null;

            try
            {
                asynResult = this.MyBus.Send(destinationEndpointName, message as object)
                                .Register(response =>
                                {
                                    var localResult = (CompletionResult)response.AsyncState;

                                    if (localResult != null && localResult.Messages.Any())
                                    {
                                        result = localResult.Messages[0] as TResult;
                                    }
                                }, null);

                WaitHandle asyncWaitHandle = asynResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(SLEEP_TIME);
            }
            catch (Exception ex)
            {
                return new ResultInfo()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = @"请求异常"
                };
            }
            

            if (asynResult.IsCompleted && result != null)
            {
                return new ResultInfo()
                {
                    IsSuccess = true,
                    Data = result,
                    Message = string.Empty
                };
            }
            else
            {
                return new ResultInfo()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = @"异步请求超时"
                };
            }
        }

        //private static void LoadPlugins()
        //{
        //    var files = new List<string>();
        //    var pathName = string.Empty;

        //    if (Directory.Exists(PathHelper.GetRunningFolder() + "bin"))
        //    {
        //        pathName = PathHelper.GetRunningFolder() + "bin";
        //    }
        //    else
        //    {
        //        pathName = PathHelper.GetRunningFolder();
        //    }

        //    files.AddRange(Directory.GetFiles(pathName, "*.Plugins.dll"));
        //    files.AddRange(Directory.GetFiles(pathName, "*.Plugins.Service.exe"));

        //    if (files.Any())
        //    {
        //        foreach (var assemblyFileName in files)
        //        {
        //            var fileName = assemblyFileName.Split('\\').Last();
        //            var busName = string.Empty;

        //            if (fileName.Split('.').Contains("Service"))
        //            {
        //                busName = fileName.Split('.')[1].ToLower() + "service";
        //            }
        //            else
        //            {
        //                busName = fileName.Split('.')[1].ToLower();
        //            }

        //            if (dictBus.Keys.Contains(busName))
        //            {
        //                continue;
        //            }

        //            BusConfiguration busConfiguration = new BusConfiguration();

        //            busConfiguration.PurgeOnStartup(true);
        //            busConfiguration.ApplyCommonConfiguration();
        //            busConfiguration.EndpointName(fileName.Substring(0, fileName.Length - 4));
                    
        //            dictBus.Add(busName, NServiceBus.Bus.Create(busConfiguration).Start());
        //        }
        //    }
        //}

        public void Dispose()
        {
            if (MyBus != null)
            {
                MyBus.Dispose();
            }
        }        
    }
}
