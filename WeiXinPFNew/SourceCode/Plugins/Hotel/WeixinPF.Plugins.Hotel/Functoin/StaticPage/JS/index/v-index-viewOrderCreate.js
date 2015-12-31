

var ViewOrderCreate = Vue.extend({
    template: '#view-orderCreate-template',
    props: ['wid','openid','hotel','room','order'],
    //data: function () {
    //    return {
    //        order:{
    //
    //        }
    //    }
    //},
    computed: {
        dateSpan:function(){
            var days=1;
            if(this.order.arriveTime&&this.order.leaveTime)
            {
                var span=Date.parse(this.order.leaveTime)-Date.parse(this.order.arriveTime);
                days=Math.floor(span/(24*3600*1000));

            }
            return days;
        },
        totalPrice:function () {

            return (this.order.orderNum *this.dateSpan* this.room.totalPrice).toFixed(2);
        },

            costPrice: function () {

                return (this.order.orderNum *this.dateSpan* this.room.costPrice).toFixed(2);
            },
        discount:function () {

            return  (this.costPrice-this.totalPrice).toFixed(2);
        }


    },
    //watch: {
    //    'order.arriveTime': function (val, oldVal) {
    //        console.log('new: %s, old: %s', val, oldVal)
    //        console.log('type:%s',typeof val)
    //    }
    //},
    activate: function (done) {
        var self = this;
        if(!this.order||!this.order.id)
        {
            self.order={
                id:0,
                discount:0,
                totalPrice:0,
                costPrice:0,
                orderNum:1
            };
            this.getOrderLastUserInfo(function(data){
                self.order.orderUser=data;
                self.$activateValidator();
            });
        }

        //this.toggleItemShow();
        done();
    },

    ready:function(){
        var self = this;
        //如果房间对象存在，则获取详细的房间信息
        if(this.room&&this.room.id){
            this.getRoom(function (data) {
                self.room=data;
            });
        }
        //如果已有订单，获取订单详情
        if(this.order&&this.order.id&&this.order.id>0)
        {
            this.getOrder(function(data){
                self.order=data;
            });
        }
        this.toggleItemShow();
    },
    methods: {

        getRoom: function (callBack) {
            // GET request
            this.$http.get('/Functoin/Service/HotelService.asmx/GetRoom',
                {wid:this.wid,openid:this.openid,hotelId:this.hotel.id,roomId:this.room.id}).then(function (response) {
                    if (response.data) {
                        callBack(response.data)
                    }

                }, function (response) {

                    // handle error
                });
        },
        getOrderLastUserInfo:function(callBack){
            this.$http.get('/Functoin/Service/HotelService.asmx/GetOrderLastUserInfo',
                {wid:this.wid,openid:this.openid}).then(function (response) {
                    if (response.data) {
                        callBack(response.data);
                    }

                }, function (response) {

                    // handle error
                });
        },
        getOrder: function (callBack) {
            // GET request
            this.$http.get('/Functoin/Service/HotelService.asmx/GetOrder',
                {wid:this.wid,openid:this.openid,orderId:this.order.id}).then(function (response) {
                    if (response.data) {
                        callBack(response.data)
                    }

                }, function (response) {

                    // handle error
                });
        },
        onSubmit: function () {

            var jsonOrder=JSON.stringify(this.order);

            this.$http.post('/Functoin/Service/HotelService.asmx/SaveOrder',
                {wid:this.wid,openid:this.openid,hotelId:this.hotel.id,roomId:this.room.id,order:jsonOrder})
                .then(function (response) {
                    if (response.data) {
                        alert('保存成功！');
                    }
                }, function (response) {

                    // handle error
                });
        },
        viewOrderCreate: function (room,$event) {
            if (room) {
                this.$dispatch('onviewOrderCreateDispatch', room);
            }
        },
        toggleItemShow:function(){
            //显影
            $(".gpd-item-title").click(function () {
                var zThis = $(this);
                var zThisParent = zThis.parents(".detailcontent");
                zThisParent.toggleClass("gdp-curr");
                zThisParent.siblings(".detailcontent").removeClass("gdp-curr");
            });
        }
    }
})