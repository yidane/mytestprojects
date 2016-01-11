
var ViewOrder = Vue.extend({
    template: '#view-order-template',
    props: ['wid', 'openid', 'hotel', 'room', 'order'],
    data: function () {
        return {
            orders: {}
        }
    },
    computed: {
        statusCss: function () {

        }
    },
    //activate: function (done) {
    //    var self = this
    //    this.getData(function (data) {
    //
    //        self.orders = data
    //
    //    })
    //    done()
    //},
    ready: function () {
        var self = this;
        this.getOrderList(function (data) {
            $.hidePreloader();
            self.orders = data;
            self.updateOrderNumber(data.length);
        });
    },
    methods: {
        getOrderList: function (callBack) {
            // GET request
            this.$http.get('api/order/GetOrderList',
                { wid: this.wid, openid: this.openid, hotelId: this.hotel.id }).then(function (response) {

                    if (response.data) {
                        callBack(response.data.orders);

                    }
                    else {
                        $.toast("获取订单失败!");
                    }

                }, function (response) {

                    // handle error
                });
        },
        updateOrderNumber: function (num) {
            this.$dispatch('onUpdateOrderNumberDispatch', num);
        },
        viewOrderCreate: function (order) {
            if (order) {
                //var room = {
                //    id: order.roomId
                //};
                //this.$dispatch('onviewOrderCreateDispatch', room);

                this.$dispatch('onOrderDispatch', order);
            }
        },
        pullRefresh:function(){
            var self=this;
            // 添加'refresh'监听器
            $(document).on('refresh', '.pull-to-refresh-content',function(e) {
                //$.showPreloader();
                this.getOrderList(function (data) {
                    //$.hidePreloader();
                    self.orders = data;
                    self.updateOrderNumber(data.length);
                    $.pullToRefreshDone('.pull-to-refresh-content');
                });
            });
        }
    }
});
