using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WeixinPF.DBUtility;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Infrastructure.BaseRepository;

namespace WeixinPF.Hotel.Plugins.Service.Infrastructure
{
    public class IdentifyingCodeRepository:EFRepository<IdentifyingCodeInfo>,IIdentifyingCodeRepository
    {
        private DbContext _context;

        public IdentifyingCodeRepository(DbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public void MakeUseOfIdentifyingCode(Guid identifyingCodeId)
        {
            var strSql = "Update wx_Verification_IdentifyingCodeInfo Set Status=2, ModifyTime=getdate() WHERE IdentifyingCodeId = @IdentifyingCodeId";
            var param = new SqlParameter[1];

            param[0] = new SqlParameter("@IdentifyingCodeId", SqlDbType.UniqueIdentifier) {Value = identifyingCodeId};            

            DbHelperSQL.ExecuteSql(strSql, param);
        }

        public IList<IdentifyingCodeDetailSearchDTO> GetIdentifyingCodeDetailById(IdentifyingCodeInfo code)
        {
            var strSql = new StringBuilder();

            if (code == null)
            {
                return null;
            }

            if (code.ModuleName.Equals("restaurant"))
            {
                strSql.Append("select a.id AS ProductId,b.OrderId AS OrderId,a.cpName as ProductName,c.price as Price,b.status as Status FROM wx_diancai_caipin_manage a INNER join ");
                strSql.Append("(select IdentifyingCodeId,CAST(ProductId AS INT) AS ProductId,CAST(OrderId AS INT) AS OrderId,");
                strSql.Append("Status FROM [wx_Verification_IdentifyingCodeInfo] WHERE IdentifyingCodeId='" + code.IdentifyingCodeId + "') as b");
                strSql.Append(" ON a.id=b.ProductId INNER JOIN (SELECT price,caiId,dingId FROM dbo.wx_diancai_dingdan_caiping)c");
                strSql.Append(" ON b.ProductId= c.caiId AND c.dingId= b.OrderId");
            }
            else if (code.ModuleName.Equals("hotel"))
            {
                strSql.Append("select a.id AS OrderId,a.roomid AS ProductId,a.roomType as ProductName,a.price as Price,b.status as Status,a.orderNum AS Number,");
                strSql.Append("CONVERT(varchar(10),a.arriveTime,120) as ArriveTime,CONVERT(varchar(10),a.leaveTime,120) as LeaveTime, CAST(DATEDIFF(DAY,a.arriveTime,a.leaveTime)*a.price*a.orderNum as FLOAT) as TotelPrice ");
                strSql.Append("FROM dbo.wx_hotel_dingdan a INNER join (select IdentifyingCodeId,CAST(OrderId AS INT) AS OrderId,Status FROM [wx_Verification_IdentifyingCodeInfo] ");
                strSql.Append("WHERE IdentifyingCodeId='" + code.IdentifyingCodeId + "') as b ON a.id=b.OrderId ");
            }

            return this._context.Database.SqlQuery<IdentifyingCodeDetailSearchDTO>(strSql.ToString()).ToList();
        }
    }
}
