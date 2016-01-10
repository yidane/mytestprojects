using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Model.Agent;
using WeixinPF.Model.Weixin.Message;
using WeixinPF.Model.WeiXin.Group;
using WeixinPF.Model.WeiXin.Message;
using WeixinPF.Model.WeiXin.Token;
using WeixinPF.Model.WeiXin.User;

namespace WeixinPF.Model.WeiXin
{
    public class WeiXinDbContext : DbContext
    {
        public WeiXinDbContext()
            : base("name=ConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(entity => entity.ToTable(entity.ClrType.Name));
            // Configure the primary key for the OfficeAssignment 
        }

        public DbSet<ManagerInfo> ManagerInfoContext { get; set; }
        public DbSet<AppInfo> AppInfoContext { get; set; }
        public DbSet<IndustryDefaultModuleInfo> IndustryDefaultModuleInfoContext { get; set; }
        public DbSet<PaymentInfo> PaymentInfoContext { get; set; }
        public DbSet<PropertyInfo> PropertyInfoContext { get; set; }
        public DbSet<SystemConfigInfo> SystemConfigInfoContext { get; set; }

        #region Group
        public DbSet<GroupInfo> GroupInfoContext { get; set; }

        #endregion

        #region Message
        public DbSet<RequestRule> RequestRuleContext { get; set; }
        public DbSet<RequestRuleContent> RequestRuleContentContext { get; set; }
        public DbSet<ResponseContentEntity> ResponseContentEntitieContext { get; set; }
        public DbSet<ResponseMessageLog> ResponseMessageLogContext { get; set; }
        #endregion

        #region Token
        public DbSet<AccessToken> AccessTokenContext { get; set; }
        public DbSet<JsApiTicket> JsApiTicketContext { get; set; }
        #endregion

        #region User
        public DbSet<UserInfo> UserInfoContext { get; set; }
        #endregion
    }
}
