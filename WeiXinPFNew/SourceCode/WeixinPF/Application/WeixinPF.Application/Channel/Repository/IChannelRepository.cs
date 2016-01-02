namespace WeixinPF.Application.Channel.Repository
{
    public interface IChannelRepository
    {
        /// <summary>
        /// 根据频道的名称查询ID
        /// </summary>
        int GetChannelId(string channel_name);
    }
}