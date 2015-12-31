using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeixinPF.Common.Helper;

namespace WeixinPF.Common
{
    /// <summary>
    /// 通用Ajax请求返回数据
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data { get; set; }

        public static AjaxResult Succeed(string message, object data)
        {
            return new AjaxResult()
            {
                Success = true,
                Message = message,
                Data = data
            };
        }
        public static AjaxResult Succeed(object data)
        {
            return Succeed(string.Empty, data);
        }

        public static AjaxResult Fail(string message)
        {
            return new AjaxResult()
            {
                Success = false,
                Message = message
            };
        }
    }
}
