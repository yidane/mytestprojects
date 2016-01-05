

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
        },
        today:function(){
            return new Date().toJSON().split('T')[0];
        },
        minLeaveDate:function(){
            var leave=  new Date();
            if(this.order.arriveTime)
            {
               var arrive=  new Date(this.order.arriveTime);

                leave=arrive;
            }
            leave.setDate(leave.getDate() + 1);
            return leave.toJSON().split('T')[0];
        }


    },
    watch: {
        'order': function (order, oldVal) {

            //不同订单，显示不同信息
            if(oldVal.id!=order.id)
            {
                if(order.id>0)
                {
                    this.getOrdrderData();
                }
                else
                {
                    this.getNoOrderData();
                }
            }
        }
    },
    activate: function (done) {
        var self = this;
        this.getNoOrderData();

        //this.toggleItemShow();
        done();
    },

    ready:function(){
        var self = this;
       this.getOrdrderData();

        this.toggleItemShow();

        //$('[type="date"].min-today').prop('min', function(){
        //    return new Date().toJSON().split('T')[0];
        //});
    },
    methods: {
        getOrdrderData:function(){
            var self = this;
            var hasOrder=false;

            //如果已有订单，获取订单详情
            if(this.order&&this.order.id&&this.order.id>0)
            {
                hasOrder=true;
                this.getOrder(function(data){
                    self.order=data;

                    self.getRoom(function (data) {
                        self.room=data;

                            if(self.$activateValidator)
                            {
                                self.$activateValidator();
                            }
                            


                    },function(){
                        if(self.$activateValidator)
                        {
                            self.$activateValidator();
                        }
                    });
                    // if(self.$activateValidator)
                    // {
                    //     self.$activateValidator();
                    // }

                },function(){
                    if(self.$activateValidator)
                    {
                        self.$activateValidator();
                    }
                });
            }
            //如果房间对象存在，则获取详细的房间信息
            else if(this.room&&this.room.id){
                this.getRoom(function (data) {
                    self.room=data;
                    if(!hasOrder)
                    {
                        if(self.$activateValidator)
                        {
                            self.$activateValidator();
                        }

                    }
                },function(){
                    if(self.$activateValidator)
                    {
                        self.$activateValidator();
                    }
                });
            }
            else
            {
                if(self.$activateValidator)
                {
                    self.$activateValidator();
                }
            }
        },
        getNoOrderData: function () {
            var self = this;
            if(!this.order||!this.order.id)
            {
                self.order={
                    id:0,
                    discount:0,
                    totalPrice:0,
                    costPrice:0,
                    orderNum:1,
                    status:-1,
                    orderUser:{}
                };

                this.getOrderLastUserInfo(function(data){
                    self.order.orderUser=data;
                     self.$dispatch('onOrderDispatch', self.order);
                    //self.$activateValidator();
                });
            }
        },
        getRoom: function (callBack,errorCallBack) {
            // GET request
            this.$http.get('/weixinpf/Functoin/Service/HotelService.asmx/GetRoom',
                {wid:this.wid,openid:this.openid,hotelId:this.hotel.id,roomId:this.room.id}).then(function (response) {
                    if (response.data&&response.data.success) {
                        callBack(response.data.data);
                    }

                }, function (response) {
                    errorCallBack(response);
                    // handle error
                });
        },
        getOrderLastUserInfo:function(callBack){
            this.$http.get('/weixinpf/Functoin/Service/HotelService.asmx/GetOrderLastUserInfo',
                {wid:this.wid,openid:this.openid}).then(function (response) {
                    if (response.data&&response.data.success) {
                        callBack(response.data.data);
                    }

                }, function (response) {

                    // handle error
                });
        },
        getOrder: function (callBack,errorCallBack) {
            // GET request
            this.$http.get('/weixinpf/Functoin/Service/HotelService.asmx/GetOrder',
                {wid:this.wid,openid:this.openid,orderId:this.order.id}).then(function (response) {
                    if (response.data&&response.data.success) {
                        callBack(response.data.data);
                    }

                }, function (response) {
                    errorCallBack(response);
                    // handle error
                });
        },
        onSubmit: function () {

            var jsonOrder=JSON.stringify(this.order);

            this.$http.post('/weixinpf/Functoin/Service/HotelService.asmx/SaveOrder',
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
