﻿
var ViewOrder = Vue.extend({
    template: '#view-order-template',
    props: ['wid','openid','hotel','room','order'],
    data: function () {
        return {
            orders: {}
        }
    },
    computed: {
        statusCss:function(){

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
    ready:function(){
        var self = this
        this.getOrderList(function (data) {

            self.orders = data;
            self.updateOrderNumber(data.length);
        });
    },
    methods: {
        getOrderList: function (callBack) {
            // GET request
            this.$http.get('/Functoin/Service/HotelService.asmx/GetOrderList',
                {wid:this.wid,openid:this.openid,hotelId:this.hotel.id}).then(function (response) {

                    if (response.data&&response.data.success) {
                        callBack(response.data.data);
                    }

            }, function (response) {

                // handle error
            });
        },
        updateOrderNumber:function(num){
            this.$dispatch('onUpdateOrderNumberDispatch', num);
        },
        viewOrderCreate: function (order) {
        if (order) {
            var room={
                id:order.roomId
            };
              
             this.$dispatch('onOrderDispatch', order);
                this.$dispatch('onviewOrderCreateDispatch', room);
            }
        }
    }
})
