

var ViewOrderCreate = Vue.extend({
    template: '#view-orderCreate-template',
    props: ['wid','openid','hotel','room','order','orderCount'],
    //data: function () {
    //    return {
    //        order:{
    //
    //        }
    //    }
    //},
    computed: {
        formCanEdit:function(){
            var result=false;
            if(!this.order||!this.order.status||this.order.status<=1)
            {
                result=true;
            }

            return result;
        },
        formCanSubmit:function(){
            var result=false;
            if(this.room.id&&this.room.id>0&&this.order&&this.order.status<=0)
            {
                result=true;
            }

            return result;
        },
        formCanCancel:function(){
            var result=false;
            if(this.order&&this.order.status>=0&&this.order.status<=2)
            {
                result=true;
            }

            return result;
        },
        formCanPay:function(){
            var result=false;
            if(this.order&&this.order.status==1)
            {
                result=true;
            }

            return result;
        },
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
        },
        idCardRequired:function(){
            var result=false;
            if(this.order.orderUser&&this.order.orderUser.userIdcard)
            {
                if(!this.isIdCard(this.order.orderUser.userIdcard))
                {
                    result=true;
                }
            }

            return result;
        },
        userMobileRequired:function(){
            var result=false;
            if(this.order.orderUser&&this.order.orderUser.userMobile)
            {
                if(!this.isMobile(this.order.orderUser.userMobile))
                {
                    result=true;
                }
            }

            return result;
        },
        canSubmit:function(){
            var result=true;
                if(this.isEmpty(this.order.orderUser.userName)
                    ||this.isEmpty(this.order.orderUser.userIdcard)
                    ||this.isEmpty(this.order.orderUser.userMobile)
                    ||this.isEmpty(this.order.arriveTime)
                    ||this.isEmpty(this.order.leaveTime)
                )
                {
                    return false;
                }
                else if(this.userMobileRequired||this.idCardRequired)
                {
                    return false;
                }
            return result;
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
        this.initItemCollapse();
        //this.toggleItemShow();

        //$('[type="date"].min-today').prop('min', function(){
        //    return new Date().toJSON().split('T')[0];
        //});
    },
    methods: {
        isIdCard:function(val){
            return (/(^\d{15}$)|(^\d{17}([0-9]|X)$)/.test(val));
        },
        isMobile:function(val){
            return (/^((1[3,5,8][0-9])|(14[5,7])|(17[0,6,7,8]))\d{8}$/ .test(val));
        },
        isEmpty:function(val){
            if(!val)
            {
                return true;
            }
           var str= val.trim();
           return (!str || 0 === str.length);
        },
        activateValidator:function(){
            //if(this.$activateValidator)
            //{
            //    this.$activateValidator();
            //}
        },
        getOrdrderData:function(){
            var self = this;
            var hasOrder=false;

            //如果已有订单，获取订单详情
            if(this.order&&this.order.id&&this.order.id>0)
            {
                hasOrder=true;
                this.getOrder(function(data){
                    self.order=data;
                    self.formReadonly();
                    self.room.id=data.roomId;
                    self.getRoom(function (data) {
                        self.room=data;
                        self.$dispatch('onimgDataDispatch', self.room.roomPictures);
                        $.hidePreloader();
                        self.activateValidator();
                            


                    },function(){
                        self.activateValidator();
                    });
                    // if(self.$activateValidator)
                    // {
                    //     self.$activateValidator();
                    // }

                },function(){
                    self.activateValidator();
                });
            }
            //如果房间对象存在，则获取详细的房间信息
            else if(this.room&&this.room.id){
                this.getRoom(function (data) {
                    self.room=data;
                    $.hidePreloader();
                    if(!hasOrder)
                    {
                        self.activateValidator();

                    }
                },function(){
                    self.activateValidator();
                });
            }
            else
            {
                self.activateValidator();
            }
        },
        getNoOrderData: function () {
            var self = this;
            if(!this.order||!this.order.id||this.order.id<=0)
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
                    $.hidePreloader();
                     self.$dispatch('onOrderDispatch', self.order);
                    //self.$activateValidator();
                });
            }
        },
        getRoom: function (callBack,errorCallBack) {
            // GET request
            this.$http.get('Service/HotelService.asmx/GetRoom',
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
            this.$http.get('Service/HotelService.asmx/GetOrderLastUserInfo',
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
            this.$http.get('Service/HotelService.asmx/GetOrder',
                {wid:this.wid,openid:this.openid,orderId:this.order.id}).then(function (response) {
                    if (response.data&&response.data.success) {
                        callBack(response.data.data);

                    }
                    else{
                        $.toast("获取订单失败!");
                    }
                }, function (response) {
                    errorCallBack(response);
                    // handle error
                });
        },
        onSubmit: function () {
            var self=this;
            if(!this.canSubmit)
            {
                return;
            }
            var jsonOrder=JSON.stringify(this.order);

            this.$http.post('Service/HotelService.asmx/SaveOrder',
                {   wid:this.wid,
                    openid:this.openid,
                    hotelId:this.hotel.id,
                    roomId:this.room.id,
                    roomType:this.room.roomType,
                    order:jsonOrder})
                .then(function (response) {
                    response.data=JSON.parse(response.data);
                    if (response.data&&response.data.success) {
                        self.orderCount++;
                        self.updateOrderNumber(self.orderCount);
                        $.toast("保存成功!");
                    }
                    else{
                        $.toast("保存失败!");
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
        },
        initItemCollapse:function(){
            $(".item-content.item-collapse .item-header").click(function () {
                var zThis = $(this).parent();

                zThis.toggleClass("in");
            });
        },
        formReadonly:function(){
            $("form").find('input,select,textarea').prop("readonly",!this.formCanEdit);
        }
    }
});
