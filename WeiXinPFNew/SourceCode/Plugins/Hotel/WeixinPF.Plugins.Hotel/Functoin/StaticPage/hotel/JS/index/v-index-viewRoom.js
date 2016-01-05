

var ViewRoom = Vue.extend({
    template: '#view-room-template',
    props: ['wid','openid','hotel','order'],
    data: function () {
        return {
           rooms:[]
        }
    },
    activate: function (done) {
        var self = this;
        this.getData(function (data) {
            self.rooms=data;

        });
        done();
    },
    methods: {
        getData: function (callBack) {
            // GET request
            this.$http.get('/weixinpf/Functoin/Service/HotelService.asmx/GetRoomList',
                {wid:this.wid,openid:this.openid,hotelId:this.hotel.id}).then(function (response) {
                    if (response.data&&response.data.success) {
                        callBack(response.data.data);
                    }


            }, function (response) {

                // handle error
            });
        },
        viewOrderCreate: function (room) {
            //var data={
            //    room:room,
            //    hotel:hotel,
            //    openid:openid,
            //    wid:wid
            //};
            if (room) {
              console.log(this.order);
               this.order={
                 
                 discount:0,
                 totalPrice:0,
                 costPrice:0,
                 orderNum:1,
                 status:-1,
                 orderUser:this.order.orderUser
               };
                this.$dispatch('onimgDataDispatch', room.roomImgs);
                 this.$dispatch('onOrderDispatch', this.order);
                this.$dispatch('onviewOrderCreateDispatch', room);
            }
        }
    }
})
