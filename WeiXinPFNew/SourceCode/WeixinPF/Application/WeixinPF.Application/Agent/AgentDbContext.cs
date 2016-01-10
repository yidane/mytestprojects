using System.Data.Entity;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent
{
    public class AgentDbContext : DbContext
    {
        public AgentDbContext()
            : base("name=ConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(entity => entity.ToTable(entity.ClrType.Name));
            // Configure the primary key for the OfficeAssignment 
        }

        public DbSet<ManagerInfo> ManagerInfoContext { get; set; }
        public DbSet<ManagerLogInfo> ManagerLogInfoContext { get; set; }
    }
}