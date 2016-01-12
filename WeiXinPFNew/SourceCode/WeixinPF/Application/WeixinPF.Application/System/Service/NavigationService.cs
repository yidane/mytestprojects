using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.System.Interface;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Model.Common;

namespace WeixinPF.Application.System.Service
{
    public class NavigationService
    {
        private readonly INavigationRepository _repository;
        public NavigationService(INavigationRepository repository)
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
        /// 查询名称是否存在
        /// </summary>
        public bool Exists(string name)
        {
            return this._repository.Exists(name);
        }

        /// <summary>
        /// 快捷添加系统默认导航
        /// </summary>
        /// <param name="parent_name">父导航名称</param>
        /// <param name="nav_name">导航名称</param>
        /// <param name="title">导航标题</param>
        /// <param name="link_url">链接地址</param>
        /// <param name="sort_id">排序数字</param>
        /// <param name="channel_id">所属频道ID</param>
        /// <param name="action_type">操作权限以英文逗号分隔开</param>
        /// <returns>int</returns>
        public int Add(string parent_name, string nav_name, string title, string link_url, int sort_id, int channel_id, string action_type)
        {
            int parent_id = this._repository.GetNavId(parent_name);
            return this._repository.Add(parent_id, nav_name, title, link_url, sort_id, channel_id, action_type);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(NavigationInfo model)
        {
            return this._repository.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            return this._repository.UpdateField(id, strValue);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string name, string strValue)
        {
            return this._repository.UpdateField(name, strValue);
        }

        /// <summary>
        /// 修改导航名称和标题
        /// </summary>
        /// <param name="old_name">旧名称</param>
        /// <param name="new_name">新名称</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public bool Update(string old_name, string new_name, string title, int sort_id)
        {
            return this._repository.Update(old_name, new_name, title, sort_id);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(NavigationInfo model)
        {
            return this._repository.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return this._repository.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NavigationInfo GetModel(string name)
        {
            return this._repository.GetModel(name);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NavigationInfo GetModel(int id)
        {
            return this._repository.GetModel(id);
        }

        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="nav_type">导航类别</param>
        /// <returns>DataTable</returns>
        public DataTable GetList(int parent_id, string nav_type, bool isAgent, ISystemConfigRepository wxSystemConfigRepository)
        {
            return this._repository.GetList(parent_id, nav_type, isAgent, wxSystemConfigRepository);
        }

        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 根据导航的名称查询其ID
        /// </summary>
        public int GetNavId(string nav_name)
        {
            return this._repository.GetNavId(nav_name);
        }

        /// <summary>
        /// 修改菜单的调用名称
        /// </summary>
        /// <param name="old_nav_name">旧调用名称</param>
        /// <param name="new_nav_name">新调用名称</param>
        /// <returns></returns>
        public bool UpdateNavName(string old_nav_name, string new_nav_name)
        {
            if (old_nav_name == new_nav_name)
            {
                return true;
            }
            return this._repository.UpdateNavName(old_nav_name, new_nav_name);
        }


        /// <summary>
        /// 取得指定类别下的列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="nav_type">导航类别</param>
        /// <returns>DataTable</returns>
        public DataTable GetChildList(int parent_id, string nav_type)
        {
            return this._repository.GetChildList(parent_id, nav_type);
        }

        /// <summary>
        /// 取得所有类别列表(没有排序好，只有数据)
        /// </summary>
        /// <param name="parent_id">父ID，0为所有类别</param>
        /// <param name="nav_type">导航类别</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataList(int parent_id, string nav_type)
        {
            return this._repository.GetDataList(parent_id, nav_type);
        }
        #endregion
    }
}
