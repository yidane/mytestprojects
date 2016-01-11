/**
 * Created by jxiao_000 on 2016/1/6.
 */
//ȫ�����ã�vue-resource
Vue.http.options.root = '/Functoin';
Vue.http.headers.common['Authorization'] = 'Basic YXBpOnBhc3N3b3Jk';


var ViewOrder = Vue.extend({
    template: '#view-order-template',
    props: ['wid', 'openid', 'currentView'],
    data: function () {
        return {
            orders: []
        }
    },
    watch: {
        'currentView': function (view, oldVal) {
            if (view != oldVal) {
                this.orders.filter(this.filterDataFunc);
            }
        }
    },
    activate: function (done) {
        var self = this;
        this.getData(function (data) {
            self.orders = data.data;
        });
        done();
    },

    methods: {

        getData: function (callBack) {
            //this.$parent.getData(callBack);

            this.$http.get('Service/HotelService.asmx/GetOrderList'
                , {openid: this.openid,wid:1,hotelId:1}).then(function (response) {
                    if (response) {
                        callBack(response.data);
                        $.hidePreloader();
                    }


                }, function (response) {

                    // handle error
                });

        },
        filterDataFunc: function (data) {
            var result = true;
            if (this.currentView == 'pay') {
                if (data.status != 3) {
                    result = false;
                }
            }
            else if (this.currentView == 'refund') {
                if (data.status != 7) {
                    result = false;
                }
            }

            return result;
        }
    }
});

var vm = new Vue({
    el: '#div_app',

    data: {
        currentView: '',
        router:{},
        headerTitle: '',
        wid: 0,
        openid: ''
    },
    components: {
        'app-header': {
            template: '#app-header-template',
            props: ['headerTitle','router','currentView'],
            methods:{
                changeView:function(view){
                    this.router.setRoute(view);
                },
            }
        },

        'view-order': ViewOrder
    },

    events: {
        'onChangeView': function (msg) {
            this.currentView = msg;

        },

    },
    watch: {
        'currentView': function (val, oldVal) {
            switch (val) {
                case 'view-about':
                    this.headerTitle = '关于';
                    window.document.title = '关于';
                    break;
                case 'view-room':
                    this.headerTitle = '房型选择';
                    window.document.title = '房型选择';
                    break;
                case 'view-order':
                    this.headerTitle = '订单';
                    window.document.title = '订单';
                    break;
                case 'view-orderCreate':
                    this.headerTitle = '订单';
                    window.document.title = '订单';
                    break;
            }
        }
    },
    methods: {



        getQueryStringByName: function (name) {

            var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));

            if (result == null || result.length < 1) {

                return "";

            }

            return result[1];

        },
        getQueryString: function () {
            var wid = this.getQueryStringByName('wid');
            var openid = this.getQueryStringByName('openid');
            if (!openid) {
                console.log('没有openid，跳转获�?');
                document.location.href = this.getOpenid();
                return;
            }
            this.getQueryData(wid, openid);
        },

        getOpenid: function () {
            //todo:跳转获取openid
            var openid = 'test';
            return document.location.href + "&openid=" + openid;
        },
        getQueryData: function (wid, openid) {
            if (!this.openid) {
                this.openid = openid;
                this.wid = wid;


            }

        },
        initRouter: function () {
            var self = this;
            var routes = {
                '/all': function () {
                    self.currentView = 'all';
                },
                '/pay': function () {

                    self.currentView = 'pay';
                },
                '/refund': function () {

                    self.currentView = 'refund';
                }
            };

            this.router = Router(routes);
            this.router.init('/all');
        },
        initPreLoader:function(){
            $.showPreloader();
        }
    },

    ready: function () {
        var self = this;
        this.getQueryString();
        this.initRouter();
        this.initPreLoader();

    }
});
