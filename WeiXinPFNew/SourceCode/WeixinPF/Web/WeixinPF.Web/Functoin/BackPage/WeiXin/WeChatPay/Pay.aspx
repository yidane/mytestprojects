<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pay.aspx.cs" Inherits="WeixinPF.Web.Functoin.BackPage.WeiXin.WeChatPay.Pay" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>微支付</title>
    <meta http-equiv="Content-type" content="text/html; charset=GBK" />
    <meta content="application/xhtml+xml;charset=GBK" http-equiv="Content-Type" />
    <meta content="telephone=no, address=no" name="format-detection" />
    <meta content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script type="text/javascript">
        if (<%= InitPayComplete %> !== 0) {
            var completeUrl = "<%=PayComplete%>";
            wx.config({
                debug: false,
                appId: "<%=this.JsApiTicket.AppId%>",
                timestamp: "<%=this.JsApiTimeStamp%>",
                nonceStr: "<%=this.JsApiNonceStr%>",
                signature: "<%=this.JsApiSingature%>",
                jsApiList: [
                    'chooseWXPay',
                    'getNetworkType',
                    'onMenuShareAppMessage',
                    'onMenuShareTimeline',
                    'onMenuShareQQ',
                    'hideOptionMenu'
                ]
            });

            wx.ready(function() {
                wx.chooseWXPay({
                    timestamp: "<%=this.JsApiParameter.timeStamp %>",
                    nonceStr: "<%=this.JsApiParameter.nonceStr%>",
                    package: "<%=this.JsApiParameter.package%>",
                    signType: 'MD5',
                    paySign: "<%=this.JsApiParameter.paySign %>",
                    success: function(res) {
                        completeUrl = completeUrl + "success";
                        document.location.href = completeUrl;
                    },
                    fail: function(res) {
                        completeUrl = completeUrl + "fail";
                        document.location.href = completeUrl;
                    },
                    cancel: function(res) {
                        completeUrl = completeUrl + "cancel";
                        document.location.href = completeUrl;
                    },
                    complete: function(res) {
                    }
                });

                wx.hideOptionMenu();
            });

            wx.error(function(res) {
                alert(res.err_code + "______" + res.err_desc + "______" + res.err_msg);
            });
        } else {
            document.getElementById("bounce1").style.display = "none";
            document.getElementById("bounce2").style.display = "none";
            document.getElementById("bounce3").style.display = "none";
        }
    </script>

    <style type="text/css">
        .spinner {
            margin: 100px auto 0;
            width: 100%;
            text-align: center;
            position: absolute;
            top: 50%;
            margin-top: -50px;
        }

            .spinner > div {
                width: 20px;
                height: 20px;
                background-color: #67CF22;
                border-radius: 100%;
                display: inline-block;
                -webkit-animation: bouncedelay 1.4s infinite ease-in-out;
                animation: bouncedelay 1.4s infinite ease-in-out;
                /* Prevent first frame from flickering when animation starts */
                -webkit-animation-fill-mode: both;
                animation-fill-mode: both;
            }

            .spinner .bounce1 {
                -webkit-animation-delay: -0.32s;
                animation-delay: -0.32s;
            }

            .spinner .bounce2 {
                -webkit-animation-delay: -0.16s;
                animation-delay: -0.16s;
            }

        @-webkit-keyframes bouncedelay {
            0%, 80%, 100% {
                -webkit-transform: scale(0.0);
            }

            40% {
                -webkit-transform: scale(1.0);
            }
        }

        @keyframes bouncedelay {
            0%, 80%, 100% {
                transform: scale(0.0);
                -webkit-transform: scale(0.0);
            }

            40% {
                transform: scale(1.0);
                -webkit-transform: scale(1.0);
            }
        }
    </style>
</head>
<body>
    <div class="spinner">
        <div id="bonce1" class="bounce1"></div>
        <div id="bonce2" class="bounce2"></div>
        <div id="bonce3" class="bounce3"></div>
    </div>
</body>
</html>
