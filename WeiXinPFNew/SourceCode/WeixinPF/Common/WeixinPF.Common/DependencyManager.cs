using System;
using System.Configuration;
using System.IO;
using Autofac;
using Autofac.Configuration;

namespace WeixinPF.Common
{
    public static class DependencyManager
    {
        private const string ConfigPath = "AutofacConfig";
        private static IContainer _container;

        private static void InitContainer()
        {
            if (_container != null)
                return;

            var containerBuilder = new ContainerBuilder();

            var configPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings[ConfigPath];
            if (string.IsNullOrEmpty(ConfigPath))
                throw new Exception("系统配置文件中尚未配置AutofacConfig");

            if (!File.Exists(configPath))
                throw new Exception(string.Format("从路径{0}无法找到autofac的配置文件", configPath));

            //从配置文件注册Module
            containerBuilder.RegisterModule(new ConfigurationSettingsReader("autofac", configPath));

            _container = containerBuilder.Build();
        }

        public static T Resolve<T>()
        {
            if (_container == null)
                InitContainer();

            return _container.Resolve<T>();
        }
    }
}