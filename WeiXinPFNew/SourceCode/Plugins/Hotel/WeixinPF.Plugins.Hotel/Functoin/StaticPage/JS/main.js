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

// 创建根实例
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
        // 在创建实例时 `events` 选项简单地调用 `$on`
        events: {
            'child-msg': function (msg) {
                // 事件回调内的 `this` 自动绑定到注册它的实例上
                this.currentView=msg;
            }
        },
    // 在 `methods` 对象中定义方法
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
    //    // 在创建实例时 `events` 选项简单地调用 `$on`
    //    events: {
    //        'child-msg': function (msg) {
    //            // 事件回调内的 `this` 自动绑定到注册它的实例上
    //            this.currentView(msg)
    //        }
    //    }
    //})
    //
