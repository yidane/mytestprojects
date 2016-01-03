using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using WeixinPF.Plugins.WeiXin.Service.Models;
using WeixinPF.Plugins.WeiXin.Service.Models.Group;
using WeixinPF.Plugins.WeiXin.Service.Models.User;

namespace WeixinPF.Plugins.WeiXin.Service.Application
{
    public sealed class WeiXinDbContext : DbContext
    {
        public WeiXinDbContext()
            : base("name=ConnectionString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(entity => entity.ToTable(entity.ClrType.Name));
            // Configure the primary key for the OfficeAssignment 
            modelBuilder.Entity<WeiXinAppInfo>().HasKey(t => t.id);
            modelBuilder.Entity<WeiXinUserInfo>().HasKey(t => t.openid);
        }

        #region 微信基础对象
        public DbSet<WeiXinAppInfo> WeiXinAppInfoContext { get; set; }
        public DbSet<WeiXinUserInfo> WeiXinUserInfoContext { get; set; }
        public DbSet<WeiXinGroupInfo> WeiXinGroupInfoContext { get; set; }
        #endregion

        #region 微信支付对象
        
        #endregion
    }
}