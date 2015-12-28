
var ViewOrder = Vue.extend({
    template: '#view-order-template',
    props: ['myMessage'],
    data: function () {
        return {
            orders: {}
        }
    },
    activate: function (done) {
        var self = this
        this.getData(function (data) {

            self.orders = data

        })
        done()
    },
    methods: {

        getData: function (callBack) {
            // GET request
            this.$http.get('/Functoin/Service/HotelService.asmx/GetListJson').then(function (response) {

                // set data on vm

                if (response.data) {
                    callBack(response.data.orders)
                }

            }, function (response) {

                // handle error
            });
        }
    }
})