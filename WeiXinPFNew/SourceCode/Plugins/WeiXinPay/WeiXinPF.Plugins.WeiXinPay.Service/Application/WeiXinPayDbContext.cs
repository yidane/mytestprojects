using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXinPF.Plugins.WeiXinPay.Service.Models;

namespace WeiXinPF.Plugins.WeiXinPay.Service.Application
{
    public class WeiXinPayDbContext : DbContext
    {
        public WeiXinPayDbContext()
            : base("name=ConnectionString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(entity => entity.ToTable(entity.ClrType.Name));
            // Configure the primary key for the OfficeAssignment 
        }

        public DbSet<WeiXinPayNotifyInfo> PayNotifyInfoContext { get; set; }
        public DbSet<WeiXinRefundOrderInfo> RefundOrderInfoContext { get; set; }
        public DbSet<WeiXinUnifiedOrderInfo> UnifiedOrderInfoContext { get; set; }
    }
}