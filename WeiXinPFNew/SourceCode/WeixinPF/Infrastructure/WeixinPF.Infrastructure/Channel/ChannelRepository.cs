using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Channel.Repository;
using WeixinPF.Common;
using WeixinPF.DBUtility;

namespace WeixinPF.Infrastructure.Channel
{
    public class ChannelRepository: IChannelRepository
    {
        private string databaseprefix; //数据库表名前缀
        public ChannelRepository(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 根据频道的名称查询ID
        /// </summary>
        public int GetChannelId(string channel_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id FROM " + databaseprefix + "channel");
            strSql.Append(" where name=@name");
            SqlParameter[] parameters = {
                    new SqlParameter("@name", SqlDbType.NVarChar,50)};
            parameters[0].Value = channel_name;
            string str = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            return Utils.StrToInt(str, 0);
        }
    }
}
