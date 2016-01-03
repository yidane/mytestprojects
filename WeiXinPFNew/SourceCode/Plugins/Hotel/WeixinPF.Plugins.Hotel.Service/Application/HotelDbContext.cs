﻿using System.Data.Entity;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application
{
    public class HotelDbContext: DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“TravelDBContext”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“Travel.Infrastructure.DomainDataAccess.TravelDBContext”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“TravelDBContext”
        //连接字符串。
        public HotelDbContext()
            : base("name=ConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(entity => entity.ToTable(entity.ClrType.Name));
            // Configure the primary key for the OfficeAssignment 
            modelBuilder.Entity<IdentifyingCodeInfo>()
                .HasKey(t => t.IdentifyingCodeId);

            modelBuilder.Entity<HotelOrderInfo>()
                .HasKey(t => t.id);

            modelBuilder.Entity<HotelUserInfo>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<HotelAdminInfo>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<HotelInfo>()
                .HasKey(t => t.id);
        }

        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。

        public virtual DbSet<IdentifyingCodeInfo> IdentifyingCodeInfo { get; set; }

        public virtual DbSet<HotelOrderInfo> HotelOrderInfo { get; set; }

        public virtual DbSet<HotelUserInfo> HotelUserInfo { get; set; }

        public virtual DbSet<HotelAdminInfo> HotelAdminInfo { get; set; }

        public virtual DbSet<HotelInfo> HotelInfo { get; set; }
    }
}
