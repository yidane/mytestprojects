// main.js
//var Vue = require('vue')
//// require a *.vue component
//var App = require('./components/App.vue')
//
//// mount a root Vue instance
//new Vue({
//    el: 'body',
//    components: {
//        // include the required component
//        // in the options
//        app: App
//    }
//})


//


var ViewAbout= Vue.extend({
    template: '#view-about-template',

    data: function() {
        return {
            imgData: []
        }
    },
    activate: function (done) {
        var self = this;
        this.getData(function (data) {
            self.imgData=data;
            self.notify();


        });
        done();
    },
    methods: {
        notify: function () {
            if (this.imgData) {
                this.$dispatch('imgData', this.imgData);
            }
        },
        getData: function (callBack) {
            this.$parent.getData(callBack);
        }
    }
})

var ViewOrder=Vue.extend({
    template: '#view-order-template',
    props: ['myMessage'],
    data: function() {
        return {
            orders:{}
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

var ViewRoom=Vue.extend({
    template: '#view-room-template',
    //props: ['myMessage'],
    data: function() {
        return {person:{}
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

// 创建根实例
var vm = new Vue({
    el: '#div_app',

    data: {
        currentView: 'view-room',
        testVal: '123',
        person: {},
        imgDatas:[],
        headerTitle:'关于'
    },
    components: {
        'app-header': {
            template: '#app-header-template',
            props:['header-title']
        },
        'app-footer': AppFooter,
        'view-about': ViewAbout,

        'view-room':  ViewRoom,
        'view-order': ViewOrder,
        'app-image':{
            template:'#view-image-template',
            props: ['viewImages']
        }
    },
    // 在创建实例时 `events` 选项简单地调用 `$on`
    events: {
        'child-msg': function (msg) {
            // 事件回调内的 `this` 自动绑定到注册它的实例上
            this.currentView = msg;
            switch (msg){
                case '':
                    this.headerTitle='';
                    break;
            }
        },
        'onimgDataDispatch':function(data){
            this.imgDatas=data;
        }
    },
    // 在 `methods` 对象中定义方法
    methods: {
        getData: function (callBack) {
            //// GET request
            //this.$http.get('/Functoin/Service/HotelService.asmx/GetJson').then(function (response) {
            //
            //    // set data on vm
            //    this.person = response.data;
            //
            //}, function (response) {
            //
            //    // handle error
            //});
            var data=[{no:1 ,img:'http://www.cloudorg.com.cn/upload/201512/14/201512141710309494.png'},
                {no:2 ,img:'http://www.cloudorg.com.cn/upload/201512/14/201512141712439846.jpg'},
            ];
            callBack(data);
        },
        initSwiper:function(){
            var mySwiper = new Swiper('.swiper-container', {
                direction: 'vertical',
                loop: true,

                // 如果需要分页器
                pagination: '.swiper-pagination',

                // 如果需要前进后退按钮
                nextButton: '.swiper-button-next',
                prevButton: '.swiper-button-prev',

                // 如果需要滚动条
                scrollbar: '.swiper-scrollbar'
            });
        }
    },
    ready: function () {
        var self=this;

        //1.从父组件获取数据，传递给app-image组件
        this.getData(function(data){
            self.imgDatas=data;

            setTimeout(function(){
                self.initSwiper();
            },1000);
        });

        //2.关于视图加载时候获取数据，传递给app-image组件
        //this.currentView='view-room';



    }
})
