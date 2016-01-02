using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Channel.Repository;

namespace WeixinPF.Application.Channel.Service
{
    public class ChannelService
    {
        private readonly IChannelRepository _repository;

        public ChannelService(IChannelRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// 根据频道的名称查询ID
        /// </summary>
        public int GetChannelId(string channel_name)
        {
            return this._repository.GetChannelId(channel_name);
        }
    }
}
