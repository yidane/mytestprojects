using System.Data;
using System.Data.SqlClient;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Model.System;

namespace WeixinPF.Application.System.Interface
{
    public interface INavigationRepository
    {
        bool Exists(int id);
        bool Exists(string name);

        int Add(int parent_id, string nav_name, string title, string link_url, int sort_id, int channel_id,
            string action_type);

        int Add(NavigationInfo model);
        bool UpdateField(int id, string strValue);
        bool UpdateField(string name, string strValue);
        bool Update(string old_name, string new_name, string title, int sort_id);
        bool Update(NavigationInfo model);
        bool Delete(int id);
        NavigationInfo GetModel(string nav_name);
        NavigationInfo GetModel(int id);
        NavigationInfo GetModel(SqlConnection conn, SqlTransaction trans, int id);
        DataTable GetChildList(int parent_id, string nav_type);
        DataTable GetDataList(int parent_id, string nav_type);
        DataTable GetList(int parent_id, string nav_type, bool isAgent, IWXSystemConfigRepository wxSystemConfigRepository);
        int GetNavId(string nav_name);

        bool UpdateNavName(string old_nav_name, string new_nav_name);
        
    }
}