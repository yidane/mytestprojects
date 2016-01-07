var ViewAbout = Vue.extend({
    template: '#view-about-template',
    props: ['wid','openid','hotel'],
    //data: function () {
    //    return {
    //        imgData: []
    //    }
    //},
    //activate: function (done) {
    //    var self = this;
    //    done();
    //},
    //methods: {
    //    notify: function () {
    //        if (this.imgData) {
    //            this.$dispatch('imgData', this.imgData);
    //        }
    //    },
    //    getData: function (callBack) {
    //        //this.$parent.getData(callBack);
    //
    //        this.$http.get('Service/HotelService.asmx/GetHotelInfo'
    //            ,{wid:this.wid,openid:this.openid}).then(function (response) {
    //                if(response)
    //                {
    //                    callBack(response.data);
    //                }
    //
    //
    //        }, function (response) {
    //
    //            // handle error
    //        });
    //
    //    }
    //}
});