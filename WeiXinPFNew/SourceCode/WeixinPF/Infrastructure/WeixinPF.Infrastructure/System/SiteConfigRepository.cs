﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.System.Interface;
using WeixinPF.Common.Helper;
using WeixinPF.Model.System;

namespace WeixinPF.Infrastructure.System
{
    /// <summary>
    /// 数据访问类:站点配置
    /// </summary>
    public class SiteConfigRepository:ISiteConfigRepository
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public siteconfig loadConfig(string configFilePath)
        {
            return (siteconfig)SerializationHelper.Load(typeof(siteconfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public siteconfig saveConifg(siteconfig model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }
    }
}
