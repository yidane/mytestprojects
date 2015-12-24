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
var AppFooter = Vue.extend( {
        template: '#app-footer-template',
    data: function () {
        return { msg: 'about' }
    },
    methods: {
        notify: function (msg) {
            this.msg=msg
            if (this.msg.trim()) {
                this.$dispatch('child-msg', this.msg)
            }
        }
    }
})

// ������ʵ��
var vm=new Vue({
    el: '#div_app',
   
    data: {
        currentView: 'view-about',
        testVal:'123',
        person:{}
        },
    components:{
                'app-header':{
                    template: '<div>this is header!</div>'
                },
        'app-footer':AppFooter,
        'view-about':{
            template:'#view-about-template',
            props: ['myMessage'],
            data: function () {
                return { msg: 'hello' }
            },
            methods: {
                notify: function () {
                    if (this.msg.trim()) {
                        this.$dispatch('child-msg', this.msg)
                        this.msg = ''
                    }
                }
            }
                },
                'view-room':{
                    template:'<div>this is room!</div>'
                },
                'view-order':{
                    template:'<div>this is order!</div>'
                }
    },
        // �ڴ���ʵ��ʱ `events` ѡ��򵥵ص��� `$on`
        events: {
            'child-msg': function (msg) {
                // �¼��ص��ڵ� `this` �Զ��󶨵�ע������ʵ����
                this.currentView=msg;
            }
        },
    // �� `methods` �����ж��巽��
    methods: {
        getData: function () {
            // GET request
            this.$http.get('/Functoin/Service/HotelService.asmx/GetJson').then(function (response) {

                // set data on vm
                this.person= response.data;

            }, function (response) {

                // handle error
            });
        }
    },
    ready: function() {
        this.getData();


    }
})

    //new Vue({
    //    el: 'div_app',
    //    //data: {
    //    //    currentView: 'view-about'
    //    //},
    //    components: {
    //        'app-header':{
    //            template: '<div>this is header!</div>'
    //        }
    //        //,'app-footer':AppFooter,
    //        //'view-about':{
    //        //    template:'<div>this is about!</div>'
    //        //},
    //        //'view-room':{
    //        //    template:'<div>this is room!</div>'
    //        //},
    //        //'view-order':{
    //        //    template:'<div>this is order!</div>'
    //        //}
    //    },
    //    // �ڴ���ʵ��ʱ `events` ѡ��򵥵ص��� `$on`
    //    events: {
    //        'child-msg': function (msg) {
    //            // �¼��ص��ڵ� `this` �Զ��󶨵�ע������ʵ����
    //            this.currentView(msg)
    //        }
    //    }
    //})
    //
