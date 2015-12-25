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

// ������ʵ��
var vm = new Vue({
    el: '#div_app',

    data: {
        currentView: 'view-room',
        testVal: '123',
        person: {},
        imgDatas:[],
        headerTitle:'����'
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
    // �ڴ���ʵ��ʱ `events` ѡ��򵥵ص��� `$on`
    events: {
        'child-msg': function (msg) {
            // �¼��ص��ڵ� `this` �Զ��󶨵�ע������ʵ����
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
    // �� `methods` �����ж��巽��
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

                // �����Ҫ��ҳ��
                pagination: '.swiper-pagination',

                // �����Ҫǰ�����˰�ť
                nextButton: '.swiper-button-next',
                prevButton: '.swiper-button-prev',

                // �����Ҫ������
                scrollbar: '.swiper-scrollbar'
            });
        }
    },
    ready: function () {
        var self=this;

        //1.�Ӹ������ȡ���ݣ����ݸ�app-image���
        this.getData(function(data){
            self.imgDatas=data;

            setTimeout(function(){
                self.initSwiper();
            },1000);
        });

        //2.������ͼ����ʱ���ȡ���ݣ����ݸ�app-image���
        //this.currentView='view-room';



    }
})
