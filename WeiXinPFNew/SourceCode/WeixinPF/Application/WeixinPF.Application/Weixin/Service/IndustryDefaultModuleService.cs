﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Article.Repository;
using WeixinPF.Application.Article.Service;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Common;
using WeixinPF.Model.Article;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Service
{
    public class IndustryDefaultModuleService
    {
        private readonly IIndustryDefaultModuleRepository _repository;

        public IndustryDefaultModuleService()
        {
            this._repository = DependencyManager.Resolve<IIndustryDefaultModuleRepository>();
        }

        public IndustryDefaultModuleService(IIndustryDefaultModuleRepository repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return this._repository.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<IndustryDefaultModuleInfo> DataTableToList(DataTable dt)
        {
            var modelList = new List<IndustryDefaultModuleInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                var model = new IndustryDefaultModuleInfo();
                for (int n = 0; n < rowsCount; n++)
                {
                    model = this._repository.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        /// <summary>
        /// 根据微用户所在行业给微账户添加默认模块
        /// </summary>
        public void addMouduleByRoleid(int roleid, int wid, IArticleCategoryRepository repository)
        {
            var acBll = new ArticleCategoryService(repository);
            //得到模型的实体类集合 
            var idList = getModelList(" role_id=" + roleid + " order by sort_id asc");

            //循环给为账户添加行业模块
            for (int i = 0; i < idList.Count; i++)
            {
                var acModel = new ArticleCategoryInfo()
                {
                    title = idList[i].mName,
                    call_index = "mubanpinyin",
                    wid = wid,
                    link_url = idList[i].url,
                    channel_id = 1,
                    sort_id = MyCommFun.Obj2Int(idList[i].sort_id)
                };
                int resId = acBll.Add(acModel);
                var upModel = acBll.GetModel(resId);
                upModel.class_list = "," + resId + ",";
                acBll.Update(upModel);
            }

        }

        /// <summary>
        /// 得到实体集合
        /// </summary>
        /// <param name="strwhere">查询条件</param>
        /// <returns></returns>
        public List<IndustryDefaultModuleInfo> getModelList(string strwhere)
        {
            DataSet ds = this._repository.GetList(strwhere);
            var idList = new List<IndustryDefaultModuleInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var idModel = this._repository.DataRowToModel(dr);
                    idList.Add(idModel);
                }
            }

            return idList;
        }
    }
}
