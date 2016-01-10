using System;
using System.Collections.Generic;
using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model;

namespace WeiXinPF.Plugins.WeiXinPay.Service.Application.Repository
{
    interface IPayNotifyRepository
    {
        bool PayNotify(PaymentNotify paymentNotify, out string message);
    }
}
