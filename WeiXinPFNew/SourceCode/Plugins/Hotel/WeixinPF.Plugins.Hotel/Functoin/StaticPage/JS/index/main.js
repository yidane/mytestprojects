

Vue.validator('idcard', function (val) {
    val = val.toUpperCase();
    //身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X。
    return (/(^\d{15}$)|(^\d{17}([0-9]|X)$)/.test(val));
})

Vue.validator('mobile', function (val) {

    return (/^1[3|4|5|8|9][0-9]\d{8}$/ .test(val));
})


// for fake fetch function
function getDispalyName(id, cb) {
    setTimeout(function () {
        cb(null, 'username1')
    }, 2000)
}

// for fake store function
function saveDisplayName(id, val, cb) {
    setTimeout(function () {
        cb(null)
    }, 2000)
}


var vm = new Vue({
    el: '#div_app',

    data: {
        currentView: '',
        person: {},
        imgDatas: [],
        headerTitle: '',
        hotel: {},
        room: {},
        order:{
            id:0
        },
        orderCount:0
    },
    components: {
        'app-header': {
            template: '#app-header-template',
            props: ['headerTitle']
        },
        'app-footer': AppFooter,
        'view-about': ViewAbout,

        'view-room': ViewRoom,
        'view-order': ViewOrder,
        'view-orderCreate': ViewOrderCreate,
        'app-image': {
            template: '#view-image-template',
            props: ['viewImages']
        }
    },

    events: {
        'onChangeView': function (msg) {
            // �¼��ص��ڵ� `this` �Զ��󶨵�ע������ʵ����
            this.currentView = msg;

        },
        'onimgDataDispatch': function (data) {
            this.imgDatas = data;
        },
        'onviewOrderCreateDispatch': function (data) {
            //this.currentView='view-orderCreate';
            this.room = data;
        },
        'onUpdateOrderNumberDispatch':function(data){
            if(data>this.orderCount)
            {
                this.orderCount=data;
            }
        },
        'onOrderDispatch':function(data){
            this.order=data;
        }
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
        getData: function (callBack) {

            var data = [{no: 1, img: 'http://www.cloudorg.com.cn/upload/201512/14/201512141710309494.png'},
                {no: 2, img: 'http://www.cloudorg.com.cn/upload/201512/14/201512141712439846.jpg'},
            ];
            callBack(data);
        },
        getOrderCount:function(callBack){
            this.$http.get('/Functoin/Service/HotelService.asmx/GetOrderCount'
                , {wid: this.wid, openid: this.openid,hotelId:this.hotel.id}).then(function (response) {
                    if (response.data&&response.data.success) {
                        callBack(response.data.data);
                    }
                }, function (response) {

                    // handle error
                });
        },
        getHotelData: function (callBack) {
            this.$http.get('/Functoin/Service/HotelService.asmx/GetHotelInfo'
                , {wid: this.wid, openid: this.openid,hotelId:this.hotel.id}).then(function (response) {
                    if (response.data&&response.data.success) {
                        callBack(response.data.data);
                    }
                }, function (response) {

                    // handle error
                });
        },

        initSwiper: function () {
            var mySwiper = new Swiper('.swiper-container', {
                direction: 'vertical',
                loop: true,

                // �����Ҫ��ҳ��?
                pagination: '.swiper-pagination',

                // �����Ҫǰ�����˰��?
                nextButton: '.swiper-button-next',
                prevButton: '.swiper-button-prev',

                // �����Ҫ������?
                scrollbar: '.swiper-scrollbar'
            });
        }
    },
    ready: function () {
        var self = this;

        this.openid = getQueryStringByName('openid');
        this.wid = getQueryStringByName('wid');
        this.hotel.id= getQueryStringByName('hotelId');

        this.getHotelData(function (data) {
            self.hotel = data;

            self.currentView = 'view-room';

            //setTimeout(function(){
            //    self.initSwiper();
            //},1000);
        });
        this.getOrderCount(function (data) {
            self.orderCount = data;

        });

    }
})
