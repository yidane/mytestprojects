﻿using System;
using System.Collections.Generic;
using WeixinPF.Common;
using WeixinPF.Model.Weixin.Message;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.WeixinFramework.Common
{
    /// <summary>
    /// 该处主要处理模块插入回复信息的处理
    /// </summary>
    public partial class WeiXCommFun
    {
        /// <summary>
        /// 判断模版名称【以后只要再这个方法里添加模版的判断，再写好自己的方法】
        /// </summary>
        /// <param name="modelFunctionName"></param>
        /// <param name="modelFunctionId"></param>
        /// <param name="openid"></param>
        /// <param name="apiid"></param>
        /// <param name="responseType"></param>
        /// <param name="responseVaule"></param>
        private IList<ResponseContentEntity> PanDuanMoudle(string modelFunctionName, int modelFunctionId, string openid, int apiid)
        {
            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();


            if (modelFunctionName == "刮刮卡")
            {
                responselist = GGKReponse(modelFunctionId, apiid, openid);
            }
            else if (modelFunctionName == "大转盘")
            {
                responselist = DZPReponse(modelFunctionId, apiid, openid);
            }
            else if (modelFunctionName == "砸金蛋")
            {
                responselist = ZJDReponse(modelFunctionId, apiid, openid);
            }
            else if (modelFunctionName == "优惠券简单版")
            {
                responselist = yhqjdReponse(modelFunctionId, apiid, openid);
            }

            else if (modelFunctionName == "喜帖")
            {
                responselist = xitieReponse(modelFunctionId, apiid, openid);
            }
            else if (modelFunctionName == "会员卡")
            {
                responselist = ucardReponse(modelFunctionId, apiid, openid);
            }
            else if (modelFunctionName == "电影院抢票")
            {
                responselist = DDYQPReponse(modelFunctionId, apiid, openid);
            }
            else if (modelFunctionName == "微房产")
            {
                responselist = WXFReponse(modelFunctionId, apiid, openid);
            }
            else if (modelFunctionName == "摇一摇")
            {
                responselist = LuckyMoneyReponse(modelFunctionId, apiid, openid);
            }
            else if (modelFunctionName == "湖怪")
            {
                responselist = HuGuaiReponse(modelFunctionId, apiid, openid);
            }


            return responselist;
        }

        /// <summary>
        /// 摇一摇回复内容
        /// </summary>
        /// <param name="id">模块主键Id</param>
        /// <param name="apiid">微帐号主键id</param>
        /// <param name="openid">openid</param>
        /// <param name="responseType">回复类型：1纯文字，2图文</param>
        /// <param name="responseVaule">回复的内容</param>
        private IList<ResponseContentEntity> LuckyMoneyReponse(int id, int apiid, string openid)
        {
            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();
            //ResponseContentEntity responseEntity = new ResponseContentEntity();
            //responseEntity.id = id;
            //responseEntity.wid = apiid;


            //BLL.wx_dzpActionInfo ggkActBll = new BLL.wx_dzpActionInfo();
            //Model.wx_dzpActionInfo actModel = ggkActBll.GetModel(id);
            //if (actModel.beginDate > DateTime.Now)
            //{  //活动尚未开始 
            //    responseEntity.rcType = Model.ReponseContentType.text;
            //    responseEntity.rContent = "活动【" + actModel.actName + "】将于" + actModel.beginDate + "开始。";
            //}
            //else if (actModel.endDate <= DateTime.Now)
            //{
            //    //活动结束
            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.endNotice;
            //    responseEntity.rContent2 = actModel.endContent;
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/shakeLuckyMoney/shakeLuckyMoney.html?wid=" + apiid + "&aid=" + id;
            //    responseEntity.picUrl = actModel.endPic;

            //}
            //else
            //{
            //    //活动正在进行中 
            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.actName;
            //    responseEntity.rContent2 = actModel.brief;
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/shakeLuckyMoney/shakeLuckyMoney.html?wid=" + apiid + "&aid=" + id;
            //    responseEntity.picUrl = "/weixin/shakeLuckyMoney/images/shareLuckyMoney.png";
            //}
            //responselist.Add(responseEntity);
            return responselist;

        }

        /// <summary>
        /// 湖怪
        /// </summary>
        /// <param name="id"></param>
        /// <param name="apiid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        private IList<ResponseContentEntity> HuGuaiReponse(int id, int apiid, string openid)
        {
            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();
            //            ResponseContentEntity responseEntity = new ResponseContentEntity();
            //            responseEntity.id = id;
            //            responseEntity.wid = apiid;


            //            var ggkActBll = new PhotoService();
            //           var actModel = ggkActBll.GetModel(id);
            //            if (DateTime.Parse(actModel.beginDate) > DateTime.Now)
            //            {  //活动尚未开始 
            //                responseEntity.rcType = Model.ReponseContentType.text;
            //                responseEntity.rContent = "活动【" + actModel.actName + "】将于" + actModel.beginDate + "开始。";
            //            }
            ////    else if (DateTime.Parse(actModel.endDate) <= DateTime.Now)
            ////    {
            ////        //活动结束
            ////        responseEntity.rcType = Model.ReponseContentType.txtpic;
            ////        responseEntity.rContent = actModel.endNotice;
            ////        responseEntity.rContent2 = actModel.endContent;
            ////        responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/photo/end.aspx?wid=" + apiid + "&aid=" + id;
            ////        responseEntity.picUrl = actModel.endPic;
            ////
            ////    }
            ////    else
            ////    { 
            //                //活动正在进行中 
            //                responseEntity.rcType = Model.ReponseContentType.txtpic;
            //                responseEntity.rContent = actModel.actName;
            //                responseEntity.rContent2 = actModel.brief;
            //                responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/photo/MakePhoto.html?wid=" + apiid + "&aid=" + id;
            ////        responseEntity.picUrl = actModel.beginPic;
            ////    }
            //            responselist.Add(responseEntity);
            return responselist;

        }


        /// <summary>
        /// 刮刮卡回复内容
        /// </summary>
        /// <param name="id">模块主键Id</param>
        /// <param name="apiid">微帐号主键id</param>
        /// <param name="openid">openid</param>
        /// <param name="responseType">回复类型：1纯文字，2图文</param>
        /// <param name="responseVaule">回复的内容</param>
        private IList<ResponseContentEntity> GGKReponse(int id, int apiid, string openid)
        {

            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();

            //ResponseContentEntity responseEntity = new ResponseContentEntity();
            //responseEntity.id = id;
            //responseEntity.wid = apiid;

            //BLL.wx_ggkActionInfo ggkActBll = new BLL.wx_ggkActionInfo();
            //wx_ggkActionInfo actModel = ggkActBll.GetModel(id);
            //if (actModel.beginDate > DateTime.Now)
            //{  //活动尚未开始 

            //    responseEntity.rcType = ReponseContentType.text;
            //    responseEntity.rContent = "活动【" + actModel.actName + "】将于" + actModel.beginDate + "开始。";
            //}
            //else if (actModel.endDate <= DateTime.Now)
            //{
            //    //活动结束
            //    responseEntity.rcType = ReponseContentType.TextWithPicture;
            //    responseEntity.rContent = actModel.endNotice;
            //    responseEntity.rContent2 = actModel.endContent;
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/ggk/end.aspx?wid=" + apiid + "&aid=" + id;
            //    responseEntity.picUrl = actModel.endPic;
            //}
            //else
            //{
            //    //活动正在进行中
            //    responseEntity.rcType = ReponseContentType.TextWithPicture;
            //    responseEntity.rContent = actModel.actName;
            //    responseEntity.rContent2 = actModel.actContent;
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/ggk/index.aspx?wid=" + apiid + "&aid=" + id;
            //    responseEntity.picUrl = actModel.beginPic;
            //}
            //responselist.Add(responseEntity);

            return responselist;
        }

        /// <summary>
        /// 大转盘回复内容
        /// </summary>
        /// <param name="id">模块主键Id</param>
        /// <param name="apiid">微帐号主键id</param>
        /// <param name="openid">openid</param>
        /// <param name="responseType">回复类型：1纯文字，2图文</param>
        /// <param name="responseVaule">回复的内容</param>
        private IList<ResponseContentEntity> DZPReponse(int id, int apiid, string openid)
        {
            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();
            //ResponseContentEntity responseEntity = new ResponseContentEntity();
            //responseEntity.id = id;
            //responseEntity.wid = apiid;


            //BLL.wx_dzpActionInfo ggkActBll = new BLL.wx_dzpActionInfo();
            //Model.wx_dzpActionInfo actModel = ggkActBll.GetModel(id);
            //if (actModel.beginDate > DateTime.Now)
            //{  //活动尚未开始 
            //    responseEntity.rcType = Model.ReponseContentType.text;
            //    responseEntity.rContent = "活动【" + actModel.actName + "】将于" + actModel.beginDate + "开始。";
            //}
            //else if (actModel.endDate <= DateTime.Now)
            //{
            //    //活动结束
            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.endNotice;
            //    responseEntity.rContent2 = actModel.endContent;
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/dzp/end.aspx?wid=" + apiid + "&aid=" + id;
            //    responseEntity.picUrl = actModel.endPic;

            //}
            //else
            //{
            //    //活动正在进行中

            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.actName;
            //    responseEntity.rContent2 = actModel.brief;
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/dzp/index.aspx?wid=" + apiid + "&aid=" + id;
            //    responseEntity.picUrl = actModel.beginPic;
            //}
            //responselist.Add(responseEntity);
            return responselist;

        }

        /// <summary>
        /// 砸金蛋回复内容
        /// </summary>
        /// <param name="id">模块主键Id</param>
        /// <param name="apiid">微帐号主键id</param>
        /// <param name="openid">openid</param>
        /// <param name="responseType">回复类型：1纯文字，2图文</param>
        /// <param name="responseVaule">回复的内容</param>
        private IList<ResponseContentEntity> ZJDReponse(int id, int apiid, string openid)
        {
            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();
            //ResponseContentEntity responseEntity = new ResponseContentEntity();
            //responseEntity.id = id;
            //responseEntity.wid = apiid;


            //BLL.wx_zjdActionInfo ggkActBll = new BLL.wx_zjdActionInfo();
            //Model.wx_zjdActionInfo actModel = ggkActBll.GetModel(id);
            //if (actModel.beginDate > DateTime.Now)
            //{  //活动尚未开始 
            //    responseEntity.rcType = Model.ReponseContentType.text;
            //    responseEntity.rContent = "活动【" + actModel.actName + "】将于" + actModel.beginDate + "开始。";
            //}
            //else if (actModel.endDate <= DateTime.Now)
            //{
            //    //活动结束
            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.endNotice;
            //    responseEntity.rContent2 = actModel.endContent;
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/zjd/end.aspx?wid=" + apiid + "&aid=" + id;
            //    responseEntity.picUrl = actModel.endPic;

            //}
            //else
            //{
            //    //活动正在进行中

            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.actName;
            //    responseEntity.rContent2 = actModel.brief;
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/zjd/index.aspx?wid=" + apiid + "&aid=" + id;
            //    responseEntity.picUrl = actModel.beginPic;
            //}
            //responselist.Add(responseEntity);
            return responselist;

        }


        /// <summary>
        /// 优惠券简单版回复内容
        /// </summary>
        /// <param name="id">模块主键Id</param>
        /// <param name="apiid">微帐号主键id</param>
        /// <param name="openid">openid</param>
        /// <param name="responseType">回复类型：1纯文字，2图文</param>
        /// <param name="responseVaule">回复的内容</param>
        private IList<ResponseContentEntity> yhqjdReponse(int id, int apiid, string openid)
        {

            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();
            //ResponseContentEntity responseEntity = new ResponseContentEntity();
            //responseEntity.id = id;
            //responseEntity.wid = apiid;


            //BLL.wx_sTicket sttActBll = new BLL.wx_sTicket();
            //Model.wx_sTicket actModel = sttActBll.GetModel(id);
            //if (actModel.beginDate > DateTime.Now)
            //{  //活动尚未开始 
            //    responseEntity.rcType = Model.ReponseContentType.text;
            //    responseEntity.rContent = "活动【" + actModel.actionName + "】将于" + actModel.beginDate + "开始。";

            //}
            //else if (actModel.endDate <= DateTime.Now)
            //{
            //    //活动结束

            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.endNotice;
            //    responseEntity.rContent2 = actModel.endContent;
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/sticket/end.aspx?wid=" + apiid + "&aid=" + id;
            //    responseEntity.picUrl = actModel.endPic;
            //}
            //else
            //{
            //    //活动正在进行中

            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.actionName;
            //    responseEntity.rContent2 = actModel.brief;
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/sticket/index.aspx?wid=" + apiid + "&aid=" + id;
            //    responseEntity.picUrl = actModel.beginPic;

            //}

            //responselist.Add(responseEntity);
            return responselist;

        }

        /// <summary>
        /// 喜帖
        /// </summary>
        /// <param name="id"></param>
        /// <param name="apiid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        private IList<ResponseContentEntity> xitieReponse(int id, int apiid, string openid)
        {
            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();
            //ResponseContentEntity responseEntity = new ResponseContentEntity();
            //responseEntity.id = id;
            //responseEntity.wid = apiid;


            //BLL.wx_xt_base xtBll = new BLL.wx_xt_base();
            //Model.wx_xt_base actModel = xtBll.GetModel(id);
            //if (actModel.statedate < DateTime.Now)
            //{  //活动尚未开始 
            //    responseEntity.rcType = Model.ReponseContentType.text;
            //    responseEntity.rContent = "【" + actModel.wxTitle + "】将于" + actModel.statedate + "举办，现已经结束";

            //}

            //else
            //{
            //    //活动正在进行中

            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.wxTitle;
            //    responseEntity.rContent2 = actModel.word;
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/xitie/index.aspx?wid=" + apiid + "&xid=" + id;
            //    responseEntity.picUrl = actModel.fengmian;

            //}

            //responselist.Add(responseEntity);
            return responselist;
        }


        /// <summary>
        /// 会员卡
        /// </summary>
        /// <param name="id"></param>
        /// <param name="apiid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        private IList<ResponseContentEntity> ucardReponse(int id, int apiid, string openid)
        {
            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();
            //ResponseContentEntity responseEntity = new ResponseContentEntity();
            //responseEntity.id = id;
            //responseEntity.wid = apiid;

            //BLL.wx_ucard_store ucardBll = new BLL.wx_ucard_store();
            //Model.wx_ucard_store ucard = ucardBll.GetModel(id);



            //responseEntity.rcType = Model.ReponseContentType.txtpic;
            //responseEntity.rContent = ucard.storeName;
            //responseEntity.rContent2 = ucard.cardBrief;
            //responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/ucard/index.aspx?wid=" + apiid + "&id=" + id;
            //responseEntity.picUrl = ucard.hfPic;
            //responselist.Add(responseEntity);
            return responselist;
        }

        /// <summary>
        /// 电影院抢票
        /// </summary>
        /// <param name="id"></param>
        /// <param name="apiid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        private IList<ResponseContentEntity> DDYQPReponse(int id, int apiid, string openid)
        {

            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();

            //ResponseContentEntity responseEntity = new ResponseContentEntity();
            //responseEntity.id = id;
            //responseEntity.wid = apiid;

            //BLL.wx_qp_base ggkActBll = new BLL.wx_qp_base();
            //Model.wx_qp_base actModel = ggkActBll.GetModel(id);
            //if (actModel.actBegin > DateTime.Now)
            //{  //活动尚未开始 

            //    responseEntity.rcType = Model.ReponseContentType.text;
            //    responseEntity.rContent = "活动【" + actModel.bName + "】将于" + actModel.actBegin + "开始。";
            //}
            //else if (actModel.actEnd <= DateTime.Now)
            //{
            //    //活动结束
            //    responseEntity.rcType = Model.ReponseContentType.text;
            //    responseEntity.rContent = "活动已经结束了。";

            //}
            //else
            //{
            //    //活动正在进行中
            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.bName;
            //    responseEntity.rContent2 = "";
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/qiangpiao/index.aspx?wid=" + apiid + "&aid=" + id;
            //    responseEntity.picUrl = actModel.beginPic;
            //}
            //responselist.Add(responseEntity);

            return responselist;
        }

        /// <summary>
        /// 微房产
        /// </summary>
        /// <param name="id"></param>
        /// <param name="apiid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        private IList<ResponseContentEntity> WXFReponse(int id, int apiid, string openid)
        {

            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();

            //ResponseContentEntity responseEntity = new ResponseContentEntity();
            //responseEntity.id = id;
            //responseEntity.wid = apiid;

            //BLL.wx_fc_floor fcBll = new BLL.wx_fc_floor();
            //Model.wx_fc_floor actModel = fcBll.GetModel(id);
            //if (actModel == null)
            //{

            //    responseEntity.rcType = Model.ReponseContentType.text;
            //    responseEntity.rContent = "该房产信息不存在";
            //}

            //else
            //{

            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.newsTitle;
            //    // responseEntity.rContent2 = actModel.pSummary;
            //    responseEntity.rContent2 = "";
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/wfangchan/index.aspx?wid=" + apiid + "&fid=" + id;
            //    responseEntity.picUrl = actModel.newsCover;
            //}
            //responselist.Add(responseEntity);

            return responselist;
        }


        /// <summary>
        /// 微汽车
        /// </summary>
        /// <param name="id"></param>
        /// <param name="apiid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        private IList<ResponseContentEntity> WQCReponse(int id, int apiid, string openid)
        {

            IList<ResponseContentEntity> responselist = new List<ResponseContentEntity>();

            //ResponseContentEntity responseEntity = new ResponseContentEntity();
            //responseEntity.id = id;
            //responseEntity.wid = apiid;

            //BLL.wx_wq_fuhuiSys wqcBll = new BLL.wx_wq_fuhuiSys();

            //Model.wx_wq_fuhuiSys actModel = wqcBll.GetModel(id);
            //if (actModel == null)
            //{

            //    responseEntity.rcType = Model.ReponseContentType.text;
            //    responseEntity.rContent = "该汽车信息不存在";
            //}

            //else
            //{

            //    responseEntity.rcType = Model.ReponseContentType.txtpic;
            //    responseEntity.rContent = actModel.newstitle;
            //    // responseEntity.rContent2 = actModel.pSummary;
            //    responseEntity.rContent2 = "";
            //    responseEntity.detailUrl = MyCommFun.getWebSite() + "/weixin/wqiche/index.aspx?wid=" + apiid;
            //    responseEntity.picUrl = actModel.titlepic;
            //}
            //responselist.Add(responseEntity);

            return responselist;
        }


    }
}
