

var ViewRoom = Vue.extend({
    template: '#view-room-template',
    //props: ['myMessage'],
    data: function () {
        return {
            person: {}
        }
    },
    activate: function (done) {
        var self = this;
        this.getData(function (data) {
            self.person = data;

        });
        done();
    },
    methods: {

        getData: function (callBack) {
            // GET request
            this.$http.get('/Functoin/Service/HotelService.asmx/GetJson').then(function (response) {

                // set data on vm

                if (response.data) {
                    callBack(response.data)
                }

            }, function (response) {

                // handle error
            });
        }
    }
})