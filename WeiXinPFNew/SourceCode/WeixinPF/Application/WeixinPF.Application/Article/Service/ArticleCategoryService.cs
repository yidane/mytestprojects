using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Article.Repository;
using WeixinPF.Application.Channel.Repository;
using WeixinPF.Application.Channel.Service;
using WeixinPF.Model.Article;

namespace WeixinPF.Application.Article.Service
{
    public class ArticleCategoryService
    {
        private readonly IArticleCategoryRepository _repository;

        public ArticleCategoryService(IArticleCategoryRepository repository)
        {
            this._repository = repository;
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return this._repository.Exists(id);
        }

        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetTitle(int id)
        {
            return this._repository.GetTitle(id);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ArticleCategoryInfo model)
        {
            return this._repository.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            this._repository.UpdateField(id, strValue);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ArticleCategoryInfo model)
        {
            return this._repository.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            this._repository.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ArticleCategoryInfo GetModel(int id)
        {
            return this._repository.GetModel(id);
        }

        /// <summary>
        /// 取得指定类别下的列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetChildList(int parent_id, int channel_id)
        {
            return this._repository.GetChildList(parent_id, channel_id);
        }

        /// <summary>
        /// 取得指定类别下的列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_name">频道名称</param>
        /// <returns></returns>
        public DataTable GetChildList(int parent_id, string channel_name, IChannelRepository repository)
        {
            var channel_id = new ChannelService(repository).GetChannelId(channel_name);
            return this._repository.GetChildList(parent_id, channel_id);
        }

        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetList(int parent_id, int channel_id)
        {
            return this._repository.GetList(parent_id, channel_id);
        }

        public DataTable GetList(string str)
        {
            return this._repository.GetList(str);
        }

        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_name">频道名称</param>
        /// <returns></returns>
        public DataTable GetList(int parent_id, string channel_name, IChannelRepository repository)
        {
            int channel_id = new ChannelService(repository).GetChannelId(channel_name);
            return this._repository.GetList(parent_id, channel_id);
        }

        #region 扩展方法================================
        /// <summary>
        /// 取得父节点的ID
        /// </summary>
        public int GetParentId(int id)
        {
            return this._repository.GetParentId(id);
        }

        /// <summary>
        /// 取得该微帐号的所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetWCodeList(int wid, int parent_id, int channel_id)
        {
            return this._repository.GetWCodeList(wid, parent_id, channel_id);
        }

        #endregion

        #endregion
    }
}
