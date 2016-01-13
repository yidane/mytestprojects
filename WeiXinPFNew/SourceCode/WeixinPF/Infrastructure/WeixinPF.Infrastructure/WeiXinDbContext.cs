using System.Data.Entity;
using WeixinPF.Model.Agent;
using WeixinPF.Model.Weixin.Message;
using WeixinPF.Model.WeiXin;
using WeixinPF.Model.WeiXin.Group;
using WeixinPF.Model.WeiXin.Message;
using WeixinPF.Model.WeiXin.Token;
using WeixinPF.Model.WeiXin.User;
using PaymentInfo = WeixinPF.Model.WeiXin.Pay.PaymentInfo;

namespace WeixinPF.Infrastructure
{
    public class WeiXinDbContext : DbContext
    {
        public WeiXinDbContext()
            : base("name=ConnectionString")
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<WeiXinDbContext>());
            //Database.SetInitializer<WeiXinDbContext>(new MyDatabaseInitializer());
            //this.Database.Initialize(true);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(entity => entity.ToTable(entity.ClrType.Name));
            // Configure the primary key for the OfficeAssignment 

            //modelBuilder.Entity<ManagerInfo>().HasKey(t => t.Id);
            //modelBuilder.Entity<AppInfo>().HasKey(t => t.id);
            //modelBuilder.Entity<IndustryDefaultModuleInfo>().HasKey(t => t.id);
            //modelBuilder.Entity<PaymentInfo>().HasKey(t => t.id);
            //modelBuilder.Entity<PropertyInfo>().HasKey(t => t.Id);
            //modelBuilder.Entity<SystemConfigInfo>().HasKey(t => t.id);
            //modelBuilder.Entity<GroupInfo>().HasKey(t => t.Id);
            //modelBuilder.Entity<RequestRule>().HasKey(t => t.Id);
            //modelBuilder.Entity<RequestRuleContent>().HasKey(t => t.Id);
            //modelBuilder.Entity<ResponseContentEntity>().HasKey(t => t.id);
            //modelBuilder.Entity<ResponseMessageLog>().HasKey(t => t.Id);
            //modelBuilder.Entity<AccessToken>().HasKey(t => t.Id);
            //modelBuilder.Entity<JsApiTicket>().HasKey(t => t.Id);
            //modelBuilder.Entity<UserInfo>().HasKey(t => t.Openid).HasKey(t => t.AppId);
        }

        #region Agent
        public DbSet<AgentInfo> AgentInfoContext { get; set; }
        public DbSet<ManagerBillInfo> ManagerBillInfoContext { get; set; }
        public DbSet<ManagerInfo> ManagerInfoContext { get; set; }
        public DbSet<ManagerLogInfo> ManagerLogInfoContext { get; set; }
        public DbSet<ManagerRoleInfo> ManagerRoleInfoContext { get; set; }
        public DbSet<ManagerRoleValueInfo> ManagerRoleValueInfoContext { get; set; }
        #endregion

        #region WeiXin

        public DbSet<AppInfo> AppInfoContext { get; set; }

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

        #region Pay
        public DbSet<PaymentInfo> PaymentInfoContext { get; set; }
        #endregion

        #endregion
    }
}