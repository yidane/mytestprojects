using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WeiXinPF.Plugins.WeiXinPay.Service.Models;

namespace WeiXinPF.Plugins.WeiXinPay.Service.Application.Repository
{
    interface IPayOrderRepository
    {
        void UnifiedOrder(WeiXinUnifiedOrderInfo unifiedOrderInfo);

        void RefundOrder(WeiXinRefundOrderInfo refundOrderInfo);
    }
}