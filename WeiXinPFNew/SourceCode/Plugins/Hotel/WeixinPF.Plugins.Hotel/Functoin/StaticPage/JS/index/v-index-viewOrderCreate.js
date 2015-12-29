

var ViewOrderCreate = Vue.extend({
    template: '#view-room-template',
    props: ['wid','openid','hotel','room'],
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
            this.$http.get('/Functoin/Service/HotelService.asmx/GetRooms',
                {wid:this.wid,openid:this.openid,hotelId:this.hotel.id}).then(function (response) {

                    // set data on vm

                    if (response.data) {
                        callBack(response.data)
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
                this.$dispatch('onviewOrderCreateDispatch', room);
            }
        }
    }
})